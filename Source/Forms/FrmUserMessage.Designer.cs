namespace EbayMaster
{
    partial class FrmUserMessage
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
            this.tabControlUserMessage = new System.Windows.Forms.TabControl();
            this.tabPageAllMessage = new System.Windows.Forms.TabPage();
            this.dataGridViewTransactionMessageSubject = new System.Windows.Forms.DataGridView();
            this.buttonAskBuyer = new System.Windows.Forms.Button();
            this.buttonReplyMessage = new System.Windows.Forms.Button();
            this.contextMenuStripMsg = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemMarkAsReplied = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlUserMessage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransactionMessageSubject)).BeginInit();
            this.contextMenuStripMsg.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlUserMessage
            // 
            this.tabControlUserMessage.Controls.Add(this.tabPageAllMessage);
            this.tabControlUserMessage.Location = new System.Drawing.Point(12, 173);
            this.tabControlUserMessage.Name = "tabControlUserMessage";
            this.tabControlUserMessage.SelectedIndex = 0;
            this.tabControlUserMessage.Size = new System.Drawing.Size(746, 390);
            this.tabControlUserMessage.TabIndex = 2;
            // 
            // tabPageAllMessage
            // 
            this.tabPageAllMessage.Location = new System.Drawing.Point(4, 22);
            this.tabPageAllMessage.Name = "tabPageAllMessage";
            this.tabPageAllMessage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAllMessage.Size = new System.Drawing.Size(738, 364);
            this.tabPageAllMessage.TabIndex = 0;
            this.tabPageAllMessage.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTransactionMessageSubject
            // 
            this.dataGridViewTransactionMessageSubject.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTransactionMessageSubject.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewTransactionMessageSubject.Name = "dataGridViewTransactionMessageSubject";
            this.dataGridViewTransactionMessageSubject.RowTemplate.Height = 23;
            this.dataGridViewTransactionMessageSubject.Size = new System.Drawing.Size(746, 119);
            this.dataGridViewTransactionMessageSubject.TabIndex = 3;
            this.dataGridViewTransactionMessageSubject.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTransactionMessageSubject_CellContentClick);
            this.dataGridViewTransactionMessageSubject.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTransactionMessageSubject_CellContentClick);
            this.dataGridViewTransactionMessageSubject.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridViewTransactionMessageSubject_DataBindingComplete);
            this.dataGridViewTransactionMessageSubject.SelectionChanged += new System.EventHandler(this.dataGridViewTransactionMessageSubject_SelectionChanged);
            this.dataGridViewTransactionMessageSubject.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridViewTransactionMessageSubject_MouseUp);
            // 
            // buttonAskBuyer
            // 
            this.buttonAskBuyer.Location = new System.Drawing.Point(671, 148);
            this.buttonAskBuyer.Name = "buttonAskBuyer";
            this.buttonAskBuyer.Size = new System.Drawing.Size(87, 35);
            this.buttonAskBuyer.TabIndex = 0;
            this.buttonAskBuyer.Text = "给买家写信";
            this.buttonAskBuyer.UseVisualStyleBackColor = true;
            this.buttonAskBuyer.Click += new System.EventHandler(this.buttonAskBuyer_Click);
            // 
            // buttonReplyMessage
            // 
            this.buttonReplyMessage.Location = new System.Drawing.Point(556, 148);
            this.buttonReplyMessage.Name = "buttonReplyMessage";
            this.buttonReplyMessage.Size = new System.Drawing.Size(87, 35);
            this.buttonReplyMessage.TabIndex = 4;
            this.buttonReplyMessage.Text = "回复消息";
            this.buttonReplyMessage.UseVisualStyleBackColor = true;
            this.buttonReplyMessage.Click += new System.EventHandler(this.buttonReplyMessage_Click);
            // 
            // contextMenuStripMsg
            // 
            this.contextMenuStripMsg.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemMarkAsReplied});
            this.contextMenuStripMsg.Name = "contextMenuStripMsg";
            this.contextMenuStripMsg.Size = new System.Drawing.Size(153, 48);
            this.contextMenuStripMsg.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripMsg_Opening);
            // 
            // ToolStripMenuItemMarkAsReplied
            // 
            this.ToolStripMenuItemMarkAsReplied.Name = "ToolStripMenuItemMarkAsReplied";
            this.ToolStripMenuItemMarkAsReplied.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemMarkAsReplied.Text = "标记已回复";
            this.ToolStripMenuItemMarkAsReplied.Click += new System.EventHandler(this.ToolStripMenuItemMarkAsReplied_Click);
            // 
            // FrmUserMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 570);
            this.Controls.Add(this.buttonReplyMessage);
            this.Controls.Add(this.buttonAskBuyer);
            this.Controls.Add(this.dataGridViewTransactionMessageSubject);
            this.Controls.Add(this.tabControlUserMessage);
            this.Name = "FrmUserMessage";
            this.Text = "站内消息";
            this.Load += new System.EventHandler(this.FrmUserMessage_Load);
            this.tabControlUserMessage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransactionMessageSubject)).EndInit();
            this.contextMenuStripMsg.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlUserMessage;
        private System.Windows.Forms.TabPage tabPageAllMessage;
        private System.Windows.Forms.DataGridView dataGridViewTransactionMessageSubject;
        private System.Windows.Forms.Button buttonAskBuyer;
        private System.Windows.Forms.Button buttonReplyMessage;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMsg;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemMarkAsReplied;
    }
}