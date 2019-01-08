using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Logger;

namespace BibTagger
{
    static class Program
    {
        static MainForm _mainForm = null;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            _mainForm = new MainForm();

            Application.Run(_mainForm);
        }

        //private static bool canLog
        //{
        //    get { return _mainForm.UseLogging; }
        //}

        //public static void PostLogInfo(string message)
        //{
        //    if (!canLog)
        //        return;
        //    string result = "#info: " + message;
        //    Logger.LogManager.Info(message);
        //    _mainForm.listBoxLog.Log(LogLevel.Info, result);
        //}
        //public static void PostLogWarning(string message)
        //{
        //    if (!canLog)
        //        return;
        //    string result = "#warn: " + message;
        //    Logger.LogManager.Warning(message);
        //    _mainForm.listBoxLog.Log(LogLevel.Warning, result);
        //}
        //public static void PostLogError(string message)
        //{
        //    if (!canLog)
        //        return;
        //    string result = "#error: " + message;
        //    Logger.LogManager.Error(message);
        //    _mainForm.listBoxLog.Log(LogLevel.Error, result);
        //}
        //public static void PostLogFatal(string message)
        //{
        //    if (!canLog)
        //        return;
        //    string result = "#fatal: " + message;
        //    Logger.LogManager.Fatal(message);
        //    _mainForm.listBoxLog.Log(LogLevel.Fatal, result);
        //}
        //public static void PostLog(LogLevel level, string message)
        //{
        //    if (!canLog)
        //        return;
        //    switch (level)
        //    {
        //        case LogLevel.Info:
        //            PostLogInfo(message);
        //            break;
        //        case LogLevel.Warning:
        //            PostLogWarning(message);
        //            break;
        //        case LogLevel.Error:
        //            PostLogError(message);
        //            break;
        //        case LogLevel.Fatal:
        //            PostLogFatal(message);
        //            break;
        //    }
        //}
    }
}
