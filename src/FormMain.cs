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
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FT_Launcher {
    public partial class FormMain : Form {

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        //dll imports are required for borderless window moving
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private Patcher patcher = new Patcher();
        private HtmlElement webBrowserDocumentClickedElement;

        public FormMain() {
            InitializeComponent();
        }

        /// <summary>
        /// Check and update files in a background thread. Make sure not to access UI elements in this thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorkerLaunch_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e) {
            Logger.Write("Checking for updates and launching application...");
            string launcherPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string applicationPath = Path.GetDirectoryName(launcherPath);
            string updateUrl = GetSetting("updateUrl");
            patcher.CheckAndUpdateFiles(applicationPath, updateUrl);

            if (patcher.RequiresRestart) {
                Logger.Write("Restarting launcher now");
                Application.Restart();
                return;
            }

            string launchFile = GetSetting("launchFile", "");
            string arguments = GetSetting("launchFileArgs", "");
            if (launchFile != "") {
                patcher.RunApplication(applicationPath + "\\" + launchFile, arguments);
                Application.Exit();
            }
        }

        /// <summary>
        /// Executed after the main launcher process finished successfully or with an error.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorkerLaunch_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e) {
            if (e.Error != null) {
                Logger.Error(e.Error.Message);
                Logger.Progress(0);
                TabClick(labelLog, null);
            }
            buttonLaunch.Enabled = true;
        }

        /// <summary>
        /// Creates the files.md5 file for a user selected directory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCreateChecksum_Click(object sender, EventArgs e) {
            buttonCreateChecksum.Enabled = false;
            TabClick(labelLog, null);

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK) {
                try {
                    Logger.Write("Building checksum file in target directory...");
                    patcher.CreateChecksumList(folderBrowserDialog.SelectedPath);
                    Logger.Write("Building checksum file completed");
                } catch (Exception ex) {
                    Logger.Error(ex.Message);
                }
            }
            buttonCreateChecksum.Enabled = true;
        }

        /// <summary>
        /// Executes the main launcher process as a separate thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonLaunch_Click(object sender, EventArgs e) {
            buttonLaunch.Enabled = false;
            backgroundWorkerLaunch.RunWorkerAsync();
        }

        /// <summary>
        /// Main form initialization
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e) {
            Logger.TextBoxLog = textBoxLog;
            Logger.ProgressBar = progressBar;

            TabClick(labelNews, null);
            webBrowserNews.Navigate(GetSetting("newsUrl", "about:blank"));
        }

        /// <summary>
        /// Enables borderless window moving
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        /// <summary>
        /// Late main form initialization to check if the shift key is pressed.
        /// If not, the "Create Checksum" button is hidden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Shown(object sender, EventArgs e) {
            if (ModifierKeys == Keys.Shift) {
            } else {
                buttonCreateChecksum.Visible = false;
                progressBar.Width = 569;
            }

            Logger.Write("Application startup");
        }

        /// <summary>
        /// Read any setting from the <appSettings> section of the FT_Launcher.exe.config file.
        /// If the key does not exist, an exception will be thrown
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        private string GetSetting(string key) {
            string value = GetSetting(key, null);
            if (value == null) {
                throw new Exception("Mandatory setting \"" + key + "\" not found");
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
        private string GetSetting(string key, string def) {
            if (ConfigurationManager.AppSettings[key] != null) {
                return ConfigurationManager.AppSettings[key];
            } else {
                return def;
            }
        }

        /// <summary>
        /// Close application on clicking the x image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureClose_Click(object sender, EventArgs e) {
            Close();
        }

        /// <summary>
        /// Switch tabs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabClick(object sender, EventArgs e) {
            Label label = (Label)sender;

            panelNews.Visible = false;
            panelLog.Visible = false;
            panelAbout.Visible = false;

            labelNews.BackColor = Color.Transparent;
            labelLog.BackColor = Color.Transparent;
            labelAbout.BackColor = Color.Transparent;
            label.BackColor = Color.White;

            Point point = new Point(label.Location.X - 7, pictureActiveTab.Location.Y);
            pictureActiveTab.Location = point;

            if (label == labelNews) {
                panelNews.Visible = true;
            } else if (label == labelLog) {
                panelLog.Visible = true;
                textBoxLog.SelectionStart = textBoxLog.TextLength;
                textBoxLog.ScrollToCaret();
            } else if (label == labelAbout) {
                panelAbout.Visible = true;
            }
        }

        /// <summary>
        /// Register mouse down event for the loaded web document. It is required to remember the clicked link
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebBrowserNews_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) {
            webBrowserNews.Document.MouseDown += WebBrowserNewsDocument_MouseDown;
        }

        /// <summary>
        /// Launch a web url in the default browser instead of creating another Internet Explorer instance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebBrowserNews_NewWindow(object sender, System.ComponentModel.CancelEventArgs e) {
            if (webBrowserDocumentClickedElement != null && webBrowserDocumentClickedElement.TagName == "A") {
                e.Cancel = true;
                string href = webBrowserDocumentClickedElement.GetAttribute("href");
                Process.Start(href);
            }
        }

        /// <summary>
        /// Remember the last link that was clicked on by the user. It is used to determine its url on WebBrowserNews_NewWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebBrowserNewsDocument_MouseDown(object sender, HtmlElementEventArgs e) {
            webBrowserDocumentClickedElement = webBrowserNews.Document.GetElementFromPoint(e.ClientMousePosition);
        }
    }
}