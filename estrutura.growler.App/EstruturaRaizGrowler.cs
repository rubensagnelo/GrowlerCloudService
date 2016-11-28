using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace estrutura.growler.App
{
    public class EstruturaRaizGrowler : EstruturaRaiz
    {


        public Growler Dados;

        public EstruturaRaizGrowler(int idcErr, int codErr, Growler dados, String exceptionMsg)
        {
            this.IdcErr = idcErr;
            this.CodErr = codErr;
            this.Dados = dados;
            this.ExceptionMsg = exceptionMsg;
        }

        public EstruturaRaizGrowler()
        {
            this.IdcErr = 0;
            this.CodErr = 0;
            this.Dados = new Growler();
            this.ExceptionMsg = "";
            this.msg = "OK";

        }

    }
}

