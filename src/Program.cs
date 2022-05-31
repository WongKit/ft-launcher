/* 
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
using System.Threading;
using System.Windows.Forms;

namespace FT_Launcher {
    static class Program {

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main() {

            Application.ThreadException += new ThreadExceptionEventHandler(ThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhadledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }

        static void ExceptionExit(Exception e) {
            MessageBox.Show(null, e.Message, "Oopsie...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Environment.Exit(1);
        }

        static void ThreadException(object sender, ThreadExceptionEventArgs args) {
            ExceptionExit(args.Exception);
        }

        static void UnhadledException(object sender, UnhandledExceptionEventArgs args) {
            ExceptionExit((Exception)args.ExceptionObject);
        }


    }
}