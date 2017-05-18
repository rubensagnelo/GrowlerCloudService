using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proxy.database
{
    public class dbDriver
    {
        public static void validainjecaoSQL(string valor)
        {
            dbutil.validainjecaoSQL(valor);
        }

        public static void validaEtruturaInjecaoSQL(object estrutura)
        {
            dbutil.validaEtruturaInjecaoSQL(estrutura);
        }
    }
}
