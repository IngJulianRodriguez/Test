using Serilog;
using Serilog.Core;

namespace Redsis.Eva.Utility.Common
{
    public class LoggerApp
    {
        private static LoggerApp instance = null;
        private static readonly object padLock = new object();
        private string LogFileName { get; set; }
        private string FormatLine { get; set; }

        /// <summary>
        /// Get current instance
        /// </summary>
        public Logger GetLogger { get; set; }

        /// <summary>
        /// Get current class instance
        /// </summary>
        public static LoggerApp Instance
        {
            private set
            {
                instance = value;
            }
            get
            {
                lock (padLock)
                {
                    if (instance == null)
                    {
                        instance = new LoggerApp();
                    }

                    return instance;
                }
            }
        }

        LoggerApp()
        {

        }

        public LoggerApp(string fullFileName)
        {
            instance = this;
            LogFileName = fullFileName;
            ConfigLog();
        }

        public LoggerApp(string fullFileName, string formatLine)
        {
            instance = this;
            LogFileName = fullFileName;
            FormatLine = formatLine;
            ConfigLog();
        }

        private void ConfigLog()
        {
            //Line format
            if (string.IsNullOrEmpty(FormatLine))
            {
                FormatLine = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff}    [{Level:u3}]    [{SourceContext}]   {Message}{NewLine}{Exception}";
            }

            //
            GetLogger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(outputTemplate: FormatLine)
                .WriteTo.RollingFile(LogFileName, outputTemplate: FormatLine)
                .CreateLogger();
        }
    }
}