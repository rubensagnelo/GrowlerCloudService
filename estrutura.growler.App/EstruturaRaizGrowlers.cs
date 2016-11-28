using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace estrutura.growler.App
{
    public class EstruturaRaizGrowlers : EstruturaRaiz
    {


    public Growlers Dados;

    public EstruturaRaizGrowlers(int idcErr, int codErr, Growlers dados, String exceptionMsg)
    {
        this.IdcErr = idcErr;
        this.CodErr = codErr;
        this.Dados = dados;
        this.ExceptionMsg = exceptionMsg;
    }

        public EstruturaRaizGrowlers()
        {
            this.IdcErr = 0;
            this.CodErr = 0;
            this.Dados = new Growlers();
            this.ExceptionMsg = "";
            this.msg = "OK";
        }


    }

}
