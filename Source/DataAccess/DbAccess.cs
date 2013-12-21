using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Configuration;

namespace EbayMaster
{
   public sealed class DbAccess
   {
       private static OleDbConnection _conn = new OleDbConnection(DBConnectionUtil.DBConn.DBConnectionString);

      private DbAccess()
      {
      }

      public static OleDbConnection DbConn
      {
         get
         {
            return _conn;
         }
      }

      public static bool ExecuteSql(String sql)
      {
         if (sql.Length == 0)
            return false;


         bool retVal = false;
         using (OleDbCommand cmd = new OleDbCommand(sql, DbAccess._conn))
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

      public static DataTable ExecuteSqlReturnTable(String sql)
      {
         DataTable dt = new DataTable();

         using (OleDbCommand cmd = new OleDbCommand(sql, DbAccess._conn))
         {
            try
            {
               cmd.CommandTimeout = 240;
               if (cmd.Connection.State == ConnectionState.Closed)
                  cmd.Connection.Open();

               OleDbDataAdapter adapter = new OleDbDataAdapter();
               adapter.SelectCommand = cmd;

               DataSet ds = new DataSet();
               adapter.Fill(ds, "tableName");
               dt = ds.Tables["tableName"];
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
   }   // class DbAccess
}
