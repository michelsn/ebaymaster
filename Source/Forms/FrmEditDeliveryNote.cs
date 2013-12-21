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
    // Form used to create/edit a delivery note.
    //
    public partial class FrmEditDeliveryNote : Form
    {
        // Three edit modes:
        //  CreateNew - create a new delivery note.
        //  EditExisting - edit an existed delivery note.
        //  CreateFromTrans - create a new delivery note for selected transactions.
        enum EditMode { Invalid, CreatNew, EditExisting, CreateFromTrans };
        private EditMode mEditMode = EditMode.Invalid;

        // Hold the transactions in the delivery note, either a new one or existing one.
        private DataTable mDtTransactions = null;

        // Only valid in the "EditExisting" mode.
        private DeliveryNoteType mDeliveryNote = null;

        public bool Modified = false;
        public bool Added = false;

        // Only valid in the "CreateFromTrans" mode.
        private List<String> mTranIdList = null;

        //
        // Constructor
        //
        public FrmEditDeliveryNote()
        {
            InitializeComponent();

            mEditMode = EditMode.CreatNew;
        }

        public FrmEditDeliveryNote(String noteId)
        {
            InitializeComponent();

            mEditMode = EditMode.EditExisting;
            mDeliveryNote = DeliveryNoteDAL.GetOneDeliveryNote(noteId);
        }

        public FrmEditDeliveryNote(List<String> tranIdList)
        {
            InitializeComponent();

            mEditMode = EditMode.CreateFromTrans;
            mTranIdList = tranIdList;
        }

        //
        // Init the datatable columns.
        //
        private void InitTransactionDataTable()
        {
            mDtTransactions = new DataTable();
            mDtTransactions.Columns.Add("TransactionId", typeof(String));
            mDtTransactions.Columns.Add("SellerName", typeof(String));
            mDtTransactions.Columns.Add("BuyerId", typeof(String));
            mDtTransactions.Columns.Add("BuyerCountry", typeof(String));
            mDtTransactions.Columns.Add("ItemSKU", typeof(String));
            mDtTransactions.Columns.Add("ItemTitle", typeof(String));
            mDtTransactions.Columns.Add("SaleQuantity", typeof(int));
        }

        private void BindData()
        {
            dataGridViewTransactions.DataSource = mDtTransactions;
        }

        //
        // Prerequsities: in EditExisting mode.
        // Load an existed delivery note.
        //
        private void LoadDeliveryNote()
        {
            if (mDeliveryNote == null)
                return;

            //this.textBoxTransactionId.Text = note.DeliveryOrderIds;
            this.textBoxFee.Text = mDeliveryNote.DeliveryFee.ToString();
            this.textBoxExtraFee.Text = mDeliveryNote.DeliveryExtraFee.ToString();
            this.textBoxComment.Text = mDeliveryNote.DeliveryComment.ToString();

            AddTransactionsToDataTable(mDeliveryNote.DeliveryOrderIds);
        }

        //
        // Prerequsities: in CreateFromTrans mode.
        // Create a new delivery note from selected transactions.
        //
        private void FillDeliveryNoteFromTrans()
        {
            if (mTranIdList == null || mTranIdList.Count == 0)
                return;

            String tranIdStr = "";
            bool firstRow = true;
            foreach (String tranId in mTranIdList)
            {
                if (firstRow)
                    firstRow = false;
                else
                    tranIdStr += ",";
                tranIdStr += tranId;
            }

            AddTransactionsToDataTable(tranIdStr);
        }

        //
        // Form loading...
        //
        private void FrmDeliveryNote_Load(object sender, EventArgs e)
        {
            SetupTransactionDataGridViewColumns();

            InitTransactionDataTable();

            // If we are modifying one existed delivery note, load its data.
            if (mEditMode == EditMode.EditExisting)
            {
                LoadDeliveryNote();
            }
            else if (mEditMode == EditMode.CreateFromTrans)
            {
                FillDeliveryNoteFromTrans();
            }

            BindData();
        }

        //
        // Setup the datagridview columns.
        //  I hate auto generating columns...
        //
        private void SetupTransactionDataGridViewColumns()
        {
            this.dataGridViewTransactions.AutoGenerateColumns = false;

            // OrderId
            this.dataGridViewTransactions.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("TransactionId", @"交易ID", typeof(string), 50, true));

            // SellerId
            this.dataGridViewTransactions.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("SellerName", @"卖家ID", typeof(string), 80, true));

            // BuyerId
            this.dataGridViewTransactions.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("BuyerId", @"买家ID", typeof(string), 90, true));

            // BuyerCountry
            this.dataGridViewTransactions.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("BuyerCountry", @"买家国家", typeof(string), 90, true));

            // ItemSKU
            this.dataGridViewTransactions.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("ItemSKU", @"SKU", typeof(string), 50, true));

            // ItemTitle
            this.dataGridViewTransactions.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("ItemTitle", @"物品名称", typeof(string), 350, true));

            this.dataGridViewTransactions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTransactions.MultiSelect = true;
            this.dataGridViewTransactions.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dgv_RowPrePaint);
        }

        private void dgv_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }

        private void ModifyDeliveryNote()
        {
            if (mEditMode != EditMode.EditExisting)
            {
                return;
            }

            // tranIds will be like "2264,2265,2266"
            String tranIds = "";
            bool firstRow = true;
            foreach (DataRow dr in mDtTransactions.Rows)
            {
                String tranIdLoc = StringUtil.GetSafeString(dr["TransactionId"]);
                if (tranIdLoc == null || tranIdLoc == "")
                    continue;

                if (!firstRow)
                    tranIds += ",";
                else
                    firstRow = false;

                tranIds += tranIdLoc;
            }

            double fee = 0.0;
            double extraFee = 0.0;
            Double.TryParse(textBoxFee.Text, out fee);
            Double.TryParse(textBoxExtraFee.Text, out extraFee);

            mDeliveryNote.DeliveryOrderIds = tranIds;
            mDeliveryNote.DeliveryFee = fee;
            mDeliveryNote.DeliveryExtraFee = extraFee;
            mDeliveryNote.DeliveryComment = textBoxComment.Text;

            bool result = DeliveryNoteDAL.ModifyOneDeliveryNote(mDeliveryNote);

            if (result)
            {
                Modified = true;
                MessageBox.Show("修改发货单成功", "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("修改发货单失败", "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateDeliveryNote()
        {
            if (mEditMode != EditMode.CreatNew && mEditMode != EditMode.CreateFromTrans)
            {
                // We are not creating a new delivery note.
                return;
            }

            // Sanity check.
            if (mDtTransactions == null || mDtTransactions.Rows.Count == 0)
                return;

            //
            // Add a new delivery note.
            //

            // tranIds will be like "2264,2265,2266"
            String tranIds = "";
            bool firstRow = true;
            foreach (DataRow dr in mDtTransactions.Rows)
            {
                String tranIdLoc = StringUtil.GetSafeString(dr["TransactionId"]);
                if (tranIdLoc == null || tranIdLoc == "")
                    continue;

                if (!firstRow)
                    tranIds += ",";
                else
                    firstRow = false;

                tranIds += tranIdLoc;
            }

            double fee = 0.0;
            double extraFee = 0.0;
            Double.TryParse(textBoxFee.Text, out fee);
            Double.TryParse(textBoxExtraFee.Text, out extraFee);

            DeliveryNoteType deliveryNote = new DeliveryNoteType();
            deliveryNote.DeliveryDate = DateTime.Now;
            deliveryNote.DeliveryOrderIds = tranIds;
            deliveryNote.DeliveryUser = "";
            deliveryNote.DeliveryFee = fee;
            deliveryNote.DeliveryExtraFee = extraFee;
            deliveryNote.DeliveryComment = textBoxComment.Text;

            // Decrease the stock.
            // Two runs: 
            //  first run check validity.
            //  second run do the actual stock decreament.
            // This is to ensure the data integrity.
            Dictionary<String, int> itemSkuToTotalDecreased = new Dictionary<string, int>();

            String stockChangePrompt = "";
            for (int ii = 0; ii < 2; ++ ii)
            {
                foreach (DataRow dr in mDtTransactions.Rows)
                {
                    String tranId = StringUtil.GetSafeString(dr["TransactionId"]);
                    String itemSku = StringUtil.GetSafeString(dr["ItemSKU"]);
                    int saleQuantity = StringUtil.GetSafeInt(dr["SaleQuantity"]);

                    if (0 == ii)
                    {
                        if (itemSku == "")
                        {
                            MessageBox.Show(String.Format("订单{0}没有关联商品", tranId),
                                "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;                          
                        }

                        InventoryItemType item = ItemDAL.GetItemBySKU(itemSku);
                        if (item == null)
                        {
                            MessageBox.Show(String.Format("商品不存在, sku={0}", itemSku),
                                 "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (!itemSkuToTotalDecreased.ContainsKey(itemSku))
                            itemSkuToTotalDecreased.Add(itemSku, saleQuantity);
                        else
                            itemSkuToTotalDecreased[itemSku] += saleQuantity;

                        if (item.ItemStockNum < itemSkuToTotalDecreased[itemSku])
                        {
                            MessageBox.Show(String.Format("商品{0}库存不足，实际库存{1} < 售出数{2}",
                                itemSku, item.ItemStockNum, itemSkuToTotalDecreased[itemSku]),
                                "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    if (1 == ii)
                    {
                        InventoryItemType item = ItemDAL.GetItemBySKU(itemSku);
                        int origStock = item.ItemStockNum;

                        if (!ItemDAL.DecreaseItem(itemSku, saleQuantity))
                        {
                            MessageBox.Show(String.Format("更新库存失败:商品{0}库存不足销售数量{1}", itemSku, saleQuantity),
                                "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;                            
                        }

                        stockChangePrompt += String.Format("\nsku: {0}, 原库存 {1} => 现库存 {2}",
                            itemSku, origStock, origStock - saleQuantity);
                    }
                }                
            } // End of two runs

            //
            // Create a new delivery note.
            //
            int deliveryNoteId = -1;
            if ((deliveryNoteId=DeliveryNoteDAL.InsertOneDeliveryNote(deliveryNote)) <= 0)
            {
                MessageBox.Show("创建发货单失败", "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //
            // Update transaction delivery status.
            //
            foreach (DataRow dr in mDtTransactions.Rows)
            {
                String tranId = StringUtil.GetSafeString(dr["TransactionId"]);
                EbayTransactionDAL.UpdateTransactionDeliveryStatus(tranId, true, deliveryNoteId);
            }

            // Indicate main form to update data.
            Added = true;

            MessageBox.Show(String.Format("创建发货单成功 {0}", stockChangePrompt),
                "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonAddDeliveryNote_Click(object sender, EventArgs e)
        {
            if (mEditMode == EditMode.CreatNew || mEditMode == EditMode.CreateFromTrans)
                CreateDeliveryNote();
            else if (mEditMode == EditMode.EditExisting)
                ModifyDeliveryNote();
            else
                return;
        }

        private bool IsTransactionAlreadyAdded(String tranId)
        {
            foreach (DataRow dr in mDtTransactions.Rows)
            {
                if (StringUtil.GetSafeString(dr["TransactionId"]) == tranId)
                    return true;
            }

            return false;
        }

        
        private bool IsTransactionAddedInOtherDeliveryNote(String tranId)
        {
            DeliveryNoteType note = DeliveryNoteDAL.GetDeliveryNoteContainsTransaction(tranId);
            if (note != null)
            {
                if (mDeliveryNote == null || mDeliveryNote.DeliveryNoteId != note.DeliveryNoteId)
                    return true;
            }
            return false;    
        }

        private void AddTransactionsToDataTable(String tranIdsStr)
        {
            if (tranIdsStr == "")
                return;

            String[] tranIds = tranIdsStr.Split(new char[] { ',', ' ' });

            foreach (String tranId in tranIds)
            {
                if (IsTransactionAlreadyAdded(tranId))
                {
                    continue;
                }

                if (IsTransactionAddedInOtherDeliveryNote(tranId))
                {
                    MessageBox.Show(String.Format("订单号{0}已被包含在其他发货单中", tranId),
                        "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }

                EbayTransactionType trans = EbayTransactionDAL.GetOneTransactonById(tranId);

                DataRow dr = mDtTransactions.NewRow();
                dr["TransactionId"] = trans.TransactionId;
                dr["SellerName"] = trans.SellerName;
                dr["BuyerId"] = trans.BuyerId;
                dr["BuyerCountry"] = trans.BuyerCountry;
                dr["ItemSKU"] = trans.ItemSKU;
                dr["ItemTitle"] = trans.ItemTitle;
                dr["SaleQuantity"] = trans.SaleQuantity;

                mDtTransactions.Rows.Add(dr);   
            }
         
        }

        private void buttonAddTransaction_Click(object sender, EventArgs e)
        {
            String tranIdsStr = textBoxTransactionId.Text.Trim();
            if (tranIdsStr == "")
                return;

            AddTransactionsToDataTable(tranIdsStr);

            textBoxTransactionId.Text = "";
        }

        private void dataGridViewTransactions_MouseUp(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hitTestInfo;
            if (e.Button == MouseButtons.Right)
            {
                hitTestInfo = dataGridViewTransactions.HitTest(e.X, e.Y);
                this.contextMenuStripTransaction.Show(dataGridViewTransactions, new Point(e.X, e.Y));
            }
        }

        //
        // Enable/disable the context menu items here.
        //
        private void contextMenuStripTransaction_Opening(object sender, CancelEventArgs e)
        {
            ContextMenuStrip cmnu = (ContextMenuStrip)sender;
        }

        // ZHI_TODO
        private void ToolStripMenuItemDel_Click(object sender, EventArgs e)
        {
            List<String> tranIds = new List<String>();

            DataGridViewSelectedRowCollection selectedRows = this.dataGridViewTransactions.SelectedRows;
            foreach (DataGridViewRow row in selectedRows)
            {
                tranIds.Add(row.Cells[0].Value.ToString());
            }

            // Remove this rows from the datatable.
            DataTable dtUpdated = mDtTransactions.Clone();
            foreach (DataRow dr in mDtTransactions.Rows)
            {
                String tranIdLoc = StringUtil.GetSafeString(dr["TransactionId"]);
                if (-1 == tranIds.IndexOf(tranIdLoc))
                {
                    dtUpdated.Rows.Add(dr.ItemArray);
                }
            }

            mDtTransactions = dtUpdated;
            dataGridViewTransactions.DataSource = mDtTransactions;
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
