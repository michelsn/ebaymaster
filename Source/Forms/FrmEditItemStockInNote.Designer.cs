namespace EbayMaster
{
    partial class FrmEditItemStockInNote
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
            this.textBoxItemSKU = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSourcingNoteId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxStockInNum = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePickerStockInTime = new System.Windows.Forms.DateTimePicker();
            this.buttonFinishEditingItemStockInNote = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxComment = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxItemSKU
            // 
            this.textBoxItemSKU.Location = new System.Drawing.Point(113, 45);
            this.textBoxItemSKU.Name = "textBoxItemSKU";
            this.textBoxItemSKU.Size = new System.Drawing.Size(165, 20);
            this.textBoxItemSKU.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "商品SKU";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "采购单号";
            // 
            // textBoxSourcingNoteId
            // 
            this.textBoxSourcingNoteId.Location = new System.Drawing.Point(113, 98);
            this.textBoxSourcingNoteId.Name = "textBoxSourcingNoteId";
            this.textBoxSourcingNoteId.Size = new System.Drawing.Size(165, 20);
            this.textBoxSourcingNoteId.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "入库数量";
            // 
            // textBoxStockInNum
            // 
            this.textBoxStockInNum.Location = new System.Drawing.Point(113, 158);
            this.textBoxStockInNum.Name = "textBoxStockInNum";
            this.textBoxStockInNum.Size = new System.Drawing.Size(165, 20);
            this.textBoxStockInNum.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 211);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "入库时间";
            // 
            // dateTimePickerStockInTime
            // 
            this.dateTimePickerStockInTime.CustomFormat = "yyyy-MM-dd";
            this.dateTimePickerStockInTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStockInTime.Location = new System.Drawing.Point(113, 211);
            this.dateTimePickerStockInTime.Name = "dateTimePickerStockInTime";
            this.dateTimePickerStockInTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dateTimePickerStockInTime.Size = new System.Drawing.Size(165, 20);
            this.dateTimePickerStockInTime.TabIndex = 8;
            // 
            // buttonFinishEditingItemStockInNote
            // 
            this.buttonFinishEditingItemStockInNote.Location = new System.Drawing.Point(203, 329);
            this.buttonFinishEditingItemStockInNote.Name = "buttonFinishEditingItemStockInNote";
            this.buttonFinishEditingItemStockInNote.Size = new System.Drawing.Size(75, 23);
            this.buttonFinishEditingItemStockInNote.TabIndex = 9;
            this.buttonFinishEditingItemStockInNote.Text = "确定";
            this.buttonFinishEditingItemStockInNote.UseVisualStyleBackColor = true;
            this.buttonFinishEditingItemStockInNote.Click += new System.EventHandler(this.buttonFinishEditingItemStockInNote_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 263);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "备注";
            // 
            // textBoxComment
            // 
            this.textBoxComment.Location = new System.Drawing.Point(113, 256);
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(165, 20);
            this.textBoxComment.TabIndex = 11;
            // 
            // FrmEditItemStockInNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 374);
            this.Controls.Add(this.textBoxComment);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonFinishEditingItemStockInNote);
            this.Controls.Add(this.dateTimePickerStockInTime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxStockInNum);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxSourcingNoteId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxItemSKU);
            this.Name = "FrmEditItemStockInNote";
            this.Text = "创建入库单";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxItemSKU;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSourcingNoteId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxStockInNum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePickerStockInTime;
        private System.Windows.Forms.Button buttonFinishEditingItemStockInNote;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxComment;
    }
}