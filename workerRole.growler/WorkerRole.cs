using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using estrutura.growler.App;
using Newtonsoft.Json;
using Microsoft.Azure.NotificationHubs;
using negocio.growler.App;
using estrutura.growler;

namespace workerRole.growler
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);
        private CloudQueue cloudQueue;


        public WorkerRole()
        {
            cloudQueue =  proxy.database.AzureQueue.GetQueue("notificacaogrowler");
        }




        public override void Run()
        {
            Trace.TraceInformation("workerRole.growler is running");

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 60;

            // For information on handling configuration changes
            // see the MSDN topic at https://go.microsoft.com/fwlink/?LinkId=166357.

            bool result = base.OnStart();

            Trace.TraceInformation("workerRole.growler has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("workerRole.growler is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("workerRole.growler has stopped");
        }

        private const int QtdTentativas = 60;

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            String strmsgque = String.Empty;

            while (!cancellationToken.IsCancellationRequested)
            {
                var msgque = cloudQueue.GetMessage();

                if (msgque == null)
                    break;
                else
                {
                    string idqueue = msgque.Id;
                    Trace.WriteLine("---------------------------------------------------------------");
                    Trace.WriteLine("Tratando queue " + idqueue + ".");
                    strmsgque = msgque.AsString;
                    GrowlerIni objque = JsonConvert.DeserializeObject<GrowlerIni>(strmsgque);

                    if ((TratarNotificacao(objque)) || (msgque.DequeueCount >= QtdTentativas))
                    {
                        if (msgque.DequeueCount >= QtdTentativas)
                            Trace.WriteLine("Foram realizadas " + msgque.DequeueCount.ToString() + " apurações. Monitoramento será descartado.");

                        Trace.WriteLine("Excluindo queue " + idqueue + ".");
                        cloudQueue.DeleteMessage(msgque);
                        Trace.WriteLine("Queue " + idqueue + "Excluida.");
                    }


                    Trace.WriteLine("Queue " + idqueue + " tratada.");
                    Trace.WriteLine("---------------------------------------------------------------");

                    await Task.Delay(1000);

                }

            }
            await Task.Delay(2000);

        }

        /// <summary>
        /// Retorno define se deve excluir a notificação
        /// </summary>
        /// <param name="objque"></param>
        /// <returns></returns>
        private bool TratarNotificacao(object objque)
        {
            bool result = false;
            try
            {

                if (objque is GrowlerIni)
                {
                    GrowlerIni gr = ((GrowlerIni)objque);
                    EstruturaRaizGrowler rg = GrowlerLogNegocio.ConsultarGrowlerAtual(gr.IdGrowler);

                    if ((rg.Dados != null) && (rg.Dados.Id != "null"))
                    {
                        Trace.WriteLine("verificando temeratura ideal do Growler " + gr.IdGrowler + "." + "Tempreatura = " + rg.Dados.Temperatura + " Temperatura ideal = " + gr.TempIdeal + ".");


                        if (System.Convert.ToDecimal(rg.Dados.Temperatura) <= System.Convert.ToDecimal(gr.TempIdeal))
                        {
                            string msg = "O Growler " + gr.IdGrowler + " atingiu a temperatura ideal." +
                                " A temperatura atual do Growler é " + rg.Dados.Temperatura + " Graus.";

                            Trace.WriteLine(msg);

                            Task t = proxy.database.AzureQueue.SendNotificationAsync(msg, gr.IdNotificacao);
                            t.Wait();
                            result = true;

                            Trace.WriteLine("Notificação enviada para ID: " + gr.IdNotificacao + " .");

                            EstruturaRaiz er = GrowlerNegocio.ExcluirMonitoramento(gr.IdGrowler);
                            if (er.IdcErr == 0)
                                Trace.WriteLine(" Notificação " + gr.IdNotificacao + " do Growler " + gr.IdGrowler + " excluida da fila reserva.");
                        }
                    } else
                        Trace.WriteLine("Não existe nenhuma apuração de temperatura até o momento.");
                }
            }
            catch (Exception)
            {
            }

            return result;

        }

        //private static void Registrar(string strid)
        //{
        //    string defaultFullSharedAccessSignature = "Endpoint=sb://hbscr13.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=hS+NQ17Qu8CwNBVxzSWVY3ORJ4nK2lMNTFENbFc5Vck=";
        //    string hubName = "hbPedidos";
        //    _hub = NotificationHubClient.CreateClientFromConnectionString(defaultFullSharedAccessSignature, hubName);

        //    //"dHjXf2hZ0rI: APA91bGJCNtMPugyVM8iqw06ZT - CV8MFk7WDIykM1iSgVvSXX2dIazjxajvKWYzIVnDib3pqcviPReTcmlfC0ikNgySgFCYKlsdqlpCmjWxilKeGO4NTJ8mzc_jIYn3zyXBGj0F_DsjL";
        //    CreateRegistrationIdAsync()

        //    GcmRegistrationDescription gcmdes = new GcmRegistrationDescription(strid);
        //    RegistrationDescription registration = new GcmRegistrationDescription(gcmdes);

        //}



        //public static async Task<string> CreateRegistrationIdAsync(DeviceRegistration deviceUpdate)
        //{
        //    string newRegistrationId = null;
        //    var handle = deviceUpdate.Handle;
        //    if (handle != null)
        //    {
        //        var registrations = await _hub.GetRegistrationsByChannelAsync(handle, 100);

        //        foreach (RegistrationDescription registration in registrations)
        //        {
        //            await _hub.DeleteRegistrationAsync(registration);
        //        }
        //    }

        //    //se quiser usar templates
        //    //newRegistrationId = await RegistrationsTemplate(deviceUpdate);
        //    newRegistrationId = await CreateOrUpdateRegistrationAsync(deviceUpdate);
        //    return newRegistrationId;
        //}
        //public static async Task<string> CreateOrUpdateRegistrationAsync(DeviceRegistration deviceUpdate)
        //{

        //    var newRegistrationId = await _hub.CreateRegistrationIdAsync();

        //    RegistrationDescription registration = null;
        //    switch (deviceUpdate.Platform)
        //    {
        //        case "mpns":
        //            registration = new MpnsRegistrationDescription(deviceUpdate.Handle);
        //            break;
        //        case "wns":
        //            registration = new WindowsRegistrationDescription(deviceUpdate.Handle);
        //            break;
        //        case "apns":
        //            registration = new AppleRegistrationDescription(deviceUpdate.Handle);
        //            break;
        //        case "gcm":
        //            registration = new GcmRegistrationDescription(deviceUpdate.Handle);
        //            break;
        //    }

        //    registration.RegistrationId = newRegistrationId;
        //    // add check if user is allowed to add these tags
        //    registration.Tags = new HashSet<string>(deviceUpdate.Tags);
        //    await _hub.CreateOrUpdateRegistrationAsync(registration);
        //    return registration.RegistrationId;
        //}


        //private static NotificationHubClient _hub;
        //public async Task<bool> SendNotificationAsync(string message, string idNotificacao)
        //{
        //    string[] userTag = new string[1];
        //    userTag[0] = "";

        //    //dHjXf2hZ0rI: APA91bGJCNtMPugyVM8iqw06ZT - CV8MFk7WDIykM1iSgVvSXX2dIazjxajvKWYzIVnDib3pqcviPReTcmlfC0ikNgySgFCYKlsdqlpCmjWxilKeGO4NTJ8mzc_jIYn3zyXBGj0F_DsjL



        //    string defaultFullSharedAccessSignature = util.configTools.getConfig("hubnotificacao"); 
        //    string hubName = "hbPedidos";                                                            


        //    _hub = NotificationHubClient.CreateClientFromConnectionString(defaultFullSharedAccessSignature, hubName);

        //    NotificationOutcome outcome = null;

        //    // Android
        //    var notif = "{ \"data\" : {\"message\":\"" + message +  " (idNotificacao="+ idNotificacao +") " + "\"}}";
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
}
