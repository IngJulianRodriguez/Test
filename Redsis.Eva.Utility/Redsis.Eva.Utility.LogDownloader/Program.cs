using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Redsis.Eva.Utility.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Redsis.Eva.Utility.LogDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            new LoggerApp(InternalSettings.LogFilePath);
            var Log = LoggerApp.Instance.GetLogger.ForContext<Program>();
            Log.Information("Iniciando Aplicación.");

            try
            {
                if (args.Length != 2)
                {
                    throw new Exception("Se debe definir la ruta del archivo de entrada.");
                }

                if (!File.Exists(args[0]))
                {
                    throw new Exception("El archivo " + args[0] + " no existe.");
                }
                if (!File.Exists(args[1]))
                {
                    throw new Exception("El archivo " + args[1] + " no existe.");
                }

                // Create Azure Storage
                string azureStringConnection = ConfigurationManager.Instance.AzureConnectionString;
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(azureStringConnection);

                // Create a Blob Client
                CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();

                // Get Container Reference
                string blobContainer = ConfigurationManager.Instance.BlobName;
                CloudBlobContainer cloudBlobContainer = blobClient.GetContainerReference(blobContainer);

                // Read Dates File
                List<string> datesList = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), args[1])).ToList();
                Log.Information(string.Format("Se encontraron "+ datesList.Count + " Fechas para procesar." ));

                foreach (string date in datesList)
                {

                    // Get Directory Reference
                    string logDirectory = ConfigurationManager.Instance.DirectoryName + "/" + ((Convert.ToDateTime(date)).AddDays(1)).ToString("yyyyMMdd");
                    CloudBlobDirectory cloudBlobDirectory = cloudBlobContainer.GetDirectoryReference(logDirectory);

                    // Create Download Folder
                    string downloadFolder = Path.Combine(Directory.GetCurrentDirectory(), date.Replace("-",""));
                    if (Directory.Exists(downloadFolder))
                    {
                        Log.Information(string.Format("El folder {0} ya existe.", downloadFolder));
                    }
                    else
                    {
                        Directory.CreateDirectory(downloadFolder);
                        Log.Information(string.Format("Folder {0} creado.", downloadFolder));
                    }

                    // Read Input File
                    List<string> posList = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory(), args[0])).ToList();
                    Log.Information(string.Format("Se encontraron [{0}] Pos para procesar.", posList.Count));

                    int count = 0;
                    Boolean all = false;
                    var logs = cloudBlobDirectory.ListBlobs();
                    if (posList.Contains("*"))
                    {
                        all = true;
                    }
                        foreach (CloudBlob log in logs)
                    {
                        string blobName = log.Name.Split('/').Last();
                        if (!all)
                        {
                            if (!IncludeLog(blobName, posList, out posList))
                                continue;

                        }
                        string path = Path.Combine(downloadFolder, blobName);
                        log.DownloadToFile(path, FileMode.Create);
                        Log.Information(string.Format("File {0} downloaded.", blobName));
                        count++;
                        if (!all)
                        {
                            if (posList.Count == 0)
                                break;
                        }
                    }
                    Log.Information(string.Format("Se procesaron [{0}] Pos.", count));
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            finally
            {
                Log.Information("Proceso Completado.");
            }

            Console.ReadKey();
        }

        static bool IncludeLog(string logName, List<string> posList, out List<string> posListOut)
        {
            foreach (string pos in posList)
            {
                if (logName.StartsWith(pos))
                {
                    if (!(pos.StartsWith("B") && (pos.IndexOf("_").Equals(4)))) {
                        posList.Remove(pos);
                    }
                    posListOut = posList;
                    return true;
                }
                    
            }

            posListOut = posList;
            return false;
        }
    }
}
