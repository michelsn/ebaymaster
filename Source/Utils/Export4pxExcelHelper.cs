using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop;

namespace EbayMaster
{
   // This class is responsible for exporting transactions to 4px excel format.
   // References:
   //    1) http://www.codeproject.com/Articles/248531/Export-Excel-File-for-Csharp
   public class Export4pxExcelHelper
   {
      public static bool ExportTransactionsTo4pxExcel(List<EbayTransactionType> transList, string filePath)
      {
         FileInfo fileInfo = new FileInfo(EbayConstants.ExcelTemplateFor4pxFilePath);
         if (!fileInfo.Exists)
         {
            Logger.WriteSystemLog(string.Format("{0} doesn't exist!", EbayConstants.ExcelTemplateFor4pxFilePath));
            return false;
         }

         Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
         if (excelApp == null)
         {
            Logger.WriteSystemLog("Cannot create Excel Application object.");
            return false;
         }

         Microsoft.Office.Interop.Excel.Workbooks workbooks = excelApp.Workbooks;
         String excelTemplateFilePath = string.Format("{0}\\{1}", Environment.CurrentDirectory, EbayConstants.ExcelTemplateFor4pxFilePath);
         Microsoft.Office.Interop.Excel.Workbook workbook = excelApp.Workbooks.Open(excelTemplateFilePath, Type.Missing, 
	         Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, 
	         Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, 
	         Type.Missing, Type.Missing, Type.Missing);

         if (workbook == null)
         {
            Logger.WriteSystemLog("不能打开excel模板文件.");
            return false;
         }

         Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.Sheets[1]; //First worksheet

         int curRow = 2;
         foreach (EbayTransactionType trans in transList)
         {
             if (trans.IsPaid == false)
             {
                 Logger.WriteSystemLog("Try to export unpaid transaction!");
                 return false;
             }
             if (trans.ItemSKU == "")
             {
                 Logger.WriteSystemLog("Try to export transaction with item that has no SKU!");
                 return false;
             }
             if (trans.ShippingServiceCode == null || trans.ShippingServiceCode == "")
             {
                 Logger.WriteSystemLog("No shipping service code.");
                 return false;
             }

             InventoryItemType item = ItemDAL.GetItemBySKU(trans.ItemSKU);
            if (item == null)
            {
                Logger.WriteSystemLog("Cannot get item");
                return false;
            }

            worksheet.Cells[curRow, 1] = "";                         // "客户单号"
            worksheet.Cells[curRow, 2] = "";                         // "服务商单号"
            worksheet.Cells[curRow, 3] = trans.ShippingServiceCode;  // "运输方式"
            worksheet.Cells[curRow, 4] = trans.BuyerCountry;         // "目的国家"
            worksheet.Cells[curRow, 5] = "";//EbayConstants.SenderCompanyName; // "寄件人公司名"

            worksheet.Cells[curRow, 6] = "";// EbayConstants.SenderName; // "寄件人姓名"
            worksheet.Cells[curRow, 7] = "";// EbayConstants.SenderAddress; // "寄件人地址"
            worksheet.Cells[curRow, 8] = "";// EbayConstants.SenderPhone; // "寄件人电话"
            worksheet.Cells[curRow, 9] = "";//EbayConstants.SenderPostalCode; // "寄件人邮编"
            worksheet.Cells[curRow, 10] = "";//EbayConstants.SenderFax; //"寄件人传真"

            worksheet.Cells[curRow, 11] = trans.BuyerCompanyName; // "收件人公司名"
            worksheet.Cells[curRow, 12] = trans.BuyerName; // "收件人姓名"
            worksheet.Cells[curRow, 13] = trans.BuyerStateOrProvince; // "州 \ 省"
            worksheet.Cells[curRow, 14] = trans.BuyerCity; // "城市"

            // 4PX联邮通挂号/平邮地址不能超过60
            if (trans.ShippingServiceCode == "A6" || trans.ShippingServiceCode == "A7")
            {
               worksheet.Cells[curRow, 15] = trans.BuyerAddressCompact; //"联系地址"
            }
            else
            {
               worksheet.Cells[curRow, 15] = trans.BuyerAddress; //"联系地址"
            }

             if (trans.BuyerTel != "Invalid Request")
                worksheet.Cells[curRow, 16] = trans.BuyerTel; // "收件人电话"
             else
                 worksheet.Cells[curRow, 16] = ""; // "收件人电话"

            worksheet.Cells[curRow, 17] = trans.BuyerMail; //"收件人邮箱"
            worksheet.Cells[curRow, 18] = trans.BuyerPostalCode; //"收件人邮编"
            worksheet.Cells[curRow, 19] = ""; //"收件人传真"
            worksheet.Cells[curRow, 20] = trans.BuyerId; //"买家ID"

            worksheet.Cells[curRow, 21] = "";// trans.EbayTransactionId; // "交易ID"
            worksheet.Cells[curRow, 22] = ""; // "保险类型"
            worksheet.Cells[curRow, 23] = ""; //"保险价值"
            worksheet.Cells[curRow, 24] = ""; // "订单备注";
            worksheet.Cells[curRow, 25] = item.ItemCustomName;   // "海关报关品名1"

            worksheet.Cells[curRow, 26] = string.Format("{0}x{1}", trans.SaleQuantity, item.ItemName);  // "配货信息1"
            worksheet.Cells[curRow, 27] = item.ItemCustomCost;   // "申报价值1"
            worksheet.Cells[curRow, 28] = trans.SaleQuantity; // "申报品数量1"
            worksheet.Cells[curRow, 29] = ""; // "配货备注1"

            curRow++;
         }

         workbook.Saved = true;
         workbook.SaveAs(filePath);

         if (workbook != null)
            workbook.Close();
         if (excelApp != null)
            excelApp.Quit();

         return true;
      }
   }
}
