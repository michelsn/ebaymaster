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
    public partial class FrmItemStockInNoteList : Form
    {
        public FrmItemStockInNoteList()
        {
            InitializeComponent();

            this.pagedDgvItem.initDgvColumns = SetupStockInNoteDataGridViewColumns;
            this.pagedDgvItem.getPagedData = ItemStockInNoteDAL.GetPagedItemStockInNotes;
            this.pagedDgvItem.getRecordCount = ItemStockInNoteDAL.GetItemStockInNoteCount;

            this.pagedDgvItem.DgvData.MouseUp += new MouseEventHandler(pagedDgvItem_MouseUp);
        }

        private void SetupStockInNoteDataGridViewColumns()
        {
            this.pagedDgvItem.DgvData.AutoGenerateColumns = false;

            this.pagedDgvItem.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("NoteId", @"入库单号", typeof(int), 60, true));

            this.pagedDgvItem.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("ItemSKU", @"商品sku", typeof(String), 80, true));

            this.pagedDgvItem.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("ItemTitle", @"商品标题", typeof(String), 140, true));

            this.pagedDgvItem.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("SourcingNoteId", @"采购单号", typeof(String), 80, true));

            this.pagedDgvItem.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("StockInNum", @"入库数量", typeof(int), 80, true));

            this.pagedDgvItem.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("StockInDate", @"入库时间", typeof(DateTime), 80, true));

            this.pagedDgvItem.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("Comment", @"备注", typeof(String), 100, true));

            this.pagedDgvItem.DgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.pagedDgvItem.DgvData.MultiSelect = true;
            this.pagedDgvItem.DgvData.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dgv_RowPrePaint);
        }

        private void dgv_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
        }

        private void FrmItemStockInNoteList_Load(object sender, EventArgs e)
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

        private void ToolStripMenuItemDelItemStockInNote_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确认删除入库单么?\r\n删除后，相应商品的库存将被扣除。",
                "确认删除?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            int rowIdx = this.pagedDgvItem.DgvData.CurrentRow.Index;
            String noteId = this.pagedDgvItem.DgvData.Rows[rowIdx].Cells[0].Value.ToString();

            ItemStockInNoteType note = ItemStockInNoteDAL.GetOneItemStockInNote(noteId);
            if (note == null)
                return;

            // Decrease the stock.
            //  First check validity.
            String itemSKU = note.ItemSKU;

            InventoryItemType item = ItemDAL.GetItemBySKU(itemSKU);
            if (item == null)
                return;

            if (item.ItemStockNum < note.StockInNum)
            {
                MessageBox.Show(String.Format("商品{0}的原库存为{1}，删除入库单后其库存为{2}，非法操作", 
                    itemSKU, item.ItemStockNum, item.ItemStockNum-note.StockInNum), 
                    "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ItemDAL.DecreaseItem(itemSKU, note.StockInNum);

            ItemStockInNoteDAL.DeleteOneItemStockInNote(noteId);
            MessageBox.Show(String.Format("入库单{0}已删除", noteId), "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);

            pagedDgvItem.LoadData();
        }

        private void pagedDgvItem_MouseUp(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hitTestInfo;
            if (e.Button == MouseButtons.Right)
            {
                hitTestInfo = pagedDgvItem.DgvData.HitTest(e.X, e.Y);
                this.contextMenuStripItemStockInNote.Show(pagedDgvItem.DgvData, new Point(e.X, e.Y));
            }
        }
    }
}
