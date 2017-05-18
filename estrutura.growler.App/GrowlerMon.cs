using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using estrutura.growler;

namespace estrutura.growler.App
{

    public class GrowlerMon : EstruturaBase
    {
        public String IdGrowler { get; set; }
        public String DataGravacao { get; set; }
        public String IdNotificacao { get; set; }
        public String DataNotificacao { get; set; }


        public GrowlerMon()
        {
            IdGrowler = "";
            DataGravacao = "";
            IdNotificacao = "";
            DataNotificacao = "";
        }

        public GrowlerMon(String idGrowler, String dataGravacao, String idNotificacao, String dataNotificacao)
        {
            IdGrowler = idGrowler;
            IdNotificacao = idNotificacao;
            DataGravacao = dataGravacao;
            DataNotificacao = dataNotificacao;
        }

        public String toString()
        {
            return ("IdGrowler:" + IdGrowler + ", DataGravacao:" + DataGravacao + ", IdNotificacao:" + IdNotificacao + ", DataNotificacao:" + DataNotificacao);
        }
    }


}
