using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using estrutura.growler;
using estrutura.growler.App;
using negocio.growler.App;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System.Text;

namespace web.api.growler.App.Controllers
{
    [RoutePrefix("api/growler")]
    public class GrowlerController : ApiController
    {


        [System.Web.Http.HttpGet]
        public HttpResponseMessage LerGarrafas()
        {
            return execResponse(GrowlerLogNegocio.lergarrafas());
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage ConsultarGrowlerAtual(String id)
        {
            return execResponse(GrowlerLogNegocio.ConsultarGrowlerAtual(id));
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage ConsultarHistoricoGrowler(String id)
        {
            return execResponse(GrowlerLogNegocio.ConsultarHistoricoGrowler(id));
        }

        [HttpPost]
        public HttpResponseMessage Iniciargrowler(GrowlerIni value)
        {

            HttpResponseMessage result = execResponse(GrowlerNegocio.iniciargrowler(value));

            try
            {
                AdicionarMensagem("notificacaogrowler", JsonConvert.SerializeObject(value));
            }
            catch (Exception ex)
            {}

            return result;
        }


        [HttpPut]
        public HttpResponseMessage SolicitaMonitoramentoGrowler(GrowlerMon value)
        {

            HttpResponseMessage result = execResponse(GrowlerNegocio.SolicitarMonitoramentoGrowler(value));

            return result;
        }


        [HttpDelete]
        public HttpResponseMessage EsvaziarGrowler(String id)
        {
            return execResponse(GrowlerNegocio.EsvaziarGrowler(id));
        }


        [HttpGet]
        public string TesteNivel(int nivel)
        {
            if (nivel == 1)
                return "Camada controller OK";
            else 
            {
                return GrowlerNegocio.TesteNivel(nivel);
            }

                

        }





        private HttpResponseMessage execResponse(EstruturaRaiz value, string media = "application/json")
        {
            HttpStatusCode st = HttpStatusCode.OK;
            if (value.IdcErr != 0)
                st = HttpStatusCode.BadRequest;

            return Request.CreateResponse(st, value, media);
        }


        #region código duplicado da bibliotecapara resolver o problema de referencia do Azure

        public static string getConfig(string key)
        {

#if !DEBUG
            string sufix = ".PRD";

#else
           string sufix = ".DEV";
#endif

            return System.Configuration.ConfigurationManager.AppSettings[new StringBuilder(key).Append(sufix).ToString()].ToString();
        }


        private const string keyConnection = "AzureQueue";

        private static void AdicionarMensagem(string queName, string message)
        {
            CloudQueue queue = GetQueue(queName);
            queue.AddMessage(new CloudQueueMessage(message));
        }

        private static CloudQueue GetQueue(string queName)
        {
            CloudQueue Queue = null;
            var connectionString = getConfig(keyConnection + "." + queName);
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

        #endregion código duplicado da bibliotecapara resolver o problema de referencia do Azure


    }
}
