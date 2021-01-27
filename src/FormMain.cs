using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FT_Launcher {
    public partial class FormMain : Form {

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private Patcher patcher = new Patcher();

        public FormMain() {
            InitializeComponent();
        }

        private void FormMain_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void pictureClose_Click(object sender, EventArgs e) {
            Close();
        }

        private void buttonLaunch_Click(object sender, EventArgs e) {
            buttonLaunch.Enabled = false;
            TabClick(labelLog, null);

            string applicationPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string updateUrl = ConfigurationManager.AppSettings.Get("updateUrl");
            patcher.CheckAndUpdateFiles(applicationPath, updateUrl);
            buttonLaunch.Enabled = true;
        }

        private void buttonCreateChecksum_Click(object sender, EventArgs e) {
            buttonCreateChecksum.Enabled = false;
            TabClick(labelLog, null);

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK) {
                patcher.CreateChecksumList(folderBrowserDialog.SelectedPath);
            }
            buttonCreateChecksum.Enabled = true;
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
            } else if (label == labelAbout) {
                panelAbout.Visible = true;
            }
        }

        private void FormMain_Load(object sender, EventArgs e) {
            Logger.TextBoxLog = textBoxLog;

            if (ModifierKeys == Keys.Shift) {
            } else {
                buttonCreateChecksum.Visible = false;
                progressBar.Width = 539;
            }

            TabClick(labelNews, null);
            webBrowserNews.Navigate(ConfigurationManager.AppSettings.Get("newsUrl"));
            webBrowserNews.Refresh(WebBrowserRefreshOption.Completely);
        }
    }
}