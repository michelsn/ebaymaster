using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EbayMaster
{
    public class MessageTemplateCategoryDAL
    {
        // Retrieve all message template categories.
        public static DataTable GetAllMessageTemplateCategories()
        {
            String sql_getAllMessages = "select * from [MessageTemplateCategory]";
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql_getAllMessages);
            return dt;
        }

        public static MessageTemplateCategoryType GetMessageTemplateCategory(String categoryName)
        {
            String sql_getCategory 
                = String.Format("select * from [MessageTemplateCategory] where CategoryName='{0}'", categoryName);
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql_getCategory);
            if (dt.Rows.Count == 0)
                return null;

            DataRow dr = dt.Rows[0];

            MessageTemplateCategoryType categoryType = new MessageTemplateCategoryType();
            categoryType.CategoryId = StringUtil.GetSafeInt(dr["CategoryId"]);
            categoryType.ParentCategoryId = StringUtil.GetSafeInt(dr["ParentCategoryId"]);
            categoryType.CategoryName = StringUtil.GetSafeString(dr["CategoryName"]);
            return categoryType;
        }

        // Insert one message template category.
        public static bool InsertOneMessageTemplateCategory(MessageTemplateCategoryType cat, out int newCatId)
        {
            newCatId = 0;

            IDbCommand cmd = DataFactory.CreateCommand(null);
            cmd.CommandText = @"Insert into [MessageTemplateCategory] (ParentCategoryId, CategoryName, CategoryDescription) values (@ParentCategoryId, @CategoryName, @CategoryDescription)";

            DataFactory.AddCommandParam(cmd, "@ParentCategoryId", DbType.Int32, cat.ParentCategoryId);
            DataFactory.AddCommandParam(cmd, "@CategoryName", DbType.String, StringUtil.GetSafeString(cat.CategoryName));
            DataFactory.AddCommandParam(cmd, "@CategoryDescription", DbType.String, StringUtil.GetSafeString(cat.CategoryDescription));

            bool result = DataFactory.ExecuteInsertCommand(cmd, out newCatId);
            return result;
        }

        public static bool UpdateOneMessageTemplateCategory(MessageTemplateCategoryType cat)
        {
            IDbCommand cmd = DataFactory.CreateCommand(null);
            cmd.CommandText = @"Update [MessageTemplateCategory] set ParentCategoryId=@ParentCategoryId, CategoryName=@CategoryName, CategoryDescription=@CategoryDescription where CategoryId=@CategoryId";

            DataFactory.AddCommandParam(cmd, "@ParentCategoryId", DbType.Int32, cat.ParentCategoryId);
            DataFactory.AddCommandParam(cmd, "@CategoryName", DbType.String, StringUtil.GetSafeString(cat.CategoryName));
            DataFactory.AddCommandParam(cmd, "@CategoryDescription", DbType.String, StringUtil.GetSafeString(cat.CategoryDescription));
            DataFactory.AddCommandParam(cmd, "@CategoryId", DbType.Int32, StringUtil.GetSafeInt(cat.CategoryId));

            bool result = DataFactory.ExecuteCommandNonQuery(cmd);
            return result;
        }

        // Delete one message template category.
        public static bool DeleteOneMessageTemplateCategory(int categoryId)
        {
            IDbCommand cmd = DataFactory.CreateCommand(null);
            cmd.CommandText = @"Delete * from [MessageTemplateCategory] where CategoryId=@CategoryId or ParentCategoryId=@CategoryId;";

            DataFactory.AddCommandParam(cmd, "CategoryId", DbType.Int32, categoryId);

            bool result = DataFactory.ExecuteCommandNonQuery(cmd);
            return result;
        }
    }
}
