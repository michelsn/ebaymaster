using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EbayMaster
{
    public class SupplierDAL
    {
        public static int GetSupplierount()
        {
            int count = 0;

            String sql = "select count(*) from [Supplier]";
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql);

            try
            {
                object obj = dt.Rows[0][0];
                if (obj != null)
                    Int32.TryParse(obj.ToString(), out count);
            }
            catch (System.Exception)
            {

            }
            return count;
        }

        public static DataTable GetAllSuppliers()
        {
            String sql = "select * from [Supplier]";
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql);
            return dt;
        }

        public static DataTable GetPagedSuppliers(int pageNum, int pageSize)
        {
            String pagedFormatSql = "select * from [Supplier] where SupplierID in (select top {0} sub.SupplierID from ("
                + " select top {1} SupplierID from [Supplier] order by SupplierID desc) [sub] order by sub.SupplierID) order by SupplierID desc";
            String pagedSql = String.Format(pagedFormatSql, pageSize, pageNum * pageSize);
            DataTable dt = DataFactory.ExecuteSqlReturnTable(pagedSql);
            return dt;
        }

        private static SupplierType GetSupplierFromDataRow(DataRow dr)
        {
            SupplierType supplier = new SupplierType();
            supplier.SupplierID = StringUtil.GetSafeInt(dr["SupplierID"]);
            supplier.SupplierName = StringUtil.GetSafeString(dr["SupplierName"]);
            supplier.SupplierTel = StringUtil.GetSafeString(dr["SupplierTel"]);
            supplier.SupplierLink1 = StringUtil.GetSafeString(dr["SupplierLink1"]);
            supplier.SupplierLink2 = StringUtil.GetSafeString(dr["SupplierLink2"]);
            supplier.SupplierLink3 = StringUtil.GetSafeString(dr["SupplierLink3"]);
            supplier.Comment = StringUtil.GetSafeString(dr["Comment"]);

            return supplier;
        }

        public static SupplierType GetSupplierById(int id)
        {
            String sql = String.Format("select * from [Supplier] where SupplierId={0}", id);
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql);

            if (dt == null || dt.Rows.Count == 0)
                return null;

            return GetSupplierFromDataRow(dt.Rows[0]);
        }

        public static int InsertOneSupplier(SupplierType supplier)
        {
            IDbCommand cmd = DataFactory.CreateCommand(null);
            cmd.CommandText = @"Insert into [Supplier] (SupplierName, SupplierTel, SupplierLink1,"
                + "SupplierLink2, SupplierLink3, Comment) values"
                + "(@SupplierName, @SupplierTel, @SupplierLink1,"
                + "@SupplierLink2, @SupplierLink3, @Comment)";

            DataFactory.AddCommandParam(cmd, "@SupplierName", DbType.String, supplier.SupplierName);
            DataFactory.AddCommandParam(cmd, "@SupplierTel", DbType.String, supplier.SupplierTel);
            DataFactory.AddCommandParam(cmd, "@SupplierLink1", DbType.String, supplier.SupplierLink1);
            DataFactory.AddCommandParam(cmd, "@SupplierLink2", DbType.String, supplier.SupplierLink2);
            DataFactory.AddCommandParam(cmd, "@SupplierLink3", DbType.String, supplier.SupplierLink3);
            DataFactory.AddCommandParam(cmd, "@Comment", DbType.String, supplier.Comment);

            int newId = 0;

            try
            {
                if (DataFactory.DbConnection.State == ConnectionState.Closed)
                    DataFactory.DbConnection.Open();
                cmd.ExecuteNonQuery();

                IDbCommand cmdNewID = DataFactory.CreateCommand("SELECT @@IDENTITY");
                // Retrieve the Autonumber and store it in the CategoryID column.
                object obj = cmdNewID.ExecuteScalar();
                Int32.TryParse(obj.ToString(), out newId);
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

            return newId;
        }

        public static bool ModifyOneSupplier(SupplierType supplier)
        {
            bool result = false;

            if (supplier == null || supplier.SupplierID <= 0)
                return false;

            IDbCommand cmd = DataFactory.CreateCommand(null);
            cmd.CommandText = @"Update [Supplier] set SupplierName=@SupplierName, SupplierTel=@SupplierTel,"
                + "SupplierLink1=@SupplierLink1, SupplierLink2=@SupplierLink2, SupplierLink3=@SupplierLink3,"
                + "Comment=@Comment where SupplierId=@SupplierId";

            DataFactory.AddCommandParam(cmd, "@SupplierName", DbType.String, supplier.SupplierName);
            DataFactory.AddCommandParam(cmd, "@SupplierTel", DbType.String, supplier.SupplierTel);
            DataFactory.AddCommandParam(cmd, "@SupplierLink1", DbType.String, supplier.SupplierLink1);
            DataFactory.AddCommandParam(cmd, "@SupplierLink2", DbType.String, supplier.SupplierLink2);
            DataFactory.AddCommandParam(cmd, "@SupplierLink3", DbType.String, supplier.SupplierLink3);
            DataFactory.AddCommandParam(cmd, "@Comment", DbType.String, supplier.Comment);

            DataFactory.AddCommandParam(cmd, "@SupplierId", DbType.Int32, supplier.SupplierID);

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
                result = false;
            }
            finally
            {
                if (DataFactory.DbConnection.State == ConnectionState.Open)
                    DataFactory.DbConnection.Close();
            }

            return result;
        }

        public static bool DeleteOneSupplier(int supplierId)
        {
            bool result = false;

            SupplierType supplier = GetSupplierById(supplierId);
            if (supplier == null)
                return false;

            String sql = string.Format("delete from [Supplier] where Supplierid={0}", supplierId);
            DataFactory.ExecuteSql(sql);
            result = true;

            return result;
        }
    }
}
