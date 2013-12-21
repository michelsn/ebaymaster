using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EbayMaster
{
    public class ItemSupplierDAL
    {
        public static bool CheckItemSupplierExisted(int itemId, int supplierId)
        {
            String sql = String.Format("select * from [ItemSupplier] where ItemId={0} and SupplierId={1}",
                itemId, supplierId);
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql);

            bool existed = dt != null && dt.Rows.Count > 0;
            return existed;
        }

        public static DataTable GetAllItemSuppliersByItemId(int itemId)
        {
            String sql = String.Format("select * from [ItemSupplier] where ItemId={0}", itemId);
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql);
            return dt;
        }

        private static bool InsertOneItemSupplier(ItemSupplierType itemSupplier)
        {
            IDbCommand cmd = DataFactory.CreateCommand(null);
            cmd.CommandText = @"Insert into [ItemSupplier](ItemId, SupplierId, SouringURL,Price, ShippingFee, Comment, CreateDate) values"
                + "(@ItemId, @SupplierId, @SouringURL,@Price,@ShippingFee, @Comment, @CreateDate)";

            DataFactory.AddCommandParam(cmd, "@ItemId", DbType.Int32, itemSupplier.ItemId);
            DataFactory.AddCommandParam(cmd, "@SupplierId", DbType.Int32, itemSupplier.SupplierId);
            DataFactory.AddCommandParam(cmd, "@SouringURL", DbType.String, itemSupplier.SourcingURL);
            DataFactory.AddCommandParam(cmd, "@Price", DbType.Double, itemSupplier.Price);
            DataFactory.AddCommandParam(cmd, "@ShippingFee", DbType.Double, itemSupplier.ShippingFee);
            DataFactory.AddCommandParam(cmd, "@Comment", DbType.String, itemSupplier.Comment);
            DataFactory.AddCommandParam(cmd, "@CreateDate", DbType.DateTime, itemSupplier.CreatedDate.ToShortDateString());

            try
            {
                if (DataFactory.DbConnection.State == ConnectionState.Closed)
                    DataFactory.DbConnection.Open();
                cmd.ExecuteNonQuery();
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

            return true;
        }

        public static bool AddOneItemSupplier(ItemSupplierType itemSupplier)
        {
            if (itemSupplier == null)
                return false;

            // Check if existed.
            if (CheckItemSupplierExisted(itemSupplier.ItemId, itemSupplier.SupplierId))
                return false;

            // Insert the new record.
            InsertOneItemSupplier(itemSupplier);
            return true;
        }

        public static bool ModifyItemSupplier(ItemSupplierType itemSupplier)
        {
            if (itemSupplier == null || itemSupplier.ItemId <= 0 || itemSupplier.SupplierId <= 0)
                return false;

            if (false == CheckItemSupplierExisted(itemSupplier.ItemId, itemSupplier.SupplierId))
                return false;

            bool result = false;

            IDbCommand cmd = DataFactory.CreateCommand(null);
            cmd.CommandText = @"Update [ItemSupplier] set SouringURL=@SouringURL, Price=@Price, ShippingFee=@ShippingFee"
                + " Comment=@Comment, CreateDate=@CreateDate where ItemId=@ItemId and SupplierId=@SupplierId";

            DataFactory.AddCommandParam(cmd, "@SouringURL", DbType.String, itemSupplier.SourcingURL);
            DataFactory.AddCommandParam(cmd, "@Price", DbType.Double, itemSupplier.Price);
            DataFactory.AddCommandParam(cmd, "@ShippingFee", DbType.Double, itemSupplier.Price);
            DataFactory.AddCommandParam(cmd, "@Comment", DbType.String, itemSupplier.Comment);
            DataFactory.AddCommandParam(cmd, "@CreateDate", DbType.DateTime, itemSupplier.CreatedDate.ToShortDateString());

            DataFactory.AddCommandParam(cmd, "@ItemId", DbType.Int32, itemSupplier.ItemId);
            DataFactory.AddCommandParam(cmd, "@SupplierId", DbType.Int32, itemSupplier.SupplierId);

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

        public static bool DeleteItemSuppliers(int itemId)
        {
            bool result = false;

            String sql = string.Format("delete from [ItemSupplier] where ItemId={0}",itemId);
            DataFactory.ExecuteSql(sql);
            result = true;

            return result;
        }

        public static bool DeleteItemSupplier(ItemSupplierType itemSupplier)
        {
            bool result = false;

            String sql = string.Format("delete from [ItemSupplier] where ItemId={0} and SupplierId={1}",
                itemSupplier.ItemId, itemSupplier.SupplierId);
            DataFactory.ExecuteSql(sql);
            result = true;

            return result;
        }
    }
}
