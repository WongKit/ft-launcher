using System;
using System.Windows.Forms;

class Logger {
    public static TextBox TextBoxLog { private get; set; }
    public static void Write(object obj) {
        if (TextBoxLog != null) {
            TextBoxLog.AppendText("" + obj);
            TextBoxLog.AppendText(Environment.NewLine);
            Application.DoEvents();
        }
    }

    public static void Error(object obj) {
        Write("[ERROR] " + obj);
    }
}