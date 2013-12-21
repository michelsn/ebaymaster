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
    public partial class FrmEditSupplier : Form
    {
        private enum EditMode { Invalid, CreateNew, EditExsiting };
        private EditMode mEditMode = EditMode.Invalid;
        private int mSupplierId = 0;

        public FrmEditSupplier()
        {
            InitializeComponent();

            mEditMode = EditMode.CreateNew;
        }

        private void ShowSupplierData()
        {
            SupplierType supplier = SupplierDAL.GetSupplierById(mSupplierId);
            if (supplier == null)
            {
                MessageBox.Show("无供应商信息", "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.textBoxSupplierName.Text = supplier.SupplierName;
            this.textBoxSupplierTel.Text = supplier.SupplierTel;
            this.textBoxSupplierLink1.Text = supplier.SupplierLink1;
            this.textBoxSupplierLink2.Text = supplier.SupplierLink2;
            this.textBoxSupplierLink3.Text = supplier.SupplierLink3;
            this.textBoxSupplierComment.Text = supplier.Comment;
        }

        public FrmEditSupplier(int supplierId)
        {
            InitializeComponent();

            mEditMode = EditMode.EditExsiting;

            mSupplierId = supplierId;

            ShowSupplierData();
        }

        private SupplierType GetSupplierInfoFromUI()
        {
            SupplierType supplier = new SupplierType();

            String supplierName = this.textBoxSupplierName.Text.Trim();
            String supplierTel = this.textBoxSupplierTel.Text.Trim();
            String supplierLink1 = this.textBoxSupplierLink1.Text.Trim();
            String supplierLink2 = this.textBoxSupplierLink2.Text.Trim();
            String supplierLink3 = this.textBoxSupplierLink3.Text.Trim();
            String comment = this.textBoxSupplierComment.Text.Trim();

            if (supplierName == "")
            {
                MessageBox.Show("供应商名称不能为空", "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            supplier.SupplierName = supplierName;
            supplier.SupplierTel = supplierTel;
            supplier.SupplierLink1 = supplierLink1;
            supplier.SupplierLink2 = supplierLink2;
            supplier.SupplierLink3 = supplierLink3;
            supplier.Comment = comment;
            return supplier;
        }

        private void buttonFinishEditingSupplier_Click(object sender, EventArgs e)
        {
            SupplierType supplier = null;
            supplier = GetSupplierInfoFromUI();

            if (mEditMode == EditMode.CreateNew)
            {
                int newId = SupplierDAL.InsertOneSupplier(supplier);
                if (newId > 0)
                    MessageBox.Show("创建供应商成功", "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("创建供应商失败", "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (mEditMode == EditMode.EditExsiting)
            {
                supplier.SupplierID = mSupplierId;
                if (SupplierDAL.ModifyOneSupplier(supplier))
                    MessageBox.Show("修改供应商成功", "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("修改供应商失败", "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
            }
        }

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
