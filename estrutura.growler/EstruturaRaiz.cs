using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace estrutura.growler
{
    

    public class EstruturaRaiz
    {

        public Int32 IdcErr;
        public Int32 CodErr;
        public String ExceptionMsg;
        public String msg;

        public EstruturaRaiz()
        {
            this.IdcErr = 0;
            this.CodErr = 0;
            this.ExceptionMsg = "";
            this.msg = "";

        }

        public EstruturaRaiz(int idcErr, int codErr, String exceptionMsg)
        {
            this.IdcErr = idcErr;
            this.CodErr = codErr;
            this.ExceptionMsg = exceptionMsg;
        }

        public String toString()
        {
            return ("IdcErr: " + IdcErr.ToString() + ", " + "CodErr: " + CodErr.ToString() + ", ExceptionMsg: " + ExceptionMsg);
        }
    }

}
