/* 
 * FT Launcher - a simple and robust game updater/launcher
 * Copyright (C) 2021-2022 WongKit
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <https://www.gnu.org/licenses/>.
 */

using FT_Launcher;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading;

class Patcher {
    private const char sep = ';';
    private const string bak = ".bak";

    private const int retryMax = 3;
    private const int retryWait = 3000;
    private int retryCount = 0;

    public bool RequiresRestart { get;  private set; }

    public Patcher() {
        //Set TLS 1.2 (SecurityProtocolType.Tls12 in .NET 4.5+) as default protocol for HTTPS communication
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
    }

    /// <summary>
    /// Main launcher method that executes the following steps
    /// - Download the files.md5 file from a remote server
    /// - Compare the checksums with the local directory
    /// - Download files which differ between checksum file and local directory
    /// </summary>
    /// <param name="targetDirectory">Path to the local directory</param>
    /// <param name="downloadUrl">Path of the remote url containing the /files.md5</param>
    public void CheckAndUpdateFiles(string targetDirectory, string downloadUrl) {
        Logger.Write("Downloading update file...");
        RequiresRestart = false;
        List<Checksum> checksums = ReadChecksumFile(downloadUrl + "files.md5");

        if (checksums.Count == 0) {
            throw new Exception("Could not contact update server");

        } else {
            Logger.Write("Checking for updates...");
            List<Checksum> different = CompareChecksumWithLocal(targetDirectory, checksums);

            if (different.Count == 0) {
                Logger.Write("No updates found");

            } else {
                Logger.Write("Downloading missing files...");
                DownloadFiles(targetDirectory, downloadUrl, different);
            }
        }
    }

    /// <summary>
    /// Loops through all files of a directory to create the files.md5 in it
    /// </summary>
    /// <param name="directory">Path to the local directory</param>
    public void CreateChecksumList(string directory) {
        Logger.Write("Building checksum file in target directory...");

        string checksumFile = directory + "/files.md5";
        if (File.Exists(checksumFile)) {
            File.Delete(checksumFile);
        }

        List<Checksum> checksums = new List<Checksum>();
        GetChecksums(directory, directory, checksums);
        
        StreamWriter streamWriter = File.CreateText(checksumFile);
        foreach(Checksum checksum in checksums) {
            streamWriter.WriteLine(checksum.path + sep + checksum.size + sep + checksum.hash);
        }
        streamWriter.Close();

        Logger.Write("Building checksum file completed");
    }

    /// <summary>
    /// Executes an application with parameters if it exists.
    /// </summary>
    /// <param name="launchApplicationPath">Path of executable</param>
    /// <param name="arguments">Program arguments</param>
    public void RunApplication(string launchApplicationPath, string arguments) {
        if (!File.Exists(launchApplicationPath)) {
            throw new Exception("Application to launch does not exist. " + launchApplicationPath);
        } else { 
            Logger.Write("Starting application");
            System.Diagnostics.Process.Start(launchApplicationPath, arguments);
        }
    }

    /// <summary>
    /// Compares a local directory with a list of checksums
    /// </summary>
    /// <param name="targetDirectory">Path to the local directory</param>
    /// <param name="checksums">List of checksums to compare with</param>
    /// <returns></returns>
    private List<Checksum> CompareChecksumWithLocal(string targetDirectory, List<Checksum> checksums) {
        List<Checksum> different = new List<Checksum>();

        FormMain.SetLoadingBarProgress(0);
        long totalSize = checksums.Sum(c => c.size);
        long checkedSize = 0;

        foreach (Checksum checksum in checksums) {
            string targetFile = targetDirectory + checksum.path;
            bool requiresDownload = false;

            if (!File.Exists(targetFile)) {
                Logger.Write(checksum.path + " is missing");
                requiresDownload = true;

            } else if (new FileInfo(targetFile).Length != checksum.size) {
                Logger.Write(checksum.path + " has a different file size");
                requiresDownload = true;

            } else if (GetMd5Hash(targetFile) != checksum.hash) {
                Logger.Write(checksum.path + " has a different checksum");
                requiresDownload = true;
            }

            if (requiresDownload) {
                different.Add(checksum);
            }

            try {
                string backupFile = targetFile + bak;
                if (File.Exists(backupFile)) {
                    File.Delete(backupFile);
                    Logger.Write("Removed old backup file " + checksum.path + bak);
                }
            } catch (Exception) { }

            checkedSize += checksum.size;
            FormMain.SetLoadingBarProgress(checkedSize, totalSize);
        }

        return different;
    }

    /// <summary>
    /// Downloads all files that are passed in the checksum list. If a file is in use, it is renamed with the
    /// extension .bak to download a newer version to its original file name.
    /// If the downloaded file contains the launcher application itself or its .config file, the flag RequiresRestart
    /// is set to true.
    /// </summary>
    /// <param name="directory">Path to the local directory</param>
    /// <param name="downloadUrl">Download url root</param>
    /// <param name="checksums">List of files to download</param>
    private void DownloadFiles(string directory, string downloadUrl, List<Checksum> checksums) {
        Logger.Write("Starting downloads...");

        string launcherPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

        FormMain.SetLoadingBarProgress(0);
        long totalSize = checksums.Sum(c => c.size);
        long processedSize = 0;

        using (var webClient = new WebClient()) {
            webClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);

            foreach (Checksum checksum in checksums) {
                string targetFile = directory + checksum.path;
                string backupFile = targetFile + bak;
                string sourceFile = (downloadUrl + checksum.path).Replace('\\', '/');

                if (File.Exists(backupFile)) {
                    File.Delete(backupFile);
                }

                if (File.Exists(targetFile)) {
                    try {
                        File.Delete(targetFile);
                    } catch (Exception) {
                        
                        try {
                            File.Move(targetFile, backupFile);
                        } catch (Exception) {
                            throw new Exception("Could not patch file \"" + checksum.path + "\". Maybe it is still in use");
                        }
                    }
                }

                Logger.Write("Downloading " + checksum.path + "...");
                string parentDirectory = Path.GetDirectoryName(targetFile);
                if (!Directory.Exists(parentDirectory)) {
                    Directory.CreateDirectory(parentDirectory);
                }
                retryCount = 0;

                while (true) {
                    try {
                        webClient.DownloadFile(sourceFile, targetFile);

                        if (!RequiresRestart) {
                            if (targetFile == launcherPath || targetFile == launcherPath + ".config") {
                                Logger.Write("Launcher is updated and requires a restart");
                                RequiresRestart = true;
                            }
                        }

                        processedSize += checksum.size;
                        FormMain.SetLoadingBarProgress(processedSize, totalSize);
                        break;

                    } catch (WebException ex) {
                        if (retryCount < retryMax) {
                            Logger.Write("Download failed. Try again...");
                            Thread.Sleep(retryWait);
                            retryCount++;
                        } else {
                            throw new Exception("Could not download file \"" + checksum.path + "\". " + ex.Message);
                        }
                    }
                }

            }
        }
        Logger.Write("Downloads finished");
    }

    /// <summary>
    /// Recursively gets checksums and file size information of a given directory
    /// </summary>
    /// <param name="rootDirectory">Root directory</param>
    /// <param name="directory">Current directory</param>
    /// <param name="checksums">Collected checksum files</param>
    private void GetChecksums(string rootDirectory, string directory, List<Checksum> checksums) {
        foreach (string file in Directory.GetFiles(directory)) {
            Checksum checksum = new Checksum();
            checksum.path = file.Replace(rootDirectory, "");
            checksum.size = new FileInfo(file).Length;
            checksum.hash = GetMd5Hash(file);
            checksums.Add(checksum);
            Logger.Write("Generating checksum for " + checksum.path);
        }
        foreach (string dir in Directory.GetDirectories(directory)) {
            GetChecksums(rootDirectory, dir, checksums);
        }
    }

    /// <summary>
    /// Returns a MD5 hash of a given file
    /// </summary>
    /// <param name="filepath">Path of file to hash</param>
    /// <returns>MD5 checksum</returns>
    private string GetMd5Hash(string filepath) {
        try {
            using (var md5 = MD5.Create()) {
                using (var stream = File.OpenRead(filepath)) {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        } catch (Exception ex) {
            throw new Exception("Could not create hash for " + filepath + " " + ex.Message);
        }
    }

    /// <summary>
    /// Parses a files.md5 and creates a list of checksum files
    /// </summary>
    /// <param name="checksumFile">Checksum files</param>
    /// <returns></returns>
    private List<Checksum> ReadChecksumFile(string checksumFile) {
        List<Checksum> checksums = new List<Checksum>();

        retryCount = 0;

        using (var webClient = new WebClient()) {
            webClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);

            string content;

            while (true) {
                try {
                    content = webClient.DownloadString(checksumFile);
                    break;
                } catch (Exception ex) {
                    if (retryCount < retryMax) {
                        Logger.Write("Could not get remote checksum file. Try again...");
                        Thread.Sleep(retryWait);
                        retryCount++;
                    } else {
                        throw new WebException("Could not get remote checksum file: " + checksumFile + " " + ex.Message);
                    }
                }
            }

            using (StringReader stringReader = new StringReader(content)) {
                string line = string.Empty;
                do {
                    line = stringReader.ReadLine();
                    if (line != null) {
                        string[] split = line.Split(new char[] { sep });
                        if (split.Length >= 3) {
                            Checksum checksum = new Checksum();
                            checksum.path = split[0];
                            checksum.size = Convert.ToInt64(split[1]);
                            checksum.hash = split[2];

                            if (checksum.size >= 0) {
                                checksums.Add(checksum);
                            }
                        }
                    }
                } while (line != null);
            }
        }
        return checksums;
    }
}