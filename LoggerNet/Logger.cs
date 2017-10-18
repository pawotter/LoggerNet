using System.Runtime.CompilerServices;
using System.IO;
using System;

namespace Pawotter.LoggerNet
{
    public class Logger : ILogger
    {
        public static Logger Shared { get; } = new Logger(LogLevel.Error);
        readonly Object thisLock = new Object();

        readonly LogLevel logLevel;
        readonly string format;
        internal bool NeedsLog(LogLevel logLevel) => (int) logLevel >= (int) this.logLevel;

        public Logger(LogLevel logLevel, string format = "[{0}] {3} ({1}:{2})")
        {
            this.logLevel = logLevel;
            this.format = format;
        }

        public void Fatal(object value, [CallerFilePath] string path = "", [CallerLineNumber] int num = 0) => Output(LogLevel.Info, value, path, num);

        public void Error(object value, [CallerFilePath] string path = "", [CallerLineNumber] int num = 0) => Output(LogLevel.Info, value, path, num);

        public void Warn(object value, [CallerFilePath] string path = "", [CallerLineNumber] int num = 0) => Output(LogLevel.Info, value, path, num);

        public void Info(object value, [CallerFilePath] string path = "", [CallerLineNumber] int num = 0) => Output(LogLevel.Info, value, path, num);

        public void Debug(object value, [CallerFilePath] string path = "", [CallerLineNumber] int num = 0) => Output(LogLevel.Info, value, path, num);

        void Output(LogLevel logLevel, object value, [CallerFilePath] string path = "", [CallerLineNumber] int num = 0)
        {
            if (!NeedsLog(logLevel)) return;
            var filename = Path.GetFileName(path);
            lock (thisLock)
            {
                Console.WriteLine(string.Format(format, logLevel, filename, num, value));
            }
        }
    }
}
