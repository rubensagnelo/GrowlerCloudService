using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proxy.database;
using estrutura.growler.Sensor;
using estrutura.growler;
using MySql.Data.MySqlClient;

namespace negocio.growler.Sensor
{
    public class processador
    {

        public static EstruturaRaiz RegistraStatusGrowler(EstruturaStatusSensor ent)
        {
            MySQLDB.validaEtruturaInjecaoSQL(ent);

            EstruturaRaiz result = new EstruturaRaiz();
            StringBuilder sql = new StringBuilder(string.Empty);
            try
            {
                sql.Append("INSERT INTO GROWLER_LOG ");
                sql.Append("(IDGROWLER, TMPGROWLER, BATGROWLER, DTALOGGROWLER) ");
                sql.Append("VALUES(");
                sql.Append("@id, @temperatura, @bateria,");
                sql.Append(" CURRENT_TIMESTAMP)");

                //Parametros
                List<MySqlParameter> prm = new List<MySqlParameter>();
                prm.Add(new MySqlParameter("@id", ent.id));
                prm.Add(new MySqlParameter("@temperatura", ent.temperatura));
                prm.Add(new MySqlParameter("@bateria", ent.bateria));

                struturaExecSQL estSql = MySQLDB.execSQL(sql.ToString(),prm);
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
            finally
            {
                MySQLDB.Close();
            }

            return result;

        }

    }
}
