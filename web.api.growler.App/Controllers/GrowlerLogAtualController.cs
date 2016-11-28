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
    //[Authorize]
    public class GrowlerLogAtualController : ApiController
    {
        // GET api/values
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, GrowlerLogNegocio.lergarrafas(), "application/json");
            
        }

        // GET api/values/5
        public HttpResponseMessage Get(String id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, GrowlerLogNegocio.ConsultarGrowlerAtual(id), "application/json");
        }

    }
}
