using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace proxy.database
{
    public class dbutil
    {

        //private static string[] palavraschaveSQL = { "union", "select", "insert", "update", "delete", "drop", "alter", "create" };

        //Fonte 
        //http://www.mvvm.ro/2011/03/sanitize-strings-against-sql-injection.html



        private static bool verificaComando(string valor)
        {
            string strregex = @"(;|\s)(exec|execute|select|insert|update|delete|create|alter|drop|rename|truncate|backup|restore)\s"; 
            Regex re = new Regex(strregex);
            return re.Match(valor).Success;
        }


        private static bool verificaComentario(string valor)
        {
            string strregex = @"[*/]+";
            Regex re = new Regex(strregex);
            return re.Match(valor).Success;
        }

        private static bool verificaComentarioIfen(string valor)
        {
            string strregex = @"-{2,}";
            Regex re = new Regex(strregex);
            return re.Match(valor).Success;
        }

        private static bool verificaSomenteNumeroInteiro(string valor)
        {
            string strregex = @"^[0-9]+$";
            Regex re = new Regex(strregex);
            return re.Match(valor).Success;
        }


        private static bool verificaSomenteNumeroDecimal(string valor)
        {
            string strregex = @"^[0-9]+?(.|,[0-9]+)$";
            Regex re = new Regex(strregex);
            return re.Match(valor).Success;
        }


        public static bool ehinjecaoSQL(string valor)
        {
            return (
                verificaComando(valor) ||
                verificaComentario(valor) ||
                verificaComentarioIfen(valor)
                );
        }


        public static void validainjecaoSQL(string valor)
        {
            if (ehinjecaoSQL(valor))
                throw new Exception("Valor não permitido. Injeção de SLQ detectado.");
        }


        public static void validaEtruturaInjecaoSQL(object estrutura)
        {
            PropertyInfo[] properties = estrutura.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                var val = propertyInfo.GetValue(estrutura);
                Type tipo = propertyInfo.GetType();
                if (val != null && !String.IsNullOrWhiteSpace(val.ToString()))
                {
                    validainjecaoSQL(val.ToString());
                }
            }
        }

    }
}
