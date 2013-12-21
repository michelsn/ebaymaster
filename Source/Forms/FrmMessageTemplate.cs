    // -------------------------------------------------------------------------------
// FrmMessageTemplate.cs - Form to manage all message template categories and message templates.
//  User can utilize this form to achieve:
//      1) Add/modify/remove message template categories.
//      2) Add/modify/remove message templates.
//      
// -------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EbayMaster
{
    public partial class FrmMessageTemplate : Form
    {
        public FrmMessageTemplate()
        {
            InitializeComponent();
        }

        // Form load event handler.
        private void FrmMessageTemplate_Load(object sender, EventArgs e)
        {
            LoadAllMessageTemplateCategories();

            LoadAllMessageTemplates();
        }

        // Load all message template categories.
        private void LoadAllMessageTemplateCategories()
        {
            DataTable dtCategories = MessageTemplateCategoryDAL.GetAllMessageTemplateCategories();

            this.treeViewMessageTemplate.Nodes.Clear();

            foreach (DataRow dr in dtCategories.Rows)
            {
                int categoryId = StringUtil.GetSafeInt(dr["CategoryId"]);
                String categoryName = StringUtil.GetSafeString(dr["CategoryName"]);

                MessageTemplateCategoryType categoryType = new MessageTemplateCategoryType();
                categoryType.CategoryId = categoryId;
                categoryType.CategoryName = categoryName;

                TreeNode newNode = new TreeNode();
                newNode.Tag = categoryType;
                newNode.Text = categoryName;
                newNode.Name = categoryId.ToString();
                this.treeViewMessageTemplate.Nodes.Add(newNode);
            }
        }

        // Load all message templates.
        private void LoadAllMessageTemplates()
        {
            DataTable dtTemplates = MessageTemplateDAL.GetAllMessageTemplates();

            foreach (DataRow dr in dtTemplates.Rows)
            {
                int templateId = StringUtil.GetSafeInt(dr["TemplateId"]);
                int templateCategoryId = StringUtil.GetSafeInt(dr["TemplateCategoryId"]);
                String templateName = StringUtil.GetSafeString(dr["TemplateName"]);
                String templateContent = StringUtil.GetSafeString(dr["TemplateContent"]);

                MessageTemplateType templateType = new MessageTemplateType();
                templateType.TemplateId = templateId;
                templateType.TemplateName = templateName;
                templateType.TemplateCategoryId = templateCategoryId;
                templateType.TemplateContent = templateContent;

                TreeNode newNode = new TreeNode();
                newNode.Tag = templateType;
                newNode.Text = templateName;
                newNode.Name = templateId.ToString();

                // Find the category node.
                TreeNode[] categoryNodes = treeViewMessageTemplate.Nodes.Find(templateCategoryId.ToString(), false);

                if (categoryNodes.Length == 1)
                {
                    categoryNodes[0].Nodes.Add(newNode);
                    categoryNodes[0].ExpandAll();
                }
            }
        }

        // Add message template category button handler.
        private void buttonAddMessageTemplateCategory_Click(object sender, EventArgs e)
        {
            this.treeViewMessageTemplate.LabelEdit = true;
            TreeNode newNode = new TreeNode();
            newNode.Text = "新消息模板";
            this.treeViewMessageTemplate.Nodes.Add(newNode);
            this.treeViewMessageTemplate.SelectedNode = newNode;
            newNode.BeginEdit();
        }

        // When user done with editing the message template category.
        //   Two possible cases:
        //      1) Add a new category, or
        //      2) Modify an existed category.
        private void treeViewMessageTemplate_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null || e.Label.Trim().Length == 0)
            {
                e.CancelEdit = true;
                e.Node.BeginEdit();
                return;
            }

            String categoryName = e.Label;

            MessageTemplateCategoryType categoryType = (MessageTemplateCategoryType)e.Node.Tag;
            if (categoryType != null)
            {
                // Modify an existed category.
                if (categoryName == categoryType.CategoryName)
                    return;

                categoryType.CategoryName = categoryName;
                MessageTemplateCategoryDAL.UpdateOneMessageTemplateCategory(categoryType);
                this.treeViewMessageTemplate.LabelEdit = false;
                this.treeViewMessageTemplate.SelectedNode = e.Node;
                return;
            }

            // Add a new category.
            categoryType = new MessageTemplateCategoryType();
            categoryType.ParentCategoryId = -1;
            categoryType.CategoryName = categoryName;
            categoryType.CategoryDescription = "";

            // Check if there is any category with the same category name.
            MessageTemplateCategoryType existedCategoryType = MessageTemplateCategoryDAL.GetMessageTemplateCategory(categoryName);
            if (existedCategoryType != null)
            {
                MessageBox.Show("已存在此消息模板主题，请输入不同模板主题！", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.CancelEdit = true;
                e.Node.BeginEdit();
                return;
            }

            // Insert the new category to database and return the new category id.
            int newCatId = 0;
            MessageTemplateCategoryDAL.InsertOneMessageTemplateCategory(categoryType, out newCatId);
            categoryType.CategoryId = newCatId;

            e.Node.EndEdit(false);
            e.Node.Tag = categoryType;
           
            this.treeViewMessageTemplate.SelectedNode = e.Node;
            this.treeViewMessageTemplate.LabelEdit = false;
        }

        // Invoke the context dialog to modify/remove message template category.
        private void treeViewMessageTemplate_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            TreeNode node = treeViewMessageTemplate.GetNodeAt(e.Location);
            if (node == null)
                return;

            if (node.Tag == null)
                return;

            if (node.Tag.GetType() == typeof(MessageTemplateCategoryType))
            {
                contextMenuStripTemplateCategory.Show(treeViewMessageTemplate, e.X, e.Y);
            }
            else if (node.Tag.GetType() == typeof(MessageTemplateType))
            {
                contextMenuStripMessageTemplate.Show(treeViewMessageTemplate, e.X, e.Y);
            }
        }

        // Delete a message template category, if there are any message templates under
        // this category, prompt user.
        private void ToolStripMenuItemDelCategory_Click(object sender, EventArgs e)
        {
            TreeNode node = treeViewMessageTemplate.SelectedNode;
            if (node == null)
                return;

            if (node.Tag == null)
                return;

            if (node.Tag.GetType() != typeof(MessageTemplateCategoryType))
                return;

            MessageTemplateCategoryType categoryType = (MessageTemplateCategoryType)node.Tag;
            int categoryId = categoryType.CategoryId;

            DataTable dtMessageTemplates = MessageTemplateDAL.GetAllMessageTemplatesWithCategoryId(categoryId);

            if (dtMessageTemplates.Rows.Count > 0)
            {
                if (MessageBox.Show(string.Format("确认删除主题 {0}？ 我们将删除所有隶属该主题的消息模板",
                    categoryType.CategoryName), "请确认",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }

            // Either cases:
            //  1) The category has message templates and user confirmed to delete the category.
            //  2) The category has no message templates.

            // Delete all the relevant message templates first.
            foreach (DataRow dr in dtMessageTemplates.Rows)
            {
                int messageTemplateId = StringUtil.GetSafeInt(dr["TemplateId"]);
                MessageTemplateDAL.DeleteOneMessageTemplate(messageTemplateId);
            }

            MessageTemplateCategoryDAL.DeleteOneMessageTemplateCategory(categoryType.CategoryId);
            treeViewMessageTemplate.Nodes.Remove(node);
        }

        // Add message template button handler.
        private void buttonAddMessageTemplate_Click(object sender, EventArgs e)
        {
            int categoryId = -1;
            {
                if (treeViewMessageTemplate.SelectedNode == null)
                    return;

                MessageTemplateCategoryType categoryType = (MessageTemplateCategoryType)treeViewMessageTemplate.SelectedNode.Tag;
                if (categoryType == null)
                    return;

                categoryId = categoryType.CategoryId;
            }

            if (categoryId < 0)
                return;

            String templateName = this.textBoxMessageTemplateName.Text.Trim();
            if (templateName == null || templateName == "")
            {
                MessageBox.Show("消息模板名称不能为空", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.buttonAddMessageTemplate.Enabled = false;

            // Whether we have already added one message template with the same name.
            MessageTemplateType existedMessagetTemplateType = MessageTemplateDAL.GetMessageTemplate(categoryId, templateName);
            if (existedMessagetTemplateType != null)
            {
                MessageBox.Show("已存在此消息模板名，请输入不同消息模板名称！", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.buttonAddMessageTemplate.Enabled = true;
                return;
            }

            String templateContent = this.richTextBoxMessageTemplateContent.Text.Trim();
            if (templateContent == null || templateContent.Trim() == "")
            {
                MessageBox.Show("消息模板正文不能为空", "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.buttonAddMessageTemplate.Enabled = true;
                return;
            }

            MessageTemplateType templateType = new MessageTemplateType();
            templateType.TemplateName = templateName;
            templateType.TemplateContent = templateContent;
            templateType.TemplateCategoryId = categoryId;

            int newTemplateId = 0;
            MessageTemplateDAL.InsertOneMessageTemplate(templateType, out newTemplateId);


            TreeNode newNode = new TreeNode();
            newNode.Text = templateName;
            newNode.Tag = templateType;
            treeViewMessageTemplate.SelectedNode.Nodes.Add(newNode);
            treeViewMessageTemplate.SelectedNode.ExpandAll();

            MessageBox.Show("成功添加消息模板！", "成功",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            this.buttonAddMessageTemplate.Enabled = true;
        }

        // Invoked when user right click one message template and chose to delete that message template.
        private void ToolStripMenuItemDelMessageTemplate_Click(object sender, EventArgs e)
        {
            TreeNode node = treeViewMessageTemplate.SelectedNode;
            if (node == null)
                return;

            if (node.Tag == null)
                return;

            if (node.Tag.GetType() != typeof(MessageTemplateType))
                return;

            MessageTemplateType messageTemplate = (MessageTemplateType)node.Tag;
            if (messageTemplate == null)
                return;

            MessageTemplateDAL.DeleteOneMessageTemplate(messageTemplate.TemplateId);

            // Find the category node.
            TreeNode[] categoryNodes = treeViewMessageTemplate.Nodes.Find(messageTemplate.TemplateCategoryId.ToString(), false);
            if (categoryNodes.Length > 0)
                categoryNodes[0].Nodes.Remove(node);
        }

        // When user selects one of the node in the treeview.
        private void treeViewMessageTemplate_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = treeViewMessageTemplate.SelectedNode;
            if (node == null)
                return;

            if (node.Tag == null)
                return;

            if (typeof(MessageTemplateType) == node.Tag.GetType())
            {
                MessageTemplateType messageTemplate = (MessageTemplateType)node.Tag;
                if (messageTemplate == null)
                    return;
                this.textBoxMessageTemplateName.Text = StringUtil.GetSafeString(messageTemplate.TemplateName);
                this.richTextBoxMessageTemplateContent.Text = StringUtil.GetSafeString(messageTemplate.TemplateContent);

                this.buttonAddMessageTemplate.Enabled = false;
                this.buttonModifyMessageTemplate.Enabled = true;
            }
            else if (typeof(MessageTemplateCategoryType) == node.Tag.GetType())
            {
                MessageTemplateCategoryType messageCategoryType = (MessageTemplateCategoryType)node.Tag;
                if (messageCategoryType == null)
                    return;
                this.textBoxMessageTemplateName.Text = "";
                this.richTextBoxMessageTemplateContent.Text = "";

                this.buttonAddMessageTemplate.Enabled = true;
                this.buttonModifyMessageTemplate.Enabled = false;
            }
        }   // treeViewMessageTemplate_AfterSelect

        private void buttonModifyMessageTemplate_Click(object sender, EventArgs e)
        {
            TreeNode node = treeViewMessageTemplate.SelectedNode;
            if (node == null)
                return;

            if (node.Tag == null)
                return;

            if (node.Tag.GetType() != typeof(MessageTemplateType))
                return;

            MessageTemplateType messageTemplate = (MessageTemplateType)node.Tag;
            if (messageTemplate == null)
                return;

            this.buttonModifyMessageTemplate.Enabled = false;

            int templateId = messageTemplate.TemplateId;
            int templateCategoryId = messageTemplate.TemplateCategoryId;
            String templateName = StringUtil.GetSafeString(this.textBoxMessageTemplateName.Text);
            String templateContent = StringUtil.GetSafeString(this.richTextBoxMessageTemplateContent.Text);

            messageTemplate.TemplateName = templateName;
            messageTemplate.TemplateContent = templateContent;

            MessageTemplateDAL.ModifyOneMessageTemplate(messageTemplate);

            MessageBox.Show("成功修改消息模板！", "成功",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            this.buttonModifyMessageTemplate.Enabled = true;

        }

        private void ToolStripMenuItemModifyCategory_Click(object sender, EventArgs e)
        {
            TreeNode node = treeViewMessageTemplate.SelectedNode;
            if (node == null)
                return;

            if (node.Tag == null)
                return;

            MessageTemplateCategoryType categoryType = (MessageTemplateCategoryType)node.Tag;
            if (categoryType == null)
                return;

            this.treeViewMessageTemplate.LabelEdit = true;
            node.BeginEdit();
        }   // buttonModifyMessageTemplate_Click


    }   // class FrmMessageTemplate
}
