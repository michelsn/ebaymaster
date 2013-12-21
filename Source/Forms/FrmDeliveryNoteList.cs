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
    public partial class FrmDeliveryNoteList : Form
    {
        public bool Deleted = false;

        public FrmDeliveryNoteList()
        {
            InitializeComponent();

            this.pagedDgvDeliveryNote.initDgvColumns = SetupDeliveryNoteDataGridViewColumns;
            this.pagedDgvDeliveryNote.getPagedData = DeliveryNoteDAL.GetPagedDeliveryNotes;
            this.pagedDgvDeliveryNote.getRecordCount = DeliveryNoteDAL.GetDeliveryNoteCount;

            this.pagedDgvDeliveryNote.DgvData.CellContentClick += new DataGridViewCellEventHandler(DgvData_CellContentClick);
            this.pagedDgvDeliveryNote.DgvData.KeyDown += new KeyEventHandler(DgvData_KeyDown);
            this.pagedDgvDeliveryNote.DgvData.MouseUp += new MouseEventHandler(DgvData_MouseUp);
        }

        private void SetupDeliveryNoteDataGridViewColumns()
        {
            this.pagedDgvDeliveryNote.DgvData.AutoGenerateColumns = false;

            // DeliveryNoteId
            this.pagedDgvDeliveryNote.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("DeliveryNoteId", @"发货单号", typeof(string), 100, true));

            // DeliveryDate
            this.pagedDgvDeliveryNote.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("DeliveryDate", @"发货日期", typeof(DateTime), 100, true));

            // DeliveryOrderIds
            this.pagedDgvDeliveryNote.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("DeliveryOrderIds", @"发货订单", typeof(string), 100, true));

            // DeliveryUser
            this.pagedDgvDeliveryNote.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("DeliveryUser", @"发货员", typeof(string), 100, true));

            // DeliveryFee
            this.pagedDgvDeliveryNote.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("DeliveryFee", @"运费", typeof(double), 70, true));

            // DeliveryExtraFee
            this.pagedDgvDeliveryNote.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("DeliveryExtraFee", @"其他运费", typeof(double), 100, true));

            // DeliveryComment
            this.pagedDgvDeliveryNote.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("DeliveryComment", @"备注", typeof(string), 140, true));

            this.pagedDgvDeliveryNote.DgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.pagedDgvDeliveryNote.DgvData.MultiSelect = true;
            this.pagedDgvDeliveryNote.DgvData.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dgv_RowPrePaint);
        }

        private void dgv_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
        }

        private void FrmDeliveryNoteList_Load(object sender, EventArgs e)
        {
        }

        private void OpenDeliveryNoteDetailForm()
        {
            int rowIdx = this.pagedDgvDeliveryNote.DgvData.CurrentRow.Index;
            String deliveryNoteId = this.pagedDgvDeliveryNote.DgvData.Rows[rowIdx].Cells[0].Value.ToString();

            FrmEditDeliveryNote frmCreateDeliveryNote = new FrmEditDeliveryNote(deliveryNoteId);
            frmCreateDeliveryNote.ShowDialog();
            if (frmCreateDeliveryNote.Modified)
                pagedDgvDeliveryNote.LoadData();
        }

        void DgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenDeliveryNoteDetailForm();
        }

        private void DgvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OpenDeliveryNoteDetailForm();
                e.Handled = true;
            }
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

        private void ToolStripMenuItemDelDeliveryNote_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确认刪除该进货单么?\r\n。",
                "确认删除?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            int rowIdx = this.pagedDgvDeliveryNote.DgvData.CurrentRow.Index;
            String deliveryNoteId = this.pagedDgvDeliveryNote.DgvData.Rows[rowIdx].Cells[0].Value.ToString();

            DeliveryNoteType note = DeliveryNoteDAL.GetOneDeliveryNote(deliveryNoteId);
            if (note == null)
                return;

            // Restore the stock.
            String tranIdStr = note.DeliveryOrderIds;
            String[] tranIds = tranIdStr.Split(new char[] { ',', ' ' });
            String promptString = "";
            foreach (String tranId in tranIds)
            {
                EbayTransactionType trans = EbayTransactionDAL.GetOneTransactonById(tranId);
                if (trans == null || trans.ItemSKU == null || trans.ItemSKU == "")
                {
                    MessageBox.Show(String.Format("订单号{0}异常", tranId),"抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }

                String itemSku = trans.ItemSKU;
                int saleQuantity = trans.SaleQuantity;
                InventoryItemType item = ItemDAL.GetItemBySKU(itemSku);
                if (item == null)
                {
                    MessageBox.Show(String.Format("无此sku商品{0}", itemSku),"抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }

                ItemDAL.IncreaseItem(itemSku, saleQuantity);
                promptString += String.Format("\nsku:{0} 原库存 {1} => 现库存 {2}", itemSku, item.ItemStockNum, item.ItemStockNum + saleQuantity);

                //
                // Update transaction delivery status.
                //
                EbayTransactionDAL.UpdateTransactionDeliveryStatus(tranId, false, -1);
            }

            DeliveryNoteDAL.DeleteOneDeliveryNote(deliveryNoteId);

            pagedDgvDeliveryNote.LoadData();

            // Indicate main form to update view.
            Deleted = true;

            MessageBox.Show(String.Format("删除发货单成功 {0}", promptString),
                 "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DgvData_MouseUp(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hitTestInfo;
            if (e.Button == MouseButtons.Right)
            {
                hitTestInfo = pagedDgvDeliveryNote.DgvData.HitTest(e.X, e.Y);
                this.contextMenuStripDeliveryNote.Show(pagedDgvDeliveryNote.DgvData, new Point(e.X, e.Y));
            }
        }
    }
}
