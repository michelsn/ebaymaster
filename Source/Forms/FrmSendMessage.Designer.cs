namespace EbayMaster
{
    partial class FrmSendMessage
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
            this.dataGridViewBuyers = new System.Windows.Forms.DataGridView();
            this.treeViewMessageTemplates = new System.Windows.Forms.TreeView();
            this.richTextBoxMessageToSend = new System.Windows.Forms.RichTextBox();
            this.buttonSendMessage = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonManageMessageTemplate = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBuyers)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewBuyers
            // 
            this.dataGridViewBuyers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBuyers.Location = new System.Drawing.Point(15, 20);
            this.dataGridViewBuyers.Name = "dataGridViewBuyers";
            this.dataGridViewBuyers.RowTemplate.Height = 23;
            this.dataGridViewBuyers.Size = new System.Drawing.Size(700, 111);
            this.dataGridViewBuyers.TabIndex = 0;
            // 
            // treeViewMessageTemplates
            // 
            this.treeViewMessageTemplates.Location = new System.Drawing.Point(15, 20);
            this.treeViewMessageTemplates.Name = "treeViewMessageTemplates";
            this.treeViewMessageTemplates.Size = new System.Drawing.Size(577, 134);
            this.treeViewMessageTemplates.TabIndex = 1;
            this.treeViewMessageTemplates.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewMessageTemplates_AfterSelect);
            // 
            // richTextBoxMessageToSend
            // 
            this.richTextBoxMessageToSend.Location = new System.Drawing.Point(15, 20);
            this.richTextBoxMessageToSend.Name = "richTextBoxMessageToSend";
            this.richTextBoxMessageToSend.Size = new System.Drawing.Size(700, 150);
            this.richTextBoxMessageToSend.TabIndex = 2;
            this.richTextBoxMessageToSend.Text = "";
            // 
            // buttonSendMessage
            // 
            this.buttonSendMessage.Location = new System.Drawing.Point(368, 567);
            this.buttonSendMessage.Name = "buttonSendMessage";
            this.buttonSendMessage.Size = new System.Drawing.Size(75, 23);
            this.buttonSendMessage.TabIndex = 3;
            this.buttonSendMessage.Text = "发送消息";
            this.buttonSendMessage.UseVisualStyleBackColor = true;
            this.buttonSendMessage.Click += new System.EventHandler(this.buttonSendMessage_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonManageMessageTemplate);
            this.groupBox1.Controls.Add(this.treeViewMessageTemplates);
            this.groupBox1.Location = new System.Drawing.Point(30, 167);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(740, 172);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "模板";
            // 
            // buttonManageMessageTemplate
            // 
            this.buttonManageMessageTemplate.Location = new System.Drawing.Point(627, 64);
            this.buttonManageMessageTemplate.Name = "buttonManageMessageTemplate";
            this.buttonManageMessageTemplate.Size = new System.Drawing.Size(88, 23);
            this.buttonManageMessageTemplate.TabIndex = 7;
            this.buttonManageMessageTemplate.Text = "管理消息模板";
            this.buttonManageMessageTemplate.UseVisualStyleBackColor = true;
            this.buttonManageMessageTemplate.Click += new System.EventHandler(this.buttonManageMessageTemplate_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.richTextBoxMessageToSend);
            this.groupBox2.Location = new System.Drawing.Point(30, 364);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(740, 176);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "消息";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridViewBuyers);
            this.groupBox3.Location = new System.Drawing.Point(30, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(740, 137);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "买家";
            // 
            // FrmSendMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 602);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonSendMessage);
            this.Name = "FrmSendMessage";
            this.Text = "给买家写信";
            this.Load += new System.EventHandler(this.FrmSendMessage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBuyers)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewBuyers;
        private System.Windows.Forms.TreeView treeViewMessageTemplates;
        private System.Windows.Forms.RichTextBox richTextBoxMessageToSend;
        private System.Windows.Forms.Button buttonSendMessage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonManageMessageTemplate;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}