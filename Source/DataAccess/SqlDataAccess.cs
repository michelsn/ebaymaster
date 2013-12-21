using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;


namespace EbayMaster
{
    public class SqlDataAccess
    {
        // The sole static Sql Server connecton obj.
        private static SqlConnection _conn = new SqlConnection(ConfigurationManager.AppSettings["DbConnectionString"].ToString());

        public SqlDataAccess()
        {
        }

        public static SqlConnection SqlConn
        {
            get { return _conn; }
        }

        public static bool ExecuteSql(String sql)
        {
            if (sql.Length == 0)
                return false;

            bool retVal = false;
            using (SqlCommand cmd = new SqlCommand(sql, _conn))
            {
                try
                {
                    cmd.CommandTimeout = 240;
                    if (cmd.Connection.State == ConnectionState.Closed)
                        cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    retVal = true;
                }
                catch (SqlException ex)
                {
                    Logger.WriteSystemLog(string.Format("Error : {0}", ex.Message));
                    Logger.WriteSystemLog(sql);
                    retVal = false;
                }
                finally
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                        cmd.Connection.Close();
                }
            }

            return retVal;
        }   // ExecuteSql

        public static DataTable ExecuteSqlReturnTable(String sql)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand(sql, _conn))
            {
                try
                {
                    cmd.CommandTimeout = 240;
                    if (cmd.Connection.State == ConnectionState.Closed)
                        cmd.Connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;

                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "tableName");
                    dt = ds.Tables["tableName"];
                }
                catch (SqlException ex)
                {
                    Logger.WriteSystemLog(string.Format("Error : {0}", ex.Message));
                    Logger.WriteSystemLog(sql);
                }
                finally
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                        cmd.Connection.Close();
                }
            }

            return dt;
        }   // ExecuteSqlReturnTable
    }
}
