using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.NotificationHubs.Messaging;
using estrutura;

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

        private class DeviceRegistration
        {
            public string Platform { get; set; }
            public string Handle { get; set; }
            public string[] Tags { get; set; }
        }

        public static async Task<bool> SendNotificationAsync(string message, string idNotificacao, bool broadcast=false)
        {
            string[] userTag = new string[1];
            
            // Obs importantíssima: O username tá na primeira parte do id da 
            // notificação antes do ":"(dois pontos). Para mandar broadcast é só 
            //enviar o username em branco
            userTag[0] = string.Empty;


            string defaultFullSharedAccessSignature = util.configTools.getConfig("hubnotificacao");
            string hubName = util.configTools.getConfig("hubname");
            string handle = idNotificacao;//hubName;

            NotificationHubClient _hub = NotificationHubClient.CreateClientFromConnectionString(defaultFullSharedAccessSignature, hubName);
            //await _hub.DeleteRegistrationAsync(idNotificacao);

            //Excluindo registros
            var registrations = await _hub.GetRegistrationsByChannelAsync(handle, 100);
            foreach (RegistrationDescription xregistration in registrations)
            {
                try
                {
                    await _hub.DeleteRegistrationAsync(xregistration);
                }
                catch (Exception ex) { string sm = ex.Message; }
            }


            if (!broadcast)
            {
                string to_tag = idNotificacao.Split(':')[0];
                userTag[0] = "username:" + to_tag;
            }

            Task<GcmRegistrationDescription> gd = _hub.CreateGcmNativeRegistrationAsync(handle, userTag);
            gd.Wait();

            NotificationOutcome outcome = null;



            // Android
            var notif = "{ \"data\" : {\"message\":\"" + message + "\"}}";
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



        //public static async Task<bool> SendNotificationAsync(string message, string idNotificacao, EstruturaContextoNottificacao contexto)
        //{


        //    string[] userTag = new string[2];
        //    if (contexto != null)
        //    {
        //        userTag = new string[2];
        //        userTag[0] = "username:" + contexto.usuarioDestino;
        //        userTag[1] = "from:" + contexto.usuarioDestino;
        //    }
        //    else
        //    {
        //        userTag = new string[1];
        //        userTag[0] = string.Empty;
        //    }

        //    string defaultFullSharedAccessSignature = util.configTools.getConfig("hubnotificacao");
        //    string hubName = util.configTools.getConfig("hubname");
        //    string handle = idNotificacao;

        //    NotificationHubClient _hub = NotificationHubClient.CreateClientFromConnectionString(defaultFullSharedAccessSignature, hubName);
        //    //await _hub.DeleteRegistrationAsync(idNotificacao);

        //    //Excluindo registros
        //    string newRegistrationId = null;
        //    var registrations = await _hub.GetRegistrationsByChannelAsync(handle, 100);
        //    //foreach (RegistrationDescription xregistration in registrations)
        //    //{
        //    //    if (newRegistrationId == null)
        //    //    {
        //    //        newRegistrationId = xregistration.RegistrationId;
        //    //    }
        //    //    else
        //    //    {
        //    //        await _hub.DeleteRegistrationAsync(xregistration);
        //    //    }
        //    //}

        //    foreach (RegistrationDescription xregistration in registrations)
        //    {
        //        await _hub.DeleteRegistrationAsync(xregistration.RegistrationId);
        //    }




        //    RegistrationDescription registration = new GcmRegistrationDescription(idNotificacao);
        //    await _hub.CreateOrUpdateRegistrationAsync(registration);




        //    //RegistrationDescription registration = new GcmRegistrationDescription(handle);


        //    Task <GcmRegistrationDescription> gd = _hub.CreateGcmNativeRegistrationAsync(idNotificacao);
        //    gd.Wait();

        //    //await _hub.CreateOrUpdateRegistrationAsync(


        //    NotificationOutcome outcome = null;


        //    // Android
        //    var notif = "{ \"data\" : {\"message\":\"" + message + "\"}}";


        //    outcome = await _hub.SendGcmNativeNotificationAsync(notif, userTag);

        //    if (outcome != null)
        //    {
        //        if (!((outcome.State == NotificationOutcomeState.Abandoned) ||
        //            (outcome.State == NotificationOutcomeState.Unknown)))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}



        //public static async Task<bool> SendNotificationAsync(string message, string idNotificacao)
        //{


        //    string[] userTag = new string[1];
        //    userTag[0] = "";

        //    string defaultFullSharedAccessSignature = util.configTools.getConfig("hubnotificacao");
        //    string hubName = "hbGrowler1";

        //    NotificationHubClient _hub = NotificationHubClient.CreateClientFromConnectionString(defaultFullSharedAccessSignature, hubName);

        //    Task<GcmRegistrationDescription> gd = _hub.CreateGcmNativeRegistrationAsync(idNotificacao);
        //    gd.Wait();


        //    NotificationOutcome outcome = null;


        //    // Android
        //    var notif = "{ \"data\" : {\"message\":\"" + message + "\"}}";
        //    outcome = await _hub.SendGcmNativeNotificationAsync(notif, userTag);

        //    if (outcome != null)
        //    {
        //        if (!((outcome.State == NotificationOutcomeState.Abandoned) ||
        //            (outcome.State == NotificationOutcomeState.Unknown)))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}




    }

    public class DeviceRegistration
    {
        public string Platform { get; set; }
        public string Handle { get; set; }
        public string[] Tags { get; set; }
    }

}

