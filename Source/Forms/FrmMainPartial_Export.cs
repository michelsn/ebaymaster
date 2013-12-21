using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EbayMaster
{
    partial class FrmMain
    {
        private List<EbayTransactionType> GetAllSelectedTransactions(bool check)
        {
            // Check if every paid-but-un-shipped item has a sku.
            List<EbayTransactionType> allSelectedTrans = new List<EbayTransactionType>();

            DataGridViewSelectedRowCollection selectedRows = this.dataGridViewAllOrders.SelectedRows;
            foreach (DataGridViewRow row in selectedRows)
            {
                String orderLineItemId = row.Cells[OrderDgv_OrderLineItemIndex].Value.ToString();

                EbayTransactionType trans = EbayTransactionDAL.GetOneTransaction(orderLineItemId);
                if (trans == null)
                    return null;

                if (!trans.IsPaid)
                    return null;

                if (check && (trans.ItemSKU == null || trans.ItemSKU.Trim() == ""))
                {
                    MessageBox.Show("订单没有关联的商品SKU！", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                // Is shipping service specified?
                if (check && (trans.ShippingService == "" || trans.ShippingServiceCode == ""))
                {
                    MessageBox.Show("没有为订单选择物流运输方式！", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return allSelectedTrans;
                }

                allSelectedTrans.Add(trans);

            }
            return allSelectedTrans;
        }

        private void buttonExportSelectedToExcel_Click(object sender, EventArgs e)
        {
            this.buttonExportSelectedToExcel.Enabled = false;

            // Check if the template file is existed.
            FileInfo fileInfoTemplate = new FileInfo(EbayConstants.ExcelTemplateFor4pxFilePath);
            if (!fileInfoTemplate.Exists)
            {
                MessageBox.Show(string.Format("递四方excel模板文件不存在，请放放置到{0}下", EbayConstants.ExcelTemplateFor4pxFilePath),
                   "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.buttonExportSelectedToExcel.Enabled = true;
                return;
            }

            // Check if every paid-but-un-shipped item has a sku.
            List<EbayTransactionType> allSelectedTrans = GetAllSelectedTransactions(true);
            if (allSelectedTrans.Count == 0)
            {
                MessageBox.Show("未选中任何交易", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.buttonExportSelectedToExcel.Enabled = true;
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog(); ;
            dlg.DefaultExt = "xls";
            dlg.Filter = "Excel files (*.xls) | *.xls";
            dlg.InitialDirectory = Directory.GetCurrentDirectory();

            if (dlg.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            string fileNameString = dlg.FileName;
            if (fileNameString.Trim() == "")
            {
                return;
            }

            FileInfo fileInfo = new FileInfo(fileNameString);
            if (fileInfo.Exists)
            {
                try
                {
                    fileInfo.Delete();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.buttonExportSelectedToExcel.Enabled = true;
                    return;
                }
            }

            bool result = Export4pxExcelHelper.ExportTransactionsTo4pxExcel(allSelectedTrans, fileNameString);
            if (result == true)
            {
                MessageBox.Show("成功导出订单信息到excel文件!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("导出订单信息到excel文件失败\r\n可能某些订单没有设置商品SKU等!", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.buttonExportSelectedToExcel.Enabled = true;
        }

        private void buttonExportEbayCVS_Click(object sender, EventArgs e)
        {
            this.buttonExportEbayCVS.Enabled = false;

            List<EbayTransactionType> allSelectedTrans = GetAllSelectedTransactions(false);
            if (allSelectedTrans==null || allSelectedTrans.Count == 0)
            {
                MessageBox.Show("未选中任何已成交交易", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.buttonExportEbayCVS.Enabled = true;
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog(); ;
            dlg.DefaultExt = "csv";
            dlg.Filter = "CSV files (*.csv) | *.csv";
            dlg.InitialDirectory = Directory.GetCurrentDirectory();

            if (dlg.ShowDialog() == DialogResult.Cancel)
            {
                this.buttonExportEbayCVS.Enabled = true;
                return;
            }

            string fileNameString = dlg.FileName;
            if (fileNameString.Trim() == "")
            {
                this.buttonExportEbayCVS.Enabled = true;
                return;
            }

            FileInfo fileInfo = new FileInfo(fileNameString);
            if (fileInfo.Exists)
            {
                try
                {
                    fileInfo.Delete();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.buttonExportSelectedToExcel.Enabled = true;
                    return;
                }
            }

            bool result = ExportHelper.ExportOrdersToEbayCSV(allSelectedTrans, fileNameString);
            if (result == true)
            {
                MessageBox.Show("成功导出订单信息到ebay csv文件!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("导出订单信息到ebay csv文件失败!", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.buttonExportEbayCVS.Enabled = true;
        }
    }
}