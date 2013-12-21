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
        #region Set up order data grid columns

        // CAUTION: Use these const members instead of plain numbers to avoid potential bugs!!!!!!!!!!
        private readonly int OrderDgv_ImageIndex = 0;
        private readonly int OrderDgv_OrderIdIndex = 1;
        private readonly int OrderDgv_OrderLineItemIndex = 2;
        private readonly int OrderDgv_TransactionIdIndex = 3;
        private readonly int OrderDgv_ItemSKUIndex = 7;
        private readonly int OrderDgv_SellerLeftFeedbackIndex = 13;
        //private readonly int OrderDgv_BuyerLeftFeedbackIndex = 14;
        private readonly int OrderDgv_ShippingServiceIndex = 15;
        private readonly int OrderDgv_TrackingNoIndex = 16;

        //
        // Set up the datagridview columns manually, rather than letting them be generated
        // by columns defined in data source automatically.
        // Columns:
        //       0       - OrderId                     - invisible (for attaching the order id to a row)
        //       1       - OrderLineItemId             - invisible
        //       2       - Transaction id.
        //       3       - SellerName
        //       4       - BuyerId
        //       5       - BuyerCountry
        //       6       - ItemSKU
        //       7       - ItemTitle
        //       8       - ItemPrice
        //       9       - SaleQuantity
        //       10      - TotalPrice
        //       11      - SaleDate
        //       12      - IsSellerLeftFeedback
        //       13      - IsBuyerLeftFeedback
        //       14      - ShippingService
        //       15      - TrackingNo
        private void SetupOrderDataGridViewColumns()
        {
            this.dataGridViewAllOrders.AutoGenerateColumns = false;
            this.dataGridViewAllOrders.AllowUserToAddRows = false;

            //
            this.dataGridViewAllOrders.Columns.Add(
                DgvUtil.createDgvImageColumn("", "", 20, true));

            // OrderId
            this.dataGridViewAllOrders.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("OrderId", @"交易ID", typeof(string), 10, false));

            // OrderLineItemId
            this.dataGridViewAllOrders.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("OrderLineItemId", @"orderLineItemIdID", typeof(string), 10, false));

            // Sale record id
            this.dataGridViewAllOrders.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("TransactionId", @"#", typeof(string), 40, true));

            // SellerId
            this.dataGridViewAllOrders.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("SellerName", @"卖家", typeof(string), 60, true));

            // BuyerId
            this.dataGridViewAllOrders.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("BuyerId", @"买家", typeof(string), 80, true));

            // BuyerCountry
            this.dataGridViewAllOrders.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("BuyerCountry", @"买家国家", typeof(string), 80, true));

            // ItemSKU
            this.dataGridViewAllOrders.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("ItemSKU", @"SKU", typeof(string), 50, true));

            // ItemTitle
            this.dataGridViewAllOrders.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("ItemTitle", @"物品名称", typeof(string), 320, true));

            // ItemPrice
            this.dataGridViewAllOrders.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("ItemPrice", @"价格", typeof(double), 40, true));

            // SaleQuantity
            this.dataGridViewAllOrders.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("SaleQuantity", @"数量", typeof(int), 40, true));

            // TotalPrice
            this.dataGridViewAllOrders.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("TotalPrice", @"总价", typeof(double), 40, true));

            // SaleDate
            this.dataGridViewAllOrders.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("SaleDateCN", @"售出时间", typeof(DateTime), 100, true));

            // IsSellerLeftFeedback
            this.dataGridViewAllOrders.Columns.Add(
                DgvUtil.createDgvCheckBoxColumn("IsSellerLeftFeedback", @"卖家评", typeof(String), 50, true));

            // IsBuyerLeftFeedback
            this.dataGridViewAllOrders.Columns.Add(
                DgvUtil.createDgvCheckBoxColumn("IsBuyerLeftFeedback", @"买家评", typeof(String), 50, true));

            // ShippingService
            this.dataGridViewAllOrders.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("ShippingService", @"物流方式", typeof(string), 100, true));

            // ShippingTrackingNo
            this.dataGridViewAllOrders.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("ShippingTrackingNo", @"跟踪号", typeof(string), 70, true));

            this.dataGridViewAllOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAllOrders.MultiSelect = true;
            this.dataGridViewAllOrders.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dgv_RowPrePaint);
        }

        #endregion

        #region Order datagridview event handlers

        private void dgv_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
        }

        private DataRow getEbayTransactionLocally(String orderLineItemId)
        {
            foreach (DataRow dr in this.AllOrdersCacheTable.Rows)
            {
                String orderLineItemIdLoc = StringUtil.GetSafeString(dr["OrderLineItemId"]);

                if (orderLineItemId == orderLineItemIdLoc)
                    return dr;
            }

            return null;
        }

        Image plusImg = Image.FromFile("plus.png");
        Image minusImage = Image.FromFile("minus.png");
        Image normalImage = Image.FromFile("normal.gif");
        Image blankImage = Image.FromFile("blank.gif");
        Image yesImage = Image.FromFile("yes.jpg");

        private void dataGridViewAllOrders_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
                return;

            for (int rowIdx = 0; rowIdx < this.dataGridViewAllOrders.Rows.Count; rowIdx++)
            {
                DataGridViewCell orderLineItemIdCell = this.dataGridViewAllOrders.Rows[rowIdx].Cells[OrderDgv_OrderLineItemIndex];
                if (orderLineItemIdCell.Value == null)
                    continue;

                String orderLineItemId = orderLineItemIdCell.Value.ToString();
                DataRow dr = getEbayTransactionLocally(orderLineItemId);
                if (dr == null)
                    continue;

                String orderId = StringUtil.GetSafeString(this.dataGridViewAllOrders.Rows[rowIdx].Cells[OrderDgv_OrderIdIndex].Value);
                bool subtrans = false;
                if (orderId.IndexOf("-") < 0)
                {
                    // An order with multiple transactions.
                    bool imageSpecified = false;
                    if (rowIdx > 0)
                    {
                        String prevOrderId = StringUtil.GetSafeString(this.dataGridViewAllOrders.Rows[rowIdx - 1].Cells[OrderDgv_OrderIdIndex].Value);
                        if (prevOrderId == orderId)
                        {
                            this.dataGridViewAllOrders.Rows[rowIdx].Cells[OrderDgv_ImageIndex].Value = blankImage;
                            //this.dataGridViewAllOrders.Rows[rowIdx].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#DBADDA");
                            imageSpecified = true;
                            subtrans = true;
                        }
                    }
                    
                    if (!imageSpecified)
                        this.dataGridViewAllOrders.Rows[rowIdx].Cells[OrderDgv_ImageIndex].Value = minusImage;

                    if (subtrans)
                        this.dataGridViewAllOrders.Rows[rowIdx].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#DBADDA");
                    //subtrans = true;
                }
                else
                {
                    this.dataGridViewAllOrders.Rows[rowIdx].Cells[OrderDgv_ImageIndex].Value = normalImage;
                }

                //if (StringUtil.GetSafeBool(dr["IsSellerLeftFeedback"]))
                //    this.dataGridViewAllOrders.Rows[rowIdx].Cells[OrderDgv_SellerLeftFeedbackIndex].Value = yesImage;

                bool isShipped = StringUtil.GetSafeBool(dr["IsShipped"]);
                bool isPaid = StringUtil.GetSafeBool(dr["IsPaid"]);

                // http://www.phpx.com/man/dhtmlcn/colors/colors.html
                if (isShipped == true && !subtrans)
                    this.dataGridViewAllOrders.Rows[rowIdx].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#90EE90");

                if (isPaid == false && !subtrans)
                    this.dataGridViewAllOrders.Rows[rowIdx].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#808080");
            }
        }

        private void dataGridViewAllOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != OrderDgv_ImageIndex)
                return;

            DataGridViewRow row = this.dataGridViewAllOrders.Rows[e.RowIndex];
            Image image = (Image)row.Cells[OrderDgv_ImageIndex].Value;
            if (image != minusImage && image != plusImg)
                return;

            String orderId = StringUtil.GetSafeString(row.Cells[OrderDgv_OrderIdIndex].Value);

            bool collapsed = true;
            if (e.RowIndex + 1 < this.dataGridViewAllOrders.Rows.Count)
            {
                DataGridViewRow nextRow = this.dataGridViewAllOrders.Rows[e.RowIndex+1];
                if (nextRow.Visible &&
                    orderId == StringUtil.GetSafeString(nextRow.Cells[OrderDgv_OrderIdIndex].Value))
                {
                    collapsed = false;
                }
            }

            if (!collapsed)
            {
                for (int ii = e.RowIndex + 1; ii < this.dataGridViewAllOrders.Rows.Count; ++ii)
                {
                    DataGridViewRow nextRow = this.dataGridViewAllOrders.Rows[ii];
                    if (orderId == StringUtil.GetSafeString(nextRow.Cells[OrderDgv_OrderIdIndex].Value))
                        nextRow.Visible = false;
                }
                row.Cells[OrderDgv_ImageIndex].Value = plusImg;
            }
            else
            {
                for (int ii = e.RowIndex + 1; ii < this.dataGridViewAllOrders.Rows.Count; ++ii)
                {
                    DataGridViewRow nextRow = this.dataGridViewAllOrders.Rows[ii];
                    if (orderId == StringUtil.GetSafeString(nextRow.Cells[OrderDgv_OrderIdIndex].Value))
                        nextRow.Visible = true;
                }
                row.Cells[OrderDgv_ImageIndex].Value = minusImage;
            }
        }

        private void buttonOrderPrevPage_Click(object sender, EventArgs e)
        {
            CurrentOrderPage -= 1;
            LoadOrderData();
        }

        private void buttonOrderNextPage_Click(object sender, EventArgs e)
        {
            CurrentOrderPage += 1;
            LoadOrderData();
        }

        private void buttonOrderFirstPage_Click(object sender, EventArgs e)
        {
            CurrentOrderPage = 1;
            LoadOrderData();
        }

        private void buttonOrderLastPage_Click(object sender, EventArgs e)
        {
            CurrentOrderPage = OrderCount / OrderPageSize + 1;
            LoadOrderData();
        }

        #endregion

        //
        // Order context menu showup.
        //
        private void dataGridViewAllOrders_MouseUp(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hitTestInfo;
            if (e.Button == MouseButtons.Right)
            {
                hitTestInfo = dataGridViewAllOrders.HitTest(e.X, e.Y);
                this.contextMenuStripTransaction.Show(dataGridViewAllOrders, new Point(e.X, e.Y));
            }
        }

        private void ToolStripMenuItemCreateDeliveryNoteFromOrders_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = this.dataGridViewAllOrders.SelectedRows;
            List<String> tranIdList = new List<string>();
            foreach (DataGridViewRow row in selectedRows)
            {
                String tranId = row.Cells[OrderDgv_TransactionIdIndex].Value.ToString();
                tranIdList.Add(tranId);
            }

            FrmEditDeliveryNote frmEditDeliveryNote = new FrmEditDeliveryNote(tranIdList);
            frmEditDeliveryNote.ShowDialog();

            if (frmEditDeliveryNote.Added)
                LoadOrderData();
        }

        private void radioButtonOrders_CheckedChanged(object sender, EventArgs e)
        {
            bool prevIsShowingPendingOrders = isShowingPendingOrders;

            if (this.radioButtonPendingOrders.Checked)
                isShowingPendingOrders = true;
            else
                isShowingPendingOrders = false;

            if (prevIsShowingPendingOrders != isShowingPendingOrders)
            {
                CurrentOrderPage = 1;
                LoadOrderData();
            }

        }

        public static readonly int ViewOrderDetailMenuItemIndex = 0;
        public static readonly int ViewOrderMessageMenuItemIndex = 1;
        public static readonly int SendMessageToBuyerMenuItemIndex = 2;
        public static readonly int SetSKUForOrderItemMenuItemIndex = 3;
        public static readonly int ViewItemMenuItemIndex = 4;
        public static readonly int SelectShippingServiceMenuItemIndex = 5;
        public static readonly int MarkAsShippedMenuItemIndex = 6;
        public static readonly int UploadTrackingNoMenuItemIndex = 7;
        public static readonly int LeaveFeedbackMenuItemIndex = 8;
        public static readonly int CreateDeliveryNoteMenuItemIndex = 10;
        public static readonly int MergeOrdersMenuItemIndex = 11;

        private void contextMenuStripTransaction_Opening(object sender, CancelEventArgs e)
        {
            ContextMenuStrip cmnu = (ContextMenuStrip)sender;

            bool enableViewOrderDetail = true;
            bool enableViewOrderMessage = true;

            bool enableSendMessageToBuyer = true;
            bool enableSetSKUForOrderItem = true;

            bool enableViewItem = true;

            bool enableSelectShippingService = true;
            bool enableMarkAsShipped = true;

            bool enableCreateDeliveryNote = true;
            bool enableUploadTrackingNo = true;
            bool enableLeaveFeedback = true;

            bool enableMergeOrders = true;

            DataGridViewSelectedRowCollection selectedRows = this.dataGridViewAllOrders.SelectedRows;

            if (selectedRows.Count != 1)
            {
                enableViewOrderDetail = false;
                enableViewOrderMessage = false;
                enableSendMessageToBuyer = false;
                enableViewItem = false;
                enableUploadTrackingNo = false;
            }

            // First check if every transaction item has a related sku.
            String lastBuyerId = null;

            foreach (DataGridViewRow row in selectedRows)
            {
                String orderLineItemId = row.Cells[OrderDgv_OrderLineItemIndex].Value.ToString();

                EbayTransactionType trans = EbayTransactionDAL.GetOneTransaction(orderLineItemId);
                if (trans == null)
                    continue;

                if (lastBuyerId == null)
                {
                    lastBuyerId = trans.BuyerId;
                }
                else if (enableMergeOrders && lastBuyerId != trans.BuyerId)
                {
                    enableMergeOrders = false;
                }

                if (enableMergeOrders && trans.OrderId.IndexOf("-") < 0)
                {
                    enableMergeOrders = false;
                }

                if (trans.IsShipped || !trans.IsPaid)
                {
                    //enableSelectShippingService = false;
                    enableMarkAsShipped = false;
                }

                if (!trans.IsPaid)
                {
                    enableSelectShippingService = false;
                }

                if (enableViewOrderDetail && !trans.IsPaid)
                {
                    enableViewOrderDetail = false;
                }

                if (enableViewOrderMessage && !trans.IsPaid)
                {
                    enableViewOrderMessage = false;
                }

                if (enableSendMessageToBuyer && !trans.IsPaid)
                {
                    enableSendMessageToBuyer = false;
                }

                if (enableSetSKUForOrderItem && !trans.IsPaid)
                {
                    enableSetSKUForOrderItem = false;
                }

                if (enableViewItem && !trans.IsPaid)
                {
                    enableViewItem = false;
                }

                if (enableCreateDeliveryNote && (!trans.IsPaid || trans.IsDelivered))
                {
                    enableCreateDeliveryNote = false;
                }

                if (enableUploadTrackingNo && trans.ShippingTrackingNo != "")
                {
                    enableUploadTrackingNo = false;
                }

                if (enableLeaveFeedback && trans.IsSellerLeftFeedback)
                {
                    enableLeaveFeedback = false;
                }
            }

            cmnu.Items[ViewOrderDetailMenuItemIndex].Enabled = enableViewOrderDetail;
            cmnu.Items[ViewOrderMessageMenuItemIndex].Enabled = enableViewOrderMessage;

            cmnu.Items[SendMessageToBuyerMenuItemIndex].Enabled = enableSendMessageToBuyer;
            cmnu.Items[SetSKUForOrderItemMenuItemIndex].Enabled = enableSetSKUForOrderItem;

            cmnu.Items[ViewItemMenuItemIndex].Enabled = enableViewItem;

            cmnu.Items[SelectShippingServiceMenuItemIndex].Enabled = enableSelectShippingService;
            cmnu.Items[MarkAsShippedMenuItemIndex].Enabled = enableMarkAsShipped;

            cmnu.Items[UploadTrackingNoMenuItemIndex].Enabled = enableUploadTrackingNo;
            cmnu.Items[LeaveFeedbackMenuItemIndex].Enabled = enableLeaveFeedback;
            cmnu.Items[CreateDeliveryNoteMenuItemIndex].Enabled = enableCreateDeliveryNote;

            cmnu.Items[MergeOrdersMenuItemIndex].Enabled = enableMergeOrders;
        }
    }
}