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
    public class GrowlerNegocio
    {
        public static EstruturaRaiz EsvaziarGrowler(String idGrowler)
        {
            EstruturaRaiz result = new EstruturaRaiz();
            String sql = "";

            try
            {
                sql = "DELETE FROM GROWLER_LOG WHERE IDGROWLER=" + idGrowler;
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
                sql = "UPDATE GROWLER SET IDCMON=0 WHERE IDGROWLER=" + idGrowler;
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
                sql = "DELETE FROM GROWLER WHERE IDGROWLER=" + grl.IdGrowler;
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

            try
            {
                sql = "INSERT INTO GROWLER (IDGROWLER, VLRTMPIDEGROWLER, IDCALATMP, IDCMON) VALUES (" + grl.IdGrowler + "," + grl.TempIdeal + "," + grl.IndNotficacaoTemp + ",1)";
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

            try
            {
                sql = "DELETE FROM GROWLER_LOG WHERE IDGROWLER=" + grl.IdGrowler;
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

            return result;

        }


    }
}
