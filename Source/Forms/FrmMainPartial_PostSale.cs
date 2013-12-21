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
    partial class FrmMain
    {
        #region Setup post sale data grid columns

        private readonly int PostSaleDgv_OrderLineItemIdIdx = 0;

        // Setup the post sale order datagridview columns.
        private void SetupPostSaleDataGridViewColumns()
        {
            this.dataGridViewPostSale.AutoGenerateColumns = false;
            this.dataGridViewPostSale.AllowUserToAddRows = false;

            // OrderLineItemId
            this.dataGridViewPostSale.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("OrderLineItemId", @"orderLineItemId", typeof(string), 10, false));

            // SellerId
            this.dataGridViewPostSale.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("SellerName", @"卖家ID", typeof(string), 90, true));

            // BuyerId
            this.dataGridViewPostSale.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("BuyerId", @"买家ID", typeof(string), 90, true));

            // BuyerCountry
            this.dataGridViewPostSale.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("BuyerCountry", @"买家国家", typeof(string), 90, true));

            // ItemSKU
            this.dataGridViewPostSale.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("ItemSKU", @"SKU", typeof(string), 50, true));

            // ItemTitle
            this.dataGridViewPostSale.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("ItemTitle", @"物品名称", typeof(string), 350, true));

            // SaleDate
            this.dataGridViewPostSale.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("SaleDateCN", @"售出时间", typeof(DateTime), 110, true));

            // ShippedDate
            this.dataGridViewPostSale.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("ShippedDate", @"发货时间", typeof(DateTime), 110, true));

            // ShippingService
            this.dataGridViewPostSale.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("ShippingService", @"物流方式", typeof(string), 100, true));

            this.dataGridViewPostSale.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPostSale.MultiSelect = true;
            this.dataGridViewPostSale.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dgv_RowPrePaint);
        }

        #endregion

        #region FrmMain post sale related event handlers

        private DataRow getPostSaleOrderLocally(String orderLineItemId)
        {
            foreach (DataRow dr in this.AllPostSaleOrdersCacheTable.Rows)
            {
                String orderLineItemIdLoc = StringUtil.GetSafeString(dr["OrderLineItemId"]);

                if (orderLineItemId == orderLineItemIdLoc)
                    return dr;
            }

            return null;
        }

        private void dataGridViewPostSale_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
                return;

            for (int rowIdx = 0; rowIdx < this.dataGridViewPostSale.Rows.Count; rowIdx++)
            {
                DataGridViewCell orderLineItemIdCell = this.dataGridViewPostSale.Rows[rowIdx].Cells[PostSaleDgv_OrderLineItemIdIdx];
                if (orderLineItemIdCell.Value == null)
                    continue;

                String orderLineItemId = orderLineItemIdCell.Value.ToString();
                DataRow dr = getPostSaleOrderLocally(orderLineItemId);
                if (dr == null)
                    continue;

                int transId = StringUtil.GetSafeInt(dr["TransactionId"]);
                String buyerId = StringUtil.GetSafeString(dr["BuyerId"]);
                String sellerName = StringUtil.GetSafeString(dr["SellerName"]);
                String itemId = StringUtil.GetSafeString(dr["ItemId"]);

                if (buyerId == "" || sellerName == "" || itemId == "")
                    continue;

                TransactionMessageStatus messageStatus = (TransactionMessageStatus)StringUtil.GetSafeInt(dr["MessageStatus"]);

                if (messageStatus == TransactionMessageStatus.BuyerRepliedSellerNotReplied)
                {
                    TransactionMessageStatus messageStatusComputed
                        = EbayMessageDAL.GetTransactionMessageStatus(buyerId, sellerName, itemId);

                    if (messageStatus != messageStatusComputed)
                    {
                        EbayTransactionDAL.UpdateTransactionMessageStatus(transId, messageStatusComputed);
                        messageStatus = messageStatusComputed;
                    }
                }

                Color rowBkColor = Color.White;
                if (messageStatus == TransactionMessageStatus.SellerInquired)
                    rowBkColor = ColorTranslator.FromHtml("#90EE90");
                else if (messageStatus == TransactionMessageStatus.BuyerRepliedSellerNotReplied)
                    rowBkColor = ColorTranslator.FromHtml("#FFFF00");
                else if (messageStatus == TransactionMessageStatus.BuyerRepliedSellerReplied)
                    rowBkColor = ColorTranslator.FromHtml("#DAA520");

                if (!StringUtil.GetSafeBool(dr["IsPaid"]))
                    rowBkColor = ColorTranslator.FromHtml("#808080"); 

                this.dataGridViewPostSale.Rows[rowIdx].DefaultCellStyle.BackColor = rowBkColor;
            }
        }

        private void buttonPostSaleFirstPage_Click(object sender, EventArgs e)
        {
            CurrentPostSalePage = 1;
            LoadPostSaleData();
        }

        private void buttonPostSalePrevPage_Click(object sender, EventArgs e)
        {
            CurrentPostSalePage -= 1;
            LoadPostSaleData();
        }

        private void buttonPostSaleNextPage_Click(object sender, EventArgs e)
        {
            CurrentPostSalePage += 1;
            LoadPostSaleData();
        }

        private void buttonPostSaleLastPage_Click(object sender, EventArgs e)
        {
            CurrentPostSalePage = PostSaleCount / PostSalePageSize + 1;
            LoadPostSaleData();
        }

        private void dataGridViewPostSale_MouseUp(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hitTestInfo;
            if (e.Button == MouseButtons.Right)
            {
                hitTestInfo = dataGridViewPostSale.HitTest(e.X, e.Y);
                this.contextMenuStripPostSale.Show(dataGridViewPostSale, new Point(e.X, e.Y));
            }
        }

        #endregion

        public static readonly int PostSaleDgv_ViewMessageMenuItemIndex = 0;
        public static readonly int PostSaleDgv_SendMessageMenuItemIndex = 1;

        private void contextMenuStripPostSale_Opening(object sender, CancelEventArgs e)
        {
            bool enableViewMessage = true;
            bool enableSendMessage = true;

            ContextMenuStrip cmnu = (ContextMenuStrip)sender;
            DataGridViewSelectedRowCollection selectedRows = this.dataGridViewPostSale.SelectedRows;

            if (selectedRows.Count != 1)
            {
                enableViewMessage = false;
            }

            foreach (DataGridViewRow row in selectedRows)
            {
                String orderLineItemId = row.Cells[PostSaleDgv_OrderLineItemIdIdx].Value.ToString();
            }

            cmnu.Items[PostSaleDgv_ViewMessageMenuItemIndex].Enabled = enableViewMessage;
            cmnu.Items[PostSaleDgv_SendMessageMenuItemIndex].Enabled = enableSendMessage;
        }

        private void ToolStripMenuItemSendMessageToBuyer_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = this.dataGridViewPostSale.SelectedRows;

            DataTable orderTable = AllPostSaleOrdersCacheTable.Clone();

            foreach (DataGridViewRow row in selectedRows)
            {
                String orderLineItemId = row.Cells[PostSaleDgv_OrderLineItemIdIdx].Value.ToString();
                DataRow orderRow = getPostSaleOrderLocally(orderLineItemId);

                DataRow newOrderRow = orderTable.NewRow();
                newOrderRow.ItemArray = orderRow.ItemArray;
                orderTable.Rows.Add(newOrderRow);
            }

            FrmSendMessage frmSendMessage = new FrmSendMessage();
            frmSendMessage.StartPosition = FormStartPosition.CenterScreen;
            frmSendMessage.OrdersDataTable = orderTable;
            frmSendMessage.ShowDialog(this);

            LoadPostSaleData();
        }

        private void ToolStripMenuItemViewMessage_Click(object sender, EventArgs e)
        {
            int rowIdx = this.dataGridViewPostSale.CurrentRow.Index;
            String orderLineItemId = this.dataGridViewPostSale.Rows[rowIdx].Cells[PostSaleDgv_OrderLineItemIdIdx].Value.ToString();

            EbayTransactionType trans = EbayTransactionDAL.GetOneTransaction(orderLineItemId);
            if (trans == null)
                return;

            AccountType account = AccountUtil.GetAccount(trans.SellerName);
            if (account == null)
            {
                MessageBox.Show(String.Format("此账号{0}未绑定到系统", trans.SellerName),
                    "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FrmUserMessage frmUserMessage = new FrmUserMessage();
            frmUserMessage.EbayTransaction = trans;
            frmUserMessage.Account = account;
            frmUserMessage.ShowDialog();

            if (frmUserMessage.SentMessage || frmUserMessage.MarkMsgAsReplied)
            {
                LoadPostSaleData();
            }
        }

        private void dataGridViewPostSale_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ToolStripMenuItemViewMessage_Click(sender, e);
        }
    }
}