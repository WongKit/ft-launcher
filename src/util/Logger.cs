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
using System.Windows.Forms;

class Logger {

    public static ProgressBar ProgressBar { private get; set; }
    public static TextBox TextBoxLog { private get; set; }

    /// <summary>
    /// Log an error message
    /// </summary>
    /// <param name="obj">Message text</param>
    public static void Error(object obj) {
        Write("[ERROR] " + obj);
    }

    /// <summary>
    /// Sets the progress bar to a specific value
    /// </summary>
    /// <param name="current">Progress value (0-100)</param>
    public static void Progress(int current) {
        Progress(current, 100);
    }

    /// <summary>
    /// Sets the progress bar to a specific value
    /// </summary>
    /// <param name="current">Progress value</param>
    /// <param name="max">Progress max</param>
    public static void Progress(long current, long max) {
        if (ProgressBar != null) {
            ProgressBar.Invoke((MethodInvoker)delegate {
                if (max == 0) {
                    ProgressBar.Value = 0;
                } else {
                    ProgressBar.Value = Convert.ToInt32((float)current / (float)max * 100F);
                }
            });
        }
    }

    /// <summary>
    /// Outputs a line to the log window
    /// </summary>
    /// <param name="obj">Message text</param>
    public static void Write(object obj) {
        if (TextBoxLog != null) {
            TextBoxLog.Invoke((MethodInvoker)delegate {
                TextBoxLog.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + obj);
                TextBoxLog.AppendText(Environment.NewLine);
                Application.DoEvents();
            });
        }
    }
}