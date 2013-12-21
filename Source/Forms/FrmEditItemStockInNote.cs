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
    public partial class FrmEditItemStockInNote : Form
    {
        enum EditMode { Invalid, CreateNew, EditExisting };
        private EditMode mEditMode = EditMode.Invalid;

        public ItemStockInNoteType ExistingNote = null;

        public FrmEditItemStockInNote()
        {
            InitializeComponent();

            mEditMode = EditMode.CreateNew;
        }

        private void CreateNewItemStockInNote()
        {
            if (mEditMode != EditMode.CreateNew)
                return;

            String itemSku = this.textBoxItemSKU.Text;
            if (itemSku == "")
            {
                MessageBox.Show("商品sku错误!", "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            InventoryItemType item = ItemDAL.GetItemBySKU(itemSku);
            if (item == null)
            {
                MessageBox.Show("无此sku商品!", "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            String sourcingNoteIdStr = this.textBoxSourcingNoteId.Text;

            String stockInNumStr = this.textBoxStockInNum.Text;
            int stockInNum = 0;
            if (stockInNumStr == "" || !Int32.TryParse(stockInNumStr, out stockInNum))
            {
                MessageBox.Show("商品入库数量错误!", "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime stockInDate = this.dateTimePickerStockInTime.Value;

            ItemStockInNoteType note = new ItemStockInNoteType();
            note.ItemSKU = itemSku;
            note.ItemTitle = item.ItemName;
            note.SourcingNoteId = sourcingNoteIdStr;
            note.StockInNum = stockInNum;
            note.StockInDate = stockInDate;
            note.Comment = textBoxComment.Text;

            int noteId = ItemStockInNoteDAL.InsertOneItemStockInNote(note);
            if (noteId <= 0)
            {
                MessageBox.Show("创建入库单失败!", "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Increase the item stock number.
            ItemDAL.IncreaseItem(itemSku, stockInNum);

            MessageBox.Show(String.Format("创建入库单成功，入库单号{0}!", noteId),
                "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonFinishEditingItemStockInNote_Click(object sender, EventArgs e)
        {
            if (mEditMode == EditMode.CreateNew)
                CreateNewItemStockInNote();

        }
    }
}
