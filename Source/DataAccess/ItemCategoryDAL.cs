//  -------------------------------------------------------------------------------
// Retrieve or update item category info from database.
//  -------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EbayMaster
{
    public class ItemCategoryDAL
    {
        public static DataTable GetAllCategories()
        {
            String sql_getAllCategories = "select * from [Category]";
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql_getAllCategories);
            return dt;
        }

        public static bool InsertOneCategory(ItemCategoryType cat)
        {
            IDbCommand cmd = DataFactory.CreateCommand(null);
            cmd.CommandText = @"Insert into [Category] (ParentCategoryId, CategoryName, CategorySkuPrefix) values (@ParentCategoryId, @CategoryName, @CategorySkuPrefix)";

            DataFactory.AddCommandParam(cmd, "@ParentCategoryId", DbType.Int32, cat.ParentCategoryId);
            DataFactory.AddCommandParam(cmd, "@CategoryName", DbType.String, StringUtil.GetSafeString(cat.CategoryName));
            DataFactory.AddCommandParam(cmd, "@CategorySkuPrefix", DbType.String, StringUtil.GetSafeString(cat.CategorySkuPrefix));

            bool result = DataFactory.ExecuteCommandNonQuery(cmd);
            return result;
        }

        public static bool DeleteOneCategory(int categoryId)
        {
            IDbCommand cmd = DataFactory.CreateCommand(null);
            cmd.CommandText = @"Delete * from [Category] where CategoryId=@CategoryId or ParentCategoryId=@CategoryId;";

            DataFactory.AddCommandParam(cmd, "CategoryId", DbType.Int32, categoryId);

            bool result = DataFactory.ExecuteCommandNonQuery(cmd);
            return result;
        }
    }
}
