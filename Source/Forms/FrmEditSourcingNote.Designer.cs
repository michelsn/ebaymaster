namespace EbayMaster
{
    partial class FrmEditSourcingNote
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
            this.label9 = new System.Windows.Forms.Label();
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.buttonSelectSupplier = new System.Windows.Forms.Button();
            this.textBoxSupplier = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labelErrMsg = new System.Windows.Forms.Label();
            this.textBoxComment = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxTotalFee = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxShippingFee = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxExtraFee = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonCalTotalFee = new System.Windows.Forms.Button();
            this.buttonFinishEditing = new System.Windows.Forms.Button();
            this.dgvItems = new EbayMaster.CustomGrid();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(403, 36);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 23;
            this.label9.Text = "时间";
            // 
            // dateTimePickerDate
            // 
            this.dateTimePickerDate.Location = new System.Drawing.Point(462, 30);
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.Size = new System.Drawing.Size(155, 21);
            this.dateTimePickerDate.TabIndex = 2;
            // 
            // buttonSelectSupplier
            // 
            this.buttonSelectSupplier.Location = new System.Drawing.Point(281, 31);
            this.buttonSelectSupplier.Name = "buttonSelectSupplier";
            this.buttonSelectSupplier.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectSupplier.TabIndex = 1;
            this.buttonSelectSupplier.Text = "选择供应商";
            this.buttonSelectSupplier.UseVisualStyleBackColor = true;
            this.buttonSelectSupplier.Click += new System.EventHandler(this.buttonSelectSupplier_Click);
            // 
            // textBoxSupplier
            // 
            this.textBoxSupplier.Location = new System.Drawing.Point(108, 33);
            this.textBoxSupplier.Name = "textBoxSupplier";
            this.textBoxSupplier.ReadOnly = true;
            this.textBoxSupplier.Size = new System.Drawing.Size(156, 21);
            this.textBoxSupplier.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 21;
            this.label4.Text = "供应商";
            // 
            // labelErrMsg
            // 
            this.labelErrMsg.AutoSize = true;
            this.labelErrMsg.ForeColor = System.Drawing.Color.Red;
            this.labelErrMsg.Location = new System.Drawing.Point(49, 261);
            this.labelErrMsg.Name = "labelErrMsg";
            this.labelErrMsg.Size = new System.Drawing.Size(41, 12);
            this.labelErrMsg.TabIndex = 25;
            this.labelErrMsg.Text = "errmsg";
            this.labelErrMsg.Visible = false;
            // 
            // textBoxComment
            // 
            this.textBoxComment.Location = new System.Drawing.Point(96, 323);
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(521, 21);
            this.textBoxComment.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(37, 326);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 33;
            this.label8.Text = "备注";
            // 
            // textBoxTotalFee
            // 
            this.textBoxTotalFee.Location = new System.Drawing.Point(96, 369);
            this.textBoxTotalFee.Name = "textBoxTotalFee";
            this.textBoxTotalFee.ReadOnly = true;
            this.textBoxTotalFee.Size = new System.Drawing.Size(155, 21);
            this.textBoxTotalFee.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(37, 372);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 32;
            this.label7.Text = "费用总计";
            // 
            // textBoxShippingFee
            // 
            this.textBoxShippingFee.Location = new System.Drawing.Point(462, 283);
            this.textBoxShippingFee.Name = "textBoxShippingFee";
            this.textBoxShippingFee.Size = new System.Drawing.Size(155, 21);
            this.textBoxShippingFee.TabIndex = 5;
            this.textBoxShippingFee.Text = "0.0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(403, 286);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 31;
            this.label6.Text = "运费";
            // 
            // textBoxExtraFee
            // 
            this.textBoxExtraFee.Location = new System.Drawing.Point(97, 283);
            this.textBoxExtraFee.Name = "textBoxExtraFee";
            this.textBoxExtraFee.Size = new System.Drawing.Size(155, 21);
            this.textBoxExtraFee.TabIndex = 4;
            this.textBoxExtraFee.Text = "0.0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 286);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 29;
            this.label5.Text = "其他费用";
            // 
            // buttonCalTotalFee
            // 
            this.buttonCalTotalFee.Location = new System.Drawing.Point(253, 401);
            this.buttonCalTotalFee.Name = "buttonCalTotalFee";
            this.buttonCalTotalFee.Size = new System.Drawing.Size(75, 23);
            this.buttonCalTotalFee.TabIndex = 8;
            this.buttonCalTotalFee.Text = "计算";
            this.buttonCalTotalFee.UseVisualStyleBackColor = true;
            this.buttonCalTotalFee.Click += new System.EventHandler(this.buttonCalTotalFee_Click);
            // 
            // buttonFinishEditing
            // 
            this.buttonFinishEditing.Location = new System.Drawing.Point(366, 401);
            this.buttonFinishEditing.Name = "buttonFinishEditing";
            this.buttonFinishEditing.Size = new System.Drawing.Size(75, 23);
            this.buttonFinishEditing.TabIndex = 9;
            this.buttonFinishEditing.Text = "完成";
            this.buttonFinishEditing.UseVisualStyleBackColor = true;
            this.buttonFinishEditing.Click += new System.EventHandler(this.buttonFinishEditing_Click);
            // 
            // dgvItems
            // 
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Location = new System.Drawing.Point(28, 73);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.RowTemplate.Height = 23;
            this.dgvItems.Size = new System.Drawing.Size(637, 176);
            this.dgvItems.TabIndex = 3;
            //this.dgvItems.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvItems_CellBeginEdit);
            this.dgvItems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItems_CellClick);
            //this.dgvItems.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItems_CellEndEdit);
            this.dgvItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvItems_KeyDown);
            this.dgvItems.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvItems_KeyPress);
            // 
            // FrmEditSourcingNoteEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 445);
            this.Controls.Add(this.buttonCalTotalFee);
            this.Controls.Add(this.buttonFinishEditing);
            this.Controls.Add(this.textBoxComment);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxTotalFee);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxShippingFee);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxExtraFee);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelErrMsg);
            this.Controls.Add(this.dgvItems);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dateTimePickerDate);
            this.Controls.Add(this.buttonSelectSupplier);
            this.Controls.Add(this.textBoxSupplier);
            this.Controls.Add(this.label4);
            this.Name = "FrmEditSourcingNoteEx";
            this.Text = "创建采购单 - 记录采购支出";
            this.Load += new System.EventHandler(this.FrmEditSourcingNote_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dateTimePickerDate;
        private System.Windows.Forms.Button buttonSelectSupplier;
        private System.Windows.Forms.TextBox textBoxSupplier;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelErrMsg;
        private System.Windows.Forms.TextBox textBoxComment;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxTotalFee;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxShippingFee;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxExtraFee;
        private System.Windows.Forms.Label label5;
        private CustomGrid dgvItems;
        private System.Windows.Forms.Button buttonCalTotalFee;
        private System.Windows.Forms.Button buttonFinishEditing;
    }
}