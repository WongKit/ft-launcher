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

namespace FT_Launcher
{
    partial class FormMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.panelNews = new System.Windows.Forms.Panel();
            this.webBrowserNews = new System.Windows.Forms.WebBrowser();
            this.buttonLaunch = new System.Windows.Forms.Button();
            this.pictureClose = new System.Windows.Forms.PictureBox();
            this.pictureActiveTab = new System.Windows.Forms.PictureBox();
            this.panelLog = new System.Windows.Forms.Panel();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonCreateChecksum = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.labelNews = new System.Windows.Forms.Label();
            this.labelLog = new System.Windows.Forms.Label();
            this.labelAbout = new System.Windows.Forms.Label();
            this.panelAbout = new System.Windows.Forms.Panel();
            this.backgroundWorkerLaunch = new System.ComponentModel.BackgroundWorker();
            this.panelNews.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureActiveTab)).BeginInit();
            this.panelLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelNews
            // 
            this.panelNews.BackColor = System.Drawing.Color.White;
            this.panelNews.Controls.Add(this.webBrowserNews);
            this.panelNews.Location = new System.Drawing.Point(30, 132);
            this.panelNews.Name = "panelNews";
            this.panelNews.Size = new System.Drawing.Size(621, 290);
            this.panelNews.TabIndex = 1;
            // 
            // webBrowserNews
            // 
            this.webBrowserNews.AllowWebBrowserDrop = false;
            this.webBrowserNews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserNews.IsWebBrowserContextMenuEnabled = false;
            this.webBrowserNews.Location = new System.Drawing.Point(0, 0);
            this.webBrowserNews.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserNews.Name = "webBrowserNews";
            this.webBrowserNews.ScriptErrorsSuppressed = true;
            this.webBrowserNews.Size = new System.Drawing.Size(621, 290);
            this.webBrowserNews.TabIndex = 1;
            this.webBrowserNews.Url = new System.Uri("", System.UriKind.Relative);
            this.webBrowserNews.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.WebBrowserNews_DocumentCompleted);
            this.webBrowserNews.NewWindow += new System.ComponentModel.CancelEventHandler(this.WebBrowserNews_NewWindow);
            // 
            // buttonLaunch
            // 
            this.buttonLaunch.Location = new System.Drawing.Point(576, 429);
            this.buttonLaunch.Name = "buttonLaunch";
            this.buttonLaunch.Size = new System.Drawing.Size(75, 23);
            this.buttonLaunch.TabIndex = 2;
            this.buttonLaunch.Text = "Launch";
            this.buttonLaunch.UseVisualStyleBackColor = true;
            this.buttonLaunch.Click += new System.EventHandler(this.ButtonLaunch_Click);
            // 
            // pictureClose
            // 
            this.pictureClose.BackColor = System.Drawing.Color.Transparent;
            this.pictureClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureClose.Location = new System.Drawing.Point(652, 5);
            this.pictureClose.Name = "pictureClose";
            this.pictureClose.Size = new System.Drawing.Size(20, 20);
            this.pictureClose.TabIndex = 3;
            this.pictureClose.TabStop = false;
            this.pictureClose.Click += new System.EventHandler(this.PictureClose_Click);
            // 
            // pictureActiveTab
            // 
            this.pictureActiveTab.BackColor = System.Drawing.Color.Transparent;
            this.pictureActiveTab.Image = ((System.Drawing.Image)(resources.GetObject("pictureActiveTab.Image")));
            this.pictureActiveTab.Location = new System.Drawing.Point(30, 92);
            this.pictureActiveTab.Name = "pictureActiveTab";
            this.pictureActiveTab.Size = new System.Drawing.Size(83, 29);
            this.pictureActiveTab.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureActiveTab.TabIndex = 4;
            this.pictureActiveTab.TabStop = false;
            // 
            // panelLog
            // 
            this.panelLog.BackColor = System.Drawing.Color.White;
            this.panelLog.Controls.Add(this.textBoxLog);
            this.panelLog.Location = new System.Drawing.Point(30, 132);
            this.panelLog.Name = "panelLog";
            this.panelLog.Size = new System.Drawing.Size(621, 290);
            this.panelLog.TabIndex = 5;
            // 
            // textBoxLog
            // 
            this.textBoxLog.BackColor = System.Drawing.Color.White;
            this.textBoxLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxLog.Location = new System.Drawing.Point(5, 0);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(615, 288);
            this.textBoxLog.TabIndex = 0;
            this.textBoxLog.WordWrap = false;
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // buttonCreateChecksum
            // 
            this.buttonCreateChecksum.Location = new System.Drawing.Point(459, 429);
            this.buttonCreateChecksum.Name = "buttonCreateChecksum";
            this.buttonCreateChecksum.Size = new System.Drawing.Size(111, 23);
            this.buttonCreateChecksum.TabIndex = 6;
            this.buttonCreateChecksum.Text = "Create Checksum";
            this.buttonCreateChecksum.UseVisualStyleBackColor = true;
            this.buttonCreateChecksum.Click += new System.EventHandler(this.ButtonCreateChecksum_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(31, 429);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(422, 23);
            this.progressBar.TabIndex = 7;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // labelNews
            // 
            this.labelNews.BackColor = System.Drawing.Color.White;
            this.labelNews.Location = new System.Drawing.Point(38, 94);
            this.labelNews.Name = "labelNews";
            this.labelNews.Size = new System.Drawing.Size(68, 26);
            this.labelNews.TabIndex = 8;
            this.labelNews.Text = "News";
            this.labelNews.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelNews.Click += new System.EventHandler(this.TabClick);
            // 
            // labelLog
            // 
            this.labelLog.BackColor = System.Drawing.Color.Transparent;
            this.labelLog.Location = new System.Drawing.Point(120, 94);
            this.labelLog.Name = "labelLog";
            this.labelLog.Size = new System.Drawing.Size(68, 26);
            this.labelLog.TabIndex = 9;
            this.labelLog.Text = "Log";
            this.labelLog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelLog.Click += new System.EventHandler(this.TabClick);
            // 
            // labelAbout
            // 
            this.labelAbout.BackColor = System.Drawing.Color.Transparent;
            this.labelAbout.Location = new System.Drawing.Point(201, 94);
            this.labelAbout.Name = "labelAbout";
            this.labelAbout.Size = new System.Drawing.Size(68, 26);
            this.labelAbout.TabIndex = 10;
            this.labelAbout.Text = "About";
            this.labelAbout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelAbout.Click += new System.EventHandler(this.TabClick);
            // 
            // panelAbout
            // 
            this.panelAbout.BackColor = System.Drawing.Color.White;
            this.panelAbout.Location = new System.Drawing.Point(30, 132);
            this.panelAbout.Name = "panelAbout";
            this.panelAbout.Size = new System.Drawing.Size(621, 290);
            this.panelAbout.TabIndex = 11;
            // 
            // backgroundWorkerLaunch
            // 
            this.backgroundWorkerLaunch.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerLaunch_DoWork);
            this.backgroundWorkerLaunch.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorkerLaunch_RunWorkerCompleted);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(680, 480);
            this.Controls.Add(this.labelAbout);
            this.Controls.Add(this.labelLog);
            this.Controls.Add(this.labelNews);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.buttonCreateChecksum);
            this.Controls.Add(this.pictureActiveTab);
            this.Controls.Add(this.pictureClose);
            this.Controls.Add(this.buttonLaunch);
            this.Controls.Add(this.panelAbout);
            this.Controls.Add(this.panelNews);
            this.Controls.Add(this.panelLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FT Launcher";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseDown);
            this.panelNews.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureActiveTab)).EndInit();
            this.panelLog.ResumeLayout(false);
            this.panelLog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelNews;
        private System.Windows.Forms.WebBrowser webBrowserNews;
        private System.Windows.Forms.Button buttonLaunch;
        private System.Windows.Forms.PictureBox pictureClose;
        private System.Windows.Forms.PictureBox pictureActiveTab;
        private System.Windows.Forms.Panel panelLog;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button buttonCreateChecksum;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label labelNews;
        private System.Windows.Forms.Label labelLog;
        private System.Windows.Forms.Label labelAbout;
        private System.Windows.Forms.Panel panelAbout;
        private System.ComponentModel.BackgroundWorker backgroundWorkerLaunch;
    }
}

