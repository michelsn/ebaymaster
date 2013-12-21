//  -------------------------------------------------------------------------------
//  EbayMaster - An automatic tool for ebay seller.
//      Author: Zhi Wang
//      Email: wangzhi0417@126.com
//      Description: This is a free source software which you can use/modify the source freely.
//
// Version 1.0
//      1) Implemented delivery note/item stock in note. (2013/12/01)
//      2) Supplier management. (2013/12/04)
//      3) Sourcing note management. (2013/12/04)
//      4) Support upload tracking number. (2013/12/06)
//      5) Paged DataGridView. (2013/12/10)
//      6) Order containing multiple transactions support. (2013/12/13)
//      7) Messages. (2013/12/14)
//      8) Export to ebay cvs file format. (2013/12/14)
//      9) Splash window.
//      10) Reply user message (2013/12/18).
//      11) Item form enhancement. (2013/12/19)
//      12) Fetch token from ebay. (2013/12/21)

// Version 0.4 (2013/04/10)
//      1) Initial implementation, migrating functionalities from another ebay tool I created.
//      Features includes:
//          a. Order management.
//          b. Message management.
//
//
//  TODOs:
//      1) Print items pick note (sellerName, buyerName, country, itemSKU, itemTitle, SaleQuantity)
//      ...
// --------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Splasher;

using eBay.Service.Core.Sdk;
using Samples.Helper;
using eBay.Service.Core.Soap;
using eBay.Service.Call;
using eBay.Service.Util;

namespace EbayMaster
{
    public enum EbayMasterAcitveTab
    {
        ActiveListingTab = 0,
        OrderTab = 1,
        MessageTab = 2,
        PostSaleTab = 3,
    }

    public partial class FrmMain : Form
    {
        #region Members

        public EbayMasterAcitveTab CurrentActiveTab = EbayMasterAcitveTab.OrderTab;

        private FrmProgress frmProgress = null;
        private BackgroundWorker asyncWorder = null;
        private bool isSyncingData = false;

        private bool isShowingPendingOrders = true;

        // Cache table for the ebay transactions stored in the database(Access or SQL Server).
        public DataTable AllOrdersCacheTable;

        // Order related paging data.
        public const int OrderPageSize = 15;
        public int CurrentOrderPage = 1;
        public int OrderCount = 0;

        // Listing related paging data.
        public const int ListingPageSize = 15;
        public int CurrentListingPage = 1;
        public int ListingCount = 0;

        public DataTable AllListingCacheTable;

        // Cache table for the postsale transaction data.
        public DataTable AllPostSaleOrdersCacheTable;

        public const int PostSalePageSize = 15;
        public int CurrentPostSalePage = 1;
        public int PostSaleCount = 0;

        // Cache table for messages data.

        #endregion

        #region FrmMain contructor and load

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // Maximize the main window.
            this.WindowState = FormWindowState.Maximized;

            // Adjust the tab page size to accomodate to the screen.
            // [ZHI_TODO]
            Size frmSize = this.Size;

            // Set the default startDate/endDate.
            TimeSpan timeSpan = new TimeSpan(5, 0, 0, 0);
            DateTime prevThreeDay = DateTime.Now.Subtract(timeSpan);
            this.dateTimePickerStartTime.Value = prevThreeDay;

            // Initialize all account info.
            AccountUtil.ReloadAllAccounts();

            // Setup all the datagridview columns:
            //  - Active listing
            //  - Order
            //  - Message
            //  - Postsale
            SetupTabControlDataGridViewColumns();

            // Check whether the Access file is existed.
            if (DBConnectionUtil.CheckAccessFileExistence() == false)
            {
                MessageBox.Show("数据库文件路径没有配置\r\n系统设置->数据库设置", "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //
            // Load all the data.
            //  - Order data.
            //  - Active listing data.
            //  - Message data.
            //  - Postsale data.
            //  This is a bit slow, however simple.
            LoadData();
        }

        #endregion

        #region Tab control datagridview columns setup

        private void SetupTabControlDataGridViewColumns()
        {
            // Setup the order datagridview columns.
            SetupOrderDataGridViewColumns();

            // Setup the active listing datagridview columns.
            SetupActiveListingDataGridViewColumns();

            // Setup the message datagridview columns.
            SetupMessageDgvColumns();

            // Setup the postsale datagridview columns.
            SetupPostSaleDataGridViewColumns();
        }

        #endregion

        #region Load all the data (active listing/order/message)

        private void LoadOrderData()
        {
            bool isDelivered = this.radioButtonDeliveredOrders.Checked;
            OrderCount = EbayTransactionDAL.GetOrdersCount(isDelivered);
            int orderPageCnt = OrderCount / OrderPageSize + 1;

            if (CurrentOrderPage < 1)
                CurrentOrderPage = 1;
            else if (CurrentOrderPage > orderPageCnt)
                CurrentOrderPage = orderPageCnt;

            this.buttonOrderFirstPage.Enabled = true;
            this.buttonOrderLastPage.Enabled = true;
            this.buttonOrderPrevPage.Enabled = true;
            this.buttonOrderNextPage.Enabled = true;

            if (CurrentOrderPage == 1)
            {
                this.buttonOrderFirstPage.Enabled = false;
                this.buttonOrderPrevPage.Enabled = false;
            }
            
            if (CurrentOrderPage == orderPageCnt)
            {
                this.buttonOrderLastPage.Enabled = false;
                this.buttonOrderNextPage.Enabled = false;
            }

            this.labelOrderPage.Text = string.Format("{0} / {1}", CurrentOrderPage, orderPageCnt);

            AllOrdersCacheTable = EbayTransactionDAL.GetPagedOrders(CurrentOrderPage, OrderPageSize, isShowingPendingOrders);
            this.dataGridViewAllOrders.DataSource = AllOrdersCacheTable;
        }

        private void LoadActiveListingData()
        {
            ListingCount = EbayListingDAL.GetListingCount();
            int listingPageCnt = ListingCount / ListingPageSize + 1;

            if (CurrentListingPage < 1)
                CurrentListingPage = 1;
            else if (CurrentListingPage > listingPageCnt)
                CurrentListingPage = listingPageCnt;

            this.buttonListingFirstPage.Enabled = true;
            this.buttonListingLastPage.Enabled = true;
            this.buttonListingPrevPage.Enabled = true;
            this.buttonListingNextPage.Enabled = true;

            if (CurrentListingPage == 1)
            {
                this.buttonListingFirstPage.Enabled = false;
                this.buttonListingPrevPage.Enabled = false;
            }

            if (CurrentListingPage == listingPageCnt)
            {
                this.buttonListingLastPage.Enabled = false;
                this.buttonListingNextPage.Enabled = false;
            }

            this.labelListingPage.Text = string.Format("{0} / {1}", CurrentListingPage, listingPageCnt);

            AllListingCacheTable = EbayListingDAL.GetPagedActiveListings(CurrentListingPage, ListingPageSize);
            this.dataGridViewActiveListing.DataSource = AllListingCacheTable;

            LoadListingImageData();
        }

        private void LoadPostSaleData()
        {
            PostSaleCount = EbayTransactionDAL.GetPendingOrdersCount(0);
            int postSalePageCnt = PostSaleCount / PostSalePageSize + 1;

            if (CurrentPostSalePage < 1)
                CurrentPostSalePage = 1;
            else if (CurrentPostSalePage > postSalePageCnt)
                CurrentPostSalePage = postSalePageCnt;

            this.buttonPostSaleFirstPage.Enabled = true;
            this.buttonPostSaleLastPage.Enabled = true;
            this.buttonPostSalePrevPage.Enabled = true;
            this.buttonPostSaleNextPage.Enabled = true;

            if (CurrentPostSalePage == 1)
            {
                this.buttonPostSaleFirstPage.Enabled = false;
                this.buttonPostSalePrevPage.Enabled = false;
            }
            if (CurrentPostSalePage == postSalePageCnt)
            {
                this.buttonPostSaleLastPage.Enabled = false;
                this.buttonPostSaleNextPage.Enabled = false;
            }

            this.labelPostSalePage.Text = string.Format("{0} / {1}", CurrentPostSalePage, postSalePageCnt);

            AllPostSaleOrdersCacheTable = EbayTransactionDAL.GetPagedPendingOrders(CurrentPostSalePage, PostSalePageSize, 0);
            this.dataGridViewPostSale.DataSource = AllPostSaleOrdersCacheTable;
        }

        private void LoadData()
        {
            if (CurrentActiveTab == EbayMasterAcitveTab.OrderTab)
            {
                LoadOrderData();
            }
            else if (CurrentActiveTab == EbayMasterAcitveTab.ActiveListingTab)
            {
                //LoadListingData();
                LoadActiveListingData();
            }
            else if (CurrentActiveTab == EbayMasterAcitveTab.PostSaleTab)
            {
                LoadPostSaleData();
            }
        }

        #endregion

        #region Sync data from ebay

        private void buttonSyncEbayData_Click(object sender, EventArgs e)
        {
            if (CurrentActiveTab == EbayMasterAcitveTab.OrderTab)
            {
                SyncOrderData();
            }
            else if (CurrentActiveTab == EbayMasterAcitveTab.ActiveListingTab)
            {
                SyncEbayData_ActiveListingTab(sender, e);

                LoadActiveListingData();
            }
        }

        private void SyncOrderData()
        {
            if (isSyncingData)
                return;

            this.buttonSyncEbayData.Enabled = false;
            isSyncingData = true;

            DateTime startDate = this.dateTimePickerStartTime.Value;
            DateTime endDate = this.dateTimePickerEndTime.Value;
            startDate = startDate.ToUniversalTime();
            endDate = endDate.ToUniversalTime();

            asyncWorder = new BackgroundWorker();
            asyncWorder.DoWork += new DoWorkEventHandler(GetDataAsync);
            asyncWorder.RunWorkerCompleted += new RunWorkerCompletedEventHandler(GetDataCompleted);

            frmProgress = new FrmProgress();

            asyncWorder.RunWorkerAsync(new object[] { startDate, endDate });

            frmProgress.StartPosition = FormStartPosition.CenterScreen;
            frmProgress.ShowDialog(this);

            frmProgress = null;
        }

        // Get the orders/messages data from ebay asynchronously.
        private void GetDataAsync(object sender, DoWorkEventArgs e)
        {
            object[] paramArr = e.Argument as object[];
            DateTime startDate = (DateTime)paramArr[0];
            DateTime endDate = (DateTime)paramArr[1];

            bool canceled = false;

            int ordersFetched = GetOrdersAsync(startDate, endDate, out canceled);
            if (canceled)
            {
                e.Cancel = true;
                e.Result = new object[] { ordersFetched, 0 };
                return;
            }

            canceled = false;
            int messagesFetched = GetMessagesAsync(startDate, endDate, out canceled);
            if (canceled)
            {
                e.Cancel = true;
                e.Result = new object[] { ordersFetched, messagesFetched };
                return;
            }

            e.Result = new object[] { ordersFetched, messagesFetched };
            return;
        }

        // Get the orders asynchronously.
        //  Returns number of orders updated.
        private int GetOrdersAsync(DateTime startDate, DateTime endDate, out bool canceled)
        {
            canceled = false;
            int ordersFetched = 0;

            List<AccountType> allAccounts = AccountUtil.GetAllAccounts();
            foreach (AccountType account in allAccounts)
            {
                StringCollection orderIds = EbayTransactionBiz.GetAllOrderIds(account, startDate, endDate);
                StringCollection newOrderIds = new StringCollection();
                foreach (String orderId in orderIds)
                {
                    newOrderIds.Add(orderId);
                }

                int orderNumRetrievePerTime = 1;

                int apiCallTimes = newOrderIds.Count / orderNumRetrievePerTime;
                bool needExtraCall = (newOrderIds.Count % orderNumRetrievePerTime) != 0;
                if (needExtraCall)
                    apiCallTimes += 1;

                List<EbayTransactionType> allTrans = new List<EbayTransactionType>();

                for (int ii = 0; ii < apiCallTimes; ++ii)
                {
                    StringCollection orderIdsLoc = new StringCollection();
                    for (int jj = ii * orderNumRetrievePerTime; jj < ii * orderNumRetrievePerTime + orderNumRetrievePerTime; ++jj)
                    {
                        if (jj > newOrderIds.Count - 1)
                            break;
                        orderIdsLoc.Add(newOrderIds[jj]);
                    }

                    TimeFilter timeFilter = new TimeFilter();
                    timeFilter.TimeFrom = startDate;
                    timeFilter.TimeTo = endDate;

                    List<EbayTransactionType> transList = EbayTransactionBiz.GetAllOrders(account, timeFilter, orderIdsLoc);
                    foreach (EbayTransactionType trans in transList)
                    {
                        allTrans.Add(trans);
                    }

                    // Update the description and progress on the modal form
                    // using Control.Invoke.  Invoke will run the anonymous
                    // function to set the label's text on the UI thread.  
                    // Since it's illegal to touch the UI control on the worker 
                    // thread that we're on right now.

                    int retrievedCount = (ii + 1) * orderNumRetrievePerTime;
                    if (retrievedCount > newOrderIds.Count)
                        retrievedCount = newOrderIds.Count;

                    String  labelHintStr = string.Format("正在从Ebay获取交易信息, 账号 {0}, 共{1}个订单, 已获得{2}个订单信息", 
                                account.ebayAccount, 
                                newOrderIds.Count, 
                                retrievedCount);
                    int     progressBarValue = (int)((double)(ii + 1) / apiCallTimes * 100);
                    frmProgress.SetLabelHintAndProgressBarValue(labelHintStr, progressBarValue);

                    foreach (EbayTransactionType trans in allTrans)
                    {
                        bool result = EbayTransactionDAL.InsertOrUpdateOneTransaction(trans);
                        if (result == false)
                        {
                            canceled = true;
                            return ordersFetched;
                        }
                    }

                    // Periodically check for a Cancellation
                    // If the user clicks the cancel button, or tries to close
                    // the progress form the m_fmProgress.Cancel flag will be set to true.
                    if (frmProgress.Cancel)
                    {
                        canceled = true;
                        return ordersFetched;
                    }

                }   // for (int ii = 0; ii < apiCallTimes; ++ii)

                ordersFetched += newOrderIds.Count;
            }   // foreach (AccountType account in AllAccounts)

            canceled = false;
            return ordersFetched;
        }   // GetOrdersAsync

        // Get messages from ebay async.
        //  Returns number of new messages retrieved.
        private int GetMessagesAsync(DateTime startDate, DateTime endDate, out bool canceled)
        {
            canceled = false;
            int messagesFetched = 0;

            List<AccountType> allAccounts = AccountUtil.GetAllAccounts();
            foreach (AccountType account in allAccounts)
            {
                StringCollection messageIds = EbayMessageBiz.GetAllMessageIds(account, startDate, endDate);
                StringCollection newMessageIds = new StringCollection();
                foreach (String messageId in messageIds)
                {
                    EbayMessageType messageType = EbayMessageDAL.GetOneMessage(messageId);
                    if (messageType == null)
                        newMessageIds.Add(messageId);
                }

                int messageNumRetrievePerTime = 1;

                int apiCallTimes = newMessageIds.Count / messageNumRetrievePerTime;
                bool needExtraCall = (newMessageIds.Count % messageNumRetrievePerTime) != 0;
                if (needExtraCall)
                    apiCallTimes += 1;

                List<EbayMessageType> allTrans = new List<EbayMessageType>();

                for (int ii = 0; ii < apiCallTimes; ++ii)
                {
                    StringCollection messageIdsLoc = new StringCollection();
                    for (int jj = ii * messageNumRetrievePerTime; jj < ii * messageNumRetrievePerTime + messageNumRetrievePerTime; ++jj)
                    {
                        if (jj > newMessageIds.Count - 1)
                            break;
                        messageIdsLoc.Add(newMessageIds[jj]);
                    }

                    TimeFilter timeFilter = new TimeFilter();
                    timeFilter.TimeFrom = startDate;
                    timeFilter.TimeTo = endDate;

                    // Update the description and progress on the modal form
                    // using Control.Invoke.  Invoke will run the anonymous
                    // function to set the label's text on the UI thread.  
                    // Since it's illegal to touch the UI control on the worker 
                    // thread that we're on right now.

                    int retrievedCount = (ii + 1) * messageNumRetrievePerTime;
                    if (retrievedCount > newMessageIds.Count)
                        retrievedCount = newMessageIds.Count;

                    List<EbayMessageType>   messageList = EbayMessageBiz.GetAllMessageByIds(account, messageIdsLoc);

                    foreach (EbayMessageType messageType in messageList)
                    {
                        // We are pretty sure the message didn't exist.
                        EbayMessageDAL.InsertOneMessage(messageType);

                        // Update the transaction message status.
                        String recipientUserId = messageType.RecipientUserId;
                        String sender = messageType.Sender;
                        String sellerName = messageType.SellerName;
                        String buyerId = sellerName == sender ? recipientUserId : sender;

                        TransactionMessageStatus messageStatus = EbayMessageDAL.GetTransactionMessageStatus(buyerId, sellerName, messageType.ItemID);

                        List<EbayTransactionType> transList = EbayTransactionDAL.GetTransactionsBySellerBuyerItem(sellerName, buyerId, messageType.ItemID);
                        foreach (EbayTransactionType trans in transList)
                        {
                            EbayTransactionDAL.UpdateTransactionMessageStatus(trans.TransactionId, messageStatus);
                        }
                    }

                    String labelHintStr = string.Format("正在从Ebay获取消息信息, 账号 {0}, 共{1}个新消息, 已获得{2}个消息",
                                account.ebayAccount,
                                newMessageIds.Count,
                                retrievedCount);
                    int progressBarValue = (int)((double)(ii + 1) / apiCallTimes * 100);
                    frmProgress.SetLabelHintAndProgressBarValue(labelHintStr, progressBarValue);

                    // Periodically check for a Cancellation
                    // If the user clicks the cancel button, or tries to close
                    // the progress form the m_fmProgress.Cancel flag will be set to true.
                    if (frmProgress.Cancel)
                    {
                        canceled = true;
                        return messagesFetched;
                    }

                }   // for (int ii = 0; ii < apiCallTimes; ++ii)

                messagesFetched += newMessageIds.Count;
            }   // foreach (AccountType account in AllAccounts)

            canceled = false;
            return messagesFetched;
        }   // GetMessagesAsync

        private void GetDataCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // The background process is complete. First we should hide the
            // modal Progress Form to unlock the UI. The we need to inspect our
            // response to see if an error occurred, a cancel was requested or
            // if we completed successfully.

            if (frmProgress != null)
            {
                frmProgress.Hide();
                frmProgress = null;
            }

            // Check to see if an error occurred in the 
            // background process.
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                return;
            }

            object[] fetchedCnts = (object[])e.Result;

            // Check to see if the background process was canceled.
            if (e.Cancelled)
            {
                MessageBox.Show(String.Format("操作取消！共更新 {0} 个订单，{1} 个消息", (int)fetchedCnts[0], (int)fetchedCnts[1]),
                                "成功",
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Information);
                this.buttonSyncEbayData.Enabled = true;
                asyncWorder = null;
                return;
            }

            // Everything completed normally.
            // process the response using e.Result
            MessageBox.Show(String.Format("恭喜！下载ebay订单完成，共更新 {0} 个订单, 下载 {1} 个新消息 .", (int)fetchedCnts[0], (int)fetchedCnts[1]),
                "成功",
                MessageBoxButtons.OK, 
                MessageBoxIcon.Information);

            this.buttonSyncEbayData.Enabled = true;
            asyncWorder = null;

            LoadData();

            isSyncingData = false;
        }

        #endregion

        private void ToolStripMenuItemCreateSupplier_Click(object sender, EventArgs e)
        {
            FrmEditSupplier frmEditSupplier = new FrmEditSupplier();
            frmEditSupplier.Show();
        }

        private void ToolStripMenuItemViewSupplier_Click(object sender, EventArgs e)
        {
            FrmSupplierList frm = new FrmSupplierList(SupplierDlgMode.ShowSupplier);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void ToolStripMenuItemCreateSourcingNote_Click(object sender, EventArgs e)
        {
            FrmEditSourcingNote frm = new FrmEditSourcingNote();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        // Upload tracking number to ebay.
        // While uploading tracking number, ebay requires two fields: 
        //  - tracking number
        //  - carrier
        // Use Ebay API CompleteSale to fulfill this task, see page:
        // https://developer.ebay.com/DevZone/XML/docs/Reference/ebay/CompleteSale.html
        private void ToolStripMenuItemUploadTrackingNum_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = this.dataGridViewAllOrders.SelectedRows;
            if (selectedRows.Count != 1)
                return;

            String orderLineItemId = selectedRows[0].Cells[OrderDgv_OrderLineItemIndex].Value.ToString();
            EbayTransactionType trans = EbayTransactionDAL.GetOneTransaction(orderLineItemId);
            if (trans == null)
                return;

            if (trans.ShippingTrackingNo != "")
            {
                MessageBox.Show("该订单已经上传跟踪号", "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FrmUploadTrackingNumber frmUploadTrackingNumber = new FrmUploadTrackingNumber();
            frmUploadTrackingNumber.account = AccountUtil.GetAccount(trans.SellerName);
            frmUploadTrackingNumber.ebayTrans = trans;
            frmUploadTrackingNumber.Show();

            trans =  EbayTransactionDAL.GetOneTransaction(orderLineItemId);
            selectedRows[0].Cells[OrderDgv_TrackingNoIndex].Value = trans.ShippingTrackingNo;
        }

        private void ToolStripMenuItemLeaveFeedback_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = this.dataGridViewAllOrders.SelectedRows;
            foreach (DataGridViewRow row in selectedRows)
            {
                String orderLineItemId = row.Cells[OrderDgv_OrderLineItemIndex].Value.ToString();
                EbayTransactionType trans = EbayTransactionDAL.GetOneTransaction(orderLineItemId);
                if (trans == null)
                    continue;

                AccountType account = AccountUtil.GetAccount(trans.SellerName);
                if (account == null)
                    continue;

                EbayTransactionBiz.LeaveFeedback(account, trans.OrderId, trans.BuyerId, trans.ItemId, trans.EbayTransactionId);

                EbayTransactionDAL.UpdateTransactionSellerLeftFeedback(trans.TransactionId, true);

                row.Cells[OrderDgv_SellerLeftFeedbackIndex].Value = 1;
            }

            MessageBox.Show("留好评成功", "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ToolStripMenuItemViewItemStat_Click(object sender, EventArgs e)
        {
            FrmItemStat frmItemStat = new FrmItemStat();
            frmItemStat.Show();
        }

        private void ToolStripMenuItemMergeOrders_Click(object sender, EventArgs e)
        {
            List<String> tranIdList = new List<String>();

            DataGridViewSelectedRowCollection selectedRows = this.dataGridViewAllOrders.SelectedRows;
            foreach (DataGridViewRow row in selectedRows)
            {
                String transId = row.Cells[OrderDgv_TransactionIdIndex].Value.ToString();
                tranIdList.Add(transId);
            }

            EbayTransactionBiz.MergeOrders(tranIdList);
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            this.Activate();
            //close the splash screen
            SplashForm.CloseSplash();

            Application.UseWaitCursor = false;
        }

        private void ToolStripMenuItemViewSourcingNote_Click(object sender, EventArgs e)
        {
            FrmSourcingNoteList frm = new FrmSourcingNoteList();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }
    }
}
