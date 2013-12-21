namespace EbayMaster
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonExportEbayCVS = new System.Windows.Forms.Button();
            this.buttonExportSelectedToExcel = new System.Windows.Forms.Button();
            this.buttonSyncEbayData = new System.Windows.Forms.Button();
            this.dateTimePickerEndTime = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerStartTime = new System.Windows.Forms.DateTimePicker();
            this.tabControlEbayMaster = new System.Windows.Forms.TabControl();
            this.tabPageOrder = new System.Windows.Forms.TabPage();
            this.radioButtonDeliveredOrders = new System.Windows.Forms.RadioButton();
            this.radioButtonPendingOrders = new System.Windows.Forms.RadioButton();
            this.buttonOrderLastPage = new System.Windows.Forms.Button();
            this.buttonOrderFirstPage = new System.Windows.Forms.Button();
            this.labelOrderPage = new System.Windows.Forms.Label();
            this.buttonOrderPrevPage = new System.Windows.Forms.Button();
            this.buttonOrderNextPage = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridViewAllOrders = new System.Windows.Forms.DataGridView();
            this.tabPageSelling = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonListingLastPage = new System.Windows.Forms.Button();
            this.buttonListingFirstPage = new System.Windows.Forms.Button();
            this.labelListingPage = new System.Windows.Forms.Label();
            this.buttonListingPrevPage = new System.Windows.Forms.Button();
            this.buttonListingNextPage = new System.Windows.Forms.Button();
            this.dataGridViewActiveListing = new System.Windows.Forms.DataGridView();
            this.tabPageMessages = new System.Windows.Forms.TabPage();
            this.pagedDgvMessages = new EbayMaster.PagedDataGridView();
            this.tabControlMessage = new System.Windows.Forms.TabControl();
            this.tabPageAllMessage = new System.Windows.Forms.TabPage();
            this.tabPagePostSale = new System.Windows.Forms.TabPage();
            this.buttonPostSaleLastPage = new System.Windows.Forms.Button();
            this.buttonPostSaleFirstPage = new System.Windows.Forms.Button();
            this.labelPostSalePage = new System.Windows.Forms.Label();
            this.buttonPostSalePrevPage = new System.Windows.Forms.Button();
            this.buttonPostSaleNextPage = new System.Windows.Forms.Button();
            this.dataGridViewPostSale = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.contextMenuStripPostSale = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemViewMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSendMessageToBuyer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripEbayMaster = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItemItemMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemGroupItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemViewItemStat = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCreateSupplier = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemViewSupplier = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCreateSourcingNote = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemViewSourcingNote = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSourcingAndDispatching = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCreateItemStockInNote = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemViewItemStockInNote = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCreateDeliveryNote = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemViewDeliveryNote = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSystemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDBConnSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemAccountSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemMessageTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemEbayFees = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripMessage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemReplyMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripTransaction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemViewTransactionDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemViewTransactionMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSendMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemAddRelationToItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemViewItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSelectShippingService = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemMarkAsShipped = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemUploadTrackingNum = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemLeaveFeedback = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSetShippingCost = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCreateDeliveryNoteFromOrders = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemMergeOrders = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.tabControlEbayMaster.SuspendLayout();
            this.tabPageOrder.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAllOrders)).BeginInit();
            this.tabPageSelling.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewActiveListing)).BeginInit();
            this.tabPageMessages.SuspendLayout();
            this.tabControlMessage.SuspendLayout();
            this.tabPagePostSale.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPostSale)).BeginInit();
            this.panel3.SuspendLayout();
            this.contextMenuStripPostSale.SuspendLayout();
            this.menuStripEbayMaster.SuspendLayout();
            this.contextMenuStripMessage.SuspendLayout();
            this.contextMenuStripTransaction.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.buttonExportEbayCVS);
            this.panel1.Controls.Add(this.buttonExportSelectedToExcel);
            this.panel1.Controls.Add(this.buttonSyncEbayData);
            this.panel1.Controls.Add(this.dateTimePickerEndTime);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dateTimePickerStartTime);
            this.panel1.Location = new System.Drawing.Point(12, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(190, 597);
            this.panel1.TabIndex = 6;
            // 
            // buttonExportEbayCVS
            // 
            this.buttonExportEbayCVS.Location = new System.Drawing.Point(23, 259);
            this.buttonExportEbayCVS.Name = "buttonExportEbayCVS";
            this.buttonExportEbayCVS.Size = new System.Drawing.Size(121, 27);
            this.buttonExportEbayCVS.TabIndex = 15;
            this.buttonExportEbayCVS.Text = "导出ebay cvs格式";
            this.buttonExportEbayCVS.UseVisualStyleBackColor = true;
            this.buttonExportEbayCVS.Click += new System.EventHandler(this.buttonExportEbayCVS_Click);
            // 
            // buttonExportSelectedToExcel
            // 
            this.buttonExportSelectedToExcel.Location = new System.Drawing.Point(24, 207);
            this.buttonExportSelectedToExcel.Name = "buttonExportSelectedToExcel";
            this.buttonExportSelectedToExcel.Size = new System.Drawing.Size(121, 27);
            this.buttonExportSelectedToExcel.TabIndex = 11;
            this.buttonExportSelectedToExcel.Text = "导出4PX xls格式";
            this.buttonExportSelectedToExcel.UseVisualStyleBackColor = true;
            this.buttonExportSelectedToExcel.Click += new System.EventHandler(this.buttonExportSelectedToExcel_Click);
            // 
            // buttonSyncEbayData
            // 
            this.buttonSyncEbayData.BackColor = System.Drawing.Color.Beige;
            this.buttonSyncEbayData.Location = new System.Drawing.Point(44, 17);
            this.buttonSyncEbayData.Name = "buttonSyncEbayData";
            this.buttonSyncEbayData.Size = new System.Drawing.Size(101, 27);
            this.buttonSyncEbayData.TabIndex = 4;
            this.buttonSyncEbayData.Text = "下载ebay数据";
            this.buttonSyncEbayData.UseVisualStyleBackColor = false;
            this.buttonSyncEbayData.Click += new System.EventHandler(this.buttonSyncEbayData_Click);
            // 
            // dateTimePickerEndTime
            // 
            this.dateTimePickerEndTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePickerEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEndTime.Location = new System.Drawing.Point(23, 123);
            this.dateTimePickerEndTime.Name = "dateTimePickerEndTime";
            this.dateTimePickerEndTime.Size = new System.Drawing.Size(142, 21);
            this.dateTimePickerEndTime.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "结束时间";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "开始时间";
            // 
            // dateTimePickerStartTime
            // 
            this.dateTimePickerStartTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePickerStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStartTime.Location = new System.Drawing.Point(23, 74);
            this.dateTimePickerStartTime.Name = "dateTimePickerStartTime";
            this.dateTimePickerStartTime.Size = new System.Drawing.Size(142, 21);
            this.dateTimePickerStartTime.TabIndex = 0;
            // 
            // tabControlEbayMaster
            // 
            this.tabControlEbayMaster.Controls.Add(this.tabPageOrder);
            this.tabControlEbayMaster.Controls.Add(this.tabPageSelling);
            this.tabControlEbayMaster.Controls.Add(this.tabPageMessages);
            this.tabControlEbayMaster.Controls.Add(this.tabPagePostSale);
            this.tabControlEbayMaster.Location = new System.Drawing.Point(218, 41);
            this.tabControlEbayMaster.Name = "tabControlEbayMaster";
            this.tabControlEbayMaster.SelectedIndex = 0;
            this.tabControlEbayMaster.Size = new System.Drawing.Size(1114, 601);
            this.tabControlEbayMaster.TabIndex = 13;
            this.tabControlEbayMaster.SelectedIndexChanged += new System.EventHandler(this.tabControlEbayMaster_SelectedIndexChanged);
            // 
            // tabPageOrder
            // 
            this.tabPageOrder.Controls.Add(this.radioButtonDeliveredOrders);
            this.tabPageOrder.Controls.Add(this.radioButtonPendingOrders);
            this.tabPageOrder.Controls.Add(this.buttonOrderLastPage);
            this.tabPageOrder.Controls.Add(this.buttonOrderFirstPage);
            this.tabPageOrder.Controls.Add(this.labelOrderPage);
            this.tabPageOrder.Controls.Add(this.buttonOrderPrevPage);
            this.tabPageOrder.Controls.Add(this.buttonOrderNextPage);
            this.tabPageOrder.Controls.Add(this.panel2);
            this.tabPageOrder.Controls.Add(this.dataGridViewAllOrders);
            this.tabPageOrder.Location = new System.Drawing.Point(4, 22);
            this.tabPageOrder.Name = "tabPageOrder";
            this.tabPageOrder.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOrder.Size = new System.Drawing.Size(1106, 575);
            this.tabPageOrder.TabIndex = 1;
            this.tabPageOrder.Text = "订单处理";
            this.tabPageOrder.UseVisualStyleBackColor = true;
            // 
            // radioButtonDeliveredOrders
            // 
            this.radioButtonDeliveredOrders.AutoSize = true;
            this.radioButtonDeliveredOrders.Location = new System.Drawing.Point(858, 18);
            this.radioButtonDeliveredOrders.Name = "radioButtonDeliveredOrders";
            this.radioButtonDeliveredOrders.Size = new System.Drawing.Size(59, 16);
            this.radioButtonDeliveredOrders.TabIndex = 21;
            this.radioButtonDeliveredOrders.Text = "已发货";
            this.radioButtonDeliveredOrders.UseVisualStyleBackColor = true;
            this.radioButtonDeliveredOrders.CheckedChanged += new System.EventHandler(this.radioButtonOrders_CheckedChanged);
            // 
            // radioButtonPendingOrders
            // 
            this.radioButtonPendingOrders.AutoSize = true;
            this.radioButtonPendingOrders.Checked = true;
            this.radioButtonPendingOrders.Location = new System.Drawing.Point(761, 18);
            this.radioButtonPendingOrders.Name = "radioButtonPendingOrders";
            this.radioButtonPendingOrders.Size = new System.Drawing.Size(59, 16);
            this.radioButtonPendingOrders.TabIndex = 20;
            this.radioButtonPendingOrders.TabStop = true;
            this.radioButtonPendingOrders.Text = "未处理";
            this.radioButtonPendingOrders.UseVisualStyleBackColor = true;
            this.radioButtonPendingOrders.CheckedChanged += new System.EventHandler(this.radioButtonOrders_CheckedChanged);
            // 
            // buttonOrderLastPage
            // 
            this.buttonOrderLastPage.Location = new System.Drawing.Point(731, 516);
            this.buttonOrderLastPage.Name = "buttonOrderLastPage";
            this.buttonOrderLastPage.Size = new System.Drawing.Size(75, 23);
            this.buttonOrderLastPage.TabIndex = 19;
            this.buttonOrderLastPage.Text = "最后页";
            this.buttonOrderLastPage.UseVisualStyleBackColor = true;
            this.buttonOrderLastPage.Click += new System.EventHandler(this.buttonOrderLastPage_Click);
            // 
            // buttonOrderFirstPage
            // 
            this.buttonOrderFirstPage.Enabled = false;
            this.buttonOrderFirstPage.Location = new System.Drawing.Point(269, 516);
            this.buttonOrderFirstPage.Name = "buttonOrderFirstPage";
            this.buttonOrderFirstPage.Size = new System.Drawing.Size(75, 23);
            this.buttonOrderFirstPage.TabIndex = 18;
            this.buttonOrderFirstPage.Text = "第一页";
            this.buttonOrderFirstPage.UseVisualStyleBackColor = true;
            this.buttonOrderFirstPage.Click += new System.EventHandler(this.buttonOrderFirstPage_Click);
            // 
            // labelOrderPage
            // 
            this.labelOrderPage.AutoSize = true;
            this.labelOrderPage.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelOrderPage.Location = new System.Drawing.Point(486, 521);
            this.labelOrderPage.Name = "labelOrderPage";
            this.labelOrderPage.Size = new System.Drawing.Size(103, 12);
            this.labelOrderPage.TabIndex = 16;
            this.labelOrderPage.Text = "labelOrderPage";
            // 
            // buttonOrderPrevPage
            // 
            this.buttonOrderPrevPage.Enabled = false;
            this.buttonOrderPrevPage.Location = new System.Drawing.Point(373, 516);
            this.buttonOrderPrevPage.Name = "buttonOrderPrevPage";
            this.buttonOrderPrevPage.Size = new System.Drawing.Size(75, 23);
            this.buttonOrderPrevPage.TabIndex = 15;
            this.buttonOrderPrevPage.Text = "上一页";
            this.buttonOrderPrevPage.UseVisualStyleBackColor = true;
            this.buttonOrderPrevPage.Click += new System.EventHandler(this.buttonOrderPrevPage_Click);
            // 
            // buttonOrderNextPage
            // 
            this.buttonOrderNextPage.Location = new System.Drawing.Point(616, 516);
            this.buttonOrderNextPage.Name = "buttonOrderNextPage";
            this.buttonOrderNextPage.Size = new System.Drawing.Size(75, 23);
            this.buttonOrderNextPage.TabIndex = 14;
            this.buttonOrderNextPage.Text = "下一页";
            this.buttonOrderNextPage.UseVisualStyleBackColor = true;
            this.buttonOrderNextPage.Click += new System.EventHandler(this.buttonOrderNextPage_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(15, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(709, 32);
            this.panel2.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(199, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 12);
            this.label5.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.LightGreen;
            this.label8.Location = new System.Drawing.Point(320, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 12);
            this.label8.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(276, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 11;
            this.label9.Text = "已发货";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Gray;
            this.label6.Location = new System.Drawing.Point(77, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 12);
            this.label6.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(33, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "未付款";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(155, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "未发货";
            // 
            // dataGridViewAllOrders
            // 
            this.dataGridViewAllOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAllOrders.Location = new System.Drawing.Point(15, 44);
            this.dataGridViewAllOrders.Name = "dataGridViewAllOrders";
            this.dataGridViewAllOrders.RowTemplate.Height = 23;
            this.dataGridViewAllOrders.Size = new System.Drawing.Size(1069, 466);
            this.dataGridViewAllOrders.TabIndex = 0;
            this.dataGridViewAllOrders.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAllOrders_CellClick);
            this.dataGridViewAllOrders.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridViewAllOrders_DataBindingComplete);
            this.dataGridViewAllOrders.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridViewAllOrders_MouseUp);
            // 
            // tabPageSelling
            // 
            this.tabPageSelling.Controls.Add(this.groupBox1);
            this.tabPageSelling.Location = new System.Drawing.Point(4, 22);
            this.tabPageSelling.Name = "tabPageSelling";
            this.tabPageSelling.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSelling.Size = new System.Drawing.Size(1106, 575);
            this.tabPageSelling.TabIndex = 0;
            this.tabPageSelling.Text = "销售概况";
            this.tabPageSelling.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonListingLastPage);
            this.groupBox1.Controls.Add(this.buttonListingFirstPage);
            this.groupBox1.Controls.Add(this.labelListingPage);
            this.groupBox1.Controls.Add(this.buttonListingPrevPage);
            this.groupBox1.Controls.Add(this.buttonListingNextPage);
            this.groupBox1.Controls.Add(this.dataGridViewActiveListing);
            this.groupBox1.Location = new System.Drawing.Point(17, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1083, 524);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "在售商品";
            // 
            // buttonListingLastPage
            // 
            this.buttonListingLastPage.Location = new System.Drawing.Point(689, 499);
            this.buttonListingLastPage.Name = "buttonListingLastPage";
            this.buttonListingLastPage.Size = new System.Drawing.Size(75, 23);
            this.buttonListingLastPage.TabIndex = 21;
            this.buttonListingLastPage.Text = "最后页";
            this.buttonListingLastPage.UseVisualStyleBackColor = true;
            this.buttonListingLastPage.Click += new System.EventHandler(this.buttonListingLastPage_Click);
            // 
            // buttonListingFirstPage
            // 
            this.buttonListingFirstPage.Location = new System.Drawing.Point(227, 499);
            this.buttonListingFirstPage.Name = "buttonListingFirstPage";
            this.buttonListingFirstPage.Size = new System.Drawing.Size(75, 23);
            this.buttonListingFirstPage.TabIndex = 20;
            this.buttonListingFirstPage.Text = "第一页";
            this.buttonListingFirstPage.UseVisualStyleBackColor = true;
            this.buttonListingFirstPage.Click += new System.EventHandler(this.buttonListingFirstPage_Click);
            // 
            // labelListingPage
            // 
            this.labelListingPage.AutoSize = true;
            this.labelListingPage.Location = new System.Drawing.Point(448, 506);
            this.labelListingPage.Name = "labelListingPage";
            this.labelListingPage.Size = new System.Drawing.Size(101, 12);
            this.labelListingPage.TabIndex = 19;
            this.labelListingPage.Text = "labelListingPage";
            // 
            // buttonListingPrevPage
            // 
            this.buttonListingPrevPage.Location = new System.Drawing.Point(333, 499);
            this.buttonListingPrevPage.Name = "buttonListingPrevPage";
            this.buttonListingPrevPage.Size = new System.Drawing.Size(75, 23);
            this.buttonListingPrevPage.TabIndex = 18;
            this.buttonListingPrevPage.Text = "上一页";
            this.buttonListingPrevPage.UseVisualStyleBackColor = true;
            this.buttonListingPrevPage.Click += new System.EventHandler(this.buttonListingPrevPage_Click);
            // 
            // buttonListingNextPage
            // 
            this.buttonListingNextPage.Location = new System.Drawing.Point(578, 499);
            this.buttonListingNextPage.Name = "buttonListingNextPage";
            this.buttonListingNextPage.Size = new System.Drawing.Size(75, 23);
            this.buttonListingNextPage.TabIndex = 17;
            this.buttonListingNextPage.Text = "下一页";
            this.buttonListingNextPage.UseVisualStyleBackColor = true;
            this.buttonListingNextPage.Click += new System.EventHandler(this.buttonListingNextPage_Click);
            // 
            // dataGridViewActiveListing
            // 
            this.dataGridViewActiveListing.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewActiveListing.Location = new System.Drawing.Point(12, 19);
            this.dataGridViewActiveListing.Name = "dataGridViewActiveListing";
            this.dataGridViewActiveListing.RowTemplate.Height = 23;
            this.dataGridViewActiveListing.Size = new System.Drawing.Size(1065, 474);
            this.dataGridViewActiveListing.TabIndex = 0;
            this.dataGridViewActiveListing.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridViewActiveListing_DataBindingComplete);
            // 
            // tabPageMessages
            // 
            this.tabPageMessages.Controls.Add(this.pagedDgvMessages);
            this.tabPageMessages.Controls.Add(this.tabControlMessage);
            this.tabPageMessages.Location = new System.Drawing.Point(4, 22);
            this.tabPageMessages.Name = "tabPageMessages";
            this.tabPageMessages.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMessages.Size = new System.Drawing.Size(1106, 575);
            this.tabPageMessages.TabIndex = 2;
            this.tabPageMessages.Text = "消息处理";
            this.tabPageMessages.UseVisualStyleBackColor = true;
            // 
            // pagedDgvMessages
            // 
            this.pagedDgvMessages.Location = new System.Drawing.Point(3, 25);
            this.pagedDgvMessages.Name = "pagedDgvMessages";
            this.pagedDgvMessages.Size = new System.Drawing.Size(543, 492);
            this.pagedDgvMessages.TabIndex = 4;
            // 
            // tabControlMessage
            // 
            this.tabControlMessage.Controls.Add(this.tabPageAllMessage);
            this.tabControlMessage.Location = new System.Drawing.Point(552, 25);
            this.tabControlMessage.Name = "tabControlMessage";
            this.tabControlMessage.SelectedIndex = 0;
            this.tabControlMessage.Size = new System.Drawing.Size(532, 496);
            this.tabControlMessage.TabIndex = 3;
            // 
            // tabPageAllMessage
            // 
            this.tabPageAllMessage.Location = new System.Drawing.Point(4, 22);
            this.tabPageAllMessage.Name = "tabPageAllMessage";
            this.tabPageAllMessage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAllMessage.Size = new System.Drawing.Size(524, 470);
            this.tabPageAllMessage.TabIndex = 0;
            this.tabPageAllMessage.UseVisualStyleBackColor = true;
            // 
            // tabPagePostSale
            // 
            this.tabPagePostSale.Controls.Add(this.buttonPostSaleLastPage);
            this.tabPagePostSale.Controls.Add(this.buttonPostSaleFirstPage);
            this.tabPagePostSale.Controls.Add(this.labelPostSalePage);
            this.tabPagePostSale.Controls.Add(this.buttonPostSalePrevPage);
            this.tabPagePostSale.Controls.Add(this.buttonPostSaleNextPage);
            this.tabPagePostSale.Controls.Add(this.dataGridViewPostSale);
            this.tabPagePostSale.Controls.Add(this.panel3);
            this.tabPagePostSale.Location = new System.Drawing.Point(4, 22);
            this.tabPagePostSale.Name = "tabPagePostSale";
            this.tabPagePostSale.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePostSale.Size = new System.Drawing.Size(1106, 575);
            this.tabPagePostSale.TabIndex = 3;
            this.tabPagePostSale.Text = "售后跟踪";
            this.tabPagePostSale.UseVisualStyleBackColor = true;
            // 
            // buttonPostSaleLastPage
            // 
            this.buttonPostSaleLastPage.Location = new System.Drawing.Point(733, 516);
            this.buttonPostSaleLastPage.Name = "buttonPostSaleLastPage";
            this.buttonPostSaleLastPage.Size = new System.Drawing.Size(75, 23);
            this.buttonPostSaleLastPage.TabIndex = 24;
            this.buttonPostSaleLastPage.Text = "最后页";
            this.buttonPostSaleLastPage.UseVisualStyleBackColor = true;
            this.buttonPostSaleLastPage.Click += new System.EventHandler(this.buttonPostSaleLastPage_Click);
            // 
            // buttonPostSaleFirstPage
            // 
            this.buttonPostSaleFirstPage.Enabled = false;
            this.buttonPostSaleFirstPage.Location = new System.Drawing.Point(271, 516);
            this.buttonPostSaleFirstPage.Name = "buttonPostSaleFirstPage";
            this.buttonPostSaleFirstPage.Size = new System.Drawing.Size(75, 23);
            this.buttonPostSaleFirstPage.TabIndex = 23;
            this.buttonPostSaleFirstPage.Text = "第一页";
            this.buttonPostSaleFirstPage.UseVisualStyleBackColor = true;
            this.buttonPostSaleFirstPage.Click += new System.EventHandler(this.buttonPostSaleFirstPage_Click);
            // 
            // labelPostSalePage
            // 
            this.labelPostSalePage.AutoSize = true;
            this.labelPostSalePage.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPostSalePage.Location = new System.Drawing.Point(498, 521);
            this.labelPostSalePage.Name = "labelPostSalePage";
            this.labelPostSalePage.Size = new System.Drawing.Size(124, 12);
            this.labelPostSalePage.TabIndex = 22;
            this.labelPostSalePage.Text = "labelPostSalePage";
            // 
            // buttonPostSalePrevPage
            // 
            this.buttonPostSalePrevPage.Enabled = false;
            this.buttonPostSalePrevPage.Location = new System.Drawing.Point(375, 516);
            this.buttonPostSalePrevPage.Name = "buttonPostSalePrevPage";
            this.buttonPostSalePrevPage.Size = new System.Drawing.Size(75, 23);
            this.buttonPostSalePrevPage.TabIndex = 21;
            this.buttonPostSalePrevPage.Text = "上一页";
            this.buttonPostSalePrevPage.UseVisualStyleBackColor = true;
            this.buttonPostSalePrevPage.Click += new System.EventHandler(this.buttonPostSalePrevPage_Click);
            // 
            // buttonPostSaleNextPage
            // 
            this.buttonPostSaleNextPage.Location = new System.Drawing.Point(618, 516);
            this.buttonPostSaleNextPage.Name = "buttonPostSaleNextPage";
            this.buttonPostSaleNextPage.Size = new System.Drawing.Size(75, 23);
            this.buttonPostSaleNextPage.TabIndex = 20;
            this.buttonPostSaleNextPage.Text = "下一页";
            this.buttonPostSaleNextPage.UseVisualStyleBackColor = true;
            this.buttonPostSaleNextPage.Click += new System.EventHandler(this.buttonPostSaleNextPage_Click);
            // 
            // dataGridViewPostSale
            // 
            this.dataGridViewPostSale.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPostSale.Location = new System.Drawing.Point(15, 44);
            this.dataGridViewPostSale.Name = "dataGridViewPostSale";
            this.dataGridViewPostSale.RowTemplate.Height = 23;
            this.dataGridViewPostSale.Size = new System.Drawing.Size(1069, 466);
            this.dataGridViewPostSale.TabIndex = 11;
            this.dataGridViewPostSale.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewPostSale_CellMouseDoubleClick);
            this.dataGridViewPostSale.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridViewPostSale_DataBindingComplete);
            this.dataGridViewPostSale.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridViewPostSale_MouseUp);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label16);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Controls.Add(this.label18);
            this.panel3.Controls.Add(this.label19);
            this.panel3.Controls.Add(this.label20);
            this.panel3.Controls.Add(this.label21);
            this.panel3.Location = new System.Drawing.Point(15, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(439, 32);
            this.panel3.TabIndex = 10;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.Goldenrod;
            this.label16.Location = new System.Drawing.Point(336, 14);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(34, 12);
            this.label16.TabIndex = 16;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(277, 14);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 12);
            this.label17.TabIndex = 15;
            this.label17.Text = "已回消息";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.Yellow;
            this.label18.Location = new System.Drawing.Point(217, 14);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(34, 12);
            this.label18.TabIndex = 14;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(158, 14);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 12);
            this.label19.TabIndex = 13;
            this.label19.Text = "未读消息";
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.LightGreen;
            this.label20.Location = new System.Drawing.Point(81, 14);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(34, 12);
            this.label20.TabIndex = 12;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(37, 14);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(41, 12);
            this.label21.TabIndex = 11;
            this.label21.Text = "已询问";
            // 
            // contextMenuStripPostSale
            // 
            this.contextMenuStripPostSale.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemViewMessage,
            this.ToolStripMenuItemSendMessageToBuyer});
            this.contextMenuStripPostSale.Name = "contextMenuStripPostSale";
            this.contextMenuStripPostSale.Size = new System.Drawing.Size(137, 48);
            this.contextMenuStripPostSale.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripPostSale_Opening);
            // 
            // ToolStripMenuItemViewMessage
            // 
            this.ToolStripMenuItemViewMessage.Name = "ToolStripMenuItemViewMessage";
            this.ToolStripMenuItemViewMessage.Size = new System.Drawing.Size(136, 22);
            this.ToolStripMenuItemViewMessage.Text = "查看消息";
            this.ToolStripMenuItemViewMessage.Click += new System.EventHandler(this.ToolStripMenuItemViewMessage_Click);
            // 
            // ToolStripMenuItemSendMessageToBuyer
            // 
            this.ToolStripMenuItemSendMessageToBuyer.Name = "ToolStripMenuItemSendMessageToBuyer";
            this.ToolStripMenuItemSendMessageToBuyer.Size = new System.Drawing.Size(136, 22);
            this.ToolStripMenuItemSendMessageToBuyer.Text = "给买家写信";
            this.ToolStripMenuItemSendMessageToBuyer.Click += new System.EventHandler(this.ToolStripMenuItemSendMessageToBuyer_Click);
            // 
            // menuStripEbayMaster
            // 
            this.menuStripEbayMaster.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemItemMenu,
            this.ToolStripMenuItemSourcingAndDispatching,
            this.ToolStripMenuItemSystemSettings,
            this.工具ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStripEbayMaster.Location = new System.Drawing.Point(0, 0);
            this.menuStripEbayMaster.Name = "menuStripEbayMaster";
            this.menuStripEbayMaster.Size = new System.Drawing.Size(1359, 25);
            this.menuStripEbayMaster.TabIndex = 17;
            this.menuStripEbayMaster.Text = "menuStrip1";
            // 
            // ToolStripMenuItemItemMenu
            // 
            this.ToolStripMenuItemItemMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemCategory,
            this.ToolStripMenuItemItem,
            this.ToolStripMenuItemGroupItem,
            this.ToolStripMenuItemViewItemStat,
            this.ToolStripMenuItemCreateSupplier,
            this.ToolStripMenuItemViewSupplier,
            this.ToolStripMenuItemCreateSourcingNote,
            this.ToolStripMenuItemViewSourcingNote});
            this.ToolStripMenuItemItemMenu.Name = "ToolStripMenuItemItemMenu";
            this.ToolStripMenuItemItemMenu.Size = new System.Drawing.Size(68, 21);
            this.ToolStripMenuItemItemMenu.Text = "商品管理";
            // 
            // ToolStripMenuItemCategory
            // 
            this.ToolStripMenuItemCategory.Name = "ToolStripMenuItemCategory";
            this.ToolStripMenuItemCategory.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemCategory.Text = "商品类别管理";
            this.ToolStripMenuItemCategory.Click += new System.EventHandler(this.ToolStripMenuItemCategory_Click);
            // 
            // ToolStripMenuItemItem
            // 
            this.ToolStripMenuItemItem.Name = "ToolStripMenuItemItem";
            this.ToolStripMenuItemItem.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemItem.Text = "商品管理";
            this.ToolStripMenuItemItem.Click += new System.EventHandler(this.ToolStripMenuItemItem_Click);
            // 
            // ToolStripMenuItemGroupItem
            // 
            this.ToolStripMenuItemGroupItem.Name = "ToolStripMenuItemGroupItem";
            this.ToolStripMenuItemGroupItem.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemGroupItem.Text = "组合商品管理";
            this.ToolStripMenuItemGroupItem.Visible = false;
            // 
            // ToolStripMenuItemViewItemStat
            // 
            this.ToolStripMenuItemViewItemStat.Name = "ToolStripMenuItemViewItemStat";
            this.ToolStripMenuItemViewItemStat.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemViewItemStat.Text = "查看所有商品";
            this.ToolStripMenuItemViewItemStat.Click += new System.EventHandler(this.ToolStripMenuItemViewItemStat_Click);
            // 
            // ToolStripMenuItemCreateSupplier
            // 
            this.ToolStripMenuItemCreateSupplier.Name = "ToolStripMenuItemCreateSupplier";
            this.ToolStripMenuItemCreateSupplier.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemCreateSupplier.Text = "创建供应商";
            this.ToolStripMenuItemCreateSupplier.Click += new System.EventHandler(this.ToolStripMenuItemCreateSupplier_Click);
            // 
            // ToolStripMenuItemViewSupplier
            // 
            this.ToolStripMenuItemViewSupplier.Name = "ToolStripMenuItemViewSupplier";
            this.ToolStripMenuItemViewSupplier.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemViewSupplier.Text = "查看供应商";
            this.ToolStripMenuItemViewSupplier.Click += new System.EventHandler(this.ToolStripMenuItemViewSupplier_Click);
            // 
            // ToolStripMenuItemCreateSourcingNote
            // 
            this.ToolStripMenuItemCreateSourcingNote.Name = "ToolStripMenuItemCreateSourcingNote";
            this.ToolStripMenuItemCreateSourcingNote.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemCreateSourcingNote.Text = "创建采购单";
            this.ToolStripMenuItemCreateSourcingNote.Click += new System.EventHandler(this.ToolStripMenuItemCreateSourcingNote_Click);
            // 
            // ToolStripMenuItemViewSourcingNote
            // 
            this.ToolStripMenuItemViewSourcingNote.Name = "ToolStripMenuItemViewSourcingNote";
            this.ToolStripMenuItemViewSourcingNote.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemViewSourcingNote.Text = "查看采购单";
            this.ToolStripMenuItemViewSourcingNote.Click += new System.EventHandler(this.ToolStripMenuItemViewSourcingNote_Click);
            // 
            // ToolStripMenuItemSourcingAndDispatching
            // 
            this.ToolStripMenuItemSourcingAndDispatching.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemCreateItemStockInNote,
            this.ToolStripMenuItemViewItemStockInNote,
            this.ToolStripMenuItemCreateDeliveryNote,
            this.ToolStripMenuItemViewDeliveryNote});
            this.ToolStripMenuItemSourcingAndDispatching.Name = "ToolStripMenuItemSourcingAndDispatching";
            this.ToolStripMenuItemSourcingAndDispatching.Size = new System.Drawing.Size(92, 21);
            this.ToolStripMenuItemSourcingAndDispatching.Text = "进货发货管理";
            // 
            // ToolStripMenuItemCreateItemStockInNote
            // 
            this.ToolStripMenuItemCreateItemStockInNote.Name = "ToolStripMenuItemCreateItemStockInNote";
            this.ToolStripMenuItemCreateItemStockInNote.Size = new System.Drawing.Size(136, 22);
            this.ToolStripMenuItemCreateItemStockInNote.Text = "创建入库单";
            this.ToolStripMenuItemCreateItemStockInNote.Click += new System.EventHandler(this.ToolStripMenuItemCreateItemStockInNote_Click);
            // 
            // ToolStripMenuItemViewItemStockInNote
            // 
            this.ToolStripMenuItemViewItemStockInNote.Name = "ToolStripMenuItemViewItemStockInNote";
            this.ToolStripMenuItemViewItemStockInNote.Size = new System.Drawing.Size(136, 22);
            this.ToolStripMenuItemViewItemStockInNote.Text = "查看入库单";
            this.ToolStripMenuItemViewItemStockInNote.Click += new System.EventHandler(this.ToolStripMenuItemViewItemStockInNote_Click);
            // 
            // ToolStripMenuItemCreateDeliveryNote
            // 
            this.ToolStripMenuItemCreateDeliveryNote.Name = "ToolStripMenuItemCreateDeliveryNote";
            this.ToolStripMenuItemCreateDeliveryNote.Size = new System.Drawing.Size(136, 22);
            this.ToolStripMenuItemCreateDeliveryNote.Text = "创建发货单";
            this.ToolStripMenuItemCreateDeliveryNote.Click += new System.EventHandler(this.ToolStripMenuItemDeliveryNote_Click);
            // 
            // ToolStripMenuItemViewDeliveryNote
            // 
            this.ToolStripMenuItemViewDeliveryNote.Name = "ToolStripMenuItemViewDeliveryNote";
            this.ToolStripMenuItemViewDeliveryNote.Size = new System.Drawing.Size(136, 22);
            this.ToolStripMenuItemViewDeliveryNote.Text = "查看发货单";
            this.ToolStripMenuItemViewDeliveryNote.Click += new System.EventHandler(this.ToolStripMenuItemViewDeliveryNote_Click);
            // 
            // ToolStripMenuItemSystemSettings
            // 
            this.ToolStripMenuItemSystemSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemDBConnSettings,
            this.ToolStripMenuItemAccountSettings,
            this.ToolStripMenuItemMessageTemplate});
            this.ToolStripMenuItemSystemSettings.Name = "ToolStripMenuItemSystemSettings";
            this.ToolStripMenuItemSystemSettings.Size = new System.Drawing.Size(68, 21);
            this.ToolStripMenuItemSystemSettings.Text = "系统设置";
            // 
            // ToolStripMenuItemDBConnSettings
            // 
            this.ToolStripMenuItemDBConnSettings.Name = "ToolStripMenuItemDBConnSettings";
            this.ToolStripMenuItemDBConnSettings.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemDBConnSettings.Text = "数据库设置";
            this.ToolStripMenuItemDBConnSettings.Click += new System.EventHandler(this.ToolStripMenuItemDBConnSettings_Click);
            // 
            // ToolStripMenuItemAccountSettings
            // 
            this.ToolStripMenuItemAccountSettings.Name = "ToolStripMenuItemAccountSettings";
            this.ToolStripMenuItemAccountSettings.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemAccountSettings.Text = "账号管理";
            this.ToolStripMenuItemAccountSettings.Click += new System.EventHandler(this.ToolStripMenuItemAccountSettings_Click);
            // 
            // ToolStripMenuItemMessageTemplate
            // 
            this.ToolStripMenuItemMessageTemplate.Name = "ToolStripMenuItemMessageTemplate";
            this.ToolStripMenuItemMessageTemplate.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemMessageTemplate.Text = "消息模板管理";
            this.ToolStripMenuItemMessageTemplate.Click += new System.EventHandler(this.ToolStripMenuItemMessageTemplate_Click);
            // 
            // 工具ToolStripMenuItem
            // 
            this.工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemEbayFees});
            this.工具ToolStripMenuItem.Name = "工具ToolStripMenuItem";
            this.工具ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.工具ToolStripMenuItem.Text = "工具";
            // 
            // ToolStripMenuItemEbayFees
            // 
            this.ToolStripMenuItemEbayFees.Name = "ToolStripMenuItemEbayFees";
            this.ToolStripMenuItemEbayFees.Size = new System.Drawing.Size(128, 22);
            this.ToolStripMenuItemEbayFees.Text = "ebay费率";
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemAbout});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // ToolStripMenuItemAbout
            // 
            this.ToolStripMenuItemAbout.Name = "ToolStripMenuItemAbout";
            this.ToolStripMenuItemAbout.Size = new System.Drawing.Size(100, 22);
            this.ToolStripMenuItemAbout.Text = "关于";
            this.ToolStripMenuItemAbout.Click += new System.EventHandler(this.ToolStripMenuItemAbout_Click);
            // 
            // contextMenuStripMessage
            // 
            this.contextMenuStripMessage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemReplyMessage});
            this.contextMenuStripMessage.Name = "contextMenuStripPostSale";
            this.contextMenuStripMessage.Size = new System.Drawing.Size(137, 26);
            // 
            // toolStripMenuItemReplyMessage
            // 
            this.toolStripMenuItemReplyMessage.Name = "toolStripMenuItemReplyMessage";
            this.toolStripMenuItemReplyMessage.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItemReplyMessage.Text = "给买家回信";
            // 
            // contextMenuStripTransaction
            // 
            this.contextMenuStripTransaction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemViewTransactionDetail,
            this.ToolStripMenuItemViewTransactionMessage,
            this.ToolStripMenuItemSendMessage,
            this.ToolStripMenuItemAddRelationToItem,
            this.ToolStripMenuItemViewItem,
            this.ToolStripMenuItemSelectShippingService,
            this.ToolStripMenuItemMarkAsShipped,
            this.ToolStripMenuItemUploadTrackingNum,
            this.ToolStripMenuItemLeaveFeedback,
            this.ToolStripMenuItemSetShippingCost,
            this.ToolStripMenuItemCreateDeliveryNoteFromOrders,
            this.ToolStripMenuItemMergeOrders});
            this.contextMenuStripTransaction.Name = "contextMenuStripTransaction";
            this.contextMenuStripTransaction.Size = new System.Drawing.Size(149, 268);
            this.contextMenuStripTransaction.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripTransaction_Opening);
            // 
            // ToolStripMenuItemViewTransactionDetail
            // 
            this.ToolStripMenuItemViewTransactionDetail.Name = "ToolStripMenuItemViewTransactionDetail";
            this.ToolStripMenuItemViewTransactionDetail.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemViewTransactionDetail.Text = "查看详情";
            // 
            // ToolStripMenuItemViewTransactionMessage
            // 
            this.ToolStripMenuItemViewTransactionMessage.Name = "ToolStripMenuItemViewTransactionMessage";
            this.ToolStripMenuItemViewTransactionMessage.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemViewTransactionMessage.Text = "查看消息";
            // 
            // ToolStripMenuItemSendMessage
            // 
            this.ToolStripMenuItemSendMessage.Name = "ToolStripMenuItemSendMessage";
            this.ToolStripMenuItemSendMessage.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemSendMessage.Text = "发消息";
            // 
            // ToolStripMenuItemAddRelationToItem
            // 
            this.ToolStripMenuItemAddRelationToItem.Name = "ToolStripMenuItemAddRelationToItem";
            this.ToolStripMenuItemAddRelationToItem.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemAddRelationToItem.Text = "关联商品";
            this.ToolStripMenuItemAddRelationToItem.Click += new System.EventHandler(this.ToolStripMenuItemAddRelationToItem_Click);
            // 
            // ToolStripMenuItemViewItem
            // 
            this.ToolStripMenuItemViewItem.Name = "ToolStripMenuItemViewItem";
            this.ToolStripMenuItemViewItem.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemViewItem.Text = "查看商品";
            // 
            // ToolStripMenuItemSelectShippingService
            // 
            this.ToolStripMenuItemSelectShippingService.Name = "ToolStripMenuItemSelectShippingService";
            this.ToolStripMenuItemSelectShippingService.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemSelectShippingService.Text = "选择物流方式";
            this.ToolStripMenuItemSelectShippingService.Click += new System.EventHandler(this.ToolStripMenuItemSelectShippingService_Click);
            // 
            // ToolStripMenuItemMarkAsShipped
            // 
            this.ToolStripMenuItemMarkAsShipped.Name = "ToolStripMenuItemMarkAsShipped";
            this.ToolStripMenuItemMarkAsShipped.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemMarkAsShipped.Text = "标记已发货";
            this.ToolStripMenuItemMarkAsShipped.Click += new System.EventHandler(this.ToolStripMenuItemMarkAsShipped_Click);
            // 
            // ToolStripMenuItemUploadTrackingNum
            // 
            this.ToolStripMenuItemUploadTrackingNum.Name = "ToolStripMenuItemUploadTrackingNum";
            this.ToolStripMenuItemUploadTrackingNum.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemUploadTrackingNum.Text = "上传跟踪号";
            this.ToolStripMenuItemUploadTrackingNum.Click += new System.EventHandler(this.ToolStripMenuItemUploadTrackingNum_Click);
            // 
            // ToolStripMenuItemLeaveFeedback
            // 
            this.ToolStripMenuItemLeaveFeedback.Name = "ToolStripMenuItemLeaveFeedback";
            this.ToolStripMenuItemLeaveFeedback.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemLeaveFeedback.Text = "留好评";
            this.ToolStripMenuItemLeaveFeedback.Click += new System.EventHandler(this.ToolStripMenuItemLeaveFeedback_Click);
            // 
            // ToolStripMenuItemSetShippingCost
            // 
            this.ToolStripMenuItemSetShippingCost.Name = "ToolStripMenuItemSetShippingCost";
            this.ToolStripMenuItemSetShippingCost.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemSetShippingCost.Text = "设置运费";
            // 
            // ToolStripMenuItemCreateDeliveryNoteFromOrders
            // 
            this.ToolStripMenuItemCreateDeliveryNoteFromOrders.Name = "ToolStripMenuItemCreateDeliveryNoteFromOrders";
            this.ToolStripMenuItemCreateDeliveryNoteFromOrders.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemCreateDeliveryNoteFromOrders.Text = "创建发货单";
            this.ToolStripMenuItemCreateDeliveryNoteFromOrders.Click += new System.EventHandler(this.ToolStripMenuItemCreateDeliveryNoteFromOrders_Click);
            // 
            // ToolStripMenuItemMergeOrders
            // 
            this.ToolStripMenuItemMergeOrders.Name = "ToolStripMenuItemMergeOrders";
            this.ToolStripMenuItemMergeOrders.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemMergeOrders.Text = "合并订单";
            this.ToolStripMenuItemMergeOrders.Visible = false;
            this.ToolStripMenuItemMergeOrders.Click += new System.EventHandler(this.ToolStripMenuItemMergeOrders_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1359, 654);
            this.Controls.Add(this.menuStripEbayMaster);
            this.Controls.Add(this.tabControlEbayMaster);
            this.Controls.Add(this.panel1);
            this.Name = "FrmMain";
            this.Text = "ebay管家婆 小卖家版 v1.0";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Shown += new System.EventHandler(this.FrmMain_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControlEbayMaster.ResumeLayout(false);
            this.tabPageOrder.ResumeLayout(false);
            this.tabPageOrder.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAllOrders)).EndInit();
            this.tabPageSelling.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewActiveListing)).EndInit();
            this.tabPageMessages.ResumeLayout(false);
            this.tabControlMessage.ResumeLayout(false);
            this.tabPagePostSale.ResumeLayout(false);
            this.tabPagePostSale.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPostSale)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.contextMenuStripPostSale.ResumeLayout(false);
            this.menuStripEbayMaster.ResumeLayout(false);
            this.menuStripEbayMaster.PerformLayout();
            this.contextMenuStripMessage.ResumeLayout(false);
            this.contextMenuStripTransaction.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonSyncEbayData;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartTime;
        private System.Windows.Forms.TabControl tabControlEbayMaster;
        private System.Windows.Forms.TabPage tabPageSelling;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelListingPage;
        private System.Windows.Forms.Button buttonListingPrevPage;
        private System.Windows.Forms.Button buttonListingNextPage;
        private System.Windows.Forms.DataGridView dataGridViewActiveListing;
        private System.Windows.Forms.TabPage tabPageOrder;
        private System.Windows.Forms.Label labelOrderPage;
        private System.Windows.Forms.Button buttonOrderPrevPage;
        private System.Windows.Forms.Button buttonOrderNextPage;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridViewAllOrders;
        private System.Windows.Forms.TabPage tabPageMessages;
        private System.Windows.Forms.TabControl tabControlMessage;
        private System.Windows.Forms.TabPage tabPageAllMessage;
        private System.Windows.Forms.TabPage tabPagePostSale;
        private System.Windows.Forms.DataGridView dataGridViewPostSale;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button buttonOrderLastPage;
        private System.Windows.Forms.Button buttonOrderFirstPage;
        private System.Windows.Forms.Button buttonPostSaleLastPage;
        private System.Windows.Forms.Button buttonPostSaleFirstPage;
        private System.Windows.Forms.Label labelPostSalePage;
        private System.Windows.Forms.Button buttonPostSalePrevPage;
        private System.Windows.Forms.Button buttonPostSaleNextPage;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripPostSale;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemViewMessage;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSendMessageToBuyer;
        private System.Windows.Forms.MenuStrip menuStripEbayMaster;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemItemMenu;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCategory;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemGroupItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemViewItemStat;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSourcingAndDispatching;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCreateItemStockInNote;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSystemSettings;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDBConnSettings;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAccountSettings;
        private System.Windows.Forms.ToolStripMenuItem 工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemEbayFees;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMessage;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemReplyMessage;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTransaction;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemViewTransactionDetail;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemViewTransactionMessage;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSendMessage;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAddRelationToItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemViewItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSelectShippingService;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemMarkAsShipped;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemUploadTrackingNum;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemLeaveFeedback;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSetShippingCost;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemMessageTemplate;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCreateDeliveryNote;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemViewDeliveryNote;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCreateDeliveryNoteFromOrders;
        private System.Windows.Forms.RadioButton radioButtonDeliveredOrders;
        private System.Windows.Forms.RadioButton radioButtonPendingOrders;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemViewItemStockInNote;
        private System.Windows.Forms.Button buttonExportSelectedToExcel;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAbout;
        private System.Windows.Forms.Button buttonListingLastPage;
        private System.Windows.Forms.Button buttonListingFirstPage;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCreateSupplier;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemViewSupplier;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCreateSourcingNote;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemMergeOrders;
        private PagedDataGridView pagedDgvMessages;
        private System.Windows.Forms.Button buttonExportEbayCVS;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemViewSourcingNote;
    }
}

