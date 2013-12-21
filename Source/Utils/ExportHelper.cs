using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace EbayMaster
{
    // What is CSV file?
    //  CSV file is a text based file in which data are separated by comma. 
    //  It can be opened by excel so you can use excel functionality. 
    //  Each row of data including the title is in separate line. 
    //  Meanwhile, each row has data separated by comma.


    // Ebay CSV format
    //  Sales Record Number, User Id, Buyer Fullname, Buyer Phone Number, Buyer Email,
    //  Buyer Address 1, Buyer Address 2, Buyer City, Buyer State, Buyer Zip,
    //  Buyer Country, Item Number, Item Title, Custom Label, Quantity,
    //  Sale Price, Shipping and Handling, US Tax, Insurance, Cash on delivery fee,
    //  Total Price, Payment Method, Sale Date, Checkout Date, Paid on Date, 
    //  Shipped on Date, Feedback left, Feedback received, Notes to yourself, PayPal Transaction ID,
    //  Shipping Service, Cash on delivery option, Transaction ID, Order ID, Variation Details
    public class ExportHelper
    {
        private static void WriteEbayCSVColumnNames(StringBuilder sb)
        {
            string columnNames = @"Sales Record Number, User Id, Buyer Fullname, Buyer Phone Number, Buyer Email,"
                + @"Buyer Address 1, Buyer Address 2, Buyer City, Buyer State, Buyer Zip,"
                + @"Buyer Country, Item Number, Item Title, Custom Label, Quantity,"
                + @"Sale Price, Shipping and Handling, US Tax, Insurance, Cash on delivery fee,"
                + @"Total Price, Payment Method, Sale Date, Checkout Date, Paid on Date, "
                + @"Shipped on Date, Feedback left, Feedback received, Notes to yourself, PayPal Transaction ID,"
                + @"Shipping Service, Cash on delivery option, Transaction ID, Order ID, Variation Details";
            sb.Append(columnNames);
            sb.AppendLine();
        }

        private static void AddComma(object value, StringBuilder stringBuilder)
        {
            stringBuilder.Append(value.ToString().Replace(',', ' '));
            stringBuilder.Append(", ");
        }

        public static bool ExportOrdersToEbayCSV(List<EbayTransactionType> transList, string csvFilePath)
        {
            StringBuilder sb = new StringBuilder();
            WriteEbayCSVColumnNames(sb);

            foreach (EbayTransactionType trans in transList)
            {
                //AddComma(trans.TransactionId, sb);
                AddComma(trans.EbayRecordId, sb);
                AddComma(trans.BuyerId, sb);
                AddComma(trans.BuyerName, sb);
                AddComma(trans.BuyerTel, sb);
                AddComma(trans.BuyerMail, sb);

                AddComma(trans.BuyerAddressLine1, sb);
                AddComma(trans.BuyerAddressLine2, sb);
                AddComma(trans.BuyerCity, sb);
                AddComma(trans.BuyerStateOrProvince, sb);
                AddComma(trans.BuyerPostalCode, sb);

                AddComma(trans.BuyerCountry, sb);
                AddComma(trans.ItemId, sb);
                AddComma(trans.ItemTitle, sb);
                AddComma(trans.ItemSKU, sb);
                AddComma(trans.SaleQuantity, sb);

                AddComma(trans.SalePrice, sb);
                AddComma("0.0", sb);            // zhi_todo: shipping and handling
                AddComma("0.0", sb);        // us_tax
                AddComma("0.0", sb);// Insurance
                AddComma("0.0", sb);// Cash on delivery fee

                // "Total Price, Payment Method, Sale Date, Checkout Date, Paid on Date, "
                AddComma(trans.TotalPrice, sb);
                AddComma("paypal", sb);
                AddComma(trans.SaleDate, sb);
                AddComma(trans.PaidDate, sb);    //zhi_todo: checkout date
                AddComma(trans.PaidDate, sb);

                // Shipped on Date, Feedback left, Feedback received, Notes to yourself, PayPal Transaction ID,
                AddComma(trans.ShippedDate, sb);
                AddComma("", sb);
                AddComma("", sb);
                AddComma("", sb);
                AddComma("", sb);

                // Shipping Service, Cash on delivery option, Transaction ID, Order ID, Variation Details
                AddComma(trans.ShippingService, sb);
                AddComma("", sb);
                AddComma(trans.EbayTransactionId, sb);
                AddComma(trans.OrderId, sb);
                AddComma("", sb);

                sb.AppendLine();
            }

            StreamWriter sw = new StreamWriter(csvFilePath, false, Encoding.Unicode);
            sw.Write(sb.ToString());
            sw.Flush();
            sw.Close();

            return true;
        }
    }
}
