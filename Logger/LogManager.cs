using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BibIO;

namespace Logger
{
    public static class LogManager
    {
        private static readonly object _logger = new object();

        private static string _directory = string.Empty;

        public delegate void OnLogEventHandler(object sender, LogEventArgs e);
        public static event OnLogEventHandler OnLog;

        private static bool _canLog = true;
        public static bool CanLog
        {
            get { return _canLog; }
            set { _canLog = value; }
        }

        public static void SetDirectory(string path)
        {
            if (Directory.Exists(path))
                _directory = path;
            else _directory = string.Empty;
        }

        private static string logFile
        {
            get
            {
                if (string.IsNullOrEmpty(_directory))
                    return FileManager.DefaultLogFile;
                else return FileManager.LogFile(_directory);
            }
        }

        public static void ClearLog()
        {
            //lock (_logger)
            //{
            //    File.Create(logFile);
            //}
        }

        private static void write(string line, LogLevel level)
        {
            if (CanLog)
            {
                lock (_logger)
                {
                    string message = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ": " + line;
                    //File.AppendAllText(logFile, message);
                    StreamWriter writer = new StreamWriter(logFile, true, Encoding.UTF8);
                    writer.WriteLine(message);
                    writer.Close();

                    OnLog?.Invoke(_logger, new LogEventArgs(line, message, level));
                }
            }
        }

        public static void Log(LogLevel level, string message)
        {
            switch (level)
            {
                case LogLevel.Info:
                    Info(message);
                    break;
                case LogLevel.Warning:
                    Warning(message);
                    break;
                case LogLevel.Error:
                    Error(message);
                    break;
                case LogLevel.Fatal:
                    Fatal(message);
                    break;
            }
        }
        
        public static void Info(string message)
        {
            write("#info: " + message, LogLevel.Info);
        }
        public static void Warning(string message)
        {
            write("#warn: " + message, LogLevel.Warning);
        }
        public static void Error(string message)
        {
            write("#error: " + message, LogLevel.Error);
        }
        public static void Fatal(string message)
        {
            write("#fatal: " + message, LogLevel.Error);
        }
    }
}
