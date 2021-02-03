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

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private Patcher patcher = new Patcher();
        private HtmlElement webBrowserDocumentClickedElement;


        public FormMain() {
            InitializeComponent();
        }

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

        private void ButtonLaunch_Click(object sender, EventArgs e) {
            buttonLaunch.Enabled = false;

            try {
                Logger.Write("Checking for updates and launching application...");
                string applicationPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string updateUrl = GetSetting("updateUrl");
                patcher.CheckAndUpdateFiles(applicationPath, updateUrl);

                string launchFile = GetSetting("launchFile", "");
                string arguments = GetSetting("launchFileArgs", "");
                if (launchFile != "") {
                    patcher.RunApplication(applicationPath + "\\" + launchFile, arguments);
                    Application.Exit();
                }

            } catch (Exception ex) {
                Logger.Error(ex.Message);
                TabClick(labelLog, null);
            }
            buttonLaunch.Enabled = true;
        }

        private void FormMain_Load(object sender, EventArgs e) {
            Logger.TextBoxLog = textBoxLog;
            Logger.ProgressBar = progressBar;

            if (ModifierKeys == Keys.Shift) {
            } else {
                buttonCreateChecksum.Visible = false;
                progressBar.Width = 539;
            }

            TabClick(labelNews, null);
            webBrowserNews.Navigate(GetSetting("newsUrl", "about:blank"));
        }

        private void FormMain_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private string GetSetting(string key) {
            string value = GetSetting(key, null);
            if (value == null) {
                throw new Exception("Mandatory setting \"" + key + "\" not found");
            } else {
                return value;
            }
        }

        private string GetSetting(string key, string def) {
            if (ConfigurationManager.AppSettings[key] != null) {
                return ConfigurationManager.AppSettings[key];
            } else {
                return def;
            }
        }

        private void PictureClose_Click(object sender, EventArgs e) {
            Close();
        }

        private void TabClick(object sender, EventArgs e) {
            Label label = (Label)sender;

            panelNews.Visible = false;
            panelLog.Visible = false;
            panelAbout.Visible = false;

            labelNews.BackColor = Color.Transparent;
            labelLog.BackColor = Color.Transparent;
            labelAbout.BackColor = Color.Transparent;
            label.BackColor = Color.White;

            Point point = new Point(label.Location.X - 8, pictureActiveTab.Location.Y);
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

        private void WebBrowserNews_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) {
            webBrowserNews.Document.MouseDown += WebBrowserNewsDocument_MouseDown;
        }

        private void WebBrowserNews_NewWindow(object sender, System.ComponentModel.CancelEventArgs e) {
            if (webBrowserDocumentClickedElement != null && webBrowserDocumentClickedElement.TagName == "A") {
                e.Cancel = true;
                string href = webBrowserDocumentClickedElement.GetAttribute("href");
                Process.Start(href);
            }
        }

        private void WebBrowserNewsDocument_MouseDown(object sender, HtmlElementEventArgs e) {
            webBrowserDocumentClickedElement = webBrowserNews.Document.GetElementFromPoint(e.ClientMousePosition);
        }
    }
}