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
        public void SetupMessageDgvColumns()
        {
            this.pagedDgvMessages.initDgvColumns = SetupMessageDgvColumnsImpl;
            this.pagedDgvMessages.getPagedData = EbayMessageDAL.GetPagedMessageTable;
            this.pagedDgvMessages.getRecordCount = EbayMessageDAL.GetMessageCount;
            this.pagedDgvMessages.onDgvDataBindCompleted = OnDgvDataBindCompleted;

            WebBrowser browser = new WebBrowser();
            this.tabPageAllMessage.Controls.Add(browser);
            browser.Navigate("about:blank");
            browser.Dock = DockStyle.Fill;

            this.pagedDgvMessages.DgvData.CellContentClick += new DataGridViewCellEventHandler(MessageDgvData_CellContentClick);

        }

         private void OnDgvDataBindCompleted()
        {
            if (this.pagedDgvMessages.DgvData.Rows.Count == 0)
                return;

            String ebayMessageId = this.pagedDgvMessages.DgvData.Rows[0].Cells[MessageId_ColumnIndex].Value.ToString();
            EbayMessageType msg = EbayMessageDAL.GetOneMessage(ebayMessageId);
            if (msg == null)
                return;

            WebBrowser browser = (WebBrowser)this.tabPageAllMessage.Controls[0];
            if (browser == null)
                return;
            browser.DocumentText = msg.Text;
        }

        void MessageDgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int colIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;

            if (rowIndex < 0 || colIndex < 0)
                return;

            String ebayMessageId = this.pagedDgvMessages.DgvData.Rows[rowIndex].Cells[MessageId_ColumnIndex].Value.ToString();
            if (null == ebayMessageId)
                return;

            // Display msg in web browser.
            EbayMessageType msg = EbayMessageDAL.GetOneMessage(ebayMessageId);
            if (msg == null)
                return;

            WebBrowser browser = (WebBrowser)this.tabPageAllMessage.Controls[0];
            if (browser == null)
                return;

            browser.DocumentText = msg.Text;
        }

        private readonly int MessageId_ColumnIndex = 0;

        private void SetupMessageDgvColumnsImpl()
        {
            this.pagedDgvMessages.DgvData.AutoGenerateColumns = false;

            // EbayMessageId
            this.pagedDgvMessages.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("EbayMessageId", @"消息Id", typeof(string), 100, false));

            // Sender
            this.pagedDgvMessages.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("Sender", @"发信人", typeof(string), 70, true));

            // RecipientUserIdCol
            this.pagedDgvMessages.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("RecipientUserId", @"收信人", typeof(string), 70, true));

            // IsRead
            // IsReplied

            // ReceiveDate
            this.pagedDgvMessages.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("ReceiveDate", @"时间", typeof(string), 80, true));

            // Subject
            this.pagedDgvMessages.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("Subject", @"标题", typeof(string), 450, true));

            this.pagedDgvMessages.DgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.pagedDgvMessages.DgvData.MultiSelect = true;
            this.pagedDgvMessages.DgvData.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dgv_RowPrePaint);
            this.pagedDgvMessages.DgvData.ScrollBars = ScrollBars.Both;
        }
    }
}
