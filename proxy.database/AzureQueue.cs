using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;



namespace proxy.database
{
    public class AzureQueue
    {


        private const string keyConnection = "AzureQueue";


        public static CloudQueue GetQueue(string queName)
        {
            CloudQueue Queue = null;
            var connectionString = util.configTools.getConfig(keyConnection+"."+queName );
            CloudStorageAccount cloudStorageAccount;
            if (!CloudStorageAccount.TryParse(connectionString, out cloudStorageAccount))
            {

            }

            var cloudQueueClient = cloudStorageAccount.CreateCloudQueueClient();
            Queue = cloudQueueClient.GetQueueReference(queName);
            // Note: Usually this statement can be executed once during application startup or maybe even never in the application.
            //       A queue in Azure Storage is often considered a persistent item which exists over a long time.
            //       Every time .CreateIfNotExists() is executed a storage transaction and a bit of latency for the call occurs.
            Queue.CreateIfNotExists();
            return Queue;
        }

        public static void AdicionarMensagem(string queName, string message)
        {
           CloudQueue queue = GetQueue(queName);
           queue.AddMessage(new CloudQueueMessage(message));
        }

        public static CloudQueueMessage RecuperarMensagem(CloudQueue queue=null)
        {

            if (queue != null)
                return queue.GetMessage();
            else
            {
                return GetQueue(keyConnection).GetMessage();

            }

        }







    }



}

