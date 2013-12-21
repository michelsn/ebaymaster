namespace EbayMaster
{
    partial class FrmItemStat
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
            this.pagedDgvItem = new EbayMaster.PagedDataGridView();
            this.SuspendLayout();
            // 
            // pagedDgvItem
            // 
            this.pagedDgvItem.Location = new System.Drawing.Point(22, 2);
            this.pagedDgvItem.Name = "pagedDgvItem";
            this.pagedDgvItem.Size = new System.Drawing.Size(776, 455);
            this.pagedDgvItem.TabIndex = 0;
            // 
            // FrmItemStat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 469);
            this.Controls.Add(this.pagedDgvItem);
            this.Name = "FrmItemStat";
            this.Text = "所有商品";
            this.Load += new System.EventHandler(this.FrmItemStat_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private PagedDataGridView pagedDgvItem;
    }
}