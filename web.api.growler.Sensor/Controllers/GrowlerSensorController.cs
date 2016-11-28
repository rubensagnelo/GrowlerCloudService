using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using negocio.growler.Sensor;
using estrutura.growler.Sensor;

namespace web.api.growler.Sensor.Controllers
{
    //[Authorize]
    public class GrowlerSensorController : ApiController
    {

        // PUT api/values/5
        public HttpResponseMessage Put(EstruturaStatusSensor value)
        {
            return Request.CreateResponse(HttpStatusCode.OK, processador.RegistraStatusGrowler(value), "application/json");
        }

    }
}
