using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using estrutura.growler;
using estrutura.growler.App;
using proxy.database;
using MySql.Data.MySqlClient;

namespace negocio.growler.App
{
    public class GrowlerLogNegocio
    {

        public static EstruturaRaizGrowler ConsultarGrowlerAtual(String IdGrowler)
        {
            MySQLDB.validainjecaoSQL(IdGrowler);

            EstruturaRaizGrowler result = new EstruturaRaizGrowler();
            try
            {
                string strSql = "SELECT IDGROWLER, TMPGROWLER, BATGROWLER,  DTALOGGROWLER FROM GROWLER_LOG WHERE IDGROWLER=@IDGROWLER ORDER BY DTALOGGROWLER DESC LIMIT 1";

                //Parametros
                List<MySqlParameter> prm = new List<MySqlParameter>();
                prm.Add(new MySqlParameter("@IDGROWLER", IdGrowler));


                struturaExecSQL resultSQL = MySQLDB.execReader(strSql, prm);

                if (!resultSQL.erro)
                {
                    System.Data.Common.DbDataReader rs = resultSQL.Reader;
                    while (rs.Read())
                    {
                        result.Dados = new Growler(
                            rs["IDGROWLER"].ToString(),
                            rs["TMPGROWLER"].ToString(),
                            rs["BATGROWLER"].ToString(),
                            rs["DTALOGGROWLER"].ToString()
                            );
                    }
                }
                result.msg = "OK";

            }
            catch (Exception ex)
            {
                result.ExceptionMsg = ex.Message;
                result.IdcErr = 1;
                result.CodErr = 2;
            }
            finally
            {
                MySQLDB.Close();
            }

            return result;

        }



        public static EstruturaRaizGrowlers lergarrafas()
        {
            Growlers grls = new Growlers();
            List<Growler> lgr = new List<Growler>();
            String sql = "";
            EstruturaRaizGrowlers result = new EstruturaRaizGrowlers();

            try
            {

                sql = "SELECT IDGROWLER FROM GROWLER WHERE IDCMON=1 ORDER BY IDGROWLER";
                struturaExecSQL resultSQL = MySQLDB.execReader(sql);
                

                List<string> ls = new List<string>();
                while (resultSQL.Reader.Read())
                {
                    ls.Add(resultSQL.Reader["IDGROWLER"].ToString());
                }

                MySQLDB.Close();

                string vlr = "";


                foreach (var item in ls)
                {

                    vlr = item.ToString(); 
                    sql = "SELECT IDGROWLER, TMPGROWLER, BATGROWLER,  DTALOGGROWLER FROM GROWLER_LOG WHERE IDGROWLER=@IDGROWLER ORDER BY DTALOGGROWLER DESC LIMIT 1";
                    MySQLDB.Close();

                    List<MySqlParameter> prm = new List<MySqlParameter>();
                    MySqlParameter pr = new MySqlParameter("@IDGROWLER", MySqlDbType.String);
                    pr.Value = vlr;
                    prm.Add(pr);

                    struturaExecSQL sre = MySQLDB.execReader(sql,prm);
                    System.Data.Common.DbDataReader rs = sre.Reader;

                    bool vazio = !rs.Read();
                    if (vazio || rs["IDGROWLER"] == null || rs["IDGROWLER"] == DBNull.Value)
                    {
                        DateTime dataAtual = DateTime.Now;

                        grls.ListaGrowlers.Add(
                            new Growler(
                                    vlr,
                                    "0",
                                    "0",
                                    dataAtual.ToString("yyyy-MM-dd hh:mm:SS.0")
                                    )
                                );
                    }
                    else
                    {
                        grls.ListaGrowlers.Add(
                            new Growler(
                                    rs["IDGROWLER"].ToString(),
                                    rs["TMPGROWLER"].ToString(),
                                    rs["BATGROWLER"].ToString(),
                                    rs["DTALOGGROWLER"].ToString()
                                    )
                                );
                    }
                }

                result.Dados = grls;
            }
            catch (Exception ex)
            {
                result.IdcErr = 1;
                result.CodErr = 3;
                result.msg = "Não foi possível obter a lista de dados dos Growlers.";
                result.ExceptionMsg = ex.Message;
            }
            finally
            {
                MySQLDB.Close();
            }

            return result;
        }


        public static EstruturaRaizGrowlers ConsultarHistoricoGrowler(String IdGrowler)
        {

            MySQLDB.validainjecaoSQL(IdGrowler);

            EstruturaRaizGrowlers result = new EstruturaRaizGrowlers();
            try
            {
                string strSql = "SELECT IDGROWLER, TMPGROWLER, BATGROWLER, DTALOGGROWLER FROM GROWLER_LOG WHERE IDGROWLER=@IDGROWLER ORDER BY DTALOGGROWLER DESC ";

                List<MySqlParameter> prm = new List<MySqlParameter>();
                MySqlParameter pr = new MySqlParameter("@IDGROWLER", MySqlDbType.String);
                pr.Value = IdGrowler;
                prm.Add(pr);

                struturaExecSQL resultSQL = MySQLDB.execReader(strSql,prm);

                if (!resultSQL.erro)
                {
                    System.Data.Common.DbDataReader rs = resultSQL.Reader;
                    while (rs.Read())
                    {
                        result.Dados.ListaGrowlers.Add( new Growler(
                            rs["IDGROWLER"].ToString(),
                            rs["TMPGROWLER"].ToString(),
                            rs["BATGROWLER"].ToString(),
                            rs["DTALOGGROWLER"].ToString()
                            ) 
                        );
                    }
                }
                result.msg = "OK";

            }
            catch (Exception ex)
            {
                result.ExceptionMsg = ex.Message;
                result.IdcErr = 1;
                result.CodErr = 2;
            }
            finally
            {
                MySQLDB.Close();
            }

            return result;
        }

    }
}
