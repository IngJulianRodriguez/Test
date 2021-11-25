using Serilog;
using System;
using System.Linq;
using System.Xml.Linq;

namespace Redsis.Eva.Utility.Common
{
    public class ConfigurationManager
    {
        #region Properties

        ILogger log = LoggerApp.Instance.GetLogger.ForContext<ConfigurationManager>();

        /// <summary>
        /// Azure Storage Account Connection String
        /// </summary>
        public string AzureConnectionString { get; set; }

        /// <summary>
        /// Blob Name
        /// </summary>
        public string BlobName { get; set; }

        /// <summary>
        /// Directory Name
        /// </summary>
        public string DirectoryName { get; set; }

        #endregion

        #region Constructor

        private static readonly object padLock = new object();
        private static ConfigurationManager instance = null;
        public static ConfigurationManager Instance
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
                        instance = new ConfigurationManager(InternalSettings.ConfigFilePath);
                    }

                    return instance;
                }
            }
        }

        public ConfigurationManager()
        {
            AzureConnectionString = "DefaultEndpointsProtocol=https;AccountName=evachef;AccountKey=9TKtp9Sj/uhxvGQyznZsjKSefgQfjLCb/q0G1a6qj/lGbiUnWT4YslmAJyTqUPADecinqUT49/bZbWfJnaQ+Bg==";
            BlobName = "logs";
            DirectoryName = "EvaClient";
        }

        public ConfigurationManager(string configFile)
        {
            try
            {
                XDocument xmlFile = XDocument.Load(configFile);

                var azure = (from c in xmlFile.Descendants("azure").Elements() select c).ToList();
                AzureConnectionString = azure.Where(c => c.Name == "connectionString").FirstOrDefault().Value;
                BlobName = azure.Where(c => c.Name == "blob").FirstOrDefault().Value;
                DirectoryName = azure.Where(c => c.Name == "folder").FirstOrDefault().Value;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                throw;
            }
        }

        #endregion

        #region Functions

        /// <summary>
        /// Method ToString()
        /// Return a string with the Object in JSON format
        /// </summary>
        public override string ToString()
        {
            string ans = "";

            if (this != null)
                ans = Newtonsoft.Json.JsonConvert.SerializeObject(this);

            return ans;
        }

        #endregion
    }
}