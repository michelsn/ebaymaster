//  -------------------------------------------------------------------------------
// Retrieve or update item info from database.
//  Item info is the  key of inventory management system, each item owns one system unique identifier,
//  called the SKU, the SKU is the sole of the inventory management.
//  -------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EbayMaster
{
    public class ItemDAL
    {
        public static int GetItemCount()
        {
            int itemCount = 0;

            String sql = "select count(*) from [Item]";
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql);

            try
            {
                object obj = dt.Rows[0][0];
                if (obj != null)
                    Int32.TryParse(obj.ToString(), out itemCount);
            }
            catch (System.Exception)
            {

            }
            return itemCount;
        }
        
        public static DataTable GetAllItems()
        {
            String sql_getAllMessages = "select * from [Item]";
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql_getAllMessages);
            return dt;
        }

        public static DataTable GetPagedItems(int pageNum, int pageSize)
        {
            String pagedFormatSql = "select * from [Item] where ItemId in (select top {0} sub.ItemId from ("
                + " select top {1} ItemId from [Item] order by ItemId desc) [sub] order by sub.ItemId) order by ItemId desc";
            String pagedSql = String.Format(pagedFormatSql, pageSize, pageNum * pageSize);
            DataTable dt = DataFactory.ExecuteSqlReturnTable(pagedSql);
            return dt;
        }

        public static bool IncreaseItem(string sku, int count)
        {
            InventoryItemType item = GetItemBySKU(sku);
            if (item == null)
                return false;

            int newStock = item.ItemStockNum + count;

            String sql = string.Format("update [Item] set ItemStockNum={0} where ItemSKU='{1}'", newStock, sku);
            DataFactory.ExecuteSql(sql);
            return true;            
        }

        public static bool DecreaseItem(string sku, int count)
        {
            InventoryItemType item = GetItemBySKU(sku);
            if (item == null)
                return false;

            if (count > item.ItemStockNum)
                return false;

            int newStock = item.ItemStockNum - count;

            if (newStock < 0)
                return false;

            String sql = string.Format("update [Item] set ItemStockNum={0} where ItemSKU='{1}'", newStock, sku);
            DataFactory.ExecuteSql(sql);
            return true;
        }

        public static bool ShipItem(string sku, int count)
        {
           InventoryItemType item = GetItemBySKU(sku);
           if (item == null)
              return false;

           if (item.IsGroupItem)
           {
              List<string> skus = item.SubItemSKUList;
              foreach (string subItemSku in skus)
              {
                 DecreaseItem(subItemSku, count);
              }
           }
           else
           {
              DecreaseItem(sku, count);
           }

           return true;
        }

        public static InventoryItemType GetItemBySKU(string SKU)
        {
            DataTable dt = GetItemTableBySKU(SKU);
            if (dt.Rows.Count == 0)
                return null;

            DataRow dr = dt.Rows[0];

            InventoryItemType item = new InventoryItemType();
            item.ItemId = StringUtil.GetSafeInt(dr["ItemId"]);
            item.CategoryId = Int32.Parse(dr["CategoryId"].ToString());
            item.ItemName = dr["ItemName"].ToString();
            item.ItemSKU = SKU;
            item.ItemImagePath = StringUtil.GetSafeString(dr["ItemImagePath"]);
            item.ItemStockShresholdNum = Int32.Parse(dr["ItemStockShresholdNum"].ToString());
            item.ItemStockNum = Int32.Parse(dr["ItemStockNum"].ToString());
            item.ItemSourcingInfo = dr["ItemSourcingInfo"].ToString();
            item.ItemSourcingURL = StringUtil.GetSafeString(dr["ItemSourcingURL"]);
            item.ItemCost = Double.Parse(dr["ItemCost"].ToString());
            item.ItemWeight = Double.Parse(dr["ItemWeight"].ToString());
            item.ItemCustomName = dr["ItemCustomName"].ToString();
            item.ItemCustomWeight = Double.Parse(dr["ItemCustomWeight"].ToString());
            item.ItemCustomCost = Double.Parse(dr["ItemCustomCost"].ToString());
            item.ItemAddDateTime = DateTime.Parse(dr["ItemAddDateTime"].ToString());
            item.IsGroupItem = Boolean.Parse(dr["IsGroupItem"].ToString());
            item.SubItemSKUs = dr["SubItemSKUs"].ToString();
            item.ItemNote = StringUtil.GetSafeString(dr["ItemNote"]);

            return item;
        }

        public static InventoryItemType GetItemById(int itemId)
        {
            DataTable dt = GetItemTableById(itemId);
            if (dt.Rows.Count == 0)
                return null;

            DataRow dr = dt.Rows[0];

            InventoryItemType item = new InventoryItemType();
            item.ItemId = itemId;
            item.CategoryId = Int32.Parse(dr["CategoryId"].ToString());
            item.ItemName = dr["ItemName"].ToString();
            item.ItemSKU = dr["ItemSKU"].ToString();
            //item.ItemImage = (byte[])dr["ItemImage"];
            item.ItemImagePath = StringUtil.GetSafeString(dr["ItemImagePath"]);
            item.ItemStockShresholdNum = Int32.Parse(dr["ItemStockShresholdNum"].ToString());
            item.ItemStockNum = Int32.Parse(dr["ItemStockNum"].ToString());
            item.ItemSourcingInfo = dr["ItemSourcingInfo"].ToString();
            item.ItemSourcingURL = StringUtil.GetSafeString(dr["ItemSourcingURL"]);
            item.ItemCost = Double.Parse(dr["ItemCost"].ToString());
            item.ItemWeight = Double.Parse(dr["ItemWeight"].ToString());
            item.ItemCustomName = dr["ItemCustomName"].ToString();
            item.ItemCustomWeight = Double.Parse(dr["ItemCustomWeight"].ToString());
            item.ItemCustomCost = Double.Parse(dr["ItemCustomCost"].ToString());
            item.ItemAddDateTime = DateTime.Parse(dr["ItemAddDateTime"].ToString());
            item.IsGroupItem = Boolean.Parse(dr["IsGroupItem"].ToString());
            item.SubItemSKUs = dr["SubItemSKUs"].ToString();
            item.ItemNote = StringUtil.GetSafeString(dr["ItemNote"]);

            return item;
        }

        public static DataTable GetItemTableById(int itemId)
        {
            String sql_getAllMessages = "select * from [Item] where ItemId=" + itemId;
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql_getAllMessages);
            return dt;
        }

        public static DataTable GetItemTableBySKU(string sku)
        {
            String sql_getAllMessages = "select * from [Item] where ItemSKU='" + sku + "'";
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql_getAllMessages);
            return dt;
        }

        private static string GetDefaultValue(string raw)
        {
            if (raw == null)
                return "";
            else
                return raw;
        }

        public static int InsertOneItem(InventoryItemType item)
        {
            string itemSKU = item.ItemSKU;
            if (itemSKU == null || itemSKU.Trim() == "")
                return -1;

            // We should ensure the item SKU is unique, this is our bottom line!!!
            InventoryItemType existedItemWithSameSKU = GetItemBySKU(itemSKU);
            if (existedItemWithSameSKU != null)
                return -1;

            IDbCommand cmd = DataFactory.CreateCommand(null);
            cmd.CommandText = @"Insert into [Item] (CategoryId, ItemName, ItemSKU, ItemImagePath,"
                + " ItemStockShresholdNum, ItemStockNum, ItemSourcingInfo, ItemCost, ItemWeight, "
                + " ItemCustomName, ItemCustomWeight, ItemCustomCost, ItemAddDateTime, "
                + " IsGroupItem, SubItemSKUs, ItemSourcingURL, ItemDispatchTips, ItemNote) values ("
                + " @CategoryId, @ItemName, @ItemSKU, @ItemImagePath, "
                + " @ItemStockShresholdNum, @ItemStockNum, @ItemSourcingInfo, @ItemCost, @ItemWeight, "
                + " @ItemCustomName, @ItemCustomWeight, @ItemCustomCost, @ItemAddDateTime, "
                + " @IsGroupItem, @SubItemSKUs, @ItemSourcingURL, @ItemDispatchTips, @ItemNote)";

            DataFactory.AddCommandParam(cmd, "@CategoryId", DbType.Int32, item.CategoryId);
            DataFactory.AddCommandParam(cmd, "@ItemName", DbType.String, GetDefaultValue(item.ItemName));

            DataFactory.AddCommandParam(cmd, "@ItemSKU", DbType.Int32, GetDefaultValue(item.ItemSKU));
            DataFactory.AddCommandParam(cmd, "@ItemImagePath", DbType.String, item.ItemImagePath);

            DataFactory.AddCommandParam(cmd, "@ItemStockShresholdNum", DbType.Int32, item.ItemStockShresholdNum);
            DataFactory.AddCommandParam(cmd, "@ItemStockNum", DbType.Int32, item.ItemStockNum);

            DataFactory.AddCommandParam(cmd, "@ItemSourcingInfo", DbType.String, GetDefaultValue(item.ItemSourcingInfo));
            DataFactory.AddCommandParam(cmd, "@ItemCost", DbType.Double, item.ItemCost);

            DataFactory.AddCommandParam(cmd, "@ItemWeight", DbType.Double, item.ItemWeight);
            DataFactory.AddCommandParam(cmd, "@ItemCustomName", DbType.String, GetDefaultValue(item.ItemCustomName));

            DataFactory.AddCommandParam(cmd, "@ItemCustomWeight", DbType.Double, item.ItemCustomWeight);
            DataFactory.AddCommandParam(cmd, "@ItemCustomCost", DbType.Double, item.ItemCustomCost);

            DataFactory.AddCommandParam(cmd, "@ItemAddDateTime", DbType.DateTime, item.ItemAddDateTime.ToString());

            DataFactory.AddCommandParam(cmd, "@IsGroupItem", DbType.Boolean, item.IsGroupItem);
            DataFactory.AddCommandParam(cmd, "@SubItemSKUs", DbType.String, GetDefaultValue(item.SubItemSKUs));

            DataFactory.AddCommandParam(cmd, "@ItemSourcingURL", DbType.String, StringUtil.GetSafeString(item.ItemSourcingURL));
            DataFactory.AddCommandParam(cmd, "@ItemDispatchTips", DbType.String, "");
            DataFactory.AddCommandParam(cmd, "@ItemNote", DbType.String, StringUtil.GetSafeString(item.ItemNote));

            int newItemId = 0;

            try
            {
                if (DataFactory.DbConnection.State == ConnectionState.Closed)
                    DataFactory.DbConnection.Open();
                cmd.ExecuteNonQuery();

                IDbCommand cmdNewID = DataFactory.CreateCommand("SELECT @@IDENTITY");
                // Retrieve the Autonumber and store it in the CategoryID column.
                 object obj = cmdNewID.ExecuteScalar();
                Int32.TryParse(obj.ToString(), out newItemId);
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

            return newItemId;
        }

        public static bool DeleteOneItem(int itemId)
        {
            bool result = false;

            InventoryItemType itemInfo = GetItemById(itemId);
            if (itemInfo == null || itemInfo.ItemId <= 0)
                return false;

            String sql = string.Format("delete from [Item] where ItemId={0}", itemId);
            DataFactory.ExecuteSql(sql);
            result = true;

            return result;
        }   // DeleteOneItem

        public static bool ModifyItemCategory(int itemId, int newCatId)
        {
            String sql = string.Format("update [Item] set CategoryId={0} where ItemId={1}", newCatId, itemId);
            DataFactory.ExecuteSql(sql);
            return true;
        }

        public static bool ModifyItemNote(InventoryItemType item)
        {
            bool result = false;

            // Delete and insert one
            if (item == null || item.ItemId < 0)
                return false;

            IDbCommand cmd = DataFactory.CreateCommand(null);
            cmd.CommandText = @"Update [Item] set ItemNote=@ItemNote where ItemId=@ItemId";

            DataFactory.AddCommandParam(cmd, "@ItemNote", DbType.String, StringUtil.GetSafeString(item.ItemNote));
            DataFactory.AddCommandParam(cmd, "@ItemId", DbType.Int32, item.ItemId);

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

        public static bool ModifyOneItem(InventoryItemType item)
        {
            bool result = false;

            // Delete and insert one
            if (item == null || item.ItemId < 0)
                return false;

            IDbCommand cmd = DataFactory.CreateCommand(null);
            cmd.CommandText = @"Update [Item] set CategoryId=@CategoryId, ItemName=@ItemName, ItemSKU=@ItemSKU, ItemImagePath=@ItemImagePath,"
                + " ItemStockShresholdNum=@ItemStockShresholdNum, ItemStockNum=@ItemStockNum, ItemSourcingInfo=@ItemSourcingInfo, ItemCost=@ItemCost, ItemWeight=@ItemWeight, "
                + " ItemCustomName=@ItemCustomName, ItemCustomWeight=@ItemCustomWeight, ItemCustomCost=@ItemCustomCost, ItemAddDateTime=@ItemAddDateTime, "
                + " IsGroupItem=@IsGroupItem, SubItemSKUs=@SubItemSKUs, ItemSourcingURL=@ItemSourcingURL, ItemDispatchTips=@ItemDispatchTips, ItemNote=@ItemNote where ItemId=@ItemId";

            DataFactory.AddCommandParam(cmd, "@CategoryId", DbType.Int32, item.CategoryId);
            DataFactory.AddCommandParam(cmd, "@ItemName", DbType.String, GetDefaultValue(item.ItemName));

            DataFactory.AddCommandParam(cmd, "@ItemSKU", DbType.String, StringUtil.GetSafeString(item.ItemSKU));
            DataFactory.AddCommandParam(cmd, "@ItemImagePath", DbType.String, item.ItemImagePath);

            DataFactory.AddCommandParam(cmd, "@ItemStockShresholdNum", DbType.Int32, item.ItemStockShresholdNum);
            DataFactory.AddCommandParam(cmd, "@ItemStockNum", DbType.Int32, item.ItemStockNum);

            DataFactory.AddCommandParam(cmd, "@ItemSourcingInfo", DbType.String, GetDefaultValue(item.ItemSourcingInfo));
            DataFactory.AddCommandParam(cmd, "@ItemCost", DbType.Double, item.ItemCost);

            DataFactory.AddCommandParam(cmd, "@ItemWeight", DbType.Double, item.ItemWeight);
            DataFactory.AddCommandParam(cmd, "@ItemCustomName", DbType.String, GetDefaultValue(item.ItemCustomName));

            DataFactory.AddCommandParam(cmd, "@ItemCustomWeight", DbType.Double, item.ItemCustomWeight);
            DataFactory.AddCommandParam(cmd, "@ItemCustomCost", DbType.Double, item.ItemCustomCost);

            DataFactory.AddCommandParam(cmd, "@ItemAddDateTime", DbType.DateTime, item.ItemAddDateTime.ToString());

            DataFactory.AddCommandParam(cmd, "@IsGroupItem", DbType.Boolean, item.IsGroupItem);
            DataFactory.AddCommandParam(cmd, "@SubItemSKUs", DbType.String, GetDefaultValue(item.SubItemSKUs));

            DataFactory.AddCommandParam(cmd, "@ItemSourcingURL", DbType.String, StringUtil.GetSafeString(item.ItemSourcingURL));
            DataFactory.AddCommandParam(cmd, "@ItemDispatchTips", DbType.String, "");
            DataFactory.AddCommandParam(cmd, "@ItemNote", DbType.String, StringUtil.GetSafeString(item.ItemNote));

            DataFactory.AddCommandParam(cmd, "@ItemId", DbType.Int32, item.ItemId);

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
        }   // ModifyOneItem
    }
}
