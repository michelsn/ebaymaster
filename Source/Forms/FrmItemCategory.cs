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
    public partial class FrmItemCategory : Form
    {
        private int maxSkuPrefix = 100;
        
        public FrmItemCategory()
        {
            InitializeComponent();
        }

        private void LoadAllCategories()
        {
            DataTable dtCategories = ItemCategoryDAL.GetAllCategories();
            if (dtCategories.Rows.Count == 0)
                return;

            this.treeViewAllCategories.Nodes.Clear();

            foreach (DataRow dr in dtCategories.Rows)
            {
                int catId = StringUtil.GetSafeInt(dr["CategoryId"]);
                int parentCatId = StringUtil.GetSafeInt(dr["ParentCategoryId"]);
                string catName = StringUtil.GetSafeString(dr["CategoryName"]);
                string catSkuPrefix = StringUtil.GetSafeString(dr["CategorySkuPrefix"]);

                ItemCategoryType catType = new ItemCategoryType();
                catType.CategoryId = catId;
                catType.ParentCategoryId = parentCatId;
                catType.CategoryName = catName;
                catType.CategorySkuPrefix = catSkuPrefix;

                TreeNode newNode = new TreeNode();
                newNode.Tag = catType;
                newNode.Text = catName;
                newNode.Name = catId.ToString();

                if (parentCatId == -1)
                {
                    this.treeViewAllCategories.Nodes.Add(newNode);
                }
                else
                {
                    TreeNode[] parentNodes = this.treeViewAllCategories.Nodes.Find(parentCatId.ToString(), true);
                    if (parentNodes.Length == 1)
                        parentNodes[0].Nodes.Add(newNode);
                }

                int catSkuPrefixVal = -1;
                if (Int32.TryParse(catSkuPrefix, out catSkuPrefixVal) && catSkuPrefixVal > maxSkuPrefix)
                {
                    maxSkuPrefix = catSkuPrefixVal;
                }
            }

            this.treeViewAllCategories.ExpandAll();
            return;
        }

        private void buttonAddNewCategory_Click(object sender, EventArgs e)
        {
            int parentCategoryId = -1;
            if (this.textBoxParentCategory.Text == "无" || this.textBoxParentCategory.Text.Trim() == "")
                parentCategoryId = -1;
            else
            {
                TreeNode treeNode = this.treeViewAllCategories.SelectedNode;
                if (treeNode != null)
                {
                    if (treeNode.Tag != null)
                    {
                        ItemCategoryType parentCatType = (ItemCategoryType)treeNode.Tag;
                        if (parentCatType != null)
                            parentCategoryId = parentCatType.CategoryId;
                    }
                }
            }

            string categoryName = this.textBoxCategoryName.Text;
            if (categoryName == "")
            {
                MessageBox.Show("请输入类别名称");
                return;
            }

            ItemCategoryType newCategory = new ItemCategoryType();
            newCategory.ParentCategoryId = parentCategoryId;
            newCategory.CategoryName = categoryName;
            newCategory.CategorySkuPrefix = string.Format("{0}", maxSkuPrefix + 1);
            ItemCategoryDAL.InsertOneCategory(newCategory);

            LoadAllCategories();

            MessageBox.Show("添加新类别成功！", "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonDeleteCategory_Click(object sender, EventArgs e)
        {
            TreeNode treeNode = this.treeViewAllCategories.SelectedNode;
            if (treeNode != null)
            {
                int categoryId = -1;
                if (treeNode.Tag != null)
                {
                    ItemCategoryType categoryType = (ItemCategoryType)treeNode.Tag;
                    if (categoryType != null)
                        categoryId = categoryType.CategoryId;
                }

                bool result = ItemCategoryDAL.DeleteOneCategory(categoryId);
                if (result)
                {
                    // Remove from category tree.
                    this.treeViewAllCategories.Nodes.Remove(treeNode);

                    MessageBox.Show("删除类别成功！\r\n注意其子类别已经一并被删除!", "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("删除类别失败", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void treeViewAllCategories_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = this.treeViewAllCategories.SelectedNode;
            if (node == null)
            {
                this.textBoxParentCategory.Text = "无";
                return;
            }

            ItemCategoryType selectedCatType = (ItemCategoryType)node.Tag;
            if (selectedCatType == null)
            {
                this.textBoxParentCategory.Text = "无";
                return;
            }

            this.textBoxParentCategory.Text = selectedCatType.CategoryName;
        }

        private void FrmItemCategory_Load(object sender, EventArgs e)
        {
            // Load all item categories on dialog show up.
            LoadAllCategories();
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
