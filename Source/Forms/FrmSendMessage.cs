using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using eBay.Service.Core.Soap;

namespace EbayMaster
{
    public partial class FrmSendMessage : Form
    {
        private FrmProgress frmProgress = null;

        private BackgroundWorker asyncWorder = null;

        private bool isSendingMessage = false;

        public DataTable OrdersDataTable = null;

        public bool MessageSent = false;

        public FrmSendMessage()
        {
            InitializeComponent();
        }

        private void FrmSendMessage_Load(object sender, EventArgs e)
        {
            LoadAllMessageTemplates();

            SetupBuyerDataGridViewColumns();

            if (OrdersDataTable == null)
                return;

            this.dataGridViewBuyers.DataSource = OrdersDataTable;
        }

        private void SetupBuyerDataGridViewColumns()
        {
            this.dataGridViewBuyers.AutoGenerateColumns = false;

            // OrderLineItemId
            this.dataGridViewBuyers.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("OrderLineItemId", @"orderLineItemIdID", typeof(string), 10, false));

            // SellerId
            this.dataGridViewBuyers.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("SellerName", @"卖家ID", typeof(string), 70, true));

            // BuyerId
            this.dataGridViewBuyers.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("BuyerId", @"买家ID", typeof(string), 80, true));

            // BuyerCountry
            this.dataGridViewBuyers.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("BuyerCountry", @"买家国家", typeof(string), 90, true));

            // ItemSKU
            this.dataGridViewBuyers.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("ItemSKU", @"SKU", typeof(string), 50, true));

            // ItemTitle
            this.dataGridViewBuyers.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("ItemTitle", @"物品名称", typeof(string), 350, true));

            this.dataGridViewBuyers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewBuyers.MultiSelect = true;
            this.dataGridViewBuyers.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dgv_RowPrePaint);
        }   // SetupBuyerDataGridViewColumns

        private void dgv_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
        }

        private void LoadAllMessageTemplates()
        {
            this.treeViewMessageTemplates.Nodes.Clear();

            DataTable dtMessageCategories = MessageTemplateCategoryDAL.GetAllMessageTemplateCategories();
            DataTable dtMessageTemplates = MessageTemplateDAL.GetAllMessageTemplates();

            foreach (DataRow drCategory in dtMessageCategories.Rows)
            {
                int categoryId = StringUtil.GetSafeInt(drCategory["CategoryId"]);
                String categoryName = StringUtil.GetSafeString(drCategory["CategoryName"]);
                MessageTemplateCategoryType catType = new MessageTemplateCategoryType();

                TreeNode    catNode = new TreeNode();
                catNode.Tag = catType;
                catNode.Text = categoryName;
                catNode.Name = categoryId.ToString();

                this.treeViewMessageTemplates.Nodes.Add(catNode);
            }

            foreach (DataRow drMessageTemplate in dtMessageTemplates.Rows)
            {
                int templateId = StringUtil.GetSafeInt(drMessageTemplate["TemplateId"]);
                int categoryId = StringUtil.GetSafeInt(drMessageTemplate["TemplateCategoryId"]);
                String templateName = StringUtil.GetSafeString(drMessageTemplate["TemplateName"]);
                String templateContent = StringUtil.GetSafeString(drMessageTemplate["TemplateContent"]);

                MessageTemplateType templateType = new MessageTemplateType();
                templateType.TemplateId = templateId;
                templateType.TemplateCategoryId = categoryId;
                templateType.TemplateName = templateName;
                templateType.TemplateContent = templateContent;

                TreeNode msgNode = new TreeNode();
                msgNode.Tag = templateType;
                msgNode.Text = templateName;

                TreeNode[] catNodes = this.treeViewMessageTemplates.Nodes.Find(categoryId.ToString(), false);
                if (catNodes.Length > 0)
                    catNodes[0].Nodes.Add(msgNode);
            }

            this.treeViewMessageTemplates.ExpandAll();
        }

        private void treeViewMessageTemplates_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = this.treeViewMessageTemplates.SelectedNode;
            if (node == null)
                return;

            if (node.Tag == null)
                return;

            if (node.Tag.GetType() != typeof(MessageTemplateType))
                return;

            MessageTemplateType messageTemplate = (MessageTemplateType)node.Tag;
            if (messageTemplate == null)
                return;

            this.richTextBoxMessageToSend.Text = messageTemplate.TemplateContent;
        }

        private void buttonManageMessageTemplate_Click(object sender, EventArgs e)
        {
            FrmMessageTemplate frmMessageTemplate = new FrmMessageTemplate();
            frmMessageTemplate.StartPosition = FormStartPosition.CenterScreen;

            frmMessageTemplate.ShowDialog(this);
            LoadAllMessageTemplates();
        }

        private void buttonSendMessage_Click(object sender, EventArgs e)
        {
            String messageToSend = this.richTextBoxMessageToSend.Text.Trim();

            if (messageToSend == "")
                return;

            if (isSendingMessage)
                return;

            this.buttonSendMessage.Enabled = false;
            isSendingMessage = true;

            asyncWorder= new BackgroundWorker();
            asyncWorder.DoWork += new DoWorkEventHandler(SendMessageAsync);
            asyncWorder.RunWorkerCompleted +=new RunWorkerCompletedEventHandler(SendMessageCompleted);

            frmProgress = new FrmProgress();

            asyncWorder.RunWorkerAsync(new object[] { OrdersDataTable, messageToSend });

            frmProgress.StartPosition = FormStartPosition.CenterScreen;
            frmProgress.ShowDialog(this);

            frmProgress = null;
        }   // buttonSendMessage_Click

        private String replaceMessageMacros(String template, EbayTransactionType trans)
        {
            if (trans == null || template == "")
                return null;

            String content = template;
            content = content.Replace("{TrackingNo}", trans.ShippingTrackingNo);

            return content;
        }

        private void SendMessageAsync(object sender, DoWorkEventArgs e)
        {
            object[] paramArr = e.Argument as object[];
            DataTable dtOrders = (DataTable)paramArr[0];
            String messageToSend = (String)paramArr[1];

            int     curIdx = 0;

            foreach (DataRow row in dtOrders.Rows)
            {
                curIdx ++;

                String sellerName = StringUtil.GetSafeString(row["SellerName"]);
                String buyerId = StringUtil.GetSafeString(row["BuyerId"]);
                String itemId = StringUtil.GetSafeString(row["ItemId"]);
                String orderLineItemId = StringUtil.GetSafeString(row["OrderLineItemId"]);

                EbayTransactionType transLoc = EbayTransactionDAL.GetOneTransaction(orderLineItemId);
                if (transLoc == null)
                    continue;

                String subject = String.Format("{0} sent a message on item {1} with itemId {2}",
                    sellerName,
                    buyerId,
                    itemId);

                AccountType account = AccountUtil.GetAccount(sellerName);
                if (account == null)
                    continue;

                String labelHintStr = String.Format("正在向买家 {0} 发送消息.... 进度 {1} / {2}",
                    buyerId, curIdx, dtOrders.Rows.Count);
                int progressBarValue = (int)((double)(curIdx) / dtOrders.Rows.Count * 100);
                frmProgress.SetLabelHintAndProgressBarValue(labelHintStr, progressBarValue);

                if (frmProgress.Cancel)
                {
                    e.Cancel = true;
                    e.Result = curIdx;
                    return;
                }

                messageToSend = replaceMessageMacros(messageToSend, transLoc);

                bool result = EbayMessageBiz.SendMessageToBuyer(account,
                    buyerId,
                    itemId,
                    subject,
                    messageToSend,
                    true/*emailCopyToSender*/,
                    eBay.Service.Core.Soap.QuestionTypeCodeType.General);

                if (result)
                {
                    DateTime startTime = DateTime.Now;
                    DateTime endTime = startTime;
                    startTime = startTime.Subtract(new TimeSpan(0, 5, 0));

                    startTime = startTime.ToUniversalTime();
                    endTime = endTime.ToUniversalTime();

                    // Get all message ids within this five minutes.
                    StringCollection msgIds = EbayMessageBiz.GetAllMessageIds(account, startTime, endTime);

                    // Skip the messages we have retrieved.
                    StringCollection newMsgIds = new StringCollection();
                    {
                        foreach (String msgId in msgIds)
                        {
                            EbayMessageType existedMessage = EbayMessageDAL.GetOneMessage(msgId);
                            if (existedMessage != null)
                                continue;

                            newMsgIds.Add(msgId);
                        }
                    }

                    List<EbayMessageType> newMessages = EbayMessageBiz.GetAllMessageByIds(account, newMsgIds);

                    Logger.WriteSystemLog(String.Format("Retrieved new messages count={0}", newMsgIds.Count));

                    foreach (EbayMessageType messageType in newMessages)
                    {
                        // We are pretty sure the message didn't exist.
                        EbayMessageDAL.InsertOneMessage(messageType);

                        // Update the transaction message status.
                        String recipientUserId = messageType.RecipientUserId;
                        String senderName = messageType.Sender;
                        String sellerNameLoc = messageType.SellerName;
                        String buyerIdLoc = sellerNameLoc == senderName ? recipientUserId : senderName;

                        // [ZHI_TODO]
                        TransactionMessageStatus messageStatus = EbayMessageDAL.GetTransactionMessageStatus(buyerIdLoc, sellerNameLoc, messageType.ItemID);

                        List<EbayTransactionType> transList = EbayTransactionDAL.GetTransactionsBySellerBuyerItem(sellerNameLoc, buyerIdLoc, messageType.ItemID);
                        foreach (EbayTransactionType trans in transList)
                        {
                            EbayTransactionDAL.UpdateTransactionMessageStatus(trans.TransactionId, messageStatus);
                        }
                    }
                }
                else
                {
                    Logger.WriteSystemUserLog("发送消息失败！");
                }
            }
            e.Result = OrdersDataTable.Rows.Count;
        } // SendMessageAsync

        private void SendMessageCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
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

            int sentMessageCnt = (int)e.Result;

            // Check to see if the background process was canceled.
            if (e.Cancelled)
            {
                MessageBox.Show(String.Format("操作取消！共发送 {0} 个消息", sentMessageCnt),
                                "成功",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                this.buttonSendMessage.Enabled = true;
                asyncWorder = null;
                return;
            }

            // Everything completed normally.
            // process the response using e.Result
            MessageBox.Show(String.Format("恭喜！发送消息成功，共发送 {0} 个消息 .", sentMessageCnt),
                "成功",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            this.buttonSendMessage.Enabled = true;
            asyncWorder = null;

            isSendingMessage = false;

            MessageSent = true;
        }   // SendMessageCompleted
    }
}
