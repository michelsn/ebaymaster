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
    public partial class FrmSelectItemSKU : Form
    {
        public FrmSelectItemSKU()
        {
            InitializeComponent();
        }

        public class ItemCompactInfo
        {
            public int ItemId;
            public int CategoryId;
            public string ItemName;

            public ItemCompactInfo(int itemId, int catId, string itemName)
            {
                this.ItemId = itemId;
                this.CategoryId = catId;
                this.ItemName = itemName;
            }
        }   // class ItemCompactInfo

        private void LoadAllCategories()
        {
            DataTable dtCategories = ItemCategoryDAL.GetAllCategories();
            if (dtCategories.Rows.Count == 0)
                return;

            this.treeViewItems.Nodes.Clear();

            for (int itNum = 0; itNum < 2; ++itNum)
            {
                foreach (DataRow dr in dtCategories.Rows)
                {
                    int catId = (int)dr["CategoryId"];
                    int parentCatId = (int)dr["ParentCategoryId"];
                    string catName = dr["CategoryName"].ToString();

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
                        this.treeViewItems.Nodes.Add(newNode);
                    }
                    else if (parentCatId != -1 && itNum == 1)
                    {
                        TreeNode[] parentNodes = this.treeViewItems.Nodes.Find(parentCatId.ToString(), true);
                        if (parentNodes.Length == 1)
                            parentNodes[0].Nodes.Add(newNode);
                    }
                }
            }


            this.treeViewItems.ExpandAll();
            return;
        }

        private void LoadAllItems()
        {
            DataTable dtItems = ItemDAL.GetAllItems();
            if (dtItems.Rows.Count == 0)
                return;

            foreach (DataRow dr in dtItems.Rows)
            {
                int itemId = (int)dr["ItemId"];
                int itemCatId = Int32.Parse(dr["CategoryId"].ToString());
                string itemName = dr["ItemName"].ToString();

                ItemCompactInfo itemInfo = new ItemCompactInfo(itemId, itemCatId, itemName);
                TreeNode itemNode = new TreeNode();
                itemNode.Text = itemName;
                itemNode.Tag = itemInfo;

                TreeNode[] nodes = this.treeViewItems.Nodes.Find(itemCatId.ToString(), true);
                if (nodes.Length == 1)
                {
                    nodes[0].Nodes.Add(itemNode);
                }
            }
        }

        private void FrmSelectItemSKU_Load(object sender, EventArgs e)
        {
            LoadAllCategories();
            LoadAllItems();
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            TreeNode node = this.treeViewItems.SelectedNode;
            if (node == null)
                return;

            object tag = node.Tag;
            if (tag.GetType() != typeof(ItemCompactInfo))
                return;

            ItemCompactInfo itemInfo = (ItemCompactInfo)tag;
            if (itemInfo == null)
                return;

            int itemId = itemInfo.ItemId;
            DataTable dtItem = ItemDAL.GetItemTableById(itemId);
            if (dtItem.Rows.Count == 0)
                return;

            string sku = dtItem.Rows[0]["ItemSKU"].ToString();
            this.SKU = sku;
            this.Close();
        }
    }
}
