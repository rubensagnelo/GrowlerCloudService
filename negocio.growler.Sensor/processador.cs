using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proxy.database;
using estrutura.growler.Sensor;
using estrutura.growler;

namespace negocio.growler.Sensor
{
    public class processador
    {

        public static EstruturaRaiz RegistraStatusGrowler(EstruturaStatusSensor ent)
        {
            EstruturaRaiz result = new EstruturaRaiz();
            StringBuilder sql = new StringBuilder(string.Empty);
            try
            {
                sql.Append("INSERT INTO GROWLER_LOG ");
                sql.Append("(IDGROWLER, TMPGROWLER, BATGROWLER, DTALOGGROWLER) ");
                sql.Append("VALUES(");
                sql.Append("'").Append(ent.id).Append("'").Append(", ");
                sql.Append(ent.temperatura).Append(", ");
                sql.Append(ent.bateria).Append(", ");
                sql.Append(" CURRENT_TIMESTAMP)");

                struturaExecSQL estSql = MySQLDB.execSQL(sql.ToString());
                if (estSql.erro)
                {
                    result.IdcErr = 1;
                    result.CodErr = 1;
                    result.ExceptionMsg = estSql.mensagem;
                    result.msg = "Não foi possível incluir o registro de monitoração.";
                } else
                    result.msg = "OK";

            }
            catch (Exception ex)
            {
                result.CodErr = 1;
                result.IdcErr = 1;
                result.ExceptionMsg = ex.Message;
            }
            return result;

        }

    }
}
