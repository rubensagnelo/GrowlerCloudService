using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.Azure.NotificationHubs;



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



        public static async Task<bool> SendNotificationAsync(string message, string idNotificacao)
        {
            string[] userTag = new string[1];
            userTag[0] = "";

            //dHjXf2hZ0rI: APA91bGJCNtMPugyVM8iqw06ZT - CV8MFk7WDIykM1iSgVvSXX2dIazjxajvKWYzIVnDib3pqcviPReTcmlfC0ikNgySgFCYKlsdqlpCmjWxilKeGO4NTJ8mzc_jIYn3zyXBGj0F_DsjL



            string defaultFullSharedAccessSignature = util.configTools.getConfig("hubnotificacao");
            string hubName = "hbPedidos";


            NotificationHubClient _hub = NotificationHubClient.CreateClientFromConnectionString(defaultFullSharedAccessSignature, hubName);

            NotificationOutcome outcome = null;

            // Android
            var notif = "{ \"data\" : {\"message\":\"" + message + " (idNotificacao=" + idNotificacao + ") " + "\"}}";
            outcome = await _hub.SendGcmNativeNotificationAsync(notif, userTag);

            if (outcome != null)
            {
                if (!((outcome.State == NotificationOutcomeState.Abandoned) ||
                    (outcome.State == NotificationOutcomeState.Unknown)))
                {
                    return true;
                }
            }
            return false;
        }




    }



}

