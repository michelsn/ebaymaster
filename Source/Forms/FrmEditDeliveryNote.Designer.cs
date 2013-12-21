namespace EbayMaster
{
    partial class FrmEditDeliveryNote
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
            this.dataGridViewTransactions = new System.Windows.Forms.DataGridView();
            this.buttonAddTransaction = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTransactionId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonAddDeliveryNote = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.contextMenuStripTransaction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemDel = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxFee = new System.Windows.Forms.TextBox();
            this.textBoxExtraFee = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxComment = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransactions)).BeginInit();
            this.contextMenuStripTransaction.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewTransactions
            // 
            this.dataGridViewTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTransactions.Location = new System.Drawing.Point(21, 134);
            this.dataGridViewTransactions.Name = "dataGridViewTransactions";
            this.dataGridViewTransactions.ReadOnly = true;
            this.dataGridViewTransactions.RowTemplate.Height = 23;
            this.dataGridViewTransactions.Size = new System.Drawing.Size(848, 315);
            this.dataGridViewTransactions.TabIndex = 0;
            this.dataGridViewTransactions.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridViewTransactions_MouseUp);
            // 
            // buttonAddTransaction
            // 
            this.buttonAddTransaction.Location = new System.Drawing.Point(621, 21);
            this.buttonAddTransaction.Name = "buttonAddTransaction";
            this.buttonAddTransaction.Size = new System.Drawing.Size(75, 25);
            this.buttonAddTransaction.TabIndex = 1;
            this.buttonAddTransaction.Text = "添加";
            this.buttonAddTransaction.UseVisualStyleBackColor = true;
            this.buttonAddTransaction.Click += new System.EventHandler(this.buttonAddTransaction_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "订单号";
            // 
            // textBoxTransactionId
            // 
            this.textBoxTransactionId.Location = new System.Drawing.Point(79, 23);
            this.textBoxTransactionId.Name = "textBoxTransactionId";
            this.textBoxTransactionId.Size = new System.Drawing.Size(508, 20);
            this.textBoxTransactionId.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(101, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(234, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "( 多个订单以逗号或空格间隔，如1000 1001 )";
            // 
            // buttonAddDeliveryNote
            // 
            this.buttonAddDeliveryNote.Location = new System.Drawing.Point(473, 473);
            this.buttonAddDeliveryNote.Name = "buttonAddDeliveryNote";
            this.buttonAddDeliveryNote.Size = new System.Drawing.Size(75, 25);
            this.buttonAddDeliveryNote.TabIndex = 5;
            this.buttonAddDeliveryNote.Text = "完成";
            this.buttonAddDeliveryNote.UseVisualStyleBackColor = true;
            this.buttonAddDeliveryNote.Click += new System.EventHandler(this.buttonAddDeliveryNote_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(348, 473);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 25);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // contextMenuStripTransaction
            // 
            this.contextMenuStripTransaction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemDel});
            this.contextMenuStripTransaction.Name = "contextMenuStripTransaction";
            this.contextMenuStripTransaction.Size = new System.Drawing.Size(99, 26);
            this.contextMenuStripTransaction.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripTransaction_Opening);
            // 
            // ToolStripMenuItemDel
            // 
            this.ToolStripMenuItemDel.Name = "ToolStripMenuItemDel";
            this.ToolStripMenuItemDel.Size = new System.Drawing.Size(98, 22);
            this.ToolStripMenuItemDel.Text = "删除";
            this.ToolStripMenuItemDel.Click += new System.EventHandler(this.ToolStripMenuItemDel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "运费";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(228, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "其他费用";
            // 
            // textBoxFee
            // 
            this.textBoxFee.Location = new System.Drawing.Point(79, 89);
            this.textBoxFee.Name = "textBoxFee";
            this.textBoxFee.Size = new System.Drawing.Size(100, 20);
            this.textBoxFee.TabIndex = 9;
            // 
            // textBoxExtraFee
            // 
            this.textBoxExtraFee.Location = new System.Drawing.Point(313, 89);
            this.textBoxExtraFee.Name = "textBoxExtraFee";
            this.textBoxExtraFee.Size = new System.Drawing.Size(100, 20);
            this.textBoxExtraFee.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(455, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "备注";
            // 
            // textBoxComment
            // 
            this.textBoxComment.Location = new System.Drawing.Point(518, 89);
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(351, 20);
            this.textBoxComment.TabIndex = 12;
            // 
            // FrmEditDeliveryNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 518);
            this.Controls.Add(this.textBoxComment);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxExtraFee);
            this.Controls.Add(this.textBoxFee);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonAddDeliveryNote);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxTransactionId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonAddTransaction);
            this.Controls.Add(this.dataGridViewTransactions);
            this.Name = "FrmEditDeliveryNote";
            this.Text = "创建发货单";
            this.Load += new System.EventHandler(this.FrmDeliveryNote_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransactions)).EndInit();
            this.contextMenuStripTransaction.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewTransactions;
        private System.Windows.Forms.Button buttonAddTransaction;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxTransactionId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonAddDeliveryNote;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTransaction;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxFee;
        private System.Windows.Forms.TextBox textBoxExtraFee;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxComment;
    }
}