using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace estrutura.growler.Sensor
{
    public class EstruturaRaizStatusSensor : EstruturaRaiz
    {

        public EstruturaStatusSensor Dados;

        public EstruturaRaizStatusSensor(int idcErr, int codErr, EstruturaStatusSensor dados, String exceptionMsg)
        {
            this.IdcErr = idcErr;
            this.CodErr = codErr;
            this.Dados = dados;
            this.ExceptionMsg = exceptionMsg;
        }

    }
}
