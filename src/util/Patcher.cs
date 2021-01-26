using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;

class Patcher {

    private const char sep = ';';

    public Patcher() {

    }

    public void CheckAndUpdateFiles(string targetDirectory, string downloadUrl) {
        Logger.Write("Downloading update file...");
        List<Checksum> checksums = ReadChecksumFile(downloadUrl + "files.md5");

        if (checksums.Count == 0) {
            Logger.Error("Could not contact update server");

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

        string client = targetDirectory + "FT_Client_Patched.exe";
        if (File.Exists(client)) {
            Logger.Write("Starting application");
            System.Diagnostics.Process.Start(client, "0");
        }

        Logger.Write("All done!");
    }

    public void CreateChecksumList(string directory) {
        string checksumFile = directory + "/files.md5";
        List<Checksum> checksums = new List<Checksum>();
        GetChecksums(directory, directory, checksums);
        
        StreamWriter streamWriter = File.CreateText(checksumFile);
        foreach(Checksum checksum in checksums) {
            streamWriter.WriteLine(checksum.path + sep + checksum.size + sep + checksum.hash);
        }
        streamWriter.Close();
    }

    private List<Checksum> CompareChecksumWithLocal(string targetDirectory, List<Checksum> checksums) {
        List<Checksum> different = new List<Checksum>();

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
        }

        return different;
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

    private void DownloadFiles(string directory, string downloadUrl, List<Checksum> checksums) {
        Logger.Write("Starting downloads...");
        using (var webClient = new WebClient()) {

            foreach (Checksum checksum in checksums) {
                string targetFile = directory + checksum.path;
                string backupFile = targetFile + ".bak";
                string sourceFile = (downloadUrl + checksum.path).Replace('\\', '/');

                if (File.Exists(backupFile)) {
                    File.Delete(backupFile);
                }

                if (File.Exists(targetFile)) {
                    File.Move(targetFile, backupFile);
                }

                Logger.Write("Downloading " + checksum.path + "...");
                string parentDirectory = Path.GetDirectoryName(targetFile);
                if (!Directory.Exists(parentDirectory)) {
                    Directory.CreateDirectory(parentDirectory);
                }
                webClient.DownloadFile(sourceFile, targetFile);
            }
        }
        Logger.Write("Downloads finished");
    }

    private string GetMd5Hash(string filepath) {
        using (var md5 = MD5.Create()) {
            using (var stream = File.OpenRead(filepath)) {
                var hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
    }

    private List<Checksum> ReadChecksumFile(string checksumFile) {
        List<Checksum> checksums = new List<Checksum>();
        using (var webClient = new WebClient()) {
            string content = webClient.DownloadString(checksumFile);

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