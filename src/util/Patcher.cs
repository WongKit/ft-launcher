/* 
 * FT Launcher - a simple and robust game updater/launcher
 * Copyright (C) 2021 WongKit
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;

class Patcher {
    private const char sep = ';';

    public void CheckAndUpdateFiles(string targetDirectory, string downloadUrl) {
        Logger.Write("Downloading update file...");
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

    public void CreateChecksumList(string directory) {
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
    }

    public void RunApplication(string launchApplicationPath, string arguments) {
        if (!File.Exists(launchApplicationPath)) {
            throw new Exception("Application to launch does not exist. " + launchApplicationPath);
        } else { 
            Logger.Write("Starting application");
            System.Diagnostics.Process.Start(launchApplicationPath, arguments);
        }
    }

    private List<Checksum> CompareChecksumWithLocal(string targetDirectory, List<Checksum> checksums) {
        List<Checksum> different = new List<Checksum>();

        Logger.Progress(0);
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

            checkedSize += checksum.size;
            Logger.Progress(checkedSize, totalSize);
        }

        return different;
    }

    private void DownloadFiles(string directory, string downloadUrl, List<Checksum> checksums) {
        Logger.Write("Starting downloads...");

        Logger.Progress(0);
        long totalSize = checksums.Sum(c => c.size);
        long processedSize = 0;

        using (var webClient = new WebClient()) {
            webClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);

            foreach (Checksum checksum in checksums) {
                string targetFile = directory + checksum.path;
                string backupFile = targetFile + ".bak";
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
                try {
                    webClient.DownloadFile(sourceFile, targetFile);

                    processedSize += checksum.size;
                    Logger.Progress(processedSize, totalSize);

                } catch (WebException ex) {
                    throw new Exception("Could not download file \"" + checksum.path + "\". " + ex.Message);
                }

            }
        }
        Logger.Write("Downloads finished");
    }

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

    private List<Checksum> ReadChecksumFile(string checksumFile) {
        List<Checksum> checksums = new List<Checksum>();
        using (var webClient = new WebClient()) {
            webClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);

            string content;

            try { 
                content = webClient.DownloadString(checksumFile);
            } catch (Exception) {
                throw new WebException("Could not get remote checksum file: " + checksumFile);
            }

            using (StringReader stringReader = new StringReader(content)) {
                string line = string.Empty;
                do {
                    line = stringReader.ReadLine();
                    if (line != null) {
                        string[] split = line.Split(new char[] { sep }, 3);
                        if (split.Length == 3) {
                            Checksum checksum = new Checksum();
                            checksum.path = split[0];
                            checksum.size = Convert.ToInt64(split[1]);
                            checksum.hash = split[2];
                            checksums.Add(checksum);
                        }
                    }
                } while (line != null);
            }
        }
        return checksums;
    }
}