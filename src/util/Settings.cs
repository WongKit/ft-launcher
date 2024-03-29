﻿/* 
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

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace FT_Launcher {
    class Settings {

        public static List<DownloadUrlElement> GetDownloadUrls() {
            List<DownloadUrlElement> downloadUrls = new List<DownloadUrlElement>();

            string legacyUrl = GetSetting("updateUrl", "");
            if (legacyUrl != "") {
                DownloadUrlElement legacyConfig = new DownloadUrlElement();
                legacyConfig.Name = "Default";
                legacyConfig.Url = legacyUrl;
                legacyConfig.LaunchFile = GetSetting("launchFile", "");
                legacyConfig.LaunchArgs = GetSetting("launchFileArgs", "");
                downloadUrls.Add(legacyConfig);
            }
            
            try {
                CustomConfigSection customConfigSection = (CustomConfigSection)ConfigurationManager.GetSection("customConfigSection");
                DownloadUrlElementCollection nodeList = customConfigSection.NodeList;
                downloadUrls.AddRange(nodeList.Cast<DownloadUrlElement>());

            } catch (ConfigurationErrorsException) {
                throw new Exception("Could not read program settings.\nMake sure that the FT_Launcher.exe.config file is in the same directory.");
            }

            return downloadUrls;
        }

        /// <summary>
        /// Read any setting from the <appSettings> section of the FT_Launcher.exe.config file.
        /// If the key does not exist, an exception will be thrown
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public static string GetSetting(string key) {
            string value = GetSetting(key, null);
            if (value == null) {
                throw new Exception("Mandatory setting \"" + key + "\" not found.\nPlease check the FT_Launcher.exe.config file.");
            } else {
                return value;
            }
        }

        /// <summary>
        /// Read any setting from the <appSettings> section of the FT_Launcher.exe.config file.
        /// If the key does not exist, the default value will be used
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="def">Default fallback value</param>
        /// <returns>Value</returns>
        public static string GetSetting(string key, string def) {
            if (ConfigurationManager.AppSettings[key] != null) {
                return ConfigurationManager.AppSettings[key];
            } else {
                return def;
            }
        }
    }
}