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
    public class GrowlerController : ApiController
    {


        // DELETE api/values/5
        public HttpResponseMessage Delete(String id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, GrowlerNegocio.EsvaziarGrowler(id), "application/json");
        }

        // PUT api/values/5
        public HttpResponseMessage Put(GrowlerIni value)
        {
            return Request.CreateResponse(HttpStatusCode.OK, GrowlerNegocio.iniciargrowler(value), "application/json");
        }


    }
}
