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
using MySql.Data.MySqlClient;
using System.Globalization;

namespace negocio.growler.App
{
    public class GrowlerNegocio
    {
        public static EstruturaRaiz EsvaziarGrowler(String idGrowler)
        {
            MySQLDB.validainjecaoSQL(idGrowler);

            EstruturaRaiz result = new EstruturaRaiz();
            String sql = "";

            List<MySqlParameter> prm = new List<MySqlParameter>();
            MySqlParameter pr = new MySqlParameter("@IDGROWLER", MySqlDbType.String);
            pr.Value = idGrowler;
            prm.Add(pr);


            try
            {
                sql = "DELETE FROM GROWLER_LOG WHERE IDGROWLER=@IDGROWLER";
                struturaExecSQL resultSQL = MySQLDB.execSQL(sql,prm);

                if (resultSQL.erro)
                    throw new Exception(resultSQL.mensagemDetalhada);
            }
            catch (Exception ex)
            {
                result.CodErr = 4;
                result.ExceptionMsg = ex.Message;
                result.IdcErr = 1;
                result.msg = $"Não foi possível excluir o Growler {idGrowler}.";
                return result;
            }
            finally
            {
                MySQLDB.Close();
            }


            try
            {
                sql = "DELETE FROM GROWLER_MON WHERE IDGROWLER=@IDGROWLER";
                struturaExecSQL resultSQL = MySQLDB.execSQL(sql,prm);

                if (resultSQL.erro)
                    throw new Exception(resultSQL.mensagemDetalhada);
            }
            catch (Exception ex)
            {
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
                sql = "UPDATE GROWLER SET IDCMON=0 WHERE IDGROWLER=@IDGROWLER";
                struturaExecSQL resultSQL = MySQLDB.execSQL(sql, prm);

                if (resultSQL.erro)
                    throw new Exception(resultSQL.mensagemDetalhada);

            }
            catch (Exception ex)
            {
                result.CodErr = 4;
                result.ExceptionMsg = ex.Message;
                result.IdcErr = 1;
                result.msg = $"Não foi possível excluir o Growler {idGrowler}.";
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

            MySQLDB.validaEtruturaInjecaoSQL(grl);


            EstruturaRaiz result = new EstruturaRaiz();
            String sql = "";

            try
            {

                List<MySqlParameter> prm = new List<MySqlParameter>();

                MySqlParameter pr = new MySqlParameter("@IDGROWLER", MySqlDbType.String);
                pr.Value = grl.IdGrowler;
                prm.Add(pr);


                sql = "DELETE FROM GROWLER WHERE IDGROWLER=@IDGROWLER";
                struturaExecSQL resultSQL = MySQLDB.execSQL(sql,prm);

                if (resultSQL.erro)
                    throw new Exception(resultSQL.mensagemDetalhada);
            }
            catch (Exception ex)
            {
                result.CodErr = 5;
                result.ExceptionMsg = ex.Message;
                result.IdcErr = 1;
                result.msg = $"Não foi possível excluir o Growler {grl.IdGrowler}.";
                return result;
            }
            finally
            {
                MySQLDB.Close();
            }




            try
            {

                List<MySqlParameter> prm = new List<MySqlParameter>();

                MySqlParameter pr = new MySqlParameter("@IDGROWLER", MySqlDbType.String);
                pr.Value = grl.IdGrowler;
                prm.Add(pr);


                //Valorado por causa da formatação decimal como americana (ponto no separador decimal)
                CultureInfo current = CultureInfo.DefaultThreadCurrentCulture;
                CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

                MySqlParameter pr2 = new MySqlParameter("@TEMPIDEAL", MySqlDbType.Decimal);
                pr2.Value = grl.TempIdeal;
                prm.Add(pr2);


                MySqlParameter pr3 = new MySqlParameter("@INDNOTFICACAOTEMP", MySqlDbType.Int32);
                pr3.Value = grl.IndNotficacaoTemp;
                prm.Add(pr3);

                sql = "INSERT INTO GROWLER (IDGROWLER, VLRTMPIDEGROWLER, IDCALATMP, IDCMON) VALUES (@IDGROWLER , @TEMPIDEAL , @INDNOTFICACAOTEMP ,1)";
                struturaExecSQL resultSQL = MySQLDB.execSQL(sql, prm);

                //retornado a cultura original após a insersão do valor com separador decimal
                CultureInfo.DefaultThreadCurrentCulture = current;

                if (resultSQL.erro)
                    throw new Exception(resultSQL.mensagemDetalhada);

            }
            catch (Exception ex)
            {
                result.CodErr = 6;
                result.ExceptionMsg = ex.Message;
                result.IdcErr = 1;
                result.msg = $"Não foi possível incluir o Growler {grl.IdGrowler}.";
                return result;

            }
            finally
            {
                MySQLDB.Close();
            }



            try
            {

                List<MySqlParameter> prm = new List<MySqlParameter>();

                MySqlParameter pr = new MySqlParameter("@IDGROWLER", MySqlDbType.String);
                pr.Value = grl.IdGrowler;
                prm.Add(pr);

                sql = "DELETE FROM GROWLER_LOG WHERE IDGROWLER=@IDGROWLER";
                struturaExecSQL resultSQL = MySQLDB.execSQL(sql,prm);

                if (resultSQL.erro)
                    throw new Exception(resultSQL.mensagemDetalhada);

            }
            catch (Exception ex)
            {
                result.CodErr = 7;
                result.ExceptionMsg = ex.Message;
                result.IdcErr = 1;
                result.msg = $"Não foi possível incluir o hitórico do Growler {grl.IdGrowler}.";
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

            MySQLDB.validainjecaoSQL(IdGrowler);


            EstruturaRaiz result = new EstruturaRaiz();
            String sql = "";

            try
            {

                List<MySqlParameter> prm = new List<MySqlParameter>();
                MySqlParameter pr = new MySqlParameter("@IDGROWLER", MySqlDbType.String);
                pr.Value = IdGrowler;
                prm.Add(pr);

                sql = "DELETE FROM GROWLER_MON WHERE IDGROWLER=@IDGROWLER";
                struturaExecSQL resultSQL = MySQLDB.execSQL(sql, prm);

                if (resultSQL.erro)
                    throw new Exception(resultSQL.mensagemDetalhada);

            }
            catch (Exception ex)
            {
                result.CodErr = 6;
                result.ExceptionMsg = ex.Message;
                result.IdcErr = 1;
                result.msg = $"Não foi possível excluir a solicitação de monitoração do Growler{IdGrowler}.";
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
            MySQLDB.validaEtruturaInjecaoSQL(value);

            EstruturaRaiz result = new EstruturaRaiz();
            String sql = "";

            try
            {

                List<MySqlParameter> prm = new List<MySqlParameter>();
                MySqlParameter pr = new MySqlParameter("@IDGROWLER", MySqlDbType.String);
                pr.Value = value.IdGrowler;
                prm.Add(pr);

                sql = "DELETE FROM GROWLER_MON WHERE IDGROWLER=@IDGROWLER";
                struturaExecSQL resultSQL = MySQLDB.execSQL(sql, prm);

                if (resultSQL.erro)
                    throw new Exception(resultSQL.mensagemDetalhada);

            }
            catch (Exception ex)
            {
                result.CodErr = 6;
                result.ExceptionMsg = ex.Message;
                result.IdcErr = 1;
                result.msg = $"Não foi possível excluir a solicitação de monitoração do Growler {value.IdGrowler}.";
                return result;

            }
            finally
            {
                MySQLDB.Close();
            }



            try
            {
                List<MySqlParameter> prm = new List<MySqlParameter>();
                MySqlParameter pr = new MySqlParameter("@IDGROWLER", MySqlDbType.String);
                pr.Value = value.IdGrowler;
                prm.Add(pr);

                MySqlParameter pr2 = new MySqlParameter("@IDNOTIFICACAO", MySqlDbType.String);
                pr2.Value = value.IdNotificacao;
                prm.Add(pr2);

                sql = "INSERT INTO GROWLER_MON (IDGROWLER, DTAGRAVACAO, IDNOTIFICACAO) VALUES (@IDGROWLER, CURRENT_TIMESTAMP, @IDNOTIFICACAO)";
                struturaExecSQL resultSQL = MySQLDB.execSQL(sql);
                if (resultSQL.erro)
                    throw new Exception(resultSQL.mensagemDetalhada);

            }
            catch (Exception ex)
            {
                result.CodErr = 6;
                result.ExceptionMsg = ex.Message;
                result.IdcErr = 1;
                result.msg = $"Não foi possível registrar a solicitação de monitoração do Growler {value.IdGrowler}.";
                return result;
            }
            finally
            {
                MySQLDB.Close();
            }

            return result;

        }

    }
}
