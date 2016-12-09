using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using estrutura.growler;
using estrutura.growler.App;
using proxy.database;
using Newtonsoft.Json;
using estrutura;

namespace negocio.growler.App
{
    public class GrowlerNegocio
    {
        public static EstruturaRaiz EsvaziarGrowler(String idGrowler)
        {
            EstruturaRaiz result = new EstruturaRaiz();
            String sql = "";

            try
            {
                sql = "DELETE FROM GROWLER_LOG WHERE IDGROWLER='" + idGrowler+"'";
                //System.out.println("SQL >" + sql);
                //statement().executeUpdate("DELETE FROM GROWLER_LOG WHERE IDGROWLER=" + idGrowler);
                struturaExecSQL resultSQL = MySQLDB.execSQL(sql);
                if (resultSQL.erro)
                    throw new Exception(resultSQL.mensagemDetalhada);
                //System.out.println("info> Sucesso");
            }
            catch (Exception ex)
            {
                //System.out.println("info> ERRO");
                //showSQLException(e);
                //e.printStackTrace();
                result.CodErr = 4;
                result.ExceptionMsg = ex.Message;
                result.IdcErr = 1;
                result.msg = "Não foi possível excluir o Growler " + idGrowler + ".";
                return result;
            }
            finally
            {
                MySQLDB.Close();
            }


            try
            {
                sql = "DELETE FROM GROWLER_MON WHERE IDGROWLER='" + idGrowler + "'";
                //System.out.println("SQL >" + sql);
                //statement().executeUpdate("DELETE FROM GROWLER_LOG WHERE IDGROWLER=" + idGrowler);
                struturaExecSQL resultSQL = MySQLDB.execSQL(sql);
                if (resultSQL.erro)
                    throw new Exception(resultSQL.mensagemDetalhada);
                //System.out.println("info> Sucesso");
            }
            catch (Exception ex)
            {
                //System.out.println("info> ERRO");
                //showSQLException(e);
                //e.printStackTrace();
                result.CodErr = 4;
                result.ExceptionMsg = ex.Message;
                result.IdcErr = 1;
                result.msg = "Não foi possível excluir a solicitação de monitoramento para o Growler " + idGrowler + ".";
                return result;
            }
            finally
            {
                MySQLDB.Close();
            }



            try
            {
                sql = "UPDATE GROWLER SET IDCMON=0 WHERE IDGROWLER='" + idGrowler +"'";
                //System.out.println("SQL >" + sql);
                //statement().executeUpdate("DELETE FROM GROWLER_LOG WHERE IDGROWLER=" + idGrowler);
                //System.out.println("info> Sucesso");
                struturaExecSQL resultSQL = MySQLDB.execSQL(sql);
                if (resultSQL.erro)
                    throw new Exception(resultSQL.mensagemDetalhada);

            }
            catch (Exception ex)
            {
                //System.out.println("info> ERRO");
                //showSQLException(e);
                //e.printStackTrace();
                result.CodErr = 4;
                result.ExceptionMsg = ex.Message;
                result.IdcErr = 1;
                result.msg = "Não foi possível excluir o Growler " + idGrowler + ".";
                return result;
            }
            finally
            {
                MySQLDB.Close();
            }

            return result;


        }




        public static EstruturaRaiz iniciargrowler(GrowlerIni grl)
        {
            EstruturaRaiz result = new EstruturaRaiz();
            String sql = "";
            try
            {
                sql = "DELETE FROM GROWLER WHERE IDGROWLER='" + grl.IdGrowler + "'";
                //System.out.println("SQL >" + sql);
                //statement().executeUpdate(sql);
                //System.out.println("info> Sucesso");
                struturaExecSQL resultSQL = MySQLDB.execSQL(sql);
                if (resultSQL.erro)
                    throw new Exception(resultSQL.mensagemDetalhada);
            }
            catch (Exception ex)
            {
                //System.out.println("info> ERRO");
                //showSQLException(e);
                //e.printStackTrace();
                //return "1";
                result.CodErr = 5;
                result.ExceptionMsg = ex.Message;
                result.IdcErr = 1;
                result.msg = "Não foi possível excluir o Growler " + grl.IdGrowler + ".";
                return result;
            }
            finally
            {
                MySQLDB.Close();
            }


            try
            {
                sql = "INSERT INTO GROWLER (IDGROWLER, VLRTMPIDEGROWLER, IDCALATMP, IDCMON) VALUES ('" + grl.IdGrowler + "'," + grl.TempIdeal + "," + grl.IndNotficacaoTemp + ",1)";
                //System.out.println("SQL >" + sql);
                //statement().executeUpdate(sql);
                //System.out.println("info> Sucesso");
                struturaExecSQL resultSQL = MySQLDB.execSQL(sql);
                if (resultSQL.erro)
                    throw new Exception(resultSQL.mensagemDetalhada);

            }
            catch (Exception ex)
            {
                //System.out.println("info> ERRO");
                //showSQLException(e);
                //e.printStackTrace();
                //return "1";
                result.CodErr = 6;
                result.ExceptionMsg = ex.Message;
                result.IdcErr = 1;
                result.msg = "Não foi possível incluir o Growler " + grl.IdGrowler + ".";
                return result;

            }
            finally
            {
                MySQLDB.Close();
            }



            try
            {
                sql = "DELETE FROM GROWLER_LOG WHERE IDGROWLER='" + grl.IdGrowler+"'";
                //System.out.println("SQL >" + sql);
                //statement().executeUpdate("DELETE FROM GROWLER_LOG WHERE IDGROWLER=" + idGrowler);
                //System.out.println("info> Sucesso");
                struturaExecSQL resultSQL = MySQLDB.execSQL(sql);
                if (resultSQL.erro)
                    throw new Exception(resultSQL.mensagemDetalhada);

            }
            catch (Exception ex)
            {
                //System.out.println("info> ERRO");
                //showSQLException(e);
                //e.printStackTrace();
                //return "1";
                result.CodErr = 7;
                result.ExceptionMsg = ex.Message;
                result.IdcErr = 1;
                result.msg = "Não foi possível incluir o hitórico do Growler " + grl.IdGrowler + ".";
                return result;

            }
            finally
            {
                MySQLDB.Close();
            }


            return result;

        }


        public static EstruturaRaiz ExcluirMonitoramento(string IdGrowler)
        {
            EstruturaRaiz result = new EstruturaRaiz();
            String sql = "";

            try
            {
                sql = "DELETE FROM GROWLER_MON WHERE IDGROWLER = '" + IdGrowler + "'";
                //System.out.println("SQL >" + sql);
                //statement().executeUpdate(sql);
                //System.out.println("info> Sucesso");
                struturaExecSQL resultSQL = MySQLDB.execSQL(sql);
                if (resultSQL.erro)
                    throw new Exception(resultSQL.mensagemDetalhada);

            }
            catch (Exception ex)
            {
                //System.out.println("info> ERRO");
                //showSQLException(e);
                //e.printStackTrace();
                //return "1";
                result.CodErr = 6;
                result.ExceptionMsg = ex.Message;
                result.IdcErr = 1;
                result.msg = "Não foi possível excluir a solicitação de monitoração do Growler " + IdGrowler + ".";
                return result;

            }
            finally
            {
                MySQLDB.Close();
            }


            return result;
        }


        public static EstruturaRaiz SolicitarMonitoramentoGrowler(GrowlerMon value)
        {
            EstruturaRaiz result = new EstruturaRaiz();
            String sql = "";

            try
            {
                sql = "DELETE FROM GROWLER_MON WHERE IDGROWLER = '" + value.IdGrowler + "'";
                //System.out.println("SQL >" + sql);
                //statement().executeUpdate(sql);
                //System.out.println("info> Sucesso");
                struturaExecSQL resultSQL = MySQLDB.execSQL(sql);
                if (resultSQL.erro)
                    throw new Exception(resultSQL.mensagemDetalhada);

            }
            catch (Exception ex)
            {
                //System.out.println("info> ERRO");
                //showSQLException(e);
                //e.printStackTrace();
                //return "1";
                result.CodErr = 6;
                result.ExceptionMsg = ex.Message;
                result.IdcErr = 1;
                result.msg = "Não foi possível excluir a solicitação de monitoração do Growler " + value.IdGrowler + ".";
                return result;

            }
            finally
            {
                MySQLDB.Close();
            }



            try
            {
                sql = "INSERT INTO GROWLER_MON (IDGROWLER, DTAGRAVACAO, IDNOTIFICACAO) VALUES ('" + value.IdGrowler + "', CURRENT_TIMESTAMP, '" + value.IdNotificacao + "')";
                //System.out.println("SQL >" + sql);
                //statement().executeUpdate(sql);
                //System.out.println("info> Sucesso");
                struturaExecSQL resultSQL = MySQLDB.execSQL(sql);
                if (resultSQL.erro)
                    throw new Exception(resultSQL.mensagemDetalhada);

            }
            catch (Exception ex)
            {
                //System.out.println("info> ERRO");
                //showSQLException(e);
                //e.printStackTrace();
                //return "1";
                result.CodErr = 6;
                result.ExceptionMsg = ex.Message;
                result.IdcErr = 1;
                result.msg = "Não foi possível registrar a solicitação de monitoração do Growler " + value.IdGrowler + ".";
                return result;

            }
            finally
            {
                MySQLDB.Close();
            }






            return result;


        }


        public static string TesteNivel(int nivel)
        {
            if (nivel == 2)
                return "Teste nivel Negocio OK";
            else if (nivel == 3)
            {
                String sql = "select curdate() 'data', current_time() 'hora'";
                return "Teste nivel BD -> " + JsonConvert.SerializeObject(ExecSql(sql));
            }
            else
                return MySQLDB.connStr;

        }




        public static ltabela ExecSql(String strsql)
        {

            ltabela result = new ltabela();
            

            try
            {
                struturaExecSQL resultSQL = MySQLDB.execReader(strsql);

                if (!resultSQL.erro)
                {
                    
                    System.Data.Common.DbDataReader rs = resultSQL.Reader;
                    while (rs.Read())
                    {
                        lcampo cp = new lcampo() { nome = "data", valor = rs["data"].ToString() };
                        llinha ln = new llinha() { cp };
                        result.Add(ln);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                MySQLDB.Close();
            }

            return result;

        }






    }
}
