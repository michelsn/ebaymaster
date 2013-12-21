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
    public partial class FrmEditSourcingNote : Form
    {
        public enum EditMode { CreateNew, EditExisting };
        public EditMode mEditMode = EditMode.CreateNew;

        private SupplierType mSupplier = null;

        private DataTable mItemsTable = null;
        private SourcingNoteType mSourcingNote = null;

        public FrmEditSourcingNote()
        {
            InitializeComponent();

            mEditMode = EditMode.CreateNew;
        }

        private void InitItemsTable()
        {
            mItemsTable = new DataTable();
            mItemsTable.Columns.Add("ItemSKU", typeof(String));
            mItemsTable.Columns.Add("ItemName", typeof(String));
            mItemsTable.Columns.Add("ItemPrice", typeof(double));
            mItemsTable.Columns.Add("ItemCount", typeof(int));
        }

        private void LoadExistingSourcingNote()
        {
            if (mSourcingNote == null)
                return;

            SupplierType supplier = SupplierDAL.GetSupplierById(mSourcingNote.SupplierId);
            if (supplier == null)
                return;

            mSupplier = supplier;

            this.textBoxSupplier.Text = supplier.SupplierName;
            this.textBoxExtraFee.Text = mSourcingNote.ExtraFee.ToString();
            this.textBoxShippingFee.Text = mSourcingNote.ShippingFee.ToString();
            this.textBoxTotalFee.Text = mSourcingNote.TotalFee.ToString();
            this.textBoxComment.Text = mSourcingNote.Comment;
            this.dateTimePickerDate.Value = mSourcingNote.SourcingDate;

            String skuListStr = mSourcingNote.ItemSkuList;
            String numListStr = mSourcingNote.ItemNumList;
            String priceListStr = mSourcingNote.ItemPriceList;

            String []skuArr = skuListStr.Split(new char[] { ',' });
            String []numArr = numListStr.Split(new char[] { ',' });
            String[] priceArr = priceListStr.Split(new char[] { ',' });

            if (skuArr.Length != numArr.Length || skuArr.Length != priceArr.Length)
                return;

            for (int ii = 0; ii < skuArr.Length; ++ii)
            {
                String sku = skuArr[ii];
                InventoryItemType item = ItemDAL.GetItemBySKU(sku);
                if (item == null)
                    continue;

                DataRow dr = mItemsTable.NewRow();
                dr["ItemSKU"] = sku;
                dr["ItemName"] = item.ItemName;
                dr["ItemPrice"] = StringUtil.GetSafeDouble(priceArr[ii]);
                dr["ItemCount"] = StringUtil.GetSafeInt(numArr[ii]);

                mItemsTable.Rows.Add(dr);
            }

            this.dgvItems.DataSource = mItemsTable;
        }

        public FrmEditSourcingNote(SourcingNoteType sourcingNote)
        {
            InitializeComponent();

            mEditMode = EditMode.EditExisting;

            mSourcingNote = sourcingNote;
        }

        private readonly int SKUColIndex = 0;
        private readonly int ItemTitleColIndex = 1;
        private readonly int ItemPriceColIndex = 2;
        private readonly int ItemCountColIndex = 3;

        private void SetupDgvColumns()
        {
            this.dgvItems.AutoGenerateColumns = false;
            this.dgvItems.AllowUserToAddRows = true;

            this.dgvItems.Columns.Add(DgvUtil.createDgvTextBoxColumn("ItemSKU", "SKU", typeof(String), 120, true));
            this.dgvItems.Columns.Add(DgvUtil.createDgvTextBoxColumn("ItemName", "商品名", typeof(String), 300, true));
            this.dgvItems.Columns.Add(DgvUtil.createDgvTextBoxColumn("ItemPrice", "价格", typeof(double), 80, true));
            this.dgvItems.Columns.Add(DgvUtil.createDgvTextBoxColumn("ItemCount", "数量", typeof(int), 80, true));
        }

        private void FrmEditSourcingNote_Load(object sender, EventArgs e)
        {
            SetupDgvColumns();

            if (mEditMode == EditMode.EditExisting)
            {
                InitItemsTable();
                LoadExistingSourcingNote();
            }
        }

        private void dgvItems_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dgvItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
            }
        }

        #region Get sourcing note from UI

        private SourcingNoteType GetSourcingNoteFromUI(out double totalFee)
        {
            totalFee = 0.0;

            SourcingNoteType note = new SourcingNoteType();

            if (mSupplier == null)
            {
                MessageBox.Show("未选中任何供应商", "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            DateTime date = dateTimePickerDate.Value;

            String skuList = "";
            String priceList = "";
            String countList = "";

            bool first = true;
            foreach (DataGridViewRow row in this.dgvItems.Rows)
            {
                String sku = StringUtil.GetSafeString(row.Cells[SKUColIndex].Value);
                Double price = StringUtil.GetSafeDouble(row.Cells[ItemPriceColIndex].Value);
                int count = StringUtil.GetSafeInt(row.Cells[ItemCountColIndex].Value);

                InventoryItemType item = ItemDAL.GetItemBySKU(sku);
                if (item == null)
                    continue;

                if (count == 0)
                    continue;

                totalFee += count * price;

                if (first)
                {
                    skuList = sku;
                    priceList = price.ToString();
                    countList = count.ToString();
                    first = false;
                }
                else
                {
                    skuList += "," + sku;
                    priceList += "," + price.ToString();
                    countList += "," + count.ToString();
                }
            }

            double extraFee = 0.0;
            if (!Double.TryParse(this.textBoxExtraFee.Text, out extraFee))
            {
                MessageBox.Show("其他费用必须是小数", "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }


            double shippingFee = 0.0;
            if (!Double.TryParse(this.textBoxShippingFee.Text, out shippingFee))
            {
                MessageBox.Show("运费必须是小数", "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            totalFee += extraFee;
            totalFee += shippingFee;

            note.SupplierId = mSupplier.SupplierID;
            note.SourcingDate = date;
            note.ItemSkuList = skuList;
            note.ItemNumList = countList;
            note.ItemPriceList = priceList;
            note.ExtraFee = extraFee;
            note.ShippingFee = shippingFee;
            note.TotalFee = totalFee;
            note.Comment = this.textBoxComment.Text;

            if (mEditMode == EditMode.EditExisting)
            {
                note.SourcingId = mSourcingNote.SourcingId;
            }

            return note;
        }

        #endregion

        private void buttonCalTotalFee_Click(object sender, EventArgs e)
        {
            double totalFee = 0.0;
            GetSourcingNoteFromUI(out totalFee);
            this.textBoxTotalFee.Text = totalFee.ToString();
        }

        private void buttonFinishEditing_Click(object sender, EventArgs e)
        {
            double totalFee = 0.0;
            SourcingNoteType note = GetSourcingNoteFromUI(out totalFee);
            if (note == null)
                return;

            if (mEditMode == EditMode.CreateNew)
            {
                int newNoteId = SourcingNoteDAL.InsertOneSourcingNote(note);
                if (newNoteId > 0)
                    MessageBox.Show("创建采购单成功", "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("创建采购单失败", "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (SourcingNoteDAL.ModifyOneSourcingNote(note))
                    MessageBox.Show("修改采购单成功", "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("创建采购单失败", "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void buttonSelectSupplier_Click(object sender, EventArgs e)
        {
            FrmSupplierList frmSupplierList = new FrmSupplierList(SupplierDlgMode.SelectSupplier);
            frmSupplierList.ShowDialog();

            if (frmSupplierList.mSelectedSupplier != null)
            {
                mSupplier = frmSupplierList.mSelectedSupplier;
                this.textBoxSupplier.Text = mSupplier.SupplierName;
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

        private void dgvItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != SKUColIndex)
                return;

            DataGridViewCell cell = this.dgvItems.CurrentCell;
            if (cell == null)
                return;

            DataGridViewRow row = cell.OwningRow;
            if (row == null)
                return;

            FrmSelectItemSKU frm = new FrmSelectItemSKU();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();

            if (frm.SKU != null)
            {
                cell.Value = frm.SKU;
                InventoryItemType item = ItemDAL.GetItemBySKU(frm.SKU);
                row.Cells[ItemTitleColIndex].Value = item.ItemName;
            }
        }

    }

}
