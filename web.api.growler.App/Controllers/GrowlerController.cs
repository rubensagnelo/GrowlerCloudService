using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using estrutura.growler;
using estrutura.growler.App;
using negocio.growler.App;

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
            return execResponse(GrowlerNegocio.iniciargrowler(value));
        }

        [HttpDelete]
        public HttpResponseMessage EsvaziarGrowler(String id)
        {
            return execResponse(GrowlerNegocio.EsvaziarGrowler(id));
        }



        private HttpResponseMessage execResponse(EstruturaRaiz value, string media = "application/json")
        {
            HttpStatusCode st = HttpStatusCode.OK;
            if (value.IdcErr != 0)
                st = HttpStatusCode.BadRequest;

            return Request.CreateResponse(st, value, media);
        }

    }
}
