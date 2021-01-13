using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BarcodePrinter
{
    public class Logger
    {
        const string logtimeFormat = @"---------{0}---------------------------------------------";
        public static readonly Logger Tracer = new Logger("trace", new LogOption { Rolling = Rolling.ByDay });
        public static readonly Logger ErrorLoger = new Logger("errors");

        private string baseFolder;
        private string name;
        private LogOption option;
        public Logger(string name) : this(name, new LogOption { Rolling = Rolling.None }) { }
        public Logger(string name, LogOption option)
        {
            this.name = name;
            this.option = option;
            this.baseFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            if (!Directory.Exists(baseFolder)) Directory.CreateDirectory(baseFolder);
        }
        public void Log(string content)
        {
            Write(content);
        }
        public void TraceWarning(string message)
        {
            Write(message);
        }
        public void Trace(string message)
        {
            Write(message);
        }

        private void Write(string content)
        {
            lock (this)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(logtimeFormat, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                sb.AppendLine();
                sb.AppendLine(content);
                sb.AppendLine();
                File.AppendAllText(GetLogPath(), sb.ToString());
            }
        }
        private string GetLogPath()
        {
            switch (option.Rolling)
            {
                case Rolling.ByDay:
                    string folder = string.Format("{0}\\{1}", baseFolder, name);
                    if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                    return string.Format("{0}\\{1}.log", folder, DateTime.Now.ToString("yyyyMMdd"));

                case Rolling.None:
                default:
                    return string.Format("{0}\\{1}.log", baseFolder, name);
            }
        }
    }
    public sealed class LogOption
    {
        public Rolling Rolling { get; set; }
    }
    public enum Rolling
    {
        None = 0,
        ByDay
    }
}
