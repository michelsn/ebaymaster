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
    public partial class FrmReplyBuyerMessage : Form
    {
        private AccountType mAccount = null;
        private EbayMessageType mMessage = null;
        private EbayTransactionType mTransaction = null;

        public FrmReplyBuyerMessage(AccountType account, EbayTransactionType trans, EbayMessageType message)
        {
            InitializeComponent();

            mTransaction = trans;
            mAccount = account;
            mMessage = message;
        }

        private void LoadAllMessageTemplates()
        {
            this.tvMessageTemplates.Nodes.Clear();

            DataTable dtMessageCategories = MessageTemplateCategoryDAL.GetAllMessageTemplateCategories();
            DataTable dtMessageTemplates = MessageTemplateDAL.GetAllMessageTemplates();

            foreach (DataRow drCategory in dtMessageCategories.Rows)
            {
                int categoryId = StringUtil.GetSafeInt(drCategory["CategoryId"]);
                String categoryName = StringUtil.GetSafeString(drCategory["CategoryName"]);
                MessageTemplateCategoryType catType = new MessageTemplateCategoryType();

                TreeNode catNode = new TreeNode();
                catNode.Tag = catType;
                catNode.Text = categoryName;
                catNode.Name = categoryId.ToString();

                this.tvMessageTemplates.Nodes.Add(catNode);
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

                TreeNode[] catNodes = this.tvMessageTemplates.Nodes.Find(categoryId.ToString(), false);
                if (catNodes.Length > 0)
                    catNodes[0].Nodes.Add(msgNode);
            }

            this.tvMessageTemplates.ExpandAll();
        }

        private void FrmReplyBuyerMessage_Load(object sender, EventArgs e)
        {
            // Load all message templates.
            LoadAllMessageTemplates();
        }

        private String replaceMessageMacros(String template, EbayTransactionType trans)
        {
            if (trans == null || template == "")
                return null;

            String content = template;
            content = content.Replace("{TrackingNo}", trans.ShippingTrackingNo);

            return content;
        }

        private void tvMessageTemplates_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = this.tvMessageTemplates.SelectedNode;
            if (node == null)
                return;

            if (node.Tag == null)
                return;

            if (node.Tag.GetType() != typeof(MessageTemplateType))
                return;

            MessageTemplateType messageTemplate = (MessageTemplateType)node.Tag;
            if (messageTemplate == null)
                return;

            String replacedMsg = replaceMessageMacros(messageTemplate.TemplateContent, mTransaction);
            this.rtbMessageToSend.Text = replacedMsg;
        }

        private void buttonSendMessage_Click(object sender, EventArgs e)
        {
            String msg = this.rtbMessageToSend.Text.Trim();
            if (msg == "")
            {
                MessageBox.Show("无效的消息正文", "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (mAccount == null || mTransaction == null || mMessage == null)
                return;

            this.buttonSendMessage.Enabled = false;

            String subject = String.Format("{0} sent a message on item {1} with itemId {2}",
                mTransaction.SellerName,
                mTransaction.ItemTitle,
                mTransaction.ItemId);

            bool result = EbayMessageBiz.ReplyBuyerMessage(mAccount,
                    mTransaction.ItemId,
                    mMessage.ExternalMessageId,
                    mTransaction.BuyerId,
                    msg);

            if (result)
            {
                DateTime startTime = DateTime.Now;
                DateTime endTime = startTime;
                startTime = startTime.Subtract(new TimeSpan(0, 5, 0));

                startTime = startTime.ToUniversalTime();
                endTime = endTime.ToUniversalTime();

                StringCollection msgIds = EbayMessageBiz.GetAllMessageIds(mAccount, startTime, endTime);
                EbayMessageBiz.GetAllMessageByIds(mAccount, msgIds);

                //SentReply = true;

                EbayMessageDAL.MarkMessageAsReplied(mMessage.EbayMessageId);

                MessageBox.Show("回复消息成功\r\n", "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("发送消息错误，请查看日志", "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.buttonSendMessage.Enabled = true;
            Close();
        }

    }
}
