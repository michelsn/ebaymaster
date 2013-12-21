//
// EbayTransactionDAL.cs
//  This file is responsible for reading/updating transaction (i.e., order) information from/to database.
//  Author: Zhi Wang
//  Date: 2013/04/13
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace EbayMaster
{
    public class EbayTransactionDAL
    {
        #region Retrieve transaction data

        private static EbayTransactionType GetTransactionTypeFromDataRow(DataRow dr)
        {
            if (dr == null)
                return null;

            EbayTransactionType trans = new EbayTransactionType();

            trans.TransactionId = StringUtil.GetSafeInt(dr["TransactionId"]);
            trans.SellerName = StringUtil.GetSafeString(dr["SellerName"]);
            trans.OrderId = StringUtil.GetSafeString(dr["OrderId"]);
            trans.OrderLineItemId = StringUtil.GetSafeString(dr["OrderLineItemId"]);
            trans.EbayTransactionId = StringUtil.GetSafeString(dr["EbayTransactionId"]);
            trans.EbayRecordId = StringUtil.GetSafeInt(dr["EbayRecordId"]);

            trans.BuyerId = StringUtil.GetSafeString(dr["BuyerId"]);
            trans.BuyerRating = StringUtil.GetSafeInt(dr["BuyerRating"]);
            trans.BuyerCountryEbayCode = StringUtil.GetSafeString(dr["BuyerCountryEbayCode"]);
            trans.BuyerCountry4PXCode = StringUtil.GetSafeString(dr["BuyerCountry4PXCode"]);
            trans.BuyerCountry = StringUtil.GetSafeString(dr["BuyerCountry"]);
            trans.BuyerCompanyName = StringUtil.GetSafeString(dr["BuyerCompanyName"]);
            trans.BuyerName = StringUtil.GetSafeString(dr["BuyerName"]);
            trans.BuyerStateOrProvince = StringUtil.GetSafeString(dr["BuyerStateOrProvince"]);
            trans.BuyerCity = StringUtil.GetSafeString(dr["BuyerCity"]);
            trans.BuyerTel = StringUtil.GetSafeString(dr["BuyerTel"]);
            trans.BuyerMail = StringUtil.GetSafeString(dr["BuyerMail"]);
            trans.BuyerPostalCode = StringUtil.GetSafeString(dr["BuyerPostalCode"]);
            trans.BuyerAddress = StringUtil.GetSafeString(dr["BuyerAddress"]);
            trans.BuyerAddressCompact = StringUtil.GetSafeString(dr["BuyerAddressCompact"]);
            trans.BuyerAddressLine1 = StringUtil.GetSafeString(dr["BuyerAddressLine1"]);
            trans.BuyerAddressLine2 = StringUtil.GetSafeString(dr["BuyerAddressLine2"]);
            trans.BuyerPayPal = StringUtil.GetSafeString(dr["BuyerPayPal"]);

            trans.ItemId = StringUtil.GetSafeString(dr["ItemId"]);
            trans.ItemTitle = StringUtil.GetSafeString(dr["ItemTitle"]);
            trans.ItemSKU = StringUtil.GetSafeString(dr["ItemSKU"]);
            trans.ItemPrice = StringUtil.GetSafeDouble(dr["ItemPrice"]);
            trans.SaleQuantity = StringUtil.GetSafeInt(dr["SaleQuantity"]);
            trans.TotalPrice = StringUtil.GetSafeDouble(dr["TotalPrice"]);
            trans.CurrencyId = StringUtil.GetSafeString(dr["CurrencyId"]);

            trans.SaleDate = StringUtil.GetSafeDateTime(dr["SaleDate"]);
            trans.SaleDateCN = StringUtil.GetSafeDateTime(dr["SaleDateCN"]);
            trans.IsPaid = StringUtil.GetSafeBool(dr["IsPaid"]);
            trans.PaidDate = StringUtil.GetSafeDateTime(dr["PaidDate"]);
            trans.IsShipped = StringUtil.GetSafeBool(dr["IsShipped"]);
            trans.ShippedDate = StringUtil.GetSafeDateTime(dr["ShippedDate"]);

            trans.ShippingServiceCode = StringUtil.GetSafeString(dr["ShippingServiceCode"]);
            trans.ShippingService = StringUtil.GetSafeString(dr["ShippingService"]);
            trans.ShippingTrackingNo = StringUtil.GetSafeString(dr["ShippingTrackingNo"]);
            trans.ShippingCost = StringUtil.GetSafeDouble(dr["ShippingCost"]);
            trans.FinalValueFee = StringUtil.GetSafeDouble(dr["FinalValueFee"]);
            trans.PayPalFee = StringUtil.GetSafeDouble(dr["PayPalFee"]);

            trans.IsReceived = StringUtil.GetSafeBool(dr["IsReceived"]);
            trans.IsBuyerLeftFeedback = StringUtil.GetSafeBool(dr["IsBuyerLeftFeedback"]);
            trans.IsSellerLeftFeedback = StringUtil.GetSafeBool(dr["IsSellerLeftFeedback"]);
            trans.IsNeedAttention = StringUtil.GetSafeBool(dr["IsNeedAttention"]);
            trans.MessageStatus = (TransactionMessageStatus)StringUtil.GetSafeInt(dr["MessageStatus"]);
            trans.IsContactedBuyer = StringUtil.GetSafeBool(dr["IsContactedBuyer"]);
            trans.LastContactedBuyerDate = StringUtil.GetSafeDateTime(dr["LastContactedBuyerDate"]);
            trans.IsResendReplacement = StringUtil.GetSafeBool(dr["IsResendReplacement"]);
            trans.UserComment = StringUtil.GetSafeString(dr["UserComment"]);

            trans.IsDelivered = StringUtil.GetSafeBool(dr["IsDelivered"]);
            trans.DeliveryNoteId = StringUtil.GetSafeInt(dr["DeliveryNoteId"]);

            return trans;
        } // GetTransactionTypeFromDataRow

        public static EbayTransactionType GetOneTransaction(String orderLineItemId)
        {
            String sql_getOneTransaction = string.Format("select * from [Transaction] where OrderLineItemId='{0}'", orderLineItemId);
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql_getOneTransaction);
            if (dt.Rows.Count == 0)
                return null;

            DataRow dr = dt.Rows[0];
            return GetTransactionTypeFromDataRow(dr);
        }

        public static EbayTransactionType GetOneTransactonById(string tranId)
        {
            String sql = String.Format("select * from [Transaction] where TransactionId={0}", tranId);
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql);
            if (dt.Rows.Count == 0)
                return null;

            return GetTransactionTypeFromDataRow(dt.Rows[0]);
        }

        // Get transactions by input sellerName/buyerId/itemId.
        //  Note that for most cases, this function should return one entry, 
        //  but it is possible that a buyer bought several items from the same list,
        //  in which case several transactions will be returned.
        public static List<EbayTransactionType> GetTransactionsBySellerBuyerItem(String sellerName, String buyerId, String itemId)
        {
            List<EbayTransactionType> transList = new List<EbayTransactionType>();

            String sql_getOneTransaction = string.Format("select * from [Transaction] where SellerName='{0}' and BuyerId='{1}' and ItemId='{2}'",
                sellerName, 
                buyerId, 
                itemId);

            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql_getOneTransaction);

            foreach (DataRow dr in dt.Rows)
            {
                EbayTransactionType trans = GetTransactionTypeFromDataRow(dr);
                transList.Add(trans);
            }

            return transList;
        }

        public static List<EbayTransactionType> GetAllTransactions()
        {
            List<EbayTransactionType> list = new List<EbayTransactionType>();

            String sql_getAllTransactions = "select * from [Transaction] order by SaleDate desc";
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql_getAllTransactions);

            foreach (DataRow row in dt.Rows)
            {
                EbayTransactionType trans = GetTransactionTypeFromDataRow(row);
                list.Add(trans);
            }

            return list;
        }   // GetAllTransactions

        // Get all orders.
        //  Returns the DataTable contains all transactions.
        //  This should be used with caution, to avoid loading to many data entries.
        public static DataTable GetAllOrders()
        {
            String sql_getAllTransactions = "select * from [Transaction]  order by SaleDate desc";
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql_getAllTransactions);
            return dt;
        }

        public static DataTable GetOneTransactionTable(int transId)
        {
            String sql = String.Format("select * from [Transaction]  where TransactionId={0}", transId);
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql);
            return dt;
        }

        // Get all order between a time period.
        //  Returns the DataTable contains all transactions in that time period.
        public static DataTable GetAllOrdersBetween(DateTime startDate, DateTime endDate)
        {
            String sql_getAllTransactions = "";
            if (DBConnectionUtil.DBConn.dbType == DatabaseType.Access)
            {
                sql_getAllTransactions = string.Format("select * from [Transaction]  where SaleDate > #{0}# and SaleDate < #{1}# order by SaleDate desc", startDate.ToShortDateString(), endDate.ToShortDateString());
            }
            else
            {
                sql_getAllTransactions = string.Format("select * from [Transaction]  where SaleDate > '{0}' and SaleDate < '{1}' order by SaleDate desc", startDate.ToShortDateString(), endDate.ToShortDateString());
            }

            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql_getAllTransactions);
            return dt;
        }

        // Get all orders that pending buyer feedback.
        //  Note some buyers didn't left feedback but they composed mail to tell the item has been received.
        public static DataTable GetAllOrdersPendingBuyerFeedback()
        {
            String sql_getAllTransactions
                = "select * from [Transaction]  where IsBuyerLeftFeedback=false and IsReceived=false order by SaleDate desc";
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql_getAllTransactions);
            return dt;
        }


        // Get all orders count.
        //  Returns the order count.
        public static int GetOrdersCount()
        {
            int ordersCnt = 0;
            String sql = "select count(*) from [Transaction]";
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql);

            try
            {
                object obj = dt.Rows[0][0];
                if (obj != null)
                    Int32.TryParse(obj.ToString(), out ordersCnt);
            }
            catch (System.Exception)
            {

            }
            return ordersCnt;
        }

        public static int GetOrdersCount(bool isDelivered)
        {
            int ordersCnt = 0;
            String sql = String.Format("select count(*) from [Transaction] where IsDelivered={0}", isDelivered ? "true":"false");
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql);

            try
            {
                object obj = dt.Rows[0][0];
                if (obj != null)
                    Int32.TryParse(obj.ToString(), out ordersCnt);
            }
            catch (System.Exception)
            {

            }
            return ordersCnt;
        }

        public static int GetPendingOrdersCount(int daysAfterShipment)
        {
            DateTime shippedDate = DateTime.Now;
            shippedDate = shippedDate.AddDays(-daysAfterShipment);

            int ordersCnt = 0;
            String sql = 
                String.Format("select count(*) from [Transaction] where IsBuyerLeftFeedback=0 and IsPaid=true and IsReceived=false and ShippedDate<#{0}#",
                shippedDate.ToShortDateString());
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql);

            try
            {
                object obj = dt.Rows[0][0];
                if (obj != null)
                    Int32.TryParse(obj.ToString(), out ordersCnt);
            }
            catch (System.Exception)
            {

            }
            return ordersCnt;
        }

        // Get all transactions that have been shipped more than daysAfterShipment and buyer didn't left feedback.
        //  
        public static DataTable GetPagedPendingOrders(int pageNum, int pageSize, int daysAfterShipment)
        {
            // Note to use "true/false" instead of "1/0" for MS Access.
            String pagedOrderFormatSql = "select * from [Transaction] where TransactionId "
                    + "in (select top {0} sub.TransactionId from ("
                    + " select top {1} TransactionId,SaleDate from [Transaction] where (Transaction.IsBuyerLeftFeedback=false and Transaction.ShippedDate<#{2}#) order by SaleDate desc"
                    + ") [sub] order by sub.SaleDate) order by SaleDate desc";

            DateTime shippedDate = DateTime.Now;
            shippedDate = shippedDate.AddDays(-daysAfterShipment);

            String pagedOrderSql = String.Format(pagedOrderFormatSql, pageSize, pageNum * pageSize, shippedDate.ToShortDateString());
            DataTable dt = DataFactory.ExecuteSqlReturnTable(pagedOrderSql);
            return dt;
        }

        // Get paged orders.
        public static DataTable GetPagedOrders(int pageNum, int pageSize, bool isPendingOrder)
        {
            String pagedOrderFormatSql = "select * from [Transaction] where TransactionId "
                    + "in (select top {0} sub.TransactionId from ("
                    + " select top {1} TransactionId,SaleDate, OrderLineItemId from [Transaction] where IsDelivered={2} order by SaleDate desc, OrderLineItemId"
                    + ") [sub] order by sub.SaleDate, sub.OrderLineItemId) order by SaleDate desc";
            String pagedOrderSql = String.Format(pagedOrderFormatSql, pageSize, pageNum * pageSize, isPendingOrder ? "false" : "true");
            DataTable dt = DataFactory.ExecuteSqlReturnTable(pagedOrderSql);
            return dt;
        }

        // Get all paid but not shipped orders.
        public static DataTable GetAllPaidButNotShippedOrders()
        {
            String sql_getAllTransactions = "select * from [Transaction]  where IsPaid=true and IsShipped=false order by SaleDate desc";
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql_getAllTransactions);
            return dt;
        }

        // Get transactions with a specified buyer id.
        public static DataTable GetTransactionsByUserId(String userId)
        {
            String sql_getAllTransactions = string.Format("select * from [Transaction]  where BuyerId='{0}' order by SaleDate desc", userId);
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql_getAllTransactions);
            return dt;
        }

        public static DataTable GetPendingTransactionByUserId(String userId)
        {
            String sql_getAllTransactions = string.Format("select * from [Transaction]  where BuyerId='{0}' and IsBuyerLeftFeedback=false order by SaleDate desc", userId);
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql_getAllTransactions);
            return dt;
        }

        // Get transactions for a specified order id.
        //  Note that for single line item, only one transaction for an order.
        //  For multiple line items, they are multiple transactions for an order.
        public static List<EbayTransactionType> GetOrderTransactions(String orderId)
        {
            String sql_getOneTransaction = "select * from [Transaction] where OrderId='" + orderId + "'";
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql_getOneTransaction);
            if (dt.Rows.Count == 0)
                return null;

            List<EbayTransactionType> trans = new List<EbayTransactionType>();
            foreach (DataRow row in dt.Rows)
            {
                EbayTransactionType tran = new EbayTransactionType();
                tran.OrderId = orderId;
                tran.EbayTransactionId = StringUtil.GetSafeString(row["EbayTransactionId"]);
                tran.OrderLineItemId = StringUtil.GetSafeString(row["OrderLineItemId"]);
                // ZHI_TODO:

                trans.Add(tran);
            }

            return trans;
        }

        #endregion

        #region Update transaction data

        public static bool UpdateTransactionDeliveryStatus(String transId, bool isDelivered, int deliveryNoteId)
        {
            String sql = "";
            if (isDelivered)
            {
                sql = string.Format("update [Transaction] set IsDelivered={0}, DeliveryNoteId={1} where TransactionId={2}",
                    1, deliveryNoteId, transId);
            }
            else
            {
                sql = string.Format("update [Transaction] set IsDelivered={0}, DeliveryNoteId={1} where TransactionId={2}",
                    0, -1, transId);
            }

            DataFactory.ExecuteSql(sql);
            return true;
        }

        public static bool UpdateTransactionMessageStatus(int transId, TransactionMessageStatus messageStatus)
        {
            String sql = string.Format("update [Transaction] set MessageStatus={0} where TransactionId={1}",
                (int)messageStatus, transId);

            DataFactory.ExecuteSql(sql);
            return true;
        }

        public static bool UpdateTransactionsShippingService(List<string> orderLineItemIds, string shippingServiceCode, string shippingServiceDesc)
        {
            // if transIds = (1001, 1002, 1003), then
            // transIdsStr = 1001, 1002, 1003
            String orderLineItemIdsStr = "";
            bool first = true;
            foreach (string orderLineItemId in orderLineItemIds)
            {
                if (first == false)
                    orderLineItemIdsStr += ",";
                orderLineItemIdsStr += "'" + orderLineItemId.ToString() + "'";
                if (first == true)
                    first = false;
            }

            String sql = "update [Transaction] set ShippingServiceCode='" + shippingServiceCode + "', ShippingService='"
                + shippingServiceDesc + "' where OrderId in (" + orderLineItemIdsStr + ")";

            DataFactory.ExecuteSql(sql);
            return true;
        }


        public static bool updateTransactionShippingCost(int transId, double shippingCost)
        {
            String sql = string.Format("update [Transaction] set ShippingCost={0} where TransactionId={1}",
                shippingCost, transId);
            DataFactory.ExecuteSql(sql);
            return true;
        }

        public static bool UpdateTransactionShippedStatus(int transId, bool shipped, DateTime shippedDate)
        {
            String sql = string.Format("update [Transaction] set IsShipped={0}, ShippedDate='{1}' where TransactionId={2}",
                shipped ? 1 : 0, StringUtil.GetSafeDateTime(shippedDate), transId);
            DataFactory.ExecuteSql(sql);
            return true;
        }

        public static bool UpdateTransactionSellerLeftFeedback(int transId, bool bSellerLeftFeedback)
        {
            String sql = string.Format("update [Transaction] set IsSellerLeftFeedback={0} where TransactionId={1}",
                bSellerLeftFeedback ? 1 : 0, transId);
            DataFactory.ExecuteSql(sql);
            return true;
        }

        public static bool UpdateTransactionBuyerLeftFeedback(int transId, bool bBuyerLeftFeedback)
        {
            String sql = string.Format("update [Transaction] set IsBuyerLeftFeedback={0} where TransactionId={1}",
                bBuyerLeftFeedback ? 1 : 0, transId);
            DataFactory.ExecuteSql(sql);
            return true;
        }

        public static bool UpdateTransactionShippingTrackingNo(int transId, String trackingNo)
        {
            String sql = string.Format("update [Transaction] set ShippingTrackingNo={0} where TransactionId={1}",
                     trackingNo, transId);
            DataFactory.ExecuteSql(sql);
            return true;
        }

        public static bool UpdateTransactionItemSKU(int transId, string SKU)
        {
            String sql = "update [Transaction] set ItemSKU='" + SKU + "' where TransactionId=" + transId;
            DataFactory.ExecuteSql(sql);
            return true;
        }

        #endregion


        public static bool InsertOrUpdateOneTransaction(EbayTransactionType trans)
        {
            if (trans.IsValid() == false)
                return false;

            bool result = true;

            EbayTransactionType transLoc = EbayTransactionDAL.GetOneTransaction(trans.OrderLineItemId);
            if (transLoc != null)
            {
                result = UpdateOneTransaction(trans);
            }
            else
            {
                result = InsertOneTransaction(trans);
            }

            return true;
        }

        #region Update one transaction

        public static bool UpdateOneTransaction(EbayTransactionType trans)
        {
            if (trans.IsValid() == false)
                return false;

            EbayTransactionType transLoc = EbayTransactionDAL.GetOneTransaction(trans.OrderLineItemId);
            if (transLoc == null)
            {
                Logger.WriteSystemLog(string.Format("No transaction with EbayTransactionId={0}", trans.EbayTransactionId));
                return false;
            }

            bool needUpdateIsPaid = false;
            if (trans.IsPaid != transLoc.IsPaid)
            {
                Logger.WriteSystemLog(string.Format("Update one transaction with id={0}, set isPaid={1}", trans.EbayTransactionId, trans.IsPaid));
                needUpdateIsPaid = true;
            }

            bool needUpdateShipped = false;
            if (trans.IsShipped != transLoc.IsShipped)
            {
                Logger.WriteSystemLog(string.Format("Update one transaction with id={0}, set IsShipped={1}", trans.EbayTransactionId, trans.IsShipped));
                needUpdateShipped = true;
            }

            bool needUpdateBuyerFeedbackLeft = false;
            if (trans.IsBuyerLeftFeedback != transLoc.IsBuyerLeftFeedback)
            {
                Logger.WriteSystemLog(string.Format("Update one transaction with id={0}, set BuyerFeedbackLeft={1}", trans.EbayTransactionId, trans.IsBuyerLeftFeedback));
                needUpdateBuyerFeedbackLeft = true;
            }

            bool needUpdateSellerFeedbackLeft = false;
            if (trans.IsSellerLeftFeedback != transLoc.IsSellerLeftFeedback)
            {
                Logger.WriteSystemLog(string.Format("Update one transaction with id={0}, set SellerFeedbackLeft={1}", trans.EbayTransactionId, trans.IsSellerLeftFeedback));
                needUpdateSellerFeedbackLeft = true;
            }

            bool needUpdateSellerName = false;
            if (trans.SellerName == "")
                needUpdateSellerName = true;

            bool needUpdateTotalPrice = false;
            if (trans.TotalPrice != transLoc.TotalPrice)
                needUpdateTotalPrice = true;

            bool needUpdateSaleRecordId = false;
            if (trans.EbayRecordId != -1 && trans.EbayRecordId != transLoc.EbayRecordId)
                needUpdateSaleRecordId = true;

            bool needUpdateSaleDateCN = false;
            if (trans.SaleDateCN != null && 0 != trans.SaleDateCN.CompareTo(transLoc.SaleDateCN))
                needUpdateSaleDateCN = true;

            bool needUpdateTrackingNumber = false;
            if (trans.ShippingTrackingNo != null && trans.ShippingTrackingNo != transLoc.ShippingTrackingNo)
                needUpdateTrackingNumber = true;

            bool needUpdateShippingDate = false;
            if (trans.ShippedDate != transLoc.ShippedDate)
                needUpdateShipped = true;

            if (!needUpdateIsPaid && !needUpdateShipped && !needUpdateBuyerFeedbackLeft &&
                !needUpdateSellerFeedbackLeft && !needUpdateSellerName && !needUpdateTotalPrice &&
                !needUpdateSaleRecordId && !needUpdateSaleDateCN && !needUpdateTrackingNumber && !needUpdateShippingDate)
                return true;

            if (transLoc.TransactionId <= 0)
            {
                Logger.WriteSystemLog("Invalid transaction id.");
                return false;
            }

            // CAUTION: we need to retain some column values in the old entry,
            //  such as the message status we calculated last time.
            if (transLoc.ItemSKU != null && transLoc.ItemSKU != "")
                trans.ItemSKU = transLoc.ItemSKU;
            if (transLoc.ShippingService != null && transLoc.ShippingService != "")
                trans.ShippingService = transLoc.ShippingService;
            if (transLoc.ShippingServiceCode != null && transLoc.ShippingServiceCode != "")
                trans.ShippingServiceCode = transLoc.ShippingServiceCode;
            trans.MessageStatus = transLoc.MessageStatus;

            bool result = UpdateOneTransactionInternal(transLoc.TransactionId, trans);
            return result;
        }

        private static bool UpdateOneTransactionInternal(int transId, EbayTransactionType trans)
        {
            bool result = false;

            IDbCommand cmd = DataFactory.CreateCommand(null);
            cmd.CommandText = @"Update [Transaction] set SellerName=@SellerName, OrderId=@OrderId, OrderLineItemId=@OrderLineItemId, EbayTransactionId=@EbayTransactionId, EbayRecordId=@EbayRecordId, BuyerId=@BuyerId, BuyerRating=@BuyerRating,"
                    + "BuyerCountryEbayCode=@BuyerCountryEbayCode, BuyerCountry4PXCode=@BuyerCountry4PXCode,"
                    + "BuyerCountry=@BuyerCountry, BuyerCompanyName=@BuyerCompanyName, BuyerName=@BuyerName, BuyerStateOrProvince=@BuyerStateOrProvince, BuyerCity=@BuyerCity,"
                    + "BuyerTel=@BuyerTel, BuyerMail=@BuyerMail, BuyerPostalCode=@BuyerPostalCode, BuyerAddress=@BuyerAddress, BuyerAddressCompact=@BuyerAddressCompact, BuyerAddressLine1=@BuyerAddressLine1, BuyerAddressLine2=@BuyerAddressLine2, "
                    + "BuyerPayPal=@BuyerPayPal,ItemId=@ItemId, ItemTitle=@ItemTitle, ItemSKU=@ItemSKU, ItemPrice=@ItemPrice, SaleQuantity=@SaleQuantity, SalePrice=@SalePrice, TotalPrice=@TotalPrice, CurrencyId=@CurrencyId,"
                    + "SaleDate=@SaleDate, SaleDateCN=@SaleDateCN, IsPaid=@IsPaid, PaidDate=@PaidDate, IsShipped=@IsShipped, ShippedDate=@ShippedDate, ShippingServiceCode=@ShippingServiceCode, ShippingService=@ShippingService, ShippingTrackingNo=@ShippingTrackingNo, ShippingCost=@ShippingCost, FinalValueFee=@FinalValueFee, PayPalFee=@PayPalFee,"
                    + "IsReceived=@IsReceived, IsBuyerLeftFeedback=@IsBuyerLeftFeedback, IsSellerLeftFeedback=@IsSellerLeftFeedback, IsNeedAttention=@IsNeedAttention, MessageStatus=@MessageStatus, IsContactedBuyer=@IsContactedBuyer,"
                    + "LastContactedBuyerDate=@LastContactedBuyerDate, IsResendReplacement=@IsResendReplacement, UserComment=@UserComment where TransactionId=@TransactionId";

            DataFactory.AddCommandParam(cmd, "@SellerName", DbType.String, trans.SellerName);
            DataFactory.AddCommandParam(cmd, "@OrderId", DbType.String, trans.OrderId);
            DataFactory.AddCommandParam(cmd, "@OrderLineItemId", DbType.String, trans.OrderLineItemId);
            DataFactory.AddCommandParam(cmd, "@EbayTransactionId", DbType.String, trans.EbayTransactionId);
            DataFactory.AddCommandParam(cmd, "@EbayRecordId", DbType.Int32, trans.EbayRecordId);
            DataFactory.AddCommandParam(cmd, "@BuyerId", DbType.String, trans.BuyerId);
            DataFactory.AddCommandParam(cmd, "@BuyerRating", DbType.Int32, trans.BuyerRating);

            DataFactory.AddCommandParam(cmd, "@BuyerCountryEbayCode", DbType.String, trans.BuyerCountryEbayCode);
            DataFactory.AddCommandParam(cmd, "@BuyerCountry4PXCode", DbType.String, trans.BuyerCountry4PXCode);
            DataFactory.AddCommandParam(cmd, "@BuyerCountry", DbType.String, trans.BuyerCountry);
            DataFactory.AddCommandParam(cmd, "@BuyerCompanyName", DbType.String, trans.BuyerCompanyName);
            DataFactory.AddCommandParam(cmd, "@BuyerName", DbType.String, trans.BuyerName);

            DataFactory.AddCommandParam(cmd, "@BuyerStateOrProvince", DbType.String, trans.BuyerStateOrProvince);
            DataFactory.AddCommandParam(cmd, "@BuyerCity", DbType.String, trans.BuyerCity);
            DataFactory.AddCommandParam(cmd, "@BuyerTel", DbType.String, trans.BuyerTel);
            DataFactory.AddCommandParam(cmd, "@BuyerMail", DbType.String, trans.BuyerMail);
            DataFactory.AddCommandParam(cmd, "@BuyerPostalCode", DbType.String, trans.BuyerPostalCode);

            DataFactory.AddCommandParam(cmd, "@BuyerAddress", DbType.String, trans.BuyerAddress);
            DataFactory.AddCommandParam(cmd, "@BuyerAddressCompact", DbType.String, trans.BuyerAddressCompact);
            DataFactory.AddCommandParam(cmd, "@BuyerAddressLine1", DbType.String, trans.BuyerAddressLine1);
            DataFactory.AddCommandParam(cmd, "@BuyerAddressLine2", DbType.String, trans.BuyerAddressLine2);
            DataFactory.AddCommandParam(cmd, "@BuyerPayPal", DbType.String, trans.BuyerPayPal);
            DataFactory.AddCommandParam(cmd, "@ItemId", DbType.String, trans.ItemId);
            DataFactory.AddCommandParam(cmd, "@ItemTitle", DbType.String, trans.ItemTitle);

            DataFactory.AddCommandParam(cmd, "@ItemSKU", DbType.String, trans.ItemSKU);
            DataFactory.AddCommandParam(cmd, "@ItemPrice", DbType.Double, trans.ItemPrice);
            DataFactory.AddCommandParam(cmd, "@SaleQuantity", DbType.Int32, trans.SaleQuantity);
            DataFactory.AddCommandParam(cmd, "@SalePrice", DbType.Double, trans.SalePrice);
            DataFactory.AddCommandParam(cmd, "@TotalPrice", DbType.Double, trans.TotalPrice);

            DataFactory.AddCommandParam(cmd, "@CurrencyId", DbType.String, trans.CurrencyId);
            DataFactory.AddCommandParam(cmd, "@SaleDate", DbType.DateTime, trans.SaleDate.ToString());
            DataFactory.AddCommandParam(cmd, "@SaleDateCN", DbType.DateTime, StringUtil.GetSafeDateTime(trans.SaleDateCN));
            DataFactory.AddCommandParam(cmd, "@IsPaid", DbType.Boolean, trans.IsPaid);
            DataFactory.AddCommandParam(cmd, "@PaidDate", DbType.DateTime, trans.PaidDate.ToString());
            DataFactory.AddCommandParam(cmd, "@IsShipped", DbType.Boolean, trans.IsShipped);

            DataFactory.AddCommandParam(cmd, "@ShippedDate", DbType.DateTime, StringUtil.GetSafeDateTime(trans.ShippedDate).ToString());
            DataFactory.AddCommandParam(cmd, "@ShippingServiceCode", DbType.String, trans.ShippingServiceCode);
            DataFactory.AddCommandParam(cmd, "@ShippingService", DbType.String, trans.ShippingService);
            DataFactory.AddCommandParam(cmd, "@ShippingTrackingNo", DbType.String, trans.ShippingTrackingNo);
            DataFactory.AddCommandParam(cmd, "@ShippingCost", DbType.Double, trans.ShippingCost);
            DataFactory.AddCommandParam(cmd, "@FinalValueFee", DbType.Double, trans.FinalValueFee);
            DataFactory.AddCommandParam(cmd, "@PayPalFee", DbType.Double, trans.PayPalFee);

            DataFactory.AddCommandParam(cmd, "@IsReceived", DbType.Boolean, trans.IsReceived);
            DataFactory.AddCommandParam(cmd, "@IsBuyerLeftFeedback", DbType.Boolean, trans.IsBuyerLeftFeedback);
            DataFactory.AddCommandParam(cmd, "@IsSellerLeftFeedback", DbType.Boolean, trans.IsSellerLeftFeedback);
            DataFactory.AddCommandParam(cmd, "@IsNeedAttention", DbType.Boolean, trans.IsNeedAttention);
            DataFactory.AddCommandParam(cmd, "@MessageStatus", DbType.Int32, trans.MessageStatus);

            DataFactory.AddCommandParam(cmd, "@IsContactedBuyer", DbType.Boolean, trans.IsContactedBuyer);
            DataFactory.AddCommandParam(cmd, "@LastContactedBuyerDate", DbType.DateTime, trans.LastContactedBuyerDate.ToString());
            DataFactory.AddCommandParam(cmd, "@IsResendReplacement", DbType.Boolean, trans.IsResendReplacement);
            DataFactory.AddCommandParam(cmd, "@UserComment", DbType.String, trans.UserComment);

            DataFactory.AddCommandParam(cmd, "@TransactionId", DbType.String, transId);

            try
            {
                if (DataFactory.DbConnection.State == ConnectionState.Closed)
                    DataFactory.DbConnection.Open();
                cmd.ExecuteNonQuery();
                result = true;

            }
            catch (DataException)
            {
                // Write to log here.
                result = false;
            }
            finally
            {
                if (DataFactory.DbConnection.State == ConnectionState.Open)
                    DataFactory.DbConnection.Close();
            }

            return result;
        }

        #endregion

        #region Insert one transaction

        // Private to prevent misuse, use public InsertOrUpdateOneTransaction instead!!!!!!!
        private static bool InsertOneTransaction(EbayTransactionType trans)
        {
            if (trans.IsValid() == false)
                return false;

            EbayTransactionType transLoc = EbayTransactionDAL.GetOneTransaction(trans.OrderLineItemId);
            if (transLoc != null)
            {
                Logger.WriteSystemLog(string.Format("Transaction already existed in database: userId={0}, itemTitle={1}",
                   trans.BuyerId, trans.ItemTitle));
                return false;
            }

            IDbCommand cmd = DataFactory.CreateCommand(null);
            cmd.CommandText = @"insert into [Transaction] (SellerName, OrderId, OrderLineItemId, EbayTransactionId, EbayRecordId, BuyerId, BuyerRating,"
                    + "BuyerCountryEbayCode, BuyerCountry4PXCode,"
                    + "BuyerCountry, BuyerCompanyName, BuyerName, BuyerStateOrProvince, BuyerCity,"
                    + "BuyerTel, BuyerMail, BuyerPostalCode, BuyerAddress, BuyerAddressCompact, BuyerAddressLine1, BuyerAddressLine2,"
                    + "BuyerPayPal,ItemId, ItemTitle, ItemSKU, ItemPrice, SaleQuantity, SalePrice, TotalPrice, CurrencyId,"
                    + "SaleDate, SaleDateCN, IsPaid, PaidDate, IsShipped, ShippedDate, ShippingServiceCode, ShippingService, ShippingTrackingNo, ShippingCost, FinalValueFee, PayPalFee,"
                    + "IsReceived, IsBuyerLeftFeedback, IsSellerLeftFeedback, IsNeedAttention, MessageStatus, IsContactedBuyer,"
                    + "LastContactedBuyerDate, IsResendReplacement, UserComment) values ("
                    + "@SellerName, @OrderId, @OrderLineItemId, @EbayTransactionId, @EbayRecordId, @BuyerId, @BuyerRating,"
                    + "@BuyerCountryEbayCode, @BuyerCountry4PXCode,"
                    + "@BuyerCountry, @BuyerCompanyName, @BuyerName, @BuyerStateOrProvince, @BuyerCity,"
                    + "@BuyerTel, @BuyerMail, @BuyerPostalCode, @BuyerAddress, @BuyerAddressCompact, @BuyerAddressLine1, @BuyerAddressLine2,"
                    + "@BuyerPayPal, @ItemId, @ItemTitle, @ItemSKU, @ItemPrice, @SaleQuantity, @SalePrice, @TotalPrice, @CurrencyId,"
                    + "@SaleDate, @SaleDateCN, @IsPaid, @PaidDate, @IsShipped, @ShippedDate, @ShippingServiceCode, @ShippingService, @ShippingTrackingNo, @ShippingCost, @FinalValueFee, @PayPalFee,"
                    + "@IsReceived, @IsBuyerLeftFeedback, @IsSellerLeftFeedback, @IsNeedAttention, @MessageStatus, @IsContactedBuyer,"
                    + "@LastContactedBuyerDate, @IsResendReplacement, @UserComment)";

            DataFactory.AddCommandParam(cmd, "@SellerName", DbType.String, trans.SellerName);
            DataFactory.AddCommandParam(cmd, "@OrderId", DbType.String, trans.OrderId);
            DataFactory.AddCommandParam(cmd, "@OrderLineItemId", DbType.String, trans.OrderLineItemId);
            DataFactory.AddCommandParam(cmd, "@EbayTransactionId", DbType.String, trans.EbayTransactionId);
            DataFactory.AddCommandParam(cmd, "@EbayRecordId", DbType.Int32, trans.EbayRecordId);
            DataFactory.AddCommandParam(cmd, "@BuyerId", DbType.String, trans.BuyerId);
            DataFactory.AddCommandParam(cmd, "@BuyerRating", DbType.Int32, trans.BuyerRating);

            DataFactory.AddCommandParam(cmd, "@BuyerCountryEbayCode", DbType.String, StringUtil.GetSafeString(trans.BuyerCountryEbayCode));
            DataFactory.AddCommandParam(cmd, "@BuyerCountry4PXCode", DbType.String, StringUtil.GetSafeString(trans.BuyerCountry4PXCode));
            DataFactory.AddCommandParam(cmd, "@BuyerCountry", DbType.String, StringUtil.GetSafeString(trans.BuyerCountry));
            DataFactory.AddCommandParam(cmd, "@BuyerCompanyName", DbType.String, StringUtil.GetSafeString(trans.BuyerCompanyName));
            DataFactory.AddCommandParam(cmd, "@BuyerName", DbType.String, trans.BuyerName);

            DataFactory.AddCommandParam(cmd, "@BuyerStateOrProvince", DbType.String, trans.BuyerStateOrProvince);
            DataFactory.AddCommandParam(cmd, "@BuyerCity", DbType.String, trans.BuyerCity);
            DataFactory.AddCommandParam(cmd, "@BuyerTel", DbType.String, StringUtil.GetSafeString(trans.BuyerTel));
            DataFactory.AddCommandParam(cmd, "@BuyerMail", DbType.String, StringUtil.GetSafeString(trans.BuyerMail));
            DataFactory.AddCommandParam(cmd, "@BuyerPostalCode", DbType.String, StringUtil.GetSafeString(trans.BuyerPostalCode));

            DataFactory.AddCommandParam(cmd, "@BuyerAddress", DbType.String, StringUtil.GetSafeString(trans.BuyerAddress));
            DataFactory.AddCommandParam(cmd, "@BuyerAddressCompact", DbType.String, StringUtil.GetSafeString(trans.BuyerAddressCompact));
            DataFactory.AddCommandParam(cmd, "@BuyerAddressLine1", DbType.String, StringUtil.GetSafeString(trans.BuyerAddressLine1));
            DataFactory.AddCommandParam(cmd, "@BuyerAddressLine2", DbType.String, StringUtil.GetSafeString(trans.BuyerAddressLine2));
            DataFactory.AddCommandParam(cmd, "@BuyerPayPal", DbType.String, StringUtil.GetSafeString(trans.BuyerPayPal));
            DataFactory.AddCommandParam(cmd, "@ItemId", DbType.String, StringUtil.GetSafeString(trans.ItemId));
            DataFactory.AddCommandParam(cmd, "@ItemTitle", DbType.String, StringUtil.GetSafeString(trans.ItemTitle));

            DataFactory.AddCommandParam(cmd, "@ItemSKU", DbType.String, StringUtil.GetSafeString(trans.ItemSKU));
            DataFactory.AddCommandParam(cmd, "@ItemPrice", DbType.Double, trans.ItemPrice);
            DataFactory.AddCommandParam(cmd, "@SaleQuantity", DbType.Int32, trans.SaleQuantity);
            DataFactory.AddCommandParam(cmd, "@SalePrice", DbType.Double, trans.SalePrice);
            DataFactory.AddCommandParam(cmd, "@TotalPrice", DbType.Double, trans.TotalPrice);

            DataFactory.AddCommandParam(cmd, "@CurrencyId", DbType.String, StringUtil.GetSafeString(trans.CurrencyId));
            DataFactory.AddCommandParam(cmd, "@SaleDate", DbType.Date, StringUtil.GetSafeDateTime(trans.SaleDate).ToString());
            DataFactory.AddCommandParam(cmd, "@SaleDateCN", DbType.Date, StringUtil.GetSafeDateTime(trans.SaleDateCN).ToString());
            DataFactory.AddCommandParam(cmd, "@IsPaid", DbType.Boolean, trans.IsPaid);
            DataFactory.AddCommandParam(cmd, "@PaidDate", DbType.Date, StringUtil.GetSafeDateTime(trans.PaidDate).ToString());
            DataFactory.AddCommandParam(cmd, "@IsShipped", DbType.Boolean, trans.IsShipped);

            DataFactory.AddCommandParam(cmd, "@ShippedDate", DbType.Date, StringUtil.GetSafeDateTime(trans.ShippedDate).ToString());
            DataFactory.AddCommandParam(cmd, "@ShippingServiceCode", DbType.String, trans.ShippingServiceCode);
            DataFactory.AddCommandParam(cmd, "@ShippingService", DbType.String, trans.ShippingService);
            DataFactory.AddCommandParam(cmd, "@ShippingTrackingNo", DbType.String, trans.ShippingTrackingNo);

            DataFactory.AddCommandParam(cmd, "@ShippingCost", DbType.Double, trans.ShippingCost);
            DataFactory.AddCommandParam(cmd, "@FinalValueFee", DbType.Double, trans.FinalValueFee);
            DataFactory.AddCommandParam(cmd, "@PayPalFee", DbType.Double, trans.PayPalFee);

            DataFactory.AddCommandParam(cmd, "@IsReceived", DbType.Boolean, trans.IsReceived);
            DataFactory.AddCommandParam(cmd, "@IsBuyerLeftFeedback", DbType.Boolean, trans.IsBuyerLeftFeedback);
            DataFactory.AddCommandParam(cmd, "@IsSellerLeftFeedback", DbType.Boolean, trans.IsSellerLeftFeedback);
            DataFactory.AddCommandParam(cmd, "@IsNeedAttention", DbType.Boolean, trans.IsNeedAttention);
            DataFactory.AddCommandParam(cmd, "@MessageStatus", DbType.Int32, trans.MessageStatus);
            DataFactory.AddCommandParam(cmd, "@IsContactedBuyer", DbType.Boolean, trans.IsContactedBuyer);

            DataFactory.AddCommandParam(cmd, "@LastContactedBuyerDate", DbType.Date, StringUtil.GetSafeDateTime(trans.LastContactedBuyerDate).ToString());
            DataFactory.AddCommandParam(cmd, "@IsResendReplacement", DbType.Boolean, trans.IsResendReplacement);
            DataFactory.AddCommandParam(cmd, "@UserComment", DbType.String, StringUtil.GetSafeString(trans.UserComment));

            bool result = false;
            int newItemId = 0;

            try
            {
                if (DataFactory.DbConnection.State == ConnectionState.Closed)
                    DataFactory.DbConnection.Open();
                cmd.ExecuteNonQuery();

                IDbCommand cmdNewID = DataFactory.CreateCommand("SELECT @@IDENTITY");

                // Retrieve the Autonumber and store it in the CategoryID column.
                object obj = cmdNewID.ExecuteScalar();
                Int32.TryParse(obj.ToString(), out newItemId);
                result = newItemId > 0;
            }
            catch (DataException ex)
            {
                // Write to log here.
                Logger.WriteSystemLog(string.Format("Error : {0}", ex.Message));
                trans.dump();
                result = false;
            }
            finally
            {
                if (DataFactory.DbConnection.State == ConnectionState.Open)
                    DataFactory.DbConnection.Close();
            }

            return result;
        }   // InsertOneTransaction

        #endregion
    }
}
