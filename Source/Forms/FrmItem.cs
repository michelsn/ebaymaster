using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EbayMaster
{
    // Form used to:
    //      1) View all existing items.
    //      2) View a specific item.
    //      3) Create new items, either by creating from blank or create by duplicating.
    //      4) Modify an existing item.
    //      5) Delete an existing item.
    public partial class FrmItem : Form
    {
        // Form editing mode.
        public enum FrmItemEditMode { Invalid, CreateNew, DupExisting, ViewItem };

        // The node tag type.
        public class ItemCompactInfo
        {
            public int ItemId;
            public int CategoryId;
            public string ItemName;
            public string ItemSKU;

            public ItemCompactInfo(int itemId, int catId, string itemName, string itemSKU)
            {
                this.ItemId = itemId;
                this.CategoryId = catId;
                this.ItemName = itemName;
                this.ItemSKU = itemSKU;
            }
        }   // class ItemCompactInfo

        // Caller may requested to view a specific item, this is the item SKU.
        public String ItemSKU = null;

        // Current edit mode.
        public FrmItemEditMode mEditMode = FrmItemEditMode.ViewItem;

        public DataTable mItemSuppliersTbl = null;

        #region Constructor and load

        public FrmItem()
        {
            InitializeComponent();
        }

        // The form 
        private void ShowItemWithSKU(String itemSKU)
        {
            DataTable dtItem = ItemDAL.GetItemTableBySKU(itemSKU);
            TreeNode[] nodes = this.treeViewCategories.Nodes.Find(itemSKU, true);
            if (nodes.Length == 0)
                return;

            this.treeViewCategories.SelectedNode = nodes[0];
        }

        private readonly int SupplierIdIndex = 0;
        private readonly int SupplierNameIndex = 1;
        private readonly int URLIndex = 2;
        private readonly int PriceIndex = 3;
        private readonly int ShippingFeeIndex = 4;
        private readonly int CommentIndex = 5;

        private void InitItemSupplierDgvColumns()
        {
            this.dgvItemSuppliers.AutoGenerateColumns = false;

            this.dgvItemSuppliers.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("SupplierId", "", typeof(String), 10, false));

            this.dgvItemSuppliers.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("SupplierName", "供应商", typeof(String), 150, true));

            this.dgvItemSuppliers.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("URL", "网址", typeof(String), 200, true));

            this.dgvItemSuppliers.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("Price", "价格", typeof(Double), 60, true));

            this.dgvItemSuppliers.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("ShippingFee", "运费", typeof(Double), 60, true));

            this.dgvItemSuppliers.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("Comment", "备注", typeof(String), 120, true));

            //this.dgvItemSuppliers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemSuppliers.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dgv_RowPrePaint);
        }

        private void dgv_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
        }

        private void InitItemSupplierTable()
        {
            mItemSuppliersTbl = new DataTable();
            mItemSuppliersTbl.Columns.Add("SupplierId", typeof(int));
            mItemSuppliersTbl.Columns.Add("SupplierName", typeof(String));
            mItemSuppliersTbl.Columns.Add("URL", typeof(String));
            mItemSuppliersTbl.Columns.Add("Price", typeof(Double));
            mItemSuppliersTbl.Columns.Add("ShippingFee", typeof(Double));
            mItemSuppliersTbl.Columns.Add("Comment", typeof(String));
        }

        private void FrmItem_Load(object sender, EventArgs e)
        {
            LoadAllCategories();
            LoadAllItems();

            InitItemSupplierDgvColumns();
            InitItemSupplierTable();

            if (ItemSKU != null && ItemSKU != "")
            {
                ShowItemWithSKU(ItemSKU);
                this.treeViewCategories.Enabled = false;
            }

            ResetButtonAndStatus();
        }

        private void LoadAllItems()
        {
            DataTable dtItems = ItemDAL.GetAllItems();
            if (dtItems.Rows.Count == 0)
                return;

            foreach (DataRow dr in dtItems.Rows)
            {
                int itemId = StringUtil.GetSafeInt(dr["ItemId"]);
                int itemCatId = StringUtil.GetSafeInt(dr["CategoryId"]);
                string itemName = StringUtil.GetSafeString(dr["ItemName"]);
                string itemSKU = StringUtil.GetSafeString(dr["ItemSKU"]);

                ItemCompactInfo itemInfo = new ItemCompactInfo(itemId, itemCatId, itemName, itemSKU);
                TreeNode itemNode = new TreeNode();
                itemNode.Text = string.Format("[{0}] {1}", itemSKU, itemName);
                itemNode.Tag = itemInfo;
                itemNode.Name = itemSKU;

                TreeNode[] nodes = this.treeViewCategories.Nodes.Find(itemCatId.ToString(), true);
                if (nodes.Length == 1)
                {
                    nodes[0].Nodes.Add(itemNode);
                }
            }
        }

        private void LoadAllCategories()
        {
            DataTable dtCategories = ItemCategoryDAL.GetAllCategories();
            if (dtCategories.Rows.Count == 0)
                return;

            this.treeViewCategories.Nodes.Clear();

            for (int itNum = 0; itNum < 2; ++itNum)
            {
                foreach (DataRow dr in dtCategories.Rows)
                {
                    int catId = StringUtil.GetSafeInt(dr["CategoryId"]);
                    int parentCatId = StringUtil.GetSafeInt(dr["ParentCategoryId"]);
                    string catName = StringUtil.GetSafeString(dr["CategoryName"]);

                    ItemCategoryType catType = new ItemCategoryType();
                    catType.CategoryId = catId;
                    catType.ParentCategoryId = parentCatId;
                    catType.CategoryName = catName;

                    TreeNode newNode = new TreeNode();
                    newNode.Tag = catType;
                    newNode.Text = catName;
                    newNode.Name = catId.ToString();

                    if (parentCatId == -1 && itNum == 0)
                    {
                        this.treeViewCategories.Nodes.Add(newNode);
                    }
                    else if (parentCatId != -1 && itNum == 1)
                    {
                        TreeNode[] parentNodes = this.treeViewCategories.Nodes.Find(parentCatId.ToString(), true);
                        if (parentNodes.Length == 1)
                            parentNodes[0].Nodes.Add(newNode);
                    }
                }
            }

            this.treeViewCategories.ExpandAll();
            return;
        }

        #endregion

        #region Get Item info from UI controls

        private InventoryItemType GetItemInfoFromUI(bool isInsert)
        {
            InventoryItemType item = new InventoryItemType();

            TreeNode node = this.treeViewCategories.SelectedNode;
            if (node == null)
            {
                MessageBox.Show("请选择商品类别");
                return null;
            }

            if (isInsert)
            {
                if (node.Tag.GetType() != typeof(ItemCategoryType))
                {
                    MessageBox.Show("请选择商品类别");
                    return null;
                }

                ItemCategoryType category = (ItemCategoryType)node.Tag;
                if (category == null)
                {
                    MessageBox.Show("请选择商品类别");
                    return null;
                }

                item.CategoryId = category.CategoryId;
                if (item.CategoryId <= 0)
                {
                    MessageBox.Show("请选择商品类别");
                    return null;
                }
            }
            else
            {
                // modify
                if (node.Tag.GetType() != typeof(ItemCompactInfo))
                {
                    MessageBox.Show("请选择商品");
                    return null;
                }
            }

            item.ItemName = this.textBoxItemName.Text;
            if (item.ItemName == "")
            {
                MessageBox.Show("请输入商品名");
                return null;
            }

            item.ItemSKU = this.textBoxItemSKU.Text;
            if (item.ItemSKU == "")
            {
                MessageBox.Show("请输入商品SKU");
                return null;
            }

            if (this.textBoxItemImagePath.Text.Trim() == "")
            {
                if (isInsert)
                {
                    MessageBox.Show("请选择商品图片");
                    return null;
                }
            }
            else
            {
                string imagePath = this.textBoxItemImagePath.Text.Trim();
                if (File.Exists(imagePath) == false)
                {
                    MessageBox.Show("商品图片路径错误.");
                    return null;
                }

                //FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                //byte[] buffer = new byte[fs.Length];
                //fs.Read(buffer, 0, buffer.Length);
                //fs.Close();
                //item.ItemImage = buffer;
                item.ItemImagePath = imagePath;
            }

            if (this.textBoxItemStockShreshold.Text == "")
            {
                MessageBox.Show("商品库存警戒值错误");
                return null;
            }

            item.ItemStockShresholdNum = Int32.Parse(this.textBoxItemStockShreshold.Text.Trim());

            string itemStockStr = this.textBoxItemStock.Text;
            if (itemStockStr.Trim() == "")
            {
                MessageBox.Show("商品库存值错误");
                return null;
            }

            item.ItemStockNum = StringUtil.GetSafeInt(itemStockStr);

            //item.ItemSourcingInfo = this.textBoxItemSourcingInfo.Text;

            //item.ItemSourcingURL = this.textBoxSourcingURL.Text;

            //string itemCostStr = this.textBoxItemCost.Text.Trim();
            //if (itemCostStr == "")
            //{
            //    MessageBox.Show("商品成本值错误");
            //    return null;
            //}
            //item.ItemCost = StringUtil.GetSafeDouble(itemCostStr);

            string itemWeightStr = this.textBoxItemWeight.Text.Trim();
            if (itemWeightStr == "")
            {
                MessageBox.Show("商品重量值错误");
                return null;
            }
            item.ItemWeight = StringUtil.GetSafeDouble(itemWeightStr);

            item.ItemCustomName = this.textBoxCustomName.Text.Trim();

            string itemCustomWeightStr = this.textBoxItemCustomWeight.Text.Trim();
            if (itemCustomWeightStr == "")
                item.ItemCustomWeight = 0.0;
            else
                item.ItemCustomWeight = StringUtil.GetSafeDouble(itemCustomWeightStr);

            string itemCustomCostStr = this.textBoxItemCustomValue.Text.Trim();
            if (itemCustomCostStr == "")
                item.ItemCustomCost = 0.0;
            else
                item.ItemCustomCost = StringUtil.GetSafeDouble(itemCustomCostStr);

            item.ItemAddDateTime = DateTime.Now;

            // IsGroupItem
            item.IsGroupItem = false;
            item.SubItemSKUs = "";

            return item;
        }

        public Image ResizeImg(Image img, int width, int height)
        {
            //create a new Bitmap the size of the new image
            Bitmap bmp = new Bitmap(width, height);
            //create a new graphic from the Bitmap
            Graphics graphic = Graphics.FromImage((Image)bmp);
            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //draw the newly resized image
            graphic.DrawImage(img, 0, 0, width, height);
            //dispose and free up the resources
            graphic.Dispose();
            //return the image
            return (Image)bmp;
        }

        /// <summary>
        /// method for resizing an image
        /// </summary>
        /// <param name="img">the image to resize</param>
        /// <param name="percentage">Percentage of change (i.e for 105% of the original provide 105)</param>
        /// <returns></returns>
        public Image ResizeImg(Image img, int percentage)
        {
            //get the height and width of the image
            int originalW = img.Width;
            int originalH = img.Height;

            //get the new size based on the percentage change
            int resizedW = (int)(originalW * percentage);
            int resizedH = (int)(originalH * percentage);

            //create a new Bitmap the size of the new image
            Bitmap bmp = new Bitmap(resizedW, resizedH);
            //create a new graphic from the Bitmap
            Graphics graphic = Graphics.FromImage((Image)bmp);
            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //draw the newly resized image
            graphic.DrawImage(img, 0, 0, resizedW, resizedH);
            //dispose and free up the resources
            graphic.Dispose();
            //return the image
            return (Image)bmp;
        }

        private void buttonNavigateItemImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "图片文件(*.jpg,*.gif,*.bmp)|*.jpg;*.gif;*.bmp ";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filePath = dlg.FileName;
                this.textBoxItemImagePath.Text = filePath;
                Image image = Image.FromFile(filePath);
                Image resizedImage = ResizeImg(image, this.pictureBoxItemPic.Width, this.pictureBoxItemPic.Height);

                this.pictureBoxItemPic.Image = resizedImage;

            }
        }

        #endregion

        #region Add/Modify Item

        // Add a new item to database.
        //  Either by create new or duplicate existing.
        private void AddItem()
        {
            InventoryItemType item = GetItemInfoFromUI(true/*insert*/);
            if (item == null)
                return;

            int newItemId = ItemDAL.InsertOneItem(item);

            if (newItemId <= 0)
            {
                MessageBox.Show("不能添加新商品，可能商品的SKU和已有商品重复了!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Add this item to tree view.
            TreeNode[] nodes = this.treeViewCategories.Nodes.Find(item.CategoryId.ToString(), true);
            if (nodes.Length == 1)
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = string.Format("[{0}] {1}", item.ItemSKU, item.ItemName);
                ItemCompactInfo itemInfo = new ItemCompactInfo(newItemId, item.CategoryId, item.ItemName, item.ItemSKU);
                newNode.Tag = itemInfo;
                nodes[0].Nodes.Add(newNode);
            }

            MessageBox.Show("恭喜，添加新商品成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Modify an existing item.
        //
        private void ModifyItem()
        {
            // Should be refactored.
            TreeNode node = treeViewCategories.SelectedNode;
            if (node == null || node.Tag == null)
                return;

            if (node.Tag.GetType() != typeof(ItemCompactInfo))
                return;

            ItemCompactInfo itemInfo = (ItemCompactInfo)(node.Tag);
            if (itemInfo == null)
                return;

            InventoryItemType item = ItemDAL.GetItemById(itemInfo.ItemId);
            if (item == null)
                return;

            InventoryItemType newItemInfo = GetItemInfoFromUI(false);
            if (newItemInfo == null)
                return;

            // CAUIION: we don't allow user to change the stock number directly.
            // ZHI_TODO:
            //newItemInfo.ItemStockNum = item.ItemStockNum;

            newItemInfo.ItemId = item.ItemId;
            newItemInfo.CategoryId = item.CategoryId;
            if (newItemInfo.ItemImagePath == null)
                newItemInfo.ItemImagePath = item.ItemImagePath;
            newItemInfo.ItemNote = item.ItemNote;

            if (MessageBox.Show(string.Format("确认修改商品 {0}", itemInfo.ItemName), "请确认", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == System.Windows.Forms.DialogResult.Yes)
            {
                bool result = ItemDAL.ModifyOneItem(newItemInfo);
                if (result)
                {
                    MessageBox.Show("修改商品成功", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    itemInfo.ItemName = newItemInfo.ItemName;
                    itemInfo.ItemSKU = newItemInfo.ItemSKU;
                    node.Tag = itemInfo;
                    if (newItemInfo.ItemName != item.ItemName)
                    {
                        node.Text = string.Format("[{0}] {1}", newItemInfo.ItemSKU, newItemInfo.ItemName);
                    }

                }
                else
                {
                    MessageBox.Show("修改商品失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonAddOrModifyItem_Click(object sender, EventArgs e)
        {
            if (mEditMode == FrmItemEditMode.CreateNew ||
                mEditMode == FrmItemEditMode.DupExisting)
            {
                AddItem();
            }
            else if (mEditMode == FrmItemEditMode.ViewItem)
            {
                ModifyItem();
            }

            ResetButtonAndStatus();
        }

        private void buttonDelItem_Click(object sender, EventArgs e)
        {
            if (buttonDelItem.Enabled == false)
                return;

            // Should be refactored.
            TreeNode node = treeViewCategories.SelectedNode;
            if (node == null || node.Tag == null)
                return;

            if (node.Tag.GetType() != typeof(ItemCompactInfo))
                return;

            ItemCompactInfo itemInfo = (ItemCompactInfo)(node.Tag);
            if (itemInfo == null)
                return;

            if (MessageBox.Show(string.Format("确认删除商品 {0}", itemInfo.ItemName), "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == System.Windows.Forms.DialogResult.Yes)
            {
                bool result = ItemDAL.DeleteOneItem(itemInfo.ItemId);
                if (result)
                {
                    MessageBox.Show("删除商品成功", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    treeViewCategories.Nodes.Remove(node);
                }
                else
                {
                    MessageBox.Show("删除商品失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            ResetButtonAndStatus();
        }

        // Reset all control content.
        private void ClearAllControls()
        {
            this.textBoxItemName.Text = "";
            this.textBoxItemSKU.Text = "";
            this.textBoxItemImagePath.Text = "";
            this.pictureBoxItemPic.Image = null;
            this.textBoxItemWeight.Text = "";
            this.textBoxCustomName.Text = "";
            this.textBoxItemCustomWeight.Text = "";
            this.textBoxItemCustomValue.Text = "";
            this.textBoxItemStockShreshold.Text = "";
            this.textBoxItemStock.Text = "";
        }

        private void buttonNewItem_Click(object sender, EventArgs e)
        {
            ClearAllControls();

            this.mEditMode = FrmItemEditMode.CreateNew;
            this.buttonNewItem.Enabled = false;
            this.buttonDupItem.Enabled = false;
            this.buttonDelItem.Enabled = false;
            this.buttonCancel.Visible = true;

            this.buttonAddOrModifyItem.Text = "完成新增";
            this.labelStatus.Text = "当前状态：空白新增商品信息";
        }

        private void buttonDupItem_Click(object sender, EventArgs e)
        {
            this.mEditMode = FrmItemEditMode.DupExisting;
            this.buttonNewItem.Enabled = false;
            this.buttonDupItem.Enabled = false;
            this.buttonDelItem.Enabled = false;

            this.buttonCancel.Visible = true;
            this.labelCurrentStock.Visible = false;
            this.textBoxItemStock.Visible = false;

            this.labelStatus.Text = "当前状态：复制新增商品信息";
            this.buttonAddOrModifyItem.Text = "完成新增";
        }

        private void ResetButtonAndStatus()
        {
            this.mEditMode = FrmItemEditMode.ViewItem;
            this.buttonNewItem.Enabled = true;
            this.buttonDupItem.Enabled = true;
            this.buttonDelItem.Enabled = true;

            this.buttonCancel.Visible = false;
            this.labelCurrentStock.Visible = false;
            this.textBoxItemStock.Visible = false;

            this.labelStatus.Text = "当前状态：浏览商品信息";
            this.buttonAddOrModifyItem.Text = "完成修改";
        }

        #endregion

        #region TreeView event handlers

        private void LoadItemSuppliers(int itemId)
        {
            mItemSuppliersTbl.Clear();

            DataTable dtSuppliers = ItemSupplierDAL.GetAllItemSuppliersByItemId(itemId);
            foreach (DataRow row in dtSuppliers.Rows)
            {
                int supplierId = StringUtil.GetSafeInt(row["SupplierId"]);
                SupplierType supplier = SupplierDAL.GetSupplierById(supplierId);
                if (supplier == null)
                    continue;
                DataRow rowLoc = mItemSuppliersTbl.NewRow();
                rowLoc["SupplierId"] = supplierId;
                rowLoc["SupplierName"] = supplier.SupplierName;
                rowLoc["URL"] = StringUtil.GetSafeString(row["SouringURL"]);
                rowLoc["Price"] = StringUtil.GetSafeDouble(row["Price"]);
                rowLoc["ShippingFee"] = StringUtil.GetSafeDouble(row["ShippingFee"]);
                rowLoc["Comment"] = StringUtil.GetSafeString(row["Comment"]);
                mItemSuppliersTbl.Rows.Add(rowLoc);
            }

            this.dgvItemSuppliers.DataSource = mItemSuppliersTbl;
        }

        private int GetSelectedItemId()
        {
            TreeNode node = this.treeViewCategories.SelectedNode;
            if (node.Tag == null)
                return -1;

            if (node.Tag.GetType() != typeof(ItemCompactInfo))
                return -1;

            ItemCompactInfo itemInfo = (ItemCompactInfo)(node.Tag);
            if (itemInfo == null)
                return -1;

            int itemId = itemInfo.ItemId;
            return itemId;
        }

        private void treeViewCategories_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.buttonDelItem.Enabled = false;

            int itemId = GetSelectedItemId();
            if (itemId <= 0)
                return;

            DataTable dtItem = ItemDAL.GetItemTableById(itemId);

            if (dtItem.Rows.Count == 0)
                return;

            DataRow dr = dtItem.Rows[0];

            this.textBoxItemName.Text = StringUtil.GetSafeString(dr["ItemName"]);
            this.textBoxItemSKU.Text = StringUtil.GetSafeString(dr["ItemSKU"]);
            this.textBoxItemStockShreshold.Text = StringUtil.GetSafeString(dr["ItemStockShresholdNum"]);
            this.textBoxItemStock.Text = StringUtil.GetSafeString(dr["ItemStockNum"]);
            //this.textBoxItemSourcingInfo.Text = StringUtil.GetSafeString(dr["ItemSourcingInfo"]);
            //this.textBoxSourcingURL.Text = StringUtil.GetSafeString(dr["ItemSourcingURL"]);
            //this.textBoxItemCost.Text = StringUtil.GetSafeString(dr["ItemCost"]);
            this.textBoxItemWeight.Text = StringUtil.GetSafeString(dr["ItemWeight"]);
            this.textBoxCustomName.Text = StringUtil.GetSafeString(dr["ItemCustomName"]);
            this.textBoxItemCustomWeight.Text = StringUtil.GetSafeString(dr["ItemCustomWeight"]);
            this.textBoxItemCustomValue.Text = StringUtil.GetSafeString(dr["ItemCustomCost"]);

            String imagePath = StringUtil.GetSafeString(dr["ItemImagePath"]);
            this.pictureBoxItemPic.Image = null;
            this.textBoxItemImagePath.Text = "";
            if (File.Exists(imagePath))
            {
                Image rawImage = Image.FromFile(imagePath);
                if (rawImage != null)
                {
                    int width = this.pictureBoxItemPic.Width;
                    int height = this.pictureBoxItemPic.Height;
                    Image image = ResizeImg(rawImage, width, height);
                    this.pictureBoxItemPic.Image = image;
                }
                this.textBoxItemImagePath.Text = imagePath;
            }

            LoadItemSuppliers(itemId);

           this.buttonDelItem.Enabled = true;
           this.buttonAddOrModifyItem.Text = "完成修改";
        }

        private void treeViewCategories_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            TreeNode node = treeViewCategories.GetNodeAt(e.Location);
            if (node == null)
                return;

            if (node.Tag == null)
                return;

            if (node.Tag.GetType() != typeof(ItemCompactInfo))
                return;

            ItemCompactInfo itemInfo = (ItemCompactInfo)(node.Tag);
            if (itemInfo == null)
                return;

            contextMenuStripItem.Show(treeViewCategories, e.X, e.Y); ;
        }

        private void ToolStripMenuItemDelItem_Click(object sender, EventArgs e)
        {
            TreeNode node = treeViewCategories.SelectedNode;
            if (node == null)
                return;

            if (node.Tag == null)
                return;

            if (node.Tag.GetType() != typeof(ItemCompactInfo))
                return;

            ItemCompactInfo itemInfo = (ItemCompactInfo)(node.Tag);
            if (itemInfo == null)
                return;

            if (MessageBox.Show(string.Format("确认删除商品 {0}", itemInfo.ItemName), "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == System.Windows.Forms.DialogResult.Yes)
            {
                bool result = ItemDAL.DeleteOneItem(itemInfo.ItemId);
                if (result)
                {
                    MessageBox.Show("删除商品成功", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    treeViewCategories.Nodes.Remove(node);
                }
                else
                {
                    MessageBox.Show("删除商品失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        #endregion

        //private void buttonNavigateItemSourcingURL_Click(object sender, EventArgs e)
        //{
        //    string sourcingURL = this.textBoxSourcingURL.Text;
        //    if (sourcingURL == null || sourcingURL.Trim().Length == 0)
        //    {
        //        MessageBox.Show("网址为空", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    if (!sourcingURL.StartsWith("http://"))
        //        sourcingURL = string.Format("http://{0}", sourcingURL);
        //    System.Diagnostics.Process.Start(sourcingURL);
        //}

        #region Misc

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

        #endregion

        private void buttonDelSupplier_Click(object sender, EventArgs e)
        {
            DataGridViewCell cell = this.dgvItemSuppliers.CurrentCell;
            if (cell == null)
                return;

            DataGridViewRow dgvRow = cell.OwningRow;
            int supplierId = StringUtil.GetSafeInt(dgvRow.Cells[SupplierIdIndex].Value);

            DataRow row = null;

            int rowCount = mItemSuppliersTbl.Rows.Count;
            for (int ii = 0; ii < rowCount; ++ii)
            {
                DataRow rowLoc = mItemSuppliersTbl.Rows[ii];
                if (StringUtil.GetSafeInt(rowLoc["SupplierId"]) == supplierId)
                {
                    row = rowLoc;
                }
            }

            if (row != null)
                mItemSuppliersTbl.Rows.Remove(row);

            buttonSaveItemSupplier_Click(sender, e);
            MessageBox.Show("删除供应商信息成功", "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        private void buttonSaveItemSupplier_Click(object sender, EventArgs e)
        {
            List<ItemSupplierType> itemSupplierList = new List<ItemSupplierType>();

            int itemId = GetSelectedItemId();
            if (itemId <= 0)
                return;

            // Delete all suppliers for this item.
            ItemSupplierDAL.DeleteItemSuppliers(itemId);

            foreach (DataGridViewRow row in dgvItemSuppliers.Rows)
            {
                int supplierId = StringUtil.GetSafeInt(row.Cells[SupplierIdIndex].Value);
                String URL = StringUtil.GetSafeString(row.Cells[URLIndex].Value);
                Double price = StringUtil.GetSafeDouble(row.Cells[PriceIndex].Value);
                Double shippingFee = StringUtil.GetSafeDouble(row.Cells[ShippingFeeIndex].Value);
                String comment = StringUtil.GetSafeString(row.Cells[CommentIndex].Value);

                if (supplierId <= 0 || URL == "")
                    continue;

                ItemSupplierType itemSupplier = new ItemSupplierType();
                itemSupplier.ItemId = itemId;
                itemSupplier.SupplierId = supplierId;
                itemSupplier.SourcingURL = URL;
                itemSupplier.Price = price;
                itemSupplier.ShippingFee = shippingFee;
                itemSupplier.Comment = comment;
                itemSupplier.CreatedDate = DateTime.Now;

                itemSupplierList.Add(itemSupplier);
            }

            foreach (ItemSupplierType itemSupplier in itemSupplierList)
            {
                ItemSupplierDAL.AddOneItemSupplier(itemSupplier);
            }

            MessageBox.Show("保存供应商信息成功", "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvItemSuppliers_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != SupplierNameIndex)
                return;

            DataGridViewRow row = this.dgvItemSuppliers.CurrentCell.OwningRow;
            if (row == null)
                return;

            FrmSupplierList frm = new FrmSupplierList(SupplierDlgMode.SelectSupplier);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();

            if (frm.mSelectedSupplier == null)
                return;

            row.Cells[SupplierNameIndex].Value = frm.mSelectedSupplier.SupplierName;
            row.Cells[SupplierIdIndex].Value = frm.mSelectedSupplier.SupplierID;
        }

        private void dgvItemSuppliers_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvItemSuppliers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow row = this.dgvItemSuppliers.Rows[e.RowIndex];
            if (StringUtil.GetSafeInt(row.Cells[SupplierIdIndex].Value) <= 0)
                return;

            this.buttonDelSupplier.Enabled = true;
        }

        private void dgvItemSuppliers_SelectionChanged(object sender, EventArgs e)
        {
            this.buttonDelSupplier.Enabled = false;
        }

        //
        // Drag & Drop
        // How to add TreeView drag-and-drop functionality in a Visual C# application
        // http://support.microsoft.com/kb/307968
        //

        private void treeViewCategories_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void treeViewCategories_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void treeViewCategories_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode oldNode;

			if(e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
			{
				Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
				TreeNode targetNode = ((TreeView)sender).GetNodeAt(pt);

                object tag = targetNode.Tag;
                if (tag == null || tag.GetType() != typeof(ItemCategoryType))
                    return;
                ItemCategoryType catInfo = (ItemCategoryType)tag;
                
                oldNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                if (oldNode.Tag==null || oldNode.Tag.GetType()!=typeof(ItemCompactInfo))
                    return;

                ItemCompactInfo itemInfo = (ItemCompactInfo)oldNode.Tag;
                if (itemInfo == null)
                    return;

                InventoryItemType item = ItemDAL.GetItemById(itemInfo.ItemId);
                if (item == null)
                    return;

                if (item.CategoryId == catInfo.CategoryId)
                    return;

                ItemDAL.ModifyItemCategory(item.ItemId, catInfo.CategoryId);

                targetNode.Nodes.Add((TreeNode)oldNode.Clone());
                targetNode.Expand();
                //Remove Original Node
                oldNode.Remove();
			}
		}
    }
}
