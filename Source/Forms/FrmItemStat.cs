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
    public partial class FrmItemStat : Form
    {
        public FrmItemStat()
        {
            InitializeComponent();

            this.pagedDgvItem.initDgvColumns = InitItemStatDgvColumns;
            this.pagedDgvItem.onDgvDataBindCompleted = OnDgvDataBindCompleted;
            this.pagedDgvItem.getPagedData = ItemDAL.GetPagedItems;
            this.pagedDgvItem.getRecordCount = ItemDAL.GetItemCount;
        }

        private void InitItemStatDgvColumns()
        {
            this.pagedDgvItem.DgvData.AutoGenerateColumns = false;
            this.pagedDgvItem.DgvData.AllowUserToAddRows = false;

            this.pagedDgvItem.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("ItemSKU", "商品SKU", typeof(String), 100, true));

            this.pagedDgvItem.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("ItemName", "商品名称", typeof(String), 180, true));

            this.pagedDgvItem.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("ItemStockNum", "当前库存", typeof(String), 80, true));

            this.pagedDgvItem.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("ItemCost", "价格￥", typeof(String), 70, true));

            this.pagedDgvItem.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("ItemWeight", "重量g", typeof(String), 70, true));

            this.pagedDgvItem.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("ItemCustomName", "商品报关名", typeof(String), 150, true));

            this.pagedDgvItem.DgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.pagedDgvItem.DgvData.MultiSelect = true;
        }

        private void FrmItemStat_Load(object sender, EventArgs e)
        {

        }

        private void OnDgvDataBindCompleted()
        {
            for (int rowIdx = 0; rowIdx < this.pagedDgvItem.DgvData.Rows.Count; rowIdx++)
            {
                DataGridViewCell cell = this.pagedDgvItem.DgvData.Rows[rowIdx].Cells[2];
                if (cell == null)
                    continue;

                int stockNum = StringUtil.GetSafeInt(cell.Value);
                if (stockNum == 0)
                {
                    this.pagedDgvItem.DgvData.Rows[rowIdx].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#D4D4D6");
                }
            }
        }

        // Enable form closing on escape key pressed.
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
