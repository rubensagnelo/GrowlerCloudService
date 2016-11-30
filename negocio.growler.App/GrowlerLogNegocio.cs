using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using estrutura.growler;
using estrutura.growler.App;
using proxy.database;


namespace negocio.growler.App
{
    public class GrowlerLogNegocio
    {

        public static EstruturaRaizGrowler ConsultarGrowlerAtual(String IdGrowler)
        {

            EstruturaRaizGrowler result = new EstruturaRaizGrowler();
            try
            {
                string strSql = "SELECT IDGROWLER, TMPGROWLER, BATGROWLER,  DTALOGGROWLER FROM GROWLER_LOG WHERE IDGROWLER=" + IdGrowler + " ORDER BY DTALOGGROWLER DESC LIMIT 1";
                struturaExecSQL resultSQL = MySQLDB.execReader(strSql);

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
                //System.out.println("SQL >" + sql);
                struturaExecSQL resultSQL = MySQLDB.execReader(sql);
                //java.sql.ResultSet rsIds = statement().executeQuery(sql);
                //System.out.println("info> Sucesso");
                

                List<string> ls = new List<string>();
                while (resultSQL.Reader.Read())
                {
                    ls.Add(resultSQL.Reader["IDGROWLER"].ToString());
                }

                MySQLDB.Close();

                string vlr = "";

                foreach (var item in ls)
                {

                    vlr = item.ToString(); //resultSQL.Reader["IDGROWLER"].ToString();
                    sql = "SELECT IDGROWLER, TMPGROWLER, BATGROWLER,  DTALOGGROWLER FROM GROWLER_LOG WHERE IDGROWLER=" + vlr + " ORDER BY DTALOGGROWLER DESC LIMIT 1";
                    //sql = "SELECT IDGROWLER, TMPGROWLER, BATGROWLER, MAX(DTALOGGROWLER) DTALOGGROWLER FROM GROWLER_LOG WHERE IDGROWLER=" + vlr;
                    //System.out.println("SQL >" + sql);
                    MySQLDB.Close();
                    struturaExecSQL sre = MySQLDB.execReader(sql);
                    System.Data.Common.DbDataReader rs = sre.Reader;
                    //System.out.println("info> Sucesso");

                    bool vazio = !rs.Read();
                    if (vazio || rs["IDGROWLER"] == null || rs["IDGROWLER"] == DBNull.Value)
                    {
                        //System.out.println("info> nenhuma medida coletada ainda");
                        DateTime dataAtual = DateTime.Now;// (System.currentTimeMillis());
                        //SimpleDateFormat sd = new SimpleDateFormat("yyyy-MM-dd hh:mm:SS.0");
                        //sd.format(dataAtual);

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
                        //System.out.println("info> " + " ID do Growler:" + rs.getString("IDGROWLER") + "  " +
                        //        " Temperatura:" + rs.getString("TMPGROWLER") + "  " +
                        //        " Bateria:" + rs.getString("BATGROWLER") + "  " +
                        //        " Data e Hora:" + rs.getString("DTALOGGROWLER"));
                        //rs.beforeFirst();
                        //while (rs.next()) {
                        grls.ListaGrowlers.Add(
                            new Growler(
                                    rs["IDGROWLER"].ToString(),
                                    rs["TMPGROWLER"].ToString(),
                                    rs["BATGROWLER"].ToString(),
                                    rs["DTALOGGROWLER"].ToString()
                                    )
                                );

                        //}
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

            EstruturaRaizGrowlers result = new EstruturaRaizGrowlers();
            try
            {
                string strSql = "SELECT IDGROWLER, TMPGROWLER, BATGROWLER, DTALOGGROWLER FROM GROWLER_LOG WHERE IDGROWLER=" + IdGrowler + " ORDER BY DTALOGGROWLER DESC ";
                struturaExecSQL resultSQL = MySQLDB.execReader(strSql);

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


        //public Growler ConsultarGrowlerAtual(String IdGrowler)
        //{
        //    Growler gr = null;

        //    try
        //    {
        //        if (!(IdGrowler == null))
        //        {
        //            rs = statement().executeQuery("SELECT IDGROWLER, TMPGROWLER, BATGROWLER,  DTALOGGROWLER FROM GROWLER_LOG WHERE IDGROWLER=" + IdGrowler + " ORDER BY DTALOGGROWLER DESC LIMIT 1");
        //            while (rs.next())
        //            {
        //                gr = new Growler(
        //                        rs.getString("IDGROWLER"),
        //                        rs.getString("TMPGROWLER"),
        //                        rs.getString("BATGROWLER"),
        //                        rs.getString("DTALOGGROWLER")
        //                        );
        //            }
        //        }

        //    }
        //    catch (java.sql.SQLException e)
        //    {
        //        System.out.println("Unable to step thru results of query");
        //        showSQLException(e);
        //    }
        //    finally
        //    {
        //        DesconectarDB();
        //    }

        //    return gr;
        //}



    }
}
