// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de



using MarcelJoachimKloubert.CLRToolbox.Configuration.Impl;
using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;
using System.IO;

namespace MarcelJoachimKloubert.MetalVZ.MySqlImport
{
    internal static class Program
    {
		#region Methods (2) 

		// Private Methods (2) 

        private static void Import_Users(MySqlConnection mysql, SqlConnection sqlsrv, SqlTransaction transaction)
        {
            using (var mysqlCmd = mysql.CreateCommand())
            {
                mysqlCmd.CommandText = "SELECT `id`, `email`, `password` FROM `users`;";

                using (var sqlsrvCmd = sqlsrv.CreateCommand())
                {
                    sqlsrvCmd.Transaction = transaction;

                    using (var mysqlReader = mysqlCmd.ExecuteReader())
                    {

                    }
                }
            }
        }

        private static void Main(string[] args)
        {
            var configFile = new FileInfo(Path.Combine(Environment.CurrentDirectory, "config.ini"));

            var config = new IniFileConfigRepository(configFile);

            using (var mysql = new MySqlConnection(config.GetValue<string>(category: "src_database", name: "connection_string")))
            {
                mysql.Open();

                using (var sqlsrv = new SqlConnection(config.GetValue<string>(category: "target_database", name: "connection_string")))
                {
                    sqlsrv.Open();

                    SqlTransaction transaction = sqlsrv.BeginTransaction();
                    try
                    {
                        var actions = new Action<MySqlConnection, SqlConnection, SqlTransaction>[]
                            {
                                Import_Users,
                            };

                        foreach (var a in actions)
                        {
                            a(mysql, sqlsrv, transaction);
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

		#endregion Methods 
    }
}
