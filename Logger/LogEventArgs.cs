using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logger
{
    public class LogEventArgs : EventArgs
    {
        public string SourceMessage { get; set; }
        public string FinalMessage { get; set; }
        public LogLevel Level { get; set; }

        public LogEventArgs(string msg, string finalMsg, LogLevel lvl)
        {
            SourceMessage = msg;
            FinalMessage = finalMsg;
            Level = lvl;
        }
    }
}
