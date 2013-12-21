using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OleDb;

using System.Configuration;

namespace EbayMaster
{
    public enum DatabaseType
    {
        NotSpecified,
        Access,
        SQLServer
        // any other data source type
    }

    public enum ParameterType
    {
        Integer,
        Char,
        VarChar
        // define a common parameter type set
    }

    public class DataFactory
    {
        public static DatabaseType DbType = DBConnectionUtil.DBConn.dbType;
        private static string DbConnectionString = DBConnectionUtil.DBConn.DBConnectionString;

        private static IDbConnection _dbConn = null;

        private DataFactory() { }

        public static IDbConnection DbConnection
        {
            get
            {
                if (_dbConn == null)
                    _dbConn = CreateConnection();
                return _dbConn;
            }
        }

        private static IDbConnection CreateConnection()
        {
            IDbConnection cnn;

            switch (DbType)
            {
                case DatabaseType.Access:
                    cnn = new OleDbConnection(DbConnectionString);
                    break;

                case DatabaseType.SQLServer:
                    cnn = new SqlConnection(DbConnectionString);
                    break;

                default:
                    cnn = new SqlConnection(DbConnectionString);
                    break;
            }

            return cnn;
        }


        public static IDbCommand CreateCommand(string CommandText)
        {
            IDbCommand cmd;
            switch (DbType)
            {
                case DatabaseType.Access:
                    cmd = new OleDbCommand(CommandText, (OleDbConnection)DbConnection);
                    break;

                case DatabaseType.SQLServer:
                    cmd = new SqlCommand(CommandText, (SqlConnection)DbConnection);
                    break;

                default:
                    cmd = new SqlCommand(CommandText, (SqlConnection)DbConnection);
                    break;
            }

            return cmd;
        }

        public static bool AddCommandParam(IDbCommand cmd, string paramName, DbType dbtype, object paramVal)
        {
            if (cmd == null || paramName == null || paramVal == null)
                return false;
            IDbDataParameter param = cmd.CreateParameter();
            param.ParameterName = paramName;
            param.Value = paramVal;
            cmd.Parameters.Add(param);
            return true;
        }

        public static DbDataAdapter CreateAdapter(IDbCommand cmd)
        {
            DbDataAdapter da;
            switch (DbType)
            {
                case DatabaseType.Access:
                    da = new OleDbDataAdapter  ((OleDbCommand)cmd);
                    break;

                case DatabaseType.SQLServer:
                    da = new SqlDataAdapter ((SqlCommand)cmd);
                    break;

                default:
                    da = new SqlDataAdapter((SqlCommand)cmd);
                    break;
            }

            return da;
        }

        public static DataTable ExecuteSqlReturnTable(String sql)
        {
            DataTable dt = new DataTable();

            using (IDbCommand cmd = CreateCommand(sql))
            {
                try
                {
                    cmd.CommandTimeout = 240;
                    if (cmd.Connection.State == ConnectionState.Closed)
                        cmd.Connection.Open();

                    IDbDataAdapter adapter = CreateAdapter(cmd);

                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dt = ds.Tables[0];
                }
                catch (OleDbException ex)
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

        public static bool ExecuteSql(String sql)
        {
            if (sql.Length == 0)
                return false;


            bool retVal = false;
            using (IDbCommand cmd = CreateCommand(sql))
            {
                try
                {
                    cmd.CommandTimeout = 240;
                    if (cmd.Connection.State == ConnectionState.Closed)
                        cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    retVal = true;
                }
                catch (OleDbException ex)
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

        public static bool ExecuteInsertCommand(IDbCommand cmd, out int newId)
        {
            newId = 0;
            bool result = false;

            try
            {
                if (DataFactory.DbConnection.State == ConnectionState.Closed)
                    DataFactory.DbConnection.Open();
                cmd.ExecuteNonQuery();

                IDbCommand cmdNewID = DataFactory.CreateCommand("SELECT @@IDENTITY");

                // Retrieve the Autonumber and store it in the CategoryID column.
                object obj = cmdNewID.ExecuteScalar();
                Int32.TryParse(obj.ToString(), out newId);
                result = newId > 0;
            }
            catch (DataException ex)
            {
                // Write to log here.
                Logger.WriteSystemLog(string.Format("Error : {0}", ex.Message));
                result = false;
            }
            finally
            {
                if (DataFactory.DbConnection.State == ConnectionState.Open)
                    DataFactory.DbConnection.Close();
            }

            return result;
        }

        public static bool ExecuteCommandNonQuery(IDbCommand cmd)
        {
            bool result = false;

            try
            {
                if (DataFactory.DbConnection.State == ConnectionState.Closed)
                    DataFactory.DbConnection.Open();
                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (DataException)
            {
                // Write to log here.
            }
            finally
            {
                if (DataFactory.DbConnection.State == ConnectionState.Open)
                    DataFactory.DbConnection.Close();
            }

            return result;
        }
    }
}
