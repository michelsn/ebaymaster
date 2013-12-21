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
    public enum SupplierDlgMode { Invalid, ShowSupplier, SelectSupplier };

    public partial class FrmSupplierList : Form
    {
        private SupplierDlgMode mMode = SupplierDlgMode.Invalid;

        public SupplierType mSelectedSupplier = null;

        public FrmSupplierList(SupplierDlgMode mode)
        {
            InitializeComponent();

            this.pagedDgvSupplier.initDgvColumns = SetupSupplierDataGridViewColumns;
            this.pagedDgvSupplier.getPagedData = SupplierDAL.GetPagedSuppliers;
            this.pagedDgvSupplier.getRecordCount = SupplierDAL.GetSupplierount;

            if (mode == SupplierDlgMode.ShowSupplier)
            {
                this.btnFinishSelecting.Visible = false;

                this.pagedDgvSupplier.DgvData.MouseUp += new MouseEventHandler(dataGridViewSupplier_MouseUp);
                this.pagedDgvSupplier.DgvData.CellContentClick += new DataGridViewCellEventHandler(DgvData_CellContentClick);

                this.Text = "供应商列表 - 右击供应商编辑/删除";
            }
            else if (mode == SupplierDlgMode.SelectSupplier)
            {
                this.btnFinishSelecting.Visible = true;

                this.pagedDgvSupplier.DgvData.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(DgvData_CellMouseDoubleClick);
                AdjustBtnFinishSelectingPos();

                this.Text = "供应商列表";
            }

            mMode = mode;
        }

        void DgvData_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            btnFinishSelecting_Click(sender, e);
        }

        private void AdjustBtnFinishSelectingPos()
        {
            this.Size = new System.Drawing.Size(this.Size.Width, this.Size.Height + this.btnFinishSelecting.Height);
            this.btnFinishSelecting.Location = new Point(this.btnFinishSelecting.Location.X,
                this.pagedDgvSupplier.Height);
        }

        private void SetupSupplierDataGridViewColumns()
        {
            this.pagedDgvSupplier.DgvData.AutoGenerateColumns = false;

            // SupplierID
            this.pagedDgvSupplier.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("SupplierID", @"编号", typeof(string), 60, true));

            // SupplierName
            this.pagedDgvSupplier.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("SupplierName", @"名称", typeof(string), 150, true));

            // SupplierTel
            this.pagedDgvSupplier.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("SupplierTel", @"电话", typeof(string), 80, true));

            // SupplierLink1
            this.pagedDgvSupplier.DgvData.Columns.Add(
                DgvUtil.createDgvLinkColumn("SupplierLink1", @"网址1", typeof(string), 200, true));

            // SupplierLink2
            this.pagedDgvSupplier.DgvData.Columns.Add(
                DgvUtil.createDgvLinkColumn("SupplierLink2", @"网址2", typeof(string), 100, true));

            // SupplierLink3
            this.pagedDgvSupplier.DgvData.Columns.Add(
                DgvUtil.createDgvLinkColumn("SupplierLink3", @"网址3", typeof(string), 100, true));

            // Comment
            this.pagedDgvSupplier.DgvData.Columns.Add(
                DgvUtil.createDgvTextBoxColumn("Comment", @"备注", typeof(string), 120, true));

            this.pagedDgvSupplier.DgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.pagedDgvSupplier.DgvData.MultiSelect = true;
            this.pagedDgvSupplier.DgvData.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dgv_RowPrePaint);
        }

        private void dgv_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
        }

        private void FrmSupplierList_Load(object sender, EventArgs e)
        {
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

        private void ToolStripMenuItemDelSupplier_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确认刪除该供应商么?\r\n删除前，必须删除所有该供应商的采购单。",
                 "确认发货?",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            // Check if any sourcing notes related to this supplier, if so, disallow delete.
            // [ZHI_TODO]

            int rowIdx = this.pagedDgvSupplier.DgvData.CurrentRow.Index;
            int supplierId = StringUtil.GetSafeInt(this.pagedDgvSupplier.DgvData.Rows[rowIdx].Cells[0].Value.ToString());

            SupplierType supplier = SupplierDAL.GetSupplierById(supplierId);
            if (supplier == null)
                return;

            SupplierDAL.DeleteOneSupplier(supplierId);
            MessageBox.Show(String.Format("删除供应商成功!", supplier.SupplierName),
                "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.pagedDgvSupplier.LoadData();
        }

        private void dataGridViewSupplier_MouseUp(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hitTestInfo;
            if (e.Button == MouseButtons.Right)
            {
                hitTestInfo = pagedDgvSupplier.DgvData.HitTest(e.X, e.Y);
                this.contextMenuStripSupplier.Show(pagedDgvSupplier.DgvData, new Point(e.X, e.Y));
            }
        }

        void DgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewColumn col = this.pagedDgvSupplier.DgvData.Columns[e.ColumnIndex];
            if (col.GetType() == typeof(DataGridViewLinkColumn))
            {
                String link = StringUtil.GetSafeString(this.pagedDgvSupplier.DgvData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                System.Diagnostics.Process.Start(link);
            }
        }

        private void btnFinishSelecting_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = this.pagedDgvSupplier.DgvData.CurrentRow;
            if (row == null)
            {
                MessageBox.Show("未选中任何供应商", "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int supplierId = StringUtil.GetSafeInt(row.Cells[0].Value);
            mSelectedSupplier = SupplierDAL.GetSupplierById(supplierId);
            if (mSelectedSupplier == null)
            {
                MessageBox.Show("未选中任何供应商", "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Close();
        }

        private void ToolStripMenuItemEditSupplier_Click(object sender, EventArgs e)
        {
            int rowIdx = this.pagedDgvSupplier.DgvData.CurrentRow.Index;
            int supplierId = StringUtil.GetSafeInt(this.pagedDgvSupplier.DgvData.Rows[rowIdx].Cells[0].Value.ToString());

            SupplierType supplier = SupplierDAL.GetSupplierById(supplierId);
            if (supplier == null)
                return;

            FrmEditSupplier frmEditSupplier = new FrmEditSupplier(supplierId);
            frmEditSupplier.ShowDialog();

            this.pagedDgvSupplier.LoadData();
        }
    }
}
