using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using eBay.Service.Core.Sdk;
using Samples.Helper;
using eBay.Service.Core.Soap;
using eBay.Service.Call;
using eBay.Service.Util;

namespace EbayMaster
{
    partial class FrmMain
    {
        private void ToolStripMenuItemCreateItemStockInNote_Click(object sender, EventArgs e)
        {
            FrmEditItemStockInNote frmEditItemStockInNote = new FrmEditItemStockInNote();
            frmEditItemStockInNote.ShowDialog();
        }

        private void ToolStripMenuItemViewItemStockInNote_Click(object sender, EventArgs e)
        {
            FrmItemStockInNoteList frmItemStockInNoteList = new FrmItemStockInNoteList();
            frmItemStockInNoteList.ShowDialog();
        }

        private static int OrderTabIndex = 0;
        private static int ActiveListingTabIndex = 1;
        private static int MessageTabIndex = 2;
        private static int PostSaleTabIndex = 3;

        private void tabControlEbayMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OrderTabIndex == tabControlEbayMaster.SelectedIndex)
            {
                this.CurrentActiveTab = EbayMasterAcitveTab.OrderTab;
            }
            else if (ActiveListingTabIndex == tabControlEbayMaster.SelectedIndex)
            {
                this.CurrentActiveTab = EbayMasterAcitveTab.ActiveListingTab;
            }
            else if (PostSaleTabIndex == tabControlEbayMaster.SelectedIndex)
            {
                this.CurrentActiveTab = EbayMasterAcitveTab.PostSaleTab;
            }
            else if (MessageTabIndex == tabControlEbayMaster.SelectedIndex)
            {
                this.CurrentActiveTab = EbayMasterAcitveTab.MessageTab;
            }

            LoadData();
        }

        private void ToolStripMenuItemSelectShippingService_Click(object sender, EventArgs e)
        {
            FrmSelectShippingService frmSelectShippingService = new FrmSelectShippingService();
            frmSelectShippingService.ShowDialog();

            KeyValuePair<string, string> shippingServicePair = frmSelectShippingService.SelectedShippingService;
            if (shippingServicePair.Key == null)
                return;

            string shippingServiceCode = shippingServicePair.Key;
            string shippingServiceDesc = shippingServicePair.Value;

            DataGridViewSelectedRowCollection selectedRows = this.dataGridViewAllOrders.SelectedRows;
            List<string> orderLineItemIds = new List<string>();
            foreach (DataGridViewRow row in selectedRows)
            {
                String orderLineItemId = row.Cells[OrderDgv_OrderLineItemIndex].Value.ToString();
                if (orderLineItemId == "")
                    continue;
                orderLineItemIds.Add(orderLineItemId);
                row.Cells[OrderDgv_ShippingServiceIndex].Value = shippingServiceDesc;
            }

            EbayTransactionDAL.UpdateTransactionsShippingService(orderLineItemIds, shippingServiceCode, shippingServiceDesc);
            MessageBox.Show("设置物流方式成功", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ToolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            FrmAboutBox frmAboutBox = new FrmAboutBox();
            frmAboutBox.ShowDialog();
        }

        private void ToolStripMenuItemMessageTemplate_Click(object sender, EventArgs e)
        {
            FrmMessageTemplate frmMessageTemplate = new FrmMessageTemplate();
            frmMessageTemplate.ShowDialog();
        }

        //
        // User selected transactions and clicked "mark as shipped".
        //
        private void ToolStripMenuItemMarkAsShipped_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确认发货么?\r\n发货后，ebay会标记成已发货状态。",
                "确认发货?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                DataGridViewSelectedRowCollection selectedRows = this.dataGridViewAllOrders.SelectedRows;

                // First check if every transaction item has a related sku.
                foreach (DataGridViewRow row in selectedRows)
                {
                    String orderLineItemId = row.Cells[OrderDgv_OrderLineItemIndex].Value.ToString();

                    EbayTransactionType trans = EbayTransactionDAL.GetOneTransaction(orderLineItemId);
                    if (trans == null)
                        return;

                    if (trans.IsPaid == false)
                    {
                        MessageBox.Show("有些商品没有付款，不能发货!", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (trans.IsShipped == true)
                    {
                        MessageBox.Show("有些商品已经发货，不能重新发货!", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (trans.ItemSKU == null || trans.ItemSKU.Trim() == "")
                    {
                        MessageBox.Show("有些商品没有指定SKU!", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                List<String> handledOrderIds = new List<String>();

                foreach (DataGridViewRow row in selectedRows)
                {
                    String orderLineItemId = row.Cells[OrderDgv_OrderLineItemIndex].Value.ToString();

                    EbayTransactionType trans = EbayTransactionDAL.GetOneTransaction(orderLineItemId);
                    if (trans == null)
                        return;

                    if (trans.IsPaid == false || trans.IsShipped == true)
                    {
                        MessageBox.Show("商品未付款或者已经发货!", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }

                    String orderId = trans.OrderId;

                    if (handledOrderIds.Contains(orderId))
                    {
                        continue;
                    }

                    AccountType account = null;
                    List<AccountType> allAccounts = AccountUtil.GetAllAccounts();
                    foreach (AccountType accountType in allAccounts)
                    {
                        if (accountType.ebayAccount == trans.SellerName)
                        {
                            account = accountType;
                            break;
                        }
                    }

                    if (account == null || account.SellerApiContext == null)
                    {
                        MessageBox.Show("账号没有初始化!", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    EbayTransactionBiz.CompleteSale(account, trans.OrderId, trans.BuyerId, trans.ItemId, trans.EbayTransactionId,
                       false, true, true, true/*isShipped*/);

                    handledOrderIds.Add(orderId);

                    StringCollection orderIds = new StringCollection();
                    orderIds.Add(trans.OrderId);
                    TimeFilter timeFilter = new TimeFilter();
                    timeFilter.TimeFrom = this.dateTimePickerStartTime.Value;
                    timeFilter.TimeTo = this.dateTimePickerEndTime.Value;
                    List<EbayTransactionType> transList = EbayTransactionBiz.GetAllOrders(account, timeFilter, orderIds);
                    if (transList.Count != 1)
                    {
                        MessageBox.Show("交易在ebay系统中不存在!", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }
                    if (transList[0].IsShipped == false)
                    {
                        MessageBox.Show(string.Format("该交易没有在ebay系统中标记成功! 用户id={0}", trans.BuyerId), "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }

                    // ZHI_TODO
                    //ItemDAL.ShipItem(trans.ItemSKU, trans.SaleQuantity);

                    // Update transaction shipped date.
                    EbayTransactionDAL.UpdateTransactionShippedStatus(trans.TransactionId, true/*shipped*/, transList[0].ShippedDate);

                    Logger.WriteSystemUserLog(string.Format("标记交易已发货成功: userId={0}, 商品名={1}", trans.BuyerId, trans.ItemTitle));

                    row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#90EE90");
                }

                MessageBox.Show("标记成发货成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ToolStripMenuItemDBConnSettings_Click(object sender, EventArgs e)
        {
            FrmDBConnection frmDBConnection = new FrmDBConnection();
            frmDBConnection.ShowDialog();
        }

        private void ToolStripMenuItemAccountSettings_Click(object sender, EventArgs e)
        {
            FrmAccount frmAccount = new FrmAccount();
            frmAccount.ShowDialog();
        }

        //
        // Open the dialog to edit the item category: add new category, 
        //  modify existing category or delete category.
        //
        private void ToolStripMenuItemCategory_Click(object sender, EventArgs e)
        {
            FrmItemCategory frmItemCategory = new FrmItemCategory();
            frmItemCategory.Show();
        }

        private void ToolStripMenuItemItem_Click(object sender, EventArgs e)
        {
            FrmItem frmItem = new FrmItem();
            frmItem.StartPosition = FormStartPosition.CenterScreen;
            frmItem.Show();
        }

        private void ToolStripMenuItemDeliveryNote_Click(object sender, EventArgs e)
        {
            FrmEditDeliveryNote frmDeliveryNote = new FrmEditDeliveryNote();
            frmDeliveryNote.Show();
        }

        private void ToolStripMenuItemViewDeliveryNote_Click(object sender, EventArgs e)
        {
            FrmDeliveryNoteList frmDeliveryNoteList = new FrmDeliveryNoteList();
            frmDeliveryNoteList.ShowDialog();

            if (frmDeliveryNoteList.Deleted)
                LoadOrderData();
        }

        private void ToolStripMenuItemAddRelationToItem_Click(object sender, EventArgs e)
        {
            FrmSelectItemSKU frmSelectSKU = new FrmSelectItemSKU();
            frmSelectSKU.ShowDialog();

            string itemSKU = frmSelectSKU.SKU;
            if (itemSKU == null || itemSKU == "")
                return;

            DataGridViewSelectedRowCollection selectedRows = this.dataGridViewAllOrders.SelectedRows;
            foreach (DataGridViewRow row in selectedRows)
            {
                String orderLineItemId = row.Cells[OrderDgv_OrderLineItemIndex].Value.ToString();
                EbayTransactionType trans = EbayTransactionDAL.GetOneTransaction(orderLineItemId);
                if (trans == null)
                    return;

                EbayTransactionDAL.UpdateTransactionItemSKU(trans.TransactionId, itemSKU);
                row.Cells[OrderDgv_ItemSKUIndex].Value = itemSKU;
            }


            MessageBox.Show("订单关联商品sku成功", "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}