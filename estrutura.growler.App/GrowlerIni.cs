using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using estrutura.growler;

namespace estrutura.growler.App
{
    public class GrowlerIni : EstruturaBase
    {
        public String IdGrowler { get; set; }
        public String TempIdeal { get; set; }
        public String IndNotficacaoTemp { get; set; }
        public String IdNotificacao { get; set; }



        public GrowlerIni()
        {
            IdGrowler = "";
            TempIdeal = "0";
            IndNotficacaoTemp = "1";
        }

        public GrowlerIni(String idGrowler, String tempIdeal, String indNotficacaoTemp)
        {
            IdGrowler = idGrowler;
            TempIdeal = tempIdeal;
            IndNotficacaoTemp = indNotficacaoTemp;
        }

        public String toString()
        {
            return ("IdGrowler: " + IdGrowler + ", " + "TempIdeal: " + TempIdeal + ", IndNotficacaoTemp:" + IndNotficacaoTemp);
        }
    }

}
