using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace estrutura.growler.Sensor
{
    public class EstruturaStatusSensor : EstruturaBase
    {
        public String id { get; set; }
        public String temperatura { get; set; }
        public String bateria { get; set; }
    }
}
