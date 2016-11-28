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
    }

    public class MySQLDB
    {

        private static MySql.Data.MySqlClient.MySqlConnection db;
        private static MySql.Data.MySqlClient.MySqlDataReader reader = null;


        private static MySqlConnection dbMySQL()
        {
            MySql.Data.MySqlClient.MySqlConnection result = null;
            MySql.Data.MySqlClient.MySqlConnection mconn = null;

            try
            {
                if (mconn != null)
                    mconn.Close();
            }
            catch (Exception) { }


            string connStr = String.Format("server={0};user id={1}; password={2}; database=GROWLERDB; pooling=false",
                "localhost", "sysdba", "masterkey");

            try
            {
                mconn = new MySqlConnection(connStr);
                mconn.Open();
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
            try { if (reader != null) reader.Close(); } catch (Exception) { }

            try
            {
                if (db != null && db.State == System.Data.ConnectionState.Open)
                    db.Close();
            }
            catch (Exception) { }



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
