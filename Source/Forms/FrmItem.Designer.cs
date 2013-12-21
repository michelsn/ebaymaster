namespace EbayMaster
{
    partial class FrmItem
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
            this.treeViewCategories = new System.Windows.Forms.TreeView();
            this.contextMenuStripItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemDelItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonDelItem = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxItemImagePath = new System.Windows.Forms.TextBox();
            this.textBoxItemCustomValue = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxItemCustomWeight = new System.Windows.Forms.TextBox();
            this.buttonAddOrModifyItem = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxCustomName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxItemWeight = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxItemStock = new System.Windows.Forms.TextBox();
            this.labelCurrentStock = new System.Windows.Forms.Label();
            this.textBoxItemStockShreshold = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonNavigateItemImage = new System.Windows.Forms.Button();
            this.pictureBoxItemPic = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxItemSKU = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxItemName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonDelSupplier = new System.Windows.Forms.Button();
            this.buttonSaveItemSupplier = new System.Windows.Forms.Button();
            this.dgvItemSuppliers = new System.Windows.Forms.DataGridView();
            this.buttonDupItem = new System.Windows.Forms.Button();
            this.buttonNewItem = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.contextMenuStripItem.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxItemPic)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemSuppliers)).BeginInit();
            this.SuspendLayout();
            // 
            // treeViewCategories
            // 
            this.treeViewCategories.AllowDrop = true;
            this.treeViewCategories.Location = new System.Drawing.Point(23, 59);
            this.treeViewCategories.Name = "treeViewCategories";
            this.treeViewCategories.Size = new System.Drawing.Size(255, 435);
            this.treeViewCategories.TabIndex = 26;
            this.treeViewCategories.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeViewCategories_ItemDrag);
            this.treeViewCategories.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewCategories_AfterSelect);
            this.treeViewCategories.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewCategories_NodeMouseClick);
            this.treeViewCategories.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeViewCategories_DragDrop);
            this.treeViewCategories.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeViewCategories_DragEnter);
            // 
            // contextMenuStripItem
            // 
            this.contextMenuStripItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemDelItem});
            this.contextMenuStripItem.Name = "contextMenuStripItem";
            this.contextMenuStripItem.Size = new System.Drawing.Size(125, 26);
            // 
            // ToolStripMenuItemDelItem
            // 
            this.ToolStripMenuItemDelItem.Name = "ToolStripMenuItemDelItem";
            this.ToolStripMenuItemDelItem.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItemDelItem.Text = "删除商品";
            this.ToolStripMenuItemDelItem.Click += new System.EventHandler(this.ToolStripMenuItemDelItem_Click);
            // 
            // buttonDelItem
            // 
            this.buttonDelItem.Enabled = false;
            this.buttonDelItem.Location = new System.Drawing.Point(192, 20);
            this.buttonDelItem.Name = "buttonDelItem";
            this.buttonDelItem.Size = new System.Drawing.Size(75, 23);
            this.buttonDelItem.TabIndex = 2;
            this.buttonDelItem.Text = "删除商品";
            this.buttonDelItem.UseVisualStyleBackColor = true;
            this.buttonDelItem.Click += new System.EventHandler(this.buttonDelItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(300, 21);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(653, 507);
            this.tabControl1.TabIndex = 33;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonCancel);
            this.tabPage1.Controls.Add(this.textBoxItemImagePath);
            this.tabPage1.Controls.Add(this.textBoxItemCustomValue);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.textBoxItemCustomWeight);
            this.tabPage1.Controls.Add(this.buttonAddOrModifyItem);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.textBoxCustomName);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.textBoxItemWeight);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.textBoxItemStock);
            this.tabPage1.Controls.Add(this.labelCurrentStock);
            this.tabPage1.Controls.Add(this.textBoxItemStockShreshold);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.buttonNavigateItemImage);
            this.tabPage1.Controls.Add(this.pictureBoxItemPic);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.textBoxItemSKU);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.textBoxItemName);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(645, 481);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "商品基本信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.SystemColors.Control;
            this.buttonCancel.Location = new System.Drawing.Point(488, 328);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Visible = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxItemImagePath
            // 
            this.textBoxItemImagePath.Location = new System.Drawing.Point(101, 121);
            this.textBoxItemImagePath.Name = "textBoxItemImagePath";
            this.textBoxItemImagePath.Size = new System.Drawing.Size(171, 21);
            this.textBoxItemImagePath.TabIndex = 5;
            // 
            // textBoxItemCustomValue
            // 
            this.textBoxItemCustomValue.Location = new System.Drawing.Point(390, 163);
            this.textBoxItemCustomValue.Name = "textBoxItemCustomValue";
            this.textBoxItemCustomValue.Size = new System.Drawing.Size(229, 21);
            this.textBoxItemCustomValue.TabIndex = 10;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(309, 163);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(71, 12);
            this.label12.TabIndex = 54;
            this.label12.Text = "报关价值($)";
            // 
            // textBoxItemCustomWeight
            // 
            this.textBoxItemCustomWeight.Location = new System.Drawing.Point(390, 111);
            this.textBoxItemCustomWeight.Name = "textBoxItemCustomWeight";
            this.textBoxItemCustomWeight.Size = new System.Drawing.Size(229, 21);
            this.textBoxItemCustomWeight.TabIndex = 9;
            // 
            // buttonAddOrModifyItem
            // 
            this.buttonAddOrModifyItem.BackColor = System.Drawing.SystemColors.Control;
            this.buttonAddOrModifyItem.Location = new System.Drawing.Point(390, 328);
            this.buttonAddOrModifyItem.Name = "buttonAddOrModifyItem";
            this.buttonAddOrModifyItem.Size = new System.Drawing.Size(75, 23);
            this.buttonAddOrModifyItem.TabIndex = 12;
            this.buttonAddOrModifyItem.Text = "新增完成";
            this.buttonAddOrModifyItem.UseVisualStyleBackColor = false;
            this.buttonAddOrModifyItem.Click += new System.EventHandler(this.buttonAddOrModifyItem_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(309, 111);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 12);
            this.label11.TabIndex = 52;
            this.label11.Text = "报关重量(kg)";
            // 
            // textBoxCustomName
            // 
            this.textBoxCustomName.Location = new System.Drawing.Point(390, 65);
            this.textBoxCustomName.Name = "textBoxCustomName";
            this.textBoxCustomName.Size = new System.Drawing.Size(229, 21);
            this.textBoxCustomName.TabIndex = 8;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(309, 65);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 50;
            this.label10.Text = "报关名";
            // 
            // textBoxItemWeight
            // 
            this.textBoxItemWeight.Location = new System.Drawing.Point(390, 24);
            this.textBoxItemWeight.Name = "textBoxItemWeight";
            this.textBoxItemWeight.Size = new System.Drawing.Size(229, 21);
            this.textBoxItemWeight.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(309, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 12);
            this.label9.TabIndex = 48;
            this.label9.Text = "商品重量(g)";
            // 
            // textBoxItemStock
            // 
            this.textBoxItemStock.Location = new System.Drawing.Point(390, 257);
            this.textBoxItemStock.Name = "textBoxItemStock";
            this.textBoxItemStock.ReadOnly = true;
            this.textBoxItemStock.Size = new System.Drawing.Size(229, 21);
            this.textBoxItemStock.TabIndex = 43;
            // 
            // labelCurrentStock
            // 
            this.labelCurrentStock.AutoSize = true;
            this.labelCurrentStock.Location = new System.Drawing.Point(309, 258);
            this.labelCurrentStock.Name = "labelCurrentStock";
            this.labelCurrentStock.Size = new System.Drawing.Size(53, 12);
            this.labelCurrentStock.TabIndex = 42;
            this.labelCurrentStock.Text = "当前库存";
            // 
            // textBoxItemStockShreshold
            // 
            this.textBoxItemStockShreshold.Location = new System.Drawing.Point(390, 208);
            this.textBoxItemStockShreshold.Name = "textBoxItemStockShreshold";
            this.textBoxItemStockShreshold.Size = new System.Drawing.Size(229, 21);
            this.textBoxItemStockShreshold.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(309, 212);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 40;
            this.label5.Text = "库存报警值";
            // 
            // buttonNavigateItemImage
            // 
            this.buttonNavigateItemImage.Location = new System.Drawing.Point(22, 151);
            this.buttonNavigateItemImage.Name = "buttonNavigateItemImage";
            this.buttonNavigateItemImage.Size = new System.Drawing.Size(66, 23);
            this.buttonNavigateItemImage.TabIndex = 6;
            this.buttonNavigateItemImage.Text = "浏览";
            this.buttonNavigateItemImage.UseVisualStyleBackColor = true;
            this.buttonNavigateItemImage.Click += new System.EventHandler(this.buttonNavigateItemImage_Click);
            // 
            // pictureBoxItemPic
            // 
            this.pictureBoxItemPic.Location = new System.Drawing.Point(22, 183);
            this.pictureBoxItemPic.Name = "pictureBoxItemPic";
            this.pictureBoxItemPic.Size = new System.Drawing.Size(250, 287);
            this.pictureBoxItemPic.TabIndex = 38;
            this.pictureBoxItemPic.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 37;
            this.label4.Text = "商品图片";
            // 
            // textBoxItemSKU
            // 
            this.textBoxItemSKU.Location = new System.Drawing.Point(101, 67);
            this.textBoxItemSKU.Name = "textBoxItemSKU";
            this.textBoxItemSKU.Size = new System.Drawing.Size(171, 21);
            this.textBoxItemSKU.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 35;
            this.label3.Text = "商品SKU";
            // 
            // textBoxItemName
            // 
            this.textBoxItemName.Location = new System.Drawing.Point(101, 24);
            this.textBoxItemName.Name = "textBoxItemName";
            this.textBoxItemName.Size = new System.Drawing.Size(171, 21);
            this.textBoxItemName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 33;
            this.label2.Text = "商品名称";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.buttonDelSupplier);
            this.tabPage2.Controls.Add(this.buttonSaveItemSupplier);
            this.tabPage2.Controls.Add(this.dgvItemSuppliers);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(645, 481);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "供应商";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonDelSupplier
            // 
            this.buttonDelSupplier.Enabled = false;
            this.buttonDelSupplier.Location = new System.Drawing.Point(216, 405);
            this.buttonDelSupplier.Name = "buttonDelSupplier";
            this.buttonDelSupplier.Size = new System.Drawing.Size(75, 23);
            this.buttonDelSupplier.TabIndex = 2;
            this.buttonDelSupplier.Text = "删除";
            this.buttonDelSupplier.UseVisualStyleBackColor = true;
            this.buttonDelSupplier.Click += new System.EventHandler(this.buttonDelSupplier_Click);
            // 
            // buttonSaveItemSupplier
            // 
            this.buttonSaveItemSupplier.Location = new System.Drawing.Point(357, 405);
            this.buttonSaveItemSupplier.Name = "buttonSaveItemSupplier";
            this.buttonSaveItemSupplier.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveItemSupplier.TabIndex = 1;
            this.buttonSaveItemSupplier.Text = "保存";
            this.buttonSaveItemSupplier.UseVisualStyleBackColor = true;
            this.buttonSaveItemSupplier.Click += new System.EventHandler(this.buttonSaveItemSupplier_Click);
            // 
            // dgvItemSuppliers
            // 
            this.dgvItemSuppliers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemSuppliers.Location = new System.Drawing.Point(18, 26);
            this.dgvItemSuppliers.Name = "dgvItemSuppliers";
            this.dgvItemSuppliers.RowTemplate.Height = 23;
            this.dgvItemSuppliers.Size = new System.Drawing.Size(611, 317);
            this.dgvItemSuppliers.TabIndex = 0;
            this.dgvItemSuppliers.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvItemSuppliers_CellBeginEdit);
            this.dgvItemSuppliers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemSuppliers_CellContentDoubleClick);
            this.dgvItemSuppliers.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvItemSuppliers_RowHeaderMouseClick);
            this.dgvItemSuppliers.SelectionChanged += new System.EventHandler(this.dgvItemSuppliers_SelectionChanged);
            // 
            // buttonDupItem
            // 
            this.buttonDupItem.BackColor = System.Drawing.SystemColors.Control;
            this.buttonDupItem.Location = new System.Drawing.Point(109, 20);
            this.buttonDupItem.Name = "buttonDupItem";
            this.buttonDupItem.Size = new System.Drawing.Size(75, 23);
            this.buttonDupItem.TabIndex = 1;
            this.buttonDupItem.Text = "复制新增";
            this.buttonDupItem.UseVisualStyleBackColor = false;
            this.buttonDupItem.Click += new System.EventHandler(this.buttonDupItem_Click);
            // 
            // buttonNewItem
            // 
            this.buttonNewItem.BackColor = System.Drawing.SystemColors.Control;
            this.buttonNewItem.Location = new System.Drawing.Point(23, 21);
            this.buttonNewItem.Name = "buttonNewItem";
            this.buttonNewItem.Size = new System.Drawing.Size(75, 23);
            this.buttonNewItem.TabIndex = 0;
            this.buttonNewItem.Text = "空白新增";
            this.buttonNewItem.UseVisualStyleBackColor = false;
            this.buttonNewItem.Click += new System.EventHandler(this.buttonNewItem_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.ForeColor = System.Drawing.Color.Green;
            this.labelStatus.Location = new System.Drawing.Point(21, 512);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(29, 12);
            this.labelStatus.TabIndex = 59;
            this.labelStatus.Text = "状态";
            // 
            // FrmItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 543);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonNewItem);
            this.Controls.Add(this.buttonDupItem);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.treeViewCategories);
            this.Controls.Add(this.buttonDelItem);
            this.Name = "FrmItem";
            this.Text = "商品信息 - 可拖动商品至其他类别";
            this.Load += new System.EventHandler(this.FrmItem_Load);
            this.contextMenuStripItem.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxItemPic)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemSuppliers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewCategories;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDelItem;
        private System.Windows.Forms.Button buttonDelItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox textBoxItemImagePath;
        private System.Windows.Forms.Button buttonAddOrModifyItem;
        private System.Windows.Forms.TextBox textBoxItemCustomValue;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxItemCustomWeight;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxCustomName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxItemWeight;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxItemStock;
        private System.Windows.Forms.Label labelCurrentStock;
        private System.Windows.Forms.TextBox textBoxItemStockShreshold;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonNavigateItemImage;
        private System.Windows.Forms.PictureBox pictureBoxItemPic;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxItemSKU;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxItemName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button buttonDupItem;
        private System.Windows.Forms.Button buttonNewItem;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSaveItemSupplier;
        private System.Windows.Forms.DataGridView dgvItemSuppliers;
        private System.Windows.Forms.Button buttonDelSupplier;
    }
}