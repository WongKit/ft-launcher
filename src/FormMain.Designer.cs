﻿/* 
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
            this.panelLog = new System.Windows.Forms.Panel();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonCreateChecksum = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panelSettings = new System.Windows.Forms.Panel();
            this.labelLanguage = new System.Windows.Forms.Label();
            this.radioButtonUpdateUrl1 = new System.Windows.Forms.RadioButton();
            this.radioButtonUpdateUrl2 = new System.Windows.Forms.RadioButton();
            this.radioButtonUpdateUrl3 = new System.Windows.Forms.RadioButton();
            this.radioButtonUpdateUrl5 = new System.Windows.Forms.RadioButton();
            this.radioButtonUpdateUrl4 = new System.Windows.Forms.RadioButton();
            this.backgroundWorkerLaunch = new System.ComponentModel.BackgroundWorker();
            this.btn_close = new System.Windows.Forms.PictureBox();
            this.webBrowserPanel = new System.Windows.Forms.WebBrowser();
            this.delayedLoad = new System.Windows.Forms.Timer(this.components);
            this.loadingBar = new System.Windows.Forms.PictureBox();
            this.timerLoadingBar = new System.Windows.Forms.Timer(this.components);
            this.btn_discord = new FT_Launcher.TransparentPictureBox();
            this.btn_register = new FT_Launcher.TransparentPictureBox();
            this.btn_log = new FT_Launcher.TransparentPictureBox();
            this.btn_news = new FT_Launcher.TransparentPictureBox();
            this.btn_settings = new FT_Launcher.TransparentPictureBox();
            this.btn_launch = new FT_Launcher.TransparentPictureBox();
            this.loadingBarGlow = new FT_Launcher.TransparentPictureBox();
            this.btn_exit = new FT_Launcher.TransparentPictureBox();
            this.panelNews.SuspendLayout();
            this.panelLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panelSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadingBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_discord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_register)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_log)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_news)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_settings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_launch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadingBarGlow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_exit)).BeginInit();
            this.SuspendLayout();
            // 
            // panelNews
            // 
            this.panelNews.BackColor = System.Drawing.Color.White;
            this.panelNews.Controls.Add(this.webBrowserNews);
            this.panelNews.Location = new System.Drawing.Point(20, 110);
            this.panelNews.Name = "panelNews";
            this.panelNews.Size = new System.Drawing.Size(650, 400);
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
            this.webBrowserNews.Size = new System.Drawing.Size(650, 400);
            this.webBrowserNews.TabIndex = 1;
            this.webBrowserNews.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.WebBrowserNews_DocumentCompleted);
            this.webBrowserNews.NewWindow += new System.ComponentModel.CancelEventHandler(this.WebBrowserNews_NewWindow);
            // 
            // panelLog
            // 
            this.panelLog.BackColor = System.Drawing.Color.White;
            this.panelLog.Controls.Add(this.textBoxLog);
            this.panelLog.Location = new System.Drawing.Point(20, 110);
            this.panelLog.Name = "panelLog";
            this.panelLog.Size = new System.Drawing.Size(650, 400);
            this.panelLog.TabIndex = 5;
            // 
            // textBoxLog
            // 
            this.textBoxLog.BackColor = System.Drawing.Color.White;
            this.textBoxLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLog.Location = new System.Drawing.Point(15, 0);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(635, 400);
            this.textBoxLog.TabIndex = 0;
            this.textBoxLog.Text = "\r\n";
            this.textBoxLog.WordWrap = false;
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // buttonCreateChecksum
            // 
            this.buttonCreateChecksum.BackColor = System.Drawing.Color.WhiteSmoke;
            this.buttonCreateChecksum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCreateChecksum.Location = new System.Drawing.Point(837, 549);
            this.buttonCreateChecksum.Name = "buttonCreateChecksum";
            this.buttonCreateChecksum.Size = new System.Drawing.Size(111, 50);
            this.buttonCreateChecksum.TabIndex = 6;
            this.buttonCreateChecksum.Text = "Create Checksum";
            this.buttonCreateChecksum.UseVisualStyleBackColor = false;
            this.buttonCreateChecksum.Click += new System.EventHandler(this.ButtonCreateChecksum_Click);
            this.buttonCreateChecksum.MouseEnter += new System.EventHandler(this.ButtonCreateChecksum_MouseEnter);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // panelSettings
            // 
            this.panelSettings.BackColor = System.Drawing.Color.White;
            this.panelSettings.Controls.Add(this.labelLanguage);
            this.panelSettings.Controls.Add(this.radioButtonUpdateUrl1);
            this.panelSettings.Controls.Add(this.radioButtonUpdateUrl2);
            this.panelSettings.Controls.Add(this.radioButtonUpdateUrl3);
            this.panelSettings.Controls.Add(this.radioButtonUpdateUrl5);
            this.panelSettings.Controls.Add(this.radioButtonUpdateUrl4);
            this.panelSettings.Location = new System.Drawing.Point(20, 110);
            this.panelSettings.Name = "panelSettings";
            this.panelSettings.Size = new System.Drawing.Size(650, 400);
            this.panelSettings.TabIndex = 11;
            // 
            // labelLanguage
            // 
            this.labelLanguage.AutoSize = true;
            this.labelLanguage.Location = new System.Drawing.Point(259, 116);
            this.labelLanguage.Name = "labelLanguage";
            this.labelLanguage.Size = new System.Drawing.Size(131, 13);
            this.labelLanguage.TabIndex = 5;
            this.labelLanguage.Text = "Select the audio language";
            // 
            // radioButtonUpdateUrl1
            // 
            this.radioButtonUpdateUrl1.Image = global::FT_Launcher.Properties.Resources.lang_thai;
            this.radioButtonUpdateUrl1.Location = new System.Drawing.Point(171, 154);
            this.radioButtonUpdateUrl1.Name = "radioButtonUpdateUrl1";
            this.radioButtonUpdateUrl1.Size = new System.Drawing.Size(120, 120);
            this.radioButtonUpdateUrl1.TabIndex = 0;
            this.radioButtonUpdateUrl1.TabStop = true;
            this.radioButtonUpdateUrl1.Text = "Download Server 1";
            this.radioButtonUpdateUrl1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonUpdateUrl1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.radioButtonUpdateUrl1.UseVisualStyleBackColor = true;
            this.radioButtonUpdateUrl1.CheckedChanged += new System.EventHandler(this.radioButtonUpdateUrl_CheckedChanged);
            // 
            // radioButtonUpdateUrl2
            // 
            this.radioButtonUpdateUrl2.Image = global::FT_Launcher.Properties.Resources.lang_japanese;
            this.radioButtonUpdateUrl2.Location = new System.Drawing.Point(357, 154);
            this.radioButtonUpdateUrl2.Name = "radioButtonUpdateUrl2";
            this.radioButtonUpdateUrl2.Size = new System.Drawing.Size(120, 120);
            this.radioButtonUpdateUrl2.TabIndex = 1;
            this.radioButtonUpdateUrl2.TabStop = true;
            this.radioButtonUpdateUrl2.Text = "Download Server 2";
            this.radioButtonUpdateUrl2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonUpdateUrl2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.radioButtonUpdateUrl2.UseVisualStyleBackColor = true;
            this.radioButtonUpdateUrl2.CheckedChanged += new System.EventHandler(this.radioButtonUpdateUrl_CheckedChanged);
            // 
            // radioButtonUpdateUrl3
            // 
            this.radioButtonUpdateUrl3.AutoSize = true;
            this.radioButtonUpdateUrl3.Location = new System.Drawing.Point(274, 297);
            this.radioButtonUpdateUrl3.Name = "radioButtonUpdateUrl3";
            this.radioButtonUpdateUrl3.Size = new System.Drawing.Size(116, 17);
            this.radioButtonUpdateUrl3.TabIndex = 2;
            this.radioButtonUpdateUrl3.TabStop = true;
            this.radioButtonUpdateUrl3.Text = "Download Server 3";
            this.radioButtonUpdateUrl3.UseVisualStyleBackColor = true;
            this.radioButtonUpdateUrl3.CheckedChanged += new System.EventHandler(this.radioButtonUpdateUrl_CheckedChanged);
            // 
            // radioButtonUpdateUrl5
            // 
            this.radioButtonUpdateUrl5.AutoSize = true;
            this.radioButtonUpdateUrl5.Location = new System.Drawing.Point(274, 343);
            this.radioButtonUpdateUrl5.Name = "radioButtonUpdateUrl5";
            this.radioButtonUpdateUrl5.Size = new System.Drawing.Size(116, 17);
            this.radioButtonUpdateUrl5.TabIndex = 4;
            this.radioButtonUpdateUrl5.TabStop = true;
            this.radioButtonUpdateUrl5.Text = "Download Server 5";
            this.radioButtonUpdateUrl5.UseVisualStyleBackColor = true;
            this.radioButtonUpdateUrl5.CheckedChanged += new System.EventHandler(this.radioButtonUpdateUrl_CheckedChanged);
            // 
            // radioButtonUpdateUrl4
            // 
            this.radioButtonUpdateUrl4.AutoSize = true;
            this.radioButtonUpdateUrl4.Location = new System.Drawing.Point(274, 320);
            this.radioButtonUpdateUrl4.Name = "radioButtonUpdateUrl4";
            this.radioButtonUpdateUrl4.Size = new System.Drawing.Size(116, 17);
            this.radioButtonUpdateUrl4.TabIndex = 3;
            this.radioButtonUpdateUrl4.TabStop = true;
            this.radioButtonUpdateUrl4.Text = "Download Server 4";
            this.radioButtonUpdateUrl4.UseVisualStyleBackColor = true;
            this.radioButtonUpdateUrl4.CheckedChanged += new System.EventHandler(this.radioButtonUpdateUrl_CheckedChanged);
            // 
            // backgroundWorkerLaunch
            // 
            this.backgroundWorkerLaunch.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerLaunch_DoWork);
            this.backgroundWorkerLaunch.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorkerLaunch_RunWorkerCompleted);
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.Transparent;
            this.btn_close.ErrorImage = null;
            this.btn_close.Image = global::FT_Launcher.Properties.Resources.btn_close;
            this.btn_close.InitialImage = null;
            this.btn_close.Location = new System.Drawing.Point(893, 0);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(50, 31);
            this.btn_close.TabIndex = 3;
            this.btn_close.TabStop = false;
            this.btn_close.Click += new System.EventHandler(this.PictureClose_Click);
            this.btn_close.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imgBtn_MouseDown);
            this.btn_close.MouseEnter += new System.EventHandler(this.imgBtn_MouseEnter);
            this.btn_close.MouseLeave += new System.EventHandler(this.imgBtn_MouseLeave);
            this.btn_close.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imgBtn_MouseUp);
            // 
            // webBrowserPanel
            // 
            this.webBrowserPanel.AllowWebBrowserDrop = false;
            this.webBrowserPanel.IsWebBrowserContextMenuEnabled = false;
            this.webBrowserPanel.Location = new System.Drawing.Point(690, 110);
            this.webBrowserPanel.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserPanel.Name = "webBrowserPanel";
            this.webBrowserPanel.ScriptErrorsSuppressed = true;
            this.webBrowserPanel.Size = new System.Drawing.Size(250, 400);
            this.webBrowserPanel.TabIndex = 20;
            // 
            // delayedLoad
            // 
            this.delayedLoad.Interval = 500;
            this.delayedLoad.Tick += new System.EventHandler(this.delayedLoad_Tick);
            // 
            // loadingBar
            // 
            this.loadingBar.BackColor = System.Drawing.Color.Transparent;
            this.loadingBar.Image = global::FT_Launcher.Properties.Resources.background_loading;
            this.loadingBar.Location = new System.Drawing.Point(0, 612);
            this.loadingBar.Name = "loadingBar";
            this.loadingBar.Size = new System.Drawing.Size(128, 28);
            this.loadingBar.TabIndex = 21;
            this.loadingBar.TabStop = false;
            // 
            // timerLoadingBar
            // 
            this.timerLoadingBar.Enabled = true;
            this.timerLoadingBar.Interval = 200;
            this.timerLoadingBar.Tick += new System.EventHandler(this.timerLoadingBar_Tick);
            // 
            // btn_discord
            // 
            this.btn_discord.BackColor = System.Drawing.Color.Transparent;
            this.btn_discord.Image = global::FT_Launcher.Properties.Resources.btn_discord;
            this.btn_discord.Location = new System.Drawing.Point(618, 540);
            this.btn_discord.Name = "btn_discord";
            this.btn_discord.Size = new System.Drawing.Size(66, 92);
            this.btn_discord.TabIndex = 18;
            this.btn_discord.TabStop = false;
            this.btn_discord.Click += new System.EventHandler(this.btn_discord_Click);
            this.btn_discord.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imgBtn_MouseDown);
            this.btn_discord.MouseEnter += new System.EventHandler(this.imgBtn_MouseEnter);
            this.btn_discord.MouseLeave += new System.EventHandler(this.imgBtn_MouseLeave);
            this.btn_discord.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imgBtn_MouseUp);
            // 
            // btn_register
            // 
            this.btn_register.BackColor = System.Drawing.Color.Transparent;
            this.btn_register.Image = global::FT_Launcher.Properties.Resources.btn_register;
            this.btn_register.Location = new System.Drawing.Point(541, 540);
            this.btn_register.Name = "btn_register";
            this.btn_register.Size = new System.Drawing.Size(66, 92);
            this.btn_register.TabIndex = 17;
            this.btn_register.TabStop = false;
            this.btn_register.Click += new System.EventHandler(this.btn_register_Click);
            this.btn_register.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imgBtn_MouseDown);
            this.btn_register.MouseEnter += new System.EventHandler(this.imgBtn_MouseEnter);
            this.btn_register.MouseLeave += new System.EventHandler(this.imgBtn_MouseLeave);
            this.btn_register.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imgBtn_MouseUp);
            // 
            // btn_log
            // 
            this.btn_log.BackColor = System.Drawing.Color.Transparent;
            this.btn_log.Image = global::FT_Launcher.Properties.Resources.btn_log;
            this.btn_log.Location = new System.Drawing.Point(274, 540);
            this.btn_log.Name = "btn_log";
            this.btn_log.Size = new System.Drawing.Size(66, 92);
            this.btn_log.TabIndex = 16;
            this.btn_log.TabStop = false;
            this.btn_log.Click += new System.EventHandler(this.btn_log_Click);
            this.btn_log.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imgBtn_MouseDown);
            this.btn_log.MouseEnter += new System.EventHandler(this.imgBtn_MouseEnter);
            this.btn_log.MouseLeave += new System.EventHandler(this.imgBtn_MouseLeave);
            this.btn_log.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imgBtn_MouseUp);
            // 
            // btn_news
            // 
            this.btn_news.BackColor = System.Drawing.Color.Transparent;
            this.btn_news.Image = global::FT_Launcher.Properties.Resources.btn_news;
            this.btn_news.Location = new System.Drawing.Point(192, 540);
            this.btn_news.Name = "btn_news";
            this.btn_news.Size = new System.Drawing.Size(66, 92);
            this.btn_news.TabIndex = 15;
            this.btn_news.TabStop = false;
            this.btn_news.Click += new System.EventHandler(this.btn_news_Click);
            this.btn_news.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imgBtn_MouseDown);
            this.btn_news.MouseEnter += new System.EventHandler(this.imgBtn_MouseEnter);
            this.btn_news.MouseLeave += new System.EventHandler(this.imgBtn_MouseLeave);
            this.btn_news.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imgBtn_MouseUp);
            // 
            // btn_settings
            // 
            this.btn_settings.BackColor = System.Drawing.Color.Transparent;
            this.btn_settings.Image = global::FT_Launcher.Properties.Resources.btn_settings;
            this.btn_settings.Location = new System.Drawing.Point(354, 540);
            this.btn_settings.Name = "btn_settings";
            this.btn_settings.Size = new System.Drawing.Size(66, 92);
            this.btn_settings.TabIndex = 13;
            this.btn_settings.TabStop = false;
            this.btn_settings.Click += new System.EventHandler(this.btn_settings_Click);
            this.btn_settings.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imgBtn_MouseDown);
            this.btn_settings.MouseEnter += new System.EventHandler(this.imgBtn_MouseEnter);
            this.btn_settings.MouseLeave += new System.EventHandler(this.imgBtn_MouseLeave);
            this.btn_settings.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imgBtn_MouseUp);
            // 
            // btn_launch
            // 
            this.btn_launch.BackColor = System.Drawing.Color.Transparent;
            this.btn_launch.Image = global::FT_Launcher.Properties.Resources.btn_launch;
            this.btn_launch.Location = new System.Drawing.Point(432, 525);
            this.btn_launch.Name = "btn_launch";
            this.btn_launch.Size = new System.Drawing.Size(98, 110);
            this.btn_launch.TabIndex = 12;
            this.btn_launch.TabStop = false;
            this.btn_launch.EnabledChanged += new System.EventHandler(this.imgBtn_EnabledChanged);
            this.btn_launch.Click += new System.EventHandler(this.ButtonLaunch_Click);
            this.btn_launch.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imgBtn_MouseDown);
            this.btn_launch.MouseEnter += new System.EventHandler(this.imgBtn_MouseEnter);
            this.btn_launch.MouseLeave += new System.EventHandler(this.imgBtn_MouseLeave);
            this.btn_launch.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imgBtn_MouseUp);
            // 
            // loadingBarGlow
            // 
            this.loadingBarGlow.BackColor = System.Drawing.Color.Transparent;
            this.loadingBarGlow.Image = global::FT_Launcher.Properties.Resources.background_loading_glow;
            this.loadingBarGlow.Location = new System.Drawing.Point(128, 602);
            this.loadingBarGlow.Name = "loadingBarGlow";
            this.loadingBarGlow.Size = new System.Drawing.Size(2, 37);
            this.loadingBarGlow.TabIndex = 22;
            this.loadingBarGlow.TabStop = false;
            // 
            // btn_exit
            // 
            this.btn_exit.BackColor = System.Drawing.Color.Transparent;
            this.btn_exit.Image = global::FT_Launcher.Properties.Resources.btn_exit;
            this.btn_exit.Location = new System.Drawing.Point(698, 540);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(66, 92);
            this.btn_exit.TabIndex = 23;
            this.btn_exit.TabStop = false;
            this.btn_exit.Click += new System.EventHandler(this.PictureClose_Click);
            this.btn_exit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imgBtn_MouseDown);
            this.btn_exit.MouseEnter += new System.EventHandler(this.imgBtn_MouseEnter);
            this.btn_exit.MouseLeave += new System.EventHandler(this.imgBtn_MouseLeave);
            this.btn_exit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imgBtn_MouseUp);
            // 
            // FormMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::FT_Launcher.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(960, 640);
            this.Controls.Add(this.panelSettings);
            this.Controls.Add(this.panelNews);
            this.Controls.Add(this.panelLog);
            this.Controls.Add(this.webBrowserPanel);
            this.Controls.Add(this.btn_discord);
            this.Controls.Add(this.btn_register);
            this.Controls.Add(this.btn_log);
            this.Controls.Add(this.btn_news);
            this.Controls.Add(this.btn_settings);
            this.Controls.Add(this.btn_launch);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.buttonCreateChecksum);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.loadingBarGlow);
            this.Controls.Add(this.loadingBar);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseDown);
            this.panelNews.ResumeLayout(false);
            this.panelLog.ResumeLayout(false);
            this.panelLog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panelSettings.ResumeLayout(false);
            this.panelSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadingBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_discord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_register)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_log)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_news)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_settings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_launch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadingBarGlow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_exit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelNews;
        private System.Windows.Forms.WebBrowser webBrowserNews;
        private System.Windows.Forms.PictureBox btn_close;
        private System.Windows.Forms.Panel panelLog;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button buttonCreateChecksum;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Panel panelSettings;
        private System.ComponentModel.BackgroundWorker backgroundWorkerLaunch;
        private System.Windows.Forms.RadioButton radioButtonUpdateUrl1;
        private System.Windows.Forms.RadioButton radioButtonUpdateUrl2;
        private System.Windows.Forms.RadioButton radioButtonUpdateUrl3;
        private System.Windows.Forms.RadioButton radioButtonUpdateUrl4;
        private System.Windows.Forms.RadioButton radioButtonUpdateUrl5;
        private System.Windows.Forms.WebBrowser webBrowserPanel;
        private System.Windows.Forms.Label labelLanguage;
        private System.Windows.Forms.Timer delayedLoad;
        private System.Windows.Forms.PictureBox loadingBar;
        private TransparentPictureBox btn_launch;
        private TransparentPictureBox btn_discord;
        private TransparentPictureBox btn_register;
        private TransparentPictureBox btn_log;
        private TransparentPictureBox btn_news;
        private TransparentPictureBox btn_settings;
        private System.Windows.Forms.Timer timerLoadingBar;
        private TransparentPictureBox loadingBarGlow;
        private TransparentPictureBox btn_exit;
    }
}

