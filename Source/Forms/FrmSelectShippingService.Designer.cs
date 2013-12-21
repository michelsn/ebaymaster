namespace EbayMaster
{
    partial class FrmSelectShippingService
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
            this.listBoxShippingService = new System.Windows.Forms.ListBox();
            this.buttonSelectShippingService = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxShippingService
            // 
            this.listBoxShippingService.FormattingEnabled = true;
            this.listBoxShippingService.ItemHeight = 12;
            this.listBoxShippingService.Location = new System.Drawing.Point(21, 12);
            this.listBoxShippingService.Name = "listBoxShippingService";
            this.listBoxShippingService.Size = new System.Drawing.Size(331, 244);
            this.listBoxShippingService.TabIndex = 0;
            this.listBoxShippingService.SelectedIndexChanged += new System.EventHandler(this.listBoxShippingService_SelectedIndexChanged);
            this.listBoxShippingService.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxShippingService_MouseDoubleClick);
            // 
            // buttonSelectShippingService
            // 
            this.buttonSelectShippingService.Location = new System.Drawing.Point(145, 285);
            this.buttonSelectShippingService.Name = "buttonSelectShippingService";
            this.buttonSelectShippingService.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectShippingService.TabIndex = 1;
            this.buttonSelectShippingService.Text = "确认";
            this.buttonSelectShippingService.UseVisualStyleBackColor = true;
            this.buttonSelectShippingService.Click += new System.EventHandler(this.buttonSelectShippingService_Click);
            // 
            // FrmSelectShippingService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 320);
            this.Controls.Add(this.buttonSelectShippingService);
            this.Controls.Add(this.listBoxShippingService);
            this.Name = "FrmSelectShippingService";
            this.Text = "FrmSelectShippingService";
            this.Load += new System.EventHandler(this.FrmSelectShippingService_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxShippingService;
        private System.Windows.Forms.Button buttonSelectShippingService;
    }
}