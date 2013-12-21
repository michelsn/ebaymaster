namespace EbayMaster
{
    partial class FrmSourcingNoteList
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
            this.contextMenuStripSourcingNote = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemEditNote = new System.Windows.Forms.ToolStripMenuItem();
            this.pagedDgvSourcingNote = new EbayMaster.PagedDataGridView();
            this.contextMenuStripSourcingNote.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStripSourcingNote
            // 
            this.contextMenuStripSourcingNote.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemEditNote});
            this.contextMenuStripSourcingNote.Name = "contextMenuStripDeliveryNote";
            this.contextMenuStripSourcingNote.Size = new System.Drawing.Size(137, 26);
            // 
            // ToolStripMenuItemEditNote
            // 
            this.ToolStripMenuItemEditNote.Name = "ToolStripMenuItemEditNote";
            this.ToolStripMenuItemEditNote.Size = new System.Drawing.Size(136, 22);
            this.ToolStripMenuItemEditNote.Text = "编辑采购单";
            this.ToolStripMenuItemEditNote.Click += new System.EventHandler(this.ToolStripMenuItemEditNote_Click);
            // 
            // pagedDgvSourcingNote
            // 
            this.pagedDgvSourcingNote.Location = new System.Drawing.Point(-2, -4);
            this.pagedDgvSourcingNote.Name = "pagedDgvSourcingNote";
            this.pagedDgvSourcingNote.Size = new System.Drawing.Size(983, 463);
            this.pagedDgvSourcingNote.TabIndex = 0;
            // 
            // FrmSourcingNoteList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 471);
            this.Controls.Add(this.pagedDgvSourcingNote);
            this.Name = "FrmSourcingNoteList";
            this.Text = "采购单列表";
            this.Load += new System.EventHandler(this.FrmSourcingNoteList_Load);
            this.contextMenuStripSourcingNote.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PagedDataGridView pagedDgvSourcingNote;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSourcingNote;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemEditNote;
    }
}