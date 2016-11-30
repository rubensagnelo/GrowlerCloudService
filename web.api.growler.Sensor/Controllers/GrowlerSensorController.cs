using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using negocio.growler.Sensor;
using estrutura.growler;
using estrutura.growler.Sensor;

namespace web.api.growler.Sensor.Controllers
{
    //[Authorize]
    [RoutePrefix("api/growlersensor")]
    public class GrowlerSensorController : ApiController
    {

        [HttpPut]
        public HttpResponseMessage RegistraStatusGrowler(EstruturaStatusSensor value)
        {
            return execResponse(processador.RegistraStatusGrowler(value));
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
