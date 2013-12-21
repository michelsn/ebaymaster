namespace EbayMaster
{
    partial class FrmMessageTemplate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.treeViewMessageTemplate = new System.Windows.Forms.TreeView();
            this.buttonAddMessageTemplateCategory = new System.Windows.Forms.Button();
            this.buttonAddMessageTemplate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxMessageTemplateName = new System.Windows.Forms.TextBox();
            this.richTextBoxMessageTemplateContent = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.contextMenuStripTemplateCategory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemModifyCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDelCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripMessageTemplate = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemDelMessageTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonModifyMessageTemplate = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.contextMenuStripTemplateCategory.SuspendLayout();
            this.contextMenuStripMessageTemplate.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewMessageTemplate
            // 
            this.treeViewMessageTemplate.Location = new System.Drawing.Point(22, 61);
            this.treeViewMessageTemplate.Name = "treeViewMessageTemplate";
            this.treeViewMessageTemplate.Size = new System.Drawing.Size(221, 365);
            this.treeViewMessageTemplate.TabIndex = 0;
            this.treeViewMessageTemplate.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeViewMessageTemplate_AfterLabelEdit);
            this.treeViewMessageTemplate.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewMessageTemplate_AfterSelect);
            this.treeViewMessageTemplate.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewMessageTemplate_NodeMouseClick);
            // 
            // buttonAddMessageTemplateCategory
            // 
            this.buttonAddMessageTemplateCategory.Location = new System.Drawing.Point(97, 22);
            this.buttonAddMessageTemplateCategory.Name = "buttonAddMessageTemplateCategory";
            this.buttonAddMessageTemplateCategory.Size = new System.Drawing.Size(86, 23);
            this.buttonAddMessageTemplateCategory.TabIndex = 2;
            this.buttonAddMessageTemplateCategory.Text = "添加模板主题";
            this.buttonAddMessageTemplateCategory.UseVisualStyleBackColor = true;
            this.buttonAddMessageTemplateCategory.Click += new System.EventHandler(this.buttonAddMessageTemplateCategory_Click);
            // 
            // buttonAddMessageTemplate
            // 
            this.buttonAddMessageTemplate.Location = new System.Drawing.Point(455, 403);
            this.buttonAddMessageTemplate.Name = "buttonAddMessageTemplate";
            this.buttonAddMessageTemplate.Size = new System.Drawing.Size(86, 23);
            this.buttonAddMessageTemplate.TabIndex = 5;
            this.buttonAddMessageTemplate.Text = "添加消息模板";
            this.buttonAddMessageTemplate.UseVisualStyleBackColor = true;
            this.buttonAddMessageTemplate.Click += new System.EventHandler(this.buttonAddMessageTemplate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "模板名";
            // 
            // textBoxMessageTemplateName
            // 
            this.textBoxMessageTemplateName.Location = new System.Drawing.Point(117, 43);
            this.textBoxMessageTemplateName.Name = "textBoxMessageTemplateName";
            this.textBoxMessageTemplateName.Size = new System.Drawing.Size(234, 21);
            this.textBoxMessageTemplateName.TabIndex = 4;
            // 
            // richTextBoxMessageTemplateContent
            // 
            this.richTextBoxMessageTemplateContent.Location = new System.Drawing.Point(117, 101);
            this.richTextBoxMessageTemplateContent.Name = "richTextBoxMessageTemplateContent";
            this.richTextBoxMessageTemplateContent.Size = new System.Drawing.Size(450, 221);
            this.richTextBoxMessageTemplateContent.TabIndex = 5;
            this.richTextBoxMessageTemplateContent.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "模板内容";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(51, 183);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(53, 12);
            this.linkLabel1.TabIndex = 9;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "插入参数";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.linkLabel1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.richTextBoxMessageTemplateContent);
            this.groupBox1.Controls.Add(this.textBoxMessageTemplateName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(276, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(590, 370);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // contextMenuStripTemplateCategory
            // 
            this.contextMenuStripTemplateCategory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemModifyCategory,
            this.ToolStripMenuItemDelCategory});
            this.contextMenuStripTemplateCategory.Name = "contextMenuStripItem";
            this.contextMenuStripTemplateCategory.Size = new System.Drawing.Size(153, 70);
            // 
            // ToolStripMenuItemModifyCategory
            // 
            this.ToolStripMenuItemModifyCategory.Name = "ToolStripMenuItemModifyCategory";
            this.ToolStripMenuItemModifyCategory.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemModifyCategory.Text = "修改模板主题";
            this.ToolStripMenuItemModifyCategory.Click += new System.EventHandler(this.ToolStripMenuItemModifyCategory_Click);
            // 
            // ToolStripMenuItemDelCategory
            // 
            this.ToolStripMenuItemDelCategory.Name = "ToolStripMenuItemDelCategory";
            this.ToolStripMenuItemDelCategory.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemDelCategory.Text = "删除模板主题";
            this.ToolStripMenuItemDelCategory.Click += new System.EventHandler(this.ToolStripMenuItemDelCategory_Click);
            // 
            // contextMenuStripMessageTemplate
            // 
            this.contextMenuStripMessageTemplate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemDelMessageTemplate});
            this.contextMenuStripMessageTemplate.Name = "contextMenuStrip1";
            this.contextMenuStripMessageTemplate.Size = new System.Drawing.Size(153, 48);
            // 
            // ToolStripMenuItemDelMessageTemplate
            // 
            this.ToolStripMenuItemDelMessageTemplate.Name = "ToolStripMenuItemDelMessageTemplate";
            this.ToolStripMenuItemDelMessageTemplate.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemDelMessageTemplate.Text = "删除消息模板";
            this.ToolStripMenuItemDelMessageTemplate.Click += new System.EventHandler(this.ToolStripMenuItemDelMessageTemplate_Click);
            // 
            // buttonModifyMessageTemplate
            // 
            this.buttonModifyMessageTemplate.Location = new System.Drawing.Point(592, 403);
            this.buttonModifyMessageTemplate.Name = "buttonModifyMessageTemplate";
            this.buttonModifyMessageTemplate.Size = new System.Drawing.Size(86, 23);
            this.buttonModifyMessageTemplate.TabIndex = 6;
            this.buttonModifyMessageTemplate.Text = "修改消息模板";
            this.buttonModifyMessageTemplate.UseVisualStyleBackColor = true;
            this.buttonModifyMessageTemplate.Click += new System.EventHandler(this.buttonModifyMessageTemplate_Click);
            // 
            // FrmMessageTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 447);
            this.Controls.Add(this.buttonModifyMessageTemplate);
            this.Controls.Add(this.buttonAddMessageTemplate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.treeViewMessageTemplate);
            this.Controls.Add(this.buttonAddMessageTemplateCategory);
            this.Name = "FrmMessageTemplate";
            this.Text = "消息模板管理";
            this.Load += new System.EventHandler(this.FrmMessageTemplate_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStripTemplateCategory.ResumeLayout(false);
            this.contextMenuStripMessageTemplate.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewMessageTemplate;
        private System.Windows.Forms.Button buttonAddMessageTemplateCategory;
        private System.Windows.Forms.Button buttonAddMessageTemplate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxMessageTemplateName;
        private System.Windows.Forms.RichTextBox richTextBoxMessageTemplateContent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTemplateCategory;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDelCategory;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMessageTemplate;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemModifyCategory;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDelMessageTemplate;
        private System.Windows.Forms.Button buttonModifyMessageTemplate;
    }
}