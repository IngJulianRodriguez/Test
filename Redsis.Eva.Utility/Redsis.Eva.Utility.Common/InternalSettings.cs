using System.IO;
using System.Reflection;

namespace Redsis.Eva.Utility.Common
{
    public class InternalSettings
    {
        /// <summary>
        /// Configuration file of application
        /// </summary>
        public static readonly string ConfigFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "config.xml");

        /// <summary>
        /// Log file of application
        /// </summary>
        public static readonly string LogFilePath = @"C:\Eva\LogDownloader\Logs\Log.log";
    }
}