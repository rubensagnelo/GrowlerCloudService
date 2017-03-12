using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace proxy.database
{
    public struct struturaExecSQL
    {
        public Int64 linhasafetadas;
        public bool erro;
        public string mensagem;
        public string mensagemDetalhada;
        public System.Data.Common.DbDataReader Reader;

        public void verificarErro()
        {
            if (erro)
                throw new Exception(this.mensagem);
        }
    }


    public class MySQLDB
    {

        private static MySql.Data.MySqlClient.MySqlConnection db;
        private static MySql.Data.MySqlClient.MySqlDataReader reader = null;
        private static MySql.Data.MySqlClient.MySqlConnection mconn = null;

        public static string connStr { get { return util.configTools.getConfig("mysqldb"); } }

        private static MySqlConnection dbMySQL()
        {
            MySql.Data.MySqlClient.MySqlConnection result = null;
            try
            {
                if (mconn == null || mconn.State != ConnectionState.Open)
                    mconn = new MySqlConnection(connStr);

                if (mconn.State == ConnectionState.Closed)
                {
                    mconn.Open();
                }
                result = mconn;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            return result;
        }




        public static struturaExecSQL execReader(string sql)
        {
            Close();
            db = dbMySQL();
            struturaExecSQL result = new struturaExecSQL();
            reader = null;
            try
            {
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, db);
                reader = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                result.erro = true;
                result.mensagem = ex.Message;
                result.mensagemDetalhada = string.Empty;
                result.Reader = null;

                Console.WriteLine(ex.Message);
                if (ex.InnerException != null && ex.InnerException.Message != null)
                {
                    result.mensagemDetalhada = ex.InnerException.Message;
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
            finally
            {
            }
            result.Reader = reader;
            return result;
        }


        public static void Close()
        {
            forceCloserReader();
            forceCloserDataBase();
        }


        private static void forceCloserReader()
        {
            try
            {
                reader.Close();
            }
            catch { };
        }

        private static void forceCloserDataBase()
        {
            try
            {
                db.Close();
            }
            catch { };
        }


        public static struturaExecSQL execSQL(string sql)
        {
            //Int64 result = -1;
            struturaExecSQL result = new struturaExecSQL();
            int linafe = 0;

            db = null;
            try
            {
                db = dbMySQL();
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, db);

                forceCloserReader();


                linafe = cmd.ExecuteNonQuery();

                result.linhasafetadas = linafe; //"0;OK;" + linafe.ToString();
                result.erro = false;
                result.mensagem = string.Empty;
            }
            catch (Exception ex)
            {
                result.linhasafetadas = 0; //"0;OK;" + linafe.ToString();
                result.erro = true;
                result.mensagem = ex.Message;
                result.mensagemDetalhada = string.Empty;
                //"1;ERRO:" + ex.Message + ";0";

                Console.WriteLine(ex.Message);
                if (ex.InnerException != null && ex.InnerException.Message != null)
                {
                    result.mensagemDetalhada = ex.InnerException.Message;
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
            finally
            {

                try
                {
                    if (db != null && db.State == System.Data.ConnectionState.Open)
                        Close();
                }
                catch (Exception) { }

            }
            return result;

        }



    }
}
