namespace EbayMaster
{
    partial class FrmReplyBuyerMessage
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnManageMessageTemplate = new System.Windows.Forms.Button();
            this.tvMessageTemplates = new System.Windows.Forms.TreeView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtbMessageToSend = new System.Windows.Forms.RichTextBox();
            this.buttonSendMessage = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnManageMessageTemplate);
            this.groupBox1.Controls.Add(this.tvMessageTemplates);
            this.groupBox1.Location = new System.Drawing.Point(23, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(622, 240);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "模板";
            // 
            // btnManageMessageTemplate
            // 
            this.btnManageMessageTemplate.Location = new System.Drawing.Point(19, 206);
            this.btnManageMessageTemplate.Name = "btnManageMessageTemplate";
            this.btnManageMessageTemplate.Size = new System.Drawing.Size(88, 23);
            this.btnManageMessageTemplate.TabIndex = 7;
            this.btnManageMessageTemplate.Text = "管理消息模板";
            this.btnManageMessageTemplate.UseVisualStyleBackColor = true;
            // 
            // tvMessageTemplates
            // 
            this.tvMessageTemplates.Location = new System.Drawing.Point(19, 20);
            this.tvMessageTemplates.Name = "tvMessageTemplates";
            this.tvMessageTemplates.Size = new System.Drawing.Size(575, 180);
            this.tvMessageTemplates.TabIndex = 1;
            this.tvMessageTemplates.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvMessageTemplates_AfterSelect);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rtbMessageToSend);
            this.groupBox2.Location = new System.Drawing.Point(23, 258);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(622, 178);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "正文";
            // 
            // rtbMessageToSend
            // 
            this.rtbMessageToSend.Location = new System.Drawing.Point(19, 20);
            this.rtbMessageToSend.Name = "rtbMessageToSend";
            this.rtbMessageToSend.Size = new System.Drawing.Size(575, 143);
            this.rtbMessageToSend.TabIndex = 3;
            this.rtbMessageToSend.Text = "";
            // 
            // buttonSendMessage
            // 
            this.buttonSendMessage.Location = new System.Drawing.Point(295, 445);
            this.buttonSendMessage.Name = "buttonSendMessage";
            this.buttonSendMessage.Size = new System.Drawing.Size(75, 23);
            this.buttonSendMessage.TabIndex = 7;
            this.buttonSendMessage.Text = "发送消息";
            this.buttonSendMessage.UseVisualStyleBackColor = true;
            this.buttonSendMessage.Click += new System.EventHandler(this.buttonSendMessage_Click);
            // 
            // FrmReplyBuyerMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 481);
            this.Controls.Add(this.buttonSendMessage);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmReplyBuyerMessage";
            this.Text = "回复买家消息";
            this.Load += new System.EventHandler(this.FrmReplyBuyerMessage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnManageMessageTemplate;
        private System.Windows.Forms.TreeView tvMessageTemplates;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox rtbMessageToSend;
        private System.Windows.Forms.Button buttonSendMessage;
    }
}