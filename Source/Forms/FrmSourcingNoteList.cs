using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EbayMaster
{
    public partial class FrmSourcingNoteList : Form
    {
        public FrmSourcingNoteList()
        {
            InitializeComponent();

            this.pagedDgvSourcingNote.initDgvColumns = SetupDgvColumns;
            this.pagedDgvSourcingNote.getPagedData = SourcingNoteDAL.GetPagedSourcingNotes;
            this.pagedDgvSourcingNote.getRecordCount = SourcingNoteDAL.GetSourceNoteCount;

            this.pagedDgvSourcingNote.DgvData.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(DgvData_DataBindingComplete);
            this.pagedDgvSourcingNote.DgvData.MouseUp += new MouseEventHandler(DgvData_MouseUp);
        }

        void DgvData_MouseUp(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hitTestInfo;
            if (e.Button == MouseButtons.Right)
            {
                hitTestInfo = pagedDgvSourcingNote.DgvData.HitTest(e.X, e.Y);
                this.contextMenuStripSourcingNote.Show(pagedDgvSourcingNote.DgvData, new Point(e.X, e.Y));
            }
        }

        void DgvData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in pagedDgvSourcingNote.DgvData.Rows)
            {
                int supplierId = StringUtil.GetSafeInt(row.Cells[1].Value);
                SupplierType supplier = SupplierDAL.GetSupplierById(supplierId);

                if (supplier == null)
                    continue;

                row.Cells[2].Value = supplier.SupplierName;
            }
        }

        private void SetupDgvColumns()
        {
            this.pagedDgvSourcingNote.DgvData.AutoGenerateColumns = false;

            this.pagedDgvSourcingNote.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("SourcingId", @"采购单号", typeof(string), 80, true));

            this.pagedDgvSourcingNote.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("SupplierId", @"", typeof(string), 80, false));

            this.pagedDgvSourcingNote.DgvData.Columns.Add(
                 DgvUtil.createDgvTextBoxColumn("", @"供应商", typeof(string), 100, true));

            this.pagedDgvSourcingNote.DgvData.Columns.Add(
                 DgvUtil.createDgvTextBoxColumn("ItemSkuList", @"商品sku", typeof(string), 100, true));

            this.pagedDgvSourcingNote.DgvData.Columns.Add(
                 DgvUtil.createDgvTextBoxColumn("ItemNumList", @"数量", typeof(string), 100, true));

            this.pagedDgvSourcingNote.DgvData.Columns.Add(
                 DgvUtil.createDgvTextBoxColumn("ItemPriceList", @"价格", typeof(string), 100, true));

            this.pagedDgvSourcingNote.DgvData.Columns.Add(
                 DgvUtil.createDgvTextBoxColumn("ExtraFee", @"额外费用", typeof(string), 80, true));

            this.pagedDgvSourcingNote.DgvData.Columns.Add(
                 DgvUtil.createDgvTextBoxColumn("ShippingFee", @"运费", typeof(string), 60, true));

            this.pagedDgvSourcingNote.DgvData.Columns.Add(
                 DgvUtil.createDgvTextBoxColumn("TotalFee", @"总费用", typeof(string), 80, true));

            this.pagedDgvSourcingNote.DgvData.Columns.Add(
                 DgvUtil.createDgvTextBoxColumn("Comment", @"备注", typeof(string), 100, true));

            this.pagedDgvSourcingNote.DgvData.Columns.Add(
                 DgvUtil.createDgvTextBoxColumn("SourcingDate", @"日期", typeof(string), 100, true));

            this.pagedDgvSourcingNote.DgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.pagedDgvSourcingNote.DgvData.MultiSelect = true;
            this.pagedDgvSourcingNote.DgvData.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dgv_RowPrePaint);
        }

        private void dgv_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
        }

            
        private void FrmSourcingNoteList_Load(object sender, EventArgs e)
        {

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ToolStripMenuItemEditNote_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = this.pagedDgvSourcingNote.DgvData.SelectedRows;
            if (rows == null || rows.Count != 1)
                return;

            DataGridViewRow row = rows[0];
            if (row == null)
                return;

            int noteId = StringUtil.GetSafeInt(row.Cells[0].Value);
            SourcingNoteType note = SourcingNoteDAL.GetSourcingNoteById(noteId);
            if (note == null)
                return;

            FrmEditSourcingNote frm = new FrmEditSourcingNote(note);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();

            this.pagedDgvSourcingNote.LoadData();
        }
    }
}
