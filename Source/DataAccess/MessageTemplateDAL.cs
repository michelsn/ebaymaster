using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EbayMaster
{
    public class MessageTemplateDAL
    {
        // Retrieve all message template.
        public static DataTable GetAllMessageTemplates()
        {
            String sql_getAllMessages = "select * from [MessageTemplate]";
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql_getAllMessages);
            return dt;
        }
        
        // Get all message templates with a specified category.
        public static DataTable GetAllMessageTemplatesWithCategoryId(int categoryId)
        {
            String sql_getAllMessages = String.Format("Select * from [MessageTemplate] where TemplateCategoryId={0}",
                categoryId);
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql_getAllMessages);
            return dt;
        }

        public static MessageTemplateType GetMessageTemplate(int parentCategoryId, String templateName)
        {
            String sql_getTemplate
                = String.Format("select * from [MessageTemplate] where TemplateCategoryId={0} and TemplateName='{1}'", 
                parentCategoryId,
                templateName);
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql_getTemplate);
            if (dt.Rows.Count == 0)
                return null;

            DataRow dr = dt.Rows[0];

            MessageTemplateType templateType = new MessageTemplateType();
            templateType.TemplateCategoryId = StringUtil.GetSafeInt(dr["TemplateCategoryId"]);
            templateType.TemplateName = StringUtil.GetSafeString(dr["TemplateName"]);
            templateType.TemplateContent = StringUtil.GetSafeString(dr["TemplateContent"]);
            return templateType;
        }

        // Insert one message template.
        public static bool InsertOneMessageTemplate(MessageTemplateType messageTemplate, out int newTemplateId)
        {
            newTemplateId = 0;

            IDbCommand cmd = DataFactory.CreateCommand(null);
            cmd.CommandText = @"Insert into [MessageTemplate] (TemplateCategoryId, TemplateName, TemplateContent) values (@TemplateCategoryId, @TemplateName, @TemplateContent)";

            DataFactory.AddCommandParam(cmd, "@TemplateCategoryId", DbType.Int32, messageTemplate.TemplateCategoryId);
            DataFactory.AddCommandParam(cmd, "@TemplateName", DbType.String, StringUtil.GetSafeString(messageTemplate.TemplateName));
            DataFactory.AddCommandParam(cmd, "@TemplateContent", DbType.String, StringUtil.GetSafeString(messageTemplate.TemplateContent));

            bool result = DataFactory.ExecuteInsertCommand(cmd, out newTemplateId);
            return result;
        }

        public static bool ModifyOneMessageTemplate(MessageTemplateType messageTemplate)
        {
            IDbCommand cmd = DataFactory.CreateCommand(null);
            cmd.CommandText = @"Update [MessageTemplate] set TemplateCategoryId=@TemplateCategoryId, TemplateName=@TemplateName, TemplateContent=@TemplateContent where TemplateId=@TemplateId";

            DataFactory.AddCommandParam(cmd, "@TemplateCategoryId", DbType.Int32, messageTemplate.TemplateCategoryId);
            DataFactory.AddCommandParam(cmd, "@TemplateName", DbType.String, StringUtil.GetSafeString(messageTemplate.TemplateName));
            DataFactory.AddCommandParam(cmd, "@TemplateContent", DbType.String, StringUtil.GetSafeString(messageTemplate.TemplateContent));
            DataFactory.AddCommandParam(cmd, "@TemplateId", DbType.Int32, StringUtil.GetSafeInt(messageTemplate.TemplateId));

            bool result = DataFactory.ExecuteCommandNonQuery(cmd);
            return result;
        }

        // Delete one message template.
        public static bool DeleteOneMessageTemplate(int templateId)
        {
            IDbCommand cmd = DataFactory.CreateCommand(null);
            cmd.CommandText = @"Delete * from [MessageTemplate] where TemplateId=@TemplateId;";

            DataFactory.AddCommandParam(cmd, "CategoryId", DbType.Int32, templateId);

            bool result = DataFactory.ExecuteCommandNonQuery(cmd);
            return result;
        }

        // Delete all message templates with a specified category id.
        public static bool DeleteMessageTemplatesWithCategoryId(int categoryId)
        {
            IDbCommand cmd = DataFactory.CreateCommand(null);
            cmd.CommandText = @"Delete * from [MessageTemplate] where TemplateCategoryId=@;";

            DataFactory.AddCommandParam(cmd, "TemplateCategoryId", DbType.Int32, categoryId);

            bool result = DataFactory.ExecuteCommandNonQuery(cmd);
            return result;
        }
    }
}
