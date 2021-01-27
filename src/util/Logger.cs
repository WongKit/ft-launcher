using System;
using System.Windows.Forms;

class Logger {
    
    public static ProgressBar ProgressBar { private get; set; }
    public static TextBox TextBoxLog { private get; set; }


    public static void Error(object obj) {
        Write("[ERROR] " + obj);
    }

    public static void Progress(int current) {
        if (ProgressBar != null) {
            ProgressBar.Value = current;
        }
    }

    public static void Progress(long current, long max) {
        if (ProgressBar != null) {
            if (max == 0) {
                ProgressBar.Value = 0;
            } else {
                ProgressBar.Value = Convert.ToInt32((float)current / (float)max * 100F);
            }
        }
    }

    public static void Write(object obj) {
        if (TextBoxLog != null) {
            TextBoxLog.AppendText("" + obj);
            TextBoxLog.AppendText(Environment.NewLine);
            Application.DoEvents();
        }
    }
}