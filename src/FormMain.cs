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
        private List<DownloadUrlElement> downloadUrls;

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
            DownloadUrlElement downloadUrl = GetSelectedDownloadUrl();
            patcher.CheckAndUpdateFiles(applicationPath, downloadUrl.Url);

            if (patcher.RequiresRestart) {
                Logger.Write("Restarting launcher now");
                Application.Restart();
                return;
            }

            if (downloadUrl.LaunchFile != "") {
                patcher.RunApplication(applicationPath + "\\" + downloadUrl.LaunchFile, downloadUrl.LaunchArgs);
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
                    patcher.CreateChecksumList(folderBrowserDialog.SelectedPath);
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

            webBrowserNews.Navigate(Settings.GetSetting("newsUrl", "about:blank"));
            this.Text = Settings.GetSetting("title", "FT Launcher");
            PrepareDownloadUrlElements();

            Logger.Write("Application startup");
            ParseCommandLineArguments();
        }


        private DownloadUrlElement GetSelectedDownloadUrl() {
            if (radioButtonUpdateUrl1.Checked) {
                return downloadUrls[0];
            } else if (radioButtonUpdateUrl2.Checked) {
                return downloadUrls[1];
            } else if (radioButtonUpdateUrl3.Checked) {
                return downloadUrls[2];
            } else if (radioButtonUpdateUrl4.Checked) {
                return downloadUrls[3];
            } else if (radioButtonUpdateUrl5.Checked) {
                return downloadUrls[4];
            } else {
                return null;
            }
        }


        private void ParseCommandLineArguments() {
            string[] args = Environment.GetCommandLineArgs();

            bool first = true;
            foreach (string arg in args) {
                if (first) {
                    first = false;

                } else {
                    if (arg.StartsWith("-") || arg.StartsWith("/")) {

                    } else {
                        try {
                            patcher.CreateChecksumList(arg);
                        } catch (Exception ex) {
                            Logger.Error(ex.Message);
                        }
                        TabClick(labelLog, null);
                    }
                }
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


        private void PrepareDownloadUrlElements() {
            bool buttonChecked = false;
            downloadUrls = Settings.GetDownloadUrls();

            for (int i = 0; i < 5; i++) {
                RadioButton radioButton = null;
                switch (i) {
                    case 0: radioButton = radioButtonUpdateUrl1; break;
                    case 1: radioButton = radioButtonUpdateUrl2; break;
                    case 2: radioButton = radioButtonUpdateUrl3; break;
                    case 3: radioButton = radioButtonUpdateUrl4; break;
                    case 4: radioButton = radioButtonUpdateUrl5; break;
                }

                if (downloadUrls.Count > i) {
                    radioButton.Text = downloadUrls[i].Name;
                    if (downloadUrls[i].Name == Properties.Settings.Default.SelectedDownloadUrl) {
                        radioButton.Checked = true;
                        buttonChecked = true;
                    }
                } else {
                    radioButton.Visible = false;
                }
            }

            if (!buttonChecked) {
                radioButtonUpdateUrl1.Checked = true;
            }
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
            panelSettings.Visible = false;

            labelNews.BackColor = Color.Transparent;
            labelLog.BackColor = Color.Transparent;
            labelSettings.BackColor = Color.Transparent;
            label.BackColor = Color.White;

            Point point = new Point(label.Location.X - 7, pictureActiveTab.Location.Y);
            pictureActiveTab.Location = point;

            if (label == labelNews) {
                panelNews.Visible = true;
            } else if (label == labelLog) {
                panelLog.Visible = true;
                textBoxLog.SelectionStart = textBoxLog.TextLength;
                textBoxLog.ScrollToCaret();
            } else if (label == labelSettings) {
                panelSettings.Visible = true;
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

        private void radioButtonUpdateUrl_CheckedChanged(object sender, EventArgs e) {
            if (((RadioButton)sender).Checked) {
                DownloadUrlElement downloadUrl = GetSelectedDownloadUrl();
                Properties.Settings.Default.SelectedDownloadUrl = downloadUrl.Name;
                Properties.Settings.Default.Save();

                Logger.Write("Switch download server to " + downloadUrl.Name);
            }
        }
    }
}