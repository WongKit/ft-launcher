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