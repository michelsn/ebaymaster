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
    //
    // This form is used to display the message threads between buyer and seller
    // on a specific transaction.
    //
    public partial class FrmUserMessage : Form
    {
        // The transaction.
        public EbayTransactionType EbayTransaction = null;

        // The ebay account info.
        public AccountType Account = null;

        // Indicator whether user has sent message, if so caller will update status.
        public bool SentMessage = false;

        public bool MarkMsgAsReplied = false;

        public FrmUserMessage()
        {
            InitializeComponent();
        }

        private readonly int MessageId_ColumnIndex = 0;
        private readonly int ReceiveDate_ColumnIndex = 3;

        private void SetupTransactionMessageSubjectDataGridViewColumns()
        {
            this.dataGridViewTransactionMessageSubject.AutoGenerateColumns = false;

            // EbayMessageId
            this.dataGridViewTransactionMessageSubject.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("EbayMessageId", @"消息Id", typeof(string), 100, false));

            // Sender
            this.dataGridViewTransactionMessageSubject.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("Sender", @"发信人", typeof(string), 80, true));

            // RecipientUserIdCol
            this.dataGridViewTransactionMessageSubject.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("RecipientUserId", @"收信人", typeof(string), 80, true));

            // IsRead
            // IsReplied

            // ReceiveDate
            this.dataGridViewTransactionMessageSubject.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("ReceiveDate", @"时间", typeof(string), 120, true));

            // Subject
            this.dataGridViewTransactionMessageSubject.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("Subject", @"标题", typeof(string), 450, true));

            this.dataGridViewTransactionMessageSubject.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTransactionMessageSubject.MultiSelect = true;
            this.dataGridViewTransactionMessageSubject.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dgv_RowPrePaint);
            this.dataGridViewTransactionMessageSubject.ScrollBars = ScrollBars.Both;
        }

        private void dgv_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
        }

        private void LoadMsgData()
        {
            DataTable dtMsg = EbayMessageDAL.GetAllTransactionMessagesCompact(EbayTransaction.BuyerId, EbayTransaction.SellerName, EbayTransaction.ItemId);
            this.dataGridViewTransactionMessageSubject.DataSource = dtMsg;

            WebBrowser browser = new WebBrowser();
            this.tabPageAllMessage.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;

            if (dtMsg != null && dtMsg.Rows.Count > 0)
            {
                String ebayMessageId = dtMsg.Rows[0]["EbayMessageId"].ToString();
                if (ebayMessageId == null)
                    return;

                // Display msg in web browser.
                EbayMessageType msg = EbayMessageDAL.GetOneMessage(ebayMessageId);
                if (msg == null)
                    return;

                browser.DocumentText = msg.Text;
            }
        }

        private void FrmUserMessage_Load(object sender, EventArgs e)
        {
            if (EbayTransaction == null)
                return;

            SetupTransactionMessageSubjectDataGridViewColumns();

            LoadMsgData();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
                this.Close();
            return base.ProcessCmdKey(ref msg, keyData);
        } 

        private void dataGridViewTransactionMessageSubject_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int colIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;

            if (rowIndex < 0 || colIndex < 0)
                return;

            String ebayMessageId = this.dataGridViewTransactionMessageSubject.Rows[rowIndex].Cells[MessageId_ColumnIndex].Value.ToString();
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

        private void dataGridViewTransactionMessageSubject_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewTransactionMessageSubject_CellContentClick(sender, e);
        }

        private void buttonAskBuyer_Click(object sender, EventArgs e)
        {
            DataTable dtTrans = EbayTransactionDAL.GetOneTransactionTable(EbayTransaction.TransactionId);
            if (dtTrans == null || dtTrans.Rows.Count == 0)
                return;

            DataTable orderTable = dtTrans.Clone();
            DataRow newRow = orderTable.NewRow();
            newRow.ItemArray = dtTrans.Rows[0].ItemArray;
            orderTable.Rows.Add(newRow);

            FrmSendMessage frm = new FrmSendMessage();
            frm.OrdersDataTable = orderTable;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            if (frm.MessageSent)
            {
                LoadMsgData();
            }
        }

        private void dataGridViewTransactionMessageSubject_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
                return;

            for (int rowIdx = 0; rowIdx < this.dataGridViewTransactionMessageSubject.Rows.Count; rowIdx++)
            {
                DataGridViewCell ebayMsgIdCell = this.dataGridViewTransactionMessageSubject.Rows[rowIdx].Cells[MessageId_ColumnIndex];
                if (ebayMsgIdCell.Value == null)
                    continue;

                String ebayMessageId = ebayMsgIdCell.Value.ToString();

                EbayMessageType msg = EbayMessageDAL.GetOneMessage(ebayMessageId);
                if (msg == null)
                    continue;

                if (msg.Sender == msg.SellerName)
                    this.dataGridViewTransactionMessageSubject.Rows[rowIdx].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#90EE90");
                else
                {
                    if (!msg.IsReplied)
                        this.dataGridViewTransactionMessageSubject.Rows[rowIdx].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FFFF00");
                    else
                        this.dataGridViewTransactionMessageSubject.Rows[rowIdx].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#90EE90");
                }

                DataGridViewCell receiveDateCell = this.dataGridViewTransactionMessageSubject.Rows[rowIdx].Cells[ReceiveDate_ColumnIndex];
                if (receiveDateCell != null)
                {
                    //DateTime receiveDate = StringUtil.GetSafeDateTime(receiveDateCell.Value);
                    //receiveDate = receiveDate.ToLocalTime();
                    //receiveDateCell.Value = receiveDate;
                }
            }
        }

        private void dataGridViewTransactionMessageSubject_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = this.dataGridViewTransactionMessageSubject.SelectedRows;
            if (selectedRows.Count != 1)
                return;

            //String ebayMessageId = this.dataGridViewTransactionMessageSubject.Rows[rowIndex].Cells[MessageId_ColumnIndex].Value.ToString();
            String ebayMessageId = selectedRows[0].Cells[MessageId_ColumnIndex].Value.ToString();
            if (null == ebayMessageId)
                return;

            // Display msg in web browser.
            EbayMessageType msg = EbayMessageDAL.GetOneMessage(ebayMessageId);
            if (msg == null)
                return;

            if (msg.IsResponseEnabled && !msg.IsReplied)
                this.buttonReplyMessage.Enabled = true;
            else
                this.buttonReplyMessage.Enabled = false;
        }

        private void buttonReplyMessage_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = this.dataGridViewTransactionMessageSubject.SelectedRows;
            if (selectedRows.Count != 1)
                return;

            String ebayMessageId = selectedRows[0].Cells[MessageId_ColumnIndex].Value.ToString();
            if (null == ebayMessageId)
                return;

            // Display msg in web browser.
            EbayMessageType msg = EbayMessageDAL.GetOneMessage(ebayMessageId);
            if (msg == null)
                return;

            FrmReplyBuyerMessage frmReplyBuyer = new FrmReplyBuyerMessage(Account, EbayTransaction, msg);
            frmReplyBuyer.ShowDialog();

            //if (frmReplyBuyer.SentReply)
            //{
            //    FrmUserMessage_Load(sender, e);
            //}

        }

        private void ToolStripMenuItemMarkAsReplied_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确认将该消息标记为已回复么?",
                "确认标记?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            DataGridViewSelectedRowCollection selectedRows = this.dataGridViewTransactionMessageSubject.SelectedRows;
            if (selectedRows.Count != 1)
                return;

            String ebayMessageId = selectedRows[0].Cells[MessageId_ColumnIndex].Value.ToString();
            if (null == ebayMessageId)
                return;

            EbayMessageType msg = EbayMessageDAL.GetOneMessage(ebayMessageId);
            if (msg == null)
                return;

            EbayMessageDAL.MarkMessageAsReplied(msg.EbayMessageId);

            TransactionMessageStatus messageStatus 
                = EbayMessageDAL.GetTransactionMessageStatus(EbayTransaction.BuyerId, EbayTransaction.SellerName, EbayTransaction.ItemId);
            EbayTransactionDAL.UpdateTransactionMessageStatus(EbayTransaction.TransactionId, messageStatus);

            MessageBox.Show("标记消息为已回复成功。", "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadMsgData();

            MarkMsgAsReplied = true;

        }

        private void dataGridViewTransactionMessageSubject_MouseUp(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hitTestInfo;
            if (e.Button == MouseButtons.Right)
            {
                hitTestInfo = dataGridViewTransactionMessageSubject.HitTest(e.X, e.Y);
                this.contextMenuStripMsg.Show(dataGridViewTransactionMessageSubject, new Point(e.X, e.Y));
            }
        }

        private void contextMenuStripMsg_Opening(object sender, CancelEventArgs e)
        {
            bool enableMarkAsReplied = false;

            this.ToolStripMenuItemMarkAsReplied.Enabled = false;

            DataGridViewSelectedRowCollection selectedRows = this.dataGridViewTransactionMessageSubject.SelectedRows;
            if (selectedRows.Count != 1)
                return;

            String ebayMessageId = selectedRows[0].Cells[MessageId_ColumnIndex].Value.ToString();
            if (null == ebayMessageId)
                return;

            EbayMessageType msg = EbayMessageDAL.GetOneMessage(ebayMessageId);
            if (msg == null)
                return;

            if (msg.Sender != msg.SellerName && msg.IsReplied == false)
            {
                enableMarkAsReplied = true;
            }

            this.ToolStripMenuItemMarkAsReplied.Enabled = enableMarkAsReplied;
        }  
    } 
}
