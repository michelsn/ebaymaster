using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using eBay.Service.Core.Sdk;
using Samples.Helper;

using eBay.Service.Core.Soap;
using eBay.Service.Call;
using eBay.Service.Util;

namespace EbayMaster
{
    public class EbayTransactionBiz
    {
        //
        // GetAccount
        //  http://developer.ebay.com/DevZone/XML/docs/Reference/eBay/GetAccount.html
        //
        public static string GetAccount(AccountType account)
        {
            if (account.SellerApiContext == null)
                return null;

            GetAccountCall getAccountApiCall = new GetAccountCall(account.SellerApiContext);
            DetailLevelCodeType[] detailLevels = new DetailLevelCodeType[] { DetailLevelCodeType.ReturnSummary };
            getAccountApiCall.DetailLevelList = new DetailLevelCodeTypeCollection(detailLevels);

            getAccountApiCall.AccountEntrySortType = AccountEntrySortTypeCodeType.AccountEntryCreatedTimeAscending;
            //  AccountHistorySelection
            //      - BetweenSpecifiedDates, BeginDate/EndDate must be used.
            //      - CustomCode
            //      - LastInvoice
            //      - SpecifiedInvoice, InvoiceDate must be used.
            getAccountApiCall.AccountHistorySelection = AccountHistorySelectionCodeType.LastInvoice;
            getAccountApiCall.BeginDate = new System.DateTime(2012, 10, 30);
            getAccountApiCall.EndDate = DateTime.Now;
            getAccountApiCall.ExcludeBalance = false;
            getAccountApiCall.ExcludeSummary = false;
            getAccountApiCall.Pagination = new eBay.Service.Core.Soap.PaginationType();
            getAccountApiCall.Pagination.PageNumber = 1;
            getAccountApiCall.Pagination.EntriesPerPage = 10;
            getAccountApiCall.Version = "705";
            getAccountApiCall.StartTimeFilter = new eBay.Service.Core.Soap.TimeFilter();
            getAccountApiCall.StartTimeFilter.TimeFrom = new System.DateTime(2012, 10, 30);
            getAccountApiCall.StartTimeFilter.TimeTo = DateTime.Now;

            //AccountHistorySelectionCodeType accountHistorySelectionCodeType = AccountHistorySelectionCodeType.BetweenSpecifiedDates;
            getAccountApiCall.GetAccount(AccountHistorySelectionCodeType.LastInvoice);

            AccountEntriesType accountEntriesType = getAccountApiCall.AccountEntries;
            AccountSummaryType accountSummary = getAccountApiCall.AccountSummary;

            return null;
        }

        public static string GetShippingAddressString(AddressType addressType)
        {
            String shippingAddress = /*addressType.Name + "\r\n" + */addressType.Street1 + "\r\n";
            if (addressType.Street2.Length > 0)
                shippingAddress += addressType.Street2 + "\r\n";
            shippingAddress += addressType.CityName + " " + addressType.StateOrProvince + " " + addressType.PostalCode + "\r\n";
            shippingAddress += addressType.CountryName;

            return shippingAddress;
        }

        public static string GetShippingAddressCompactString(AddressType addressType)
        {
            String shippingAddress = /*addressType.Name + "\r\n" + */addressType.Street1 + "\r\n";
            if (addressType.Street2.Length > 0)
                shippingAddress += addressType.Street2 + "\r\n";
            shippingAddress += addressType.CityName + " " + addressType.StateOrProvince + " " + addressType.PostalCode;

            return shippingAddress;
        }

        // Given a time period specified by startDate and endDate, returns all the order ids created in that period.
        public static StringCollection GetAllOrderIds(AccountType account, DateTime startDate, DateTime endDate)
        {
            if (account.SellerApiContext == null)
                return null;

            TimeFilter timeFilter = new TimeFilter();
            timeFilter.TimeFrom = startDate;
            timeFilter.TimeTo = endDate;

            GetOrdersCall getOrdersApiCall = new GetOrdersCall(account.SellerApiContext);
            DetailLevelCodeType[] detailLevels = new DetailLevelCodeType[] { DetailLevelCodeType.ReturnSummary };
            getOrdersApiCall.DetailLevelList = new DetailLevelCodeTypeCollection(detailLevels);

            getOrdersApiCall.GetOrders(timeFilter, TradingRoleCodeType.Seller, OrderStatusCodeType.All);

            StringCollection orderIds = new StringCollection();
            foreach (OrderType order in getOrdersApiCall.OrderList)
            {
                orderIds.Add(order.OrderID);
            }

            return orderIds;
        }

        // Upload tracking number to ebay.
        //  CompleteSale
        //  https://developer.ebay.com/DevZone/XML/docs/Reference/ebay/CompleteSale.html
        public static bool UploadTrackingNumber(AccountType account, string itemId, string ebayTransId, 
            string shippingCarrier, string shipmentTrackingNumber)
        {
            CompleteSaleCall completeSaleApiCall = new CompleteSaleCall(account.SellerApiContext);
            completeSaleApiCall.Shipment = new eBay.Service.Core.Soap.ShipmentType();

            ShipmentTrackingDetailsType shippingDetailsType = new ShipmentTrackingDetailsType();
            shippingDetailsType.ShippingCarrierUsed = shippingCarrier;
            shippingDetailsType.ShipmentTrackingNumber = shipmentTrackingNumber;
            completeSaleApiCall.Shipment.ShipmentTrackingDetails = new eBay.Service.Core.Soap.ShipmentTrackingDetailsTypeCollection();
            completeSaleApiCall.Shipment.ShipmentTrackingDetails.Add(shippingDetailsType);

            completeSaleApiCall.ItemID = itemId;
            completeSaleApiCall.TransactionID = ebayTransId;

            bool result = false;
            try
            {
                completeSaleApiCall.Execute();
                result = true;
            }
            catch (System.Exception ex)
            {
                Logger.WriteSystemLog(string.Format("ERROR: UploadTrackingNumber failed with error msg={0}", ex.Message));
            }
            

            return result;
        }

        // Leave feedback for an order.
        public static bool LeaveFeedback(AccountType account, string orderId, string buyerId, string itemId, string ebayTransId)
        {
            CompleteSaleCall completeSaleApiCall = new CompleteSaleCall(account.SellerApiContext);

            FeedbackInfoType feedback = new FeedbackInfoType();
            feedback.CommentText = "Great Buyer, fast payment, thanks for purchasing!";
            feedback.CommentType = CommentTypeCodeType.Positive;
            feedback.TargetUser = buyerId;

            completeSaleApiCall.FeedbackInfo = feedback;

            completeSaleApiCall.ItemID = itemId;
            completeSaleApiCall.TransactionID = ebayTransId;

            completeSaleApiCall.Execute();
            
            return true;
        }

        // Complete a order: e.g., 
        //    1) mark item as paid,
        //    2) mark item as shipped,
        //    3) leave feedback for user
        //  Unique identifier for an eBay order line item (transaction). 
        //  The TransactionID can be paired up with the corresponding ItemID and used in the CompleteSale request 
        //  to identify a single line item order. 
        //  Unless an OrderLineItemID is used to identify a single line item order, 
        //  or the OrderID is used to identify a single or multiple line item order, 
        //  the ItemID/TransactionID pair must be specified. 
        //  For a multiple line item order, OrderID must be used. 
        //  If OrderID or OrderLineItemID are specified, the ItemID/TransactionID pair is ignored if present in the same request.
        public static bool CompleteSale(AccountType account, string orderId, string buyerId, string itemId, string ebayTransId,
                                        bool setIsPaid, bool isPaid, bool setIsShipped, bool isShipped)
        {
            Logger.WriteSystemLog(string.Format("buyerId={0} orderId={1} :Mark item as shipped.", buyerId, orderId));

            FeedbackInfoType feedback = new FeedbackInfoType();
            feedback.CommentText = "Great Buyer, fast payment, thanks for purchasing!";
            feedback.CommentType = CommentTypeCodeType.Positive;
            feedback.TargetUser = buyerId;

            CompleteSaleCall completeSaleApiCall = new CompleteSaleCall(account.SellerApiContext);
            completeSaleApiCall.FeedbackInfo = feedback;

            if (setIsPaid)
                completeSaleApiCall.Paid = isPaid;
            if (setIsShipped)
                completeSaleApiCall.Shipped = isShipped;

            //completeSaleApiCall.ItemID = itemId;
            //completeSaleApiCall.TransactionID = ebayTransId;
            completeSaleApiCall.OrderID = orderId;

            completeSaleApiCall.Execute();

            return true;
        }

        // GetAllOrders: Get all orders for a seller in the time period user specified.
        // Future enhancement: add multiple users support.
        // API call ref: http://developer.ebay.com/DevZone/xml/docs/Reference/ebay/GetOrders.html
        public static List<EbayTransactionType> GetAllOrders(AccountType account, DateTime startDate, DateTime endDate)
        {
            TimeFilter timeFilter = new TimeFilter();
            timeFilter.TimeFrom = startDate;
            timeFilter.TimeTo = endDate;
            return GetAllOrders(account, timeFilter, null);
        }

        public static List<EbayTransactionType> GetAllOrders(AccountType account, TimeFilter timeFilter, StringCollection orderIds)
        {
            List<EbayTransactionType> transList = new List<EbayTransactionType>();

            GetOrdersCall getOrdersApiCall = new GetOrdersCall(account.SellerApiContext);
            getOrdersApiCall.IncludeFinalValueFee = true;
            DetailLevelCodeType[] detailLevels = new DetailLevelCodeType[] { DetailLevelCodeType.ReturnAll };
            getOrdersApiCall.DetailLevelList = new DetailLevelCodeTypeCollection(detailLevels);
            if (orderIds != null)
                getOrdersApiCall.OrderIDList = orderIds;

            try
            {
                OrderTypeCollection orders = getOrdersApiCall.GetOrders(timeFilter, TradingRoleCodeType.Seller, OrderStatusCodeType.All);

                foreach (OrderType order in orders)
                {
                    AddressType addressType = order.ShippingAddress;
                    String shippingAddress = GetShippingAddressString(addressType);
                    String shippingAddressCompact = GetShippingAddressCompactString(addressType);

                    bool completed = order.OrderStatus == OrderStatusCodeType.Completed;

                    foreach (TransactionType trans in order.TransactionArray)
                    {
                        #region Process each ebay transaction
                        // Check if this transaction has already be recorded in system.
                        String transId = trans.TransactionID;
                        if (transId == null || transId == "")
                        {
                            Logger.WriteSystemLog("GetAllOrders: Invalid transaction id, skip and continue.");
                            continue;
                        }

                        EbayTransactionType ebayTrans = new EbayTransactionType();
                        ebayTrans.SellerName = account.ebayAccount;
                        ebayTrans.OrderId = order.OrderID;
                        ebayTrans.OrderLineItemId = trans.OrderLineItemID;
                        ebayTrans.EbayTransactionId = trans.TransactionID;
                        ebayTrans.EbayRecordId = order.ShippingDetails.SellingManagerSalesRecordNumberSpecified ? order.ShippingDetails.SellingManagerSalesRecordNumber : -1;
                        ebayTrans.BuyerId = order.BuyerUserID;

                        GetUserCall getUserApiCall = new GetUserCall(account.SellerApiContext);
                        getUserApiCall.UserID = order.BuyerUserID;
                        UserType user = getUserApiCall.GetUser();

                        // BuyerRating
                        if (user.FeedbackScoreSpecified)
                            ebayTrans.BuyerRating = user.FeedbackScore;
                        else
                            ebayTrans.BuyerRating = -1;

                        // BuyerCountryEbayCode
                        ebayTrans.BuyerCountryEbayCode = addressType.Country.ToString();
                        // BuyerCountry4PXCode
                        ebayTrans.BuyerCountry4PXCode = "";

                        // BuyerCountry
                        ebayTrans.BuyerCountry = addressType.CountryName;
                        // BuyerCompanyName
                        ebayTrans.BuyerCompanyName = StringUtil.GetSafeString(addressType.CompanyName);
                        // BuyerName
                        ebayTrans.BuyerName = addressType.Name;
                        // BuyerStateOrProvince
                        ebayTrans.BuyerStateOrProvince = addressType.StateOrProvince;
                        // BuyerCity
                        ebayTrans.BuyerCity = addressType.CityName;
                        // BuyerTel
                        ebayTrans.BuyerTel = addressType.Phone;
                        // BuyerMail
                        ebayTrans.BuyerMail = trans.Buyer.Email;
                        // BuyerPostalCode
                        ebayTrans.BuyerPostalCode = addressType.PostalCode;

                        // BuyerAddress
                        ebayTrans.BuyerAddress = shippingAddress;
                        // BuyerAddressCompact
                        ebayTrans.BuyerAddressCompact = shippingAddressCompact;
                        // BuyerAddressLine1
                        ebayTrans.BuyerAddressLine1 = addressType.Street1;
                        // BuyerAddressLine2
                        ebayTrans.BuyerAddressLine2 = addressType.Street2;
                        // BuyerPayPal
                        ebayTrans.BuyerPayPal = trans.Buyer.Email;

                        // ItemId
                        ebayTrans.ItemId = trans.Item.ItemID;

                        // What is the valid way to determine if there is a variation.
                        if (trans.Variation != null && trans.Variation.VariationTitle != null && trans.Variation.VariationTitle.Trim() != "")
                        {
                            // ItemTitle
                            ebayTrans.ItemTitle = trans.Variation.VariationTitle;
                            // ItemSKU
                            ebayTrans.ItemSKU = trans.Variation.SKU;
                        }
                        else
                        {
                            // ItemTitle
                            ebayTrans.ItemTitle = trans.Item.Title;
                            // ItemSKU
                            ebayTrans.ItemSKU = trans.Item.SKU;
                        }

                        // ItemPrice
                        if (trans.TransactionPrice != null)
                            ebayTrans.ItemPrice = trans.TransactionPrice.Value;
                        // SaleQuantity
                        ebayTrans.SaleQuantity = trans.QuantityPurchased;

                        if (trans.TransactionPrice != null)
                        {
                            // SalePrice
                            ebayTrans.SalePrice = trans.TransactionPrice.Value * trans.QuantityPurchased;
                            // TotalPrice
                            ebayTrans.TotalPrice = trans.TransactionPrice.Value * trans.QuantityPurchased;
                        }

                        // TODO: there may be multiple transactions in one order.
                        if (order.Total != null)
                        {
                            ebayTrans.TotalPrice = order.Total.Value;
                            ebayTrans.CurrencyId = order.Total.currencyID.ToString();
                        }
                        else
                        {
                            // Set a default value.
                            ebayTrans.TotalPrice = 0.0;
                            ebayTrans.CurrencyId = "";
                        }

                        // SaleDate
                        ebayTrans.SaleDate = order.CreatedTime;
                        // SaleDateCN
                        ebayTrans.SaleDateCN = order.CreatedTime.ToLocalTime();
                        // IsPaid
                        ebayTrans.IsPaid = order.PaidTimeSpecified;

                        // order.AmountPaid
                        // order.CheckoutStatus
                        //      ebayPaymentStatus
                        //      Status
                        // orderStatus
                        if (ebayTrans.IsPaid == false)
                        {
                            // Some payment is paid using credit card, and while PayPal is processing the payment,
                            // the transaction is marked as unpaid. we should view it as paid.
                            if (order.OrderStatusSpecified && order.OrderStatus == OrderStatusCodeType.Completed)
                                ebayTrans.IsPaid = true;
                        }

                        if (ebayTrans.IsPaid == false)
                        {
                            if (order.CheckoutStatus.StatusSpecified && order.CheckoutStatus.Status == CompleteStatusCodeType.Complete)
                                ebayTrans.IsPaid = true;
                        }

                        // PaidDate
                        ebayTrans.PaidDate = StringUtil.GetSafeDateTime(order.PaidTime);
                        // IsShipped
                        ebayTrans.IsShipped = order.ShippedTimeSpecified;
                        if (order.ShippedTimeSpecified)
                            ebayTrans.ShippedDate = StringUtil.GetSafeDateTime(order.ShippedTime);
                        else
                            ebayTrans.ShippedDate = DateTime.Now.AddYears(-10);

                        // Store the shippedDate as the local date time.
                        ebayTrans.ShippedDate = ebayTrans.ShippedDate.ToLocalTime();

                        // ShippingServiceCode
                        ebayTrans.ShippingServiceCode = "";
                        // ShippingService
                        ebayTrans.ShippingService = "";
                        // ShippingTrackingNo
                        ebayTrans.ShippingTrackingNo = "";
                        // ShippingCost
                        ebayTrans.ShippingCost = 0.0;
                        // FinalValueFee
                        if (trans.FinalValueFee != null)
                            ebayTrans.FinalValueFee = trans.FinalValueFee.Value;
                        else
                            ebayTrans.FinalValueFee = 0.0;
                        // PayPalFee
                        ebayTrans.PayPalFee = 0.034 * ebayTrans.TotalPrice + 0.3;

                        // IsReceived
                        ebayTrans.IsReceived = false;
                        ebayTrans.IsBuyerLeftFeedback = false;
                        ebayTrans.IsSellerLeftFeedback = false;
                        ebayTrans.IsNeedAttention = false;
                        ebayTrans.MessageStatus = TransactionMessageStatus.NoMessage;
                        ebayTrans.IsContactedBuyer = false;
                        ebayTrans.LastContactedBuyerDate = DateTime.Now.AddYears(-10);
                        ebayTrans.IsResendReplacement = false;
                        ebayTrans.UserComment = "";

                        GetFeedbackCall getFeedbackApiCall = new GetFeedbackCall(account.SellerApiContext);
                        //DetailLevelCodeType[] detailLevels = new DetailLevelCodeType[] { DetailLevelCodeType.ReturnAll };
                        getFeedbackApiCall.DetailLevelList = new DetailLevelCodeTypeCollection(detailLevels);
                        getFeedbackApiCall.OrderLineItemID = trans.OrderLineItemID;
                        FeedbackDetailTypeCollection feedbacks = getFeedbackApiCall.GetFeedback();
                        foreach (FeedbackDetailType feedback in feedbacks)
                        {
                            if (feedback.CommentingUser == account.ebayAccount)
                                ebayTrans.IsSellerLeftFeedback = true;

                            if (feedback.CommentingUser == ebayTrans.BuyerId)
                                ebayTrans.IsBuyerLeftFeedback = true;
                        }

                        if (trans.ShippingDetails != null)
                        {
                            if (trans.ShippingDetails.ShipmentTrackingDetails.Count == 1)
                            {
                                ShipmentTrackingDetailsType shipmentDetails = trans.ShippingDetails.ShipmentTrackingDetails[0];
                                ebayTrans.ShippingTrackingNo = shipmentDetails.ShipmentTrackingNumber;
                            }
                        }
                        
                        transList.Add(ebayTrans);

                        #endregion
                    }      
                }
            }
            catch (Exception ex)
            {
                Logger.WriteSystemLog(string.Format("Unexpected expection : {0}", ex.Message));
            }

            return transList;
        }   // GetAllOrders

        public static bool GetBuyerFeedbackLeft(AccountType account, String buyerId, String orderId)
        {
            //bool sellerLeftFeedback = false;
            //bool buyerLeftFeedback = false;

            GetFeedbackCall getFeedbackApiCall = new GetFeedbackCall(account.SellerApiContext);
            DetailLevelCodeType[] detailLevels = new DetailLevelCodeType[] { DetailLevelCodeType.ReturnAll };
            getFeedbackApiCall.DetailLevelList = new DetailLevelCodeTypeCollection(detailLevels);
            getFeedbackApiCall.OrderLineItemID = orderId;


            try
            {
                FeedbackDetailTypeCollection feedbacks = getFeedbackApiCall.GetFeedback();
                foreach (FeedbackDetailType feedback in feedbacks)
                {
                    //if (feedback.CommentingUser == account.ebayAccount)
                    //    sellerLeftFeedback = true;

                    if (feedback.CommentingUser == buyerId)
                    {
                        return true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.WriteSystemLog(String.Format("GetFeedback failed with error msg={0}", ex.Message));
                Logger.WriteSystemLog(String.Format("Transaction with BuyerId={0} maybe happened more than 90 days.", buyerId));
                return true;
            }

            return false;
        }

        //
        // http://developer.ebay.com/DevZone/XML/docs/Reference/ebay/AddOrder.html
        //  Only incomplete transactions can be combined, otherwise there will be error message:
        //      "Some transactions have already been completed, you can only combine incomplete transactions".
        //
        public static bool MergeOrders(List<String> transactionIds)
        {
            if (transactionIds.Count < 2)
                return false;

            // Verify that all transactions are between same buyer and seller.
            String buyer = "";
            String seller = "";
            Double total = 0.0;
            foreach (String tranId in transactionIds)
            {
                EbayTransactionType trans = EbayTransactionDAL.GetOneTransactonById(tranId);
                if (trans == null)
                    return false;

                if (buyer != "" && buyer != trans.BuyerName)
                    return false;

                if (seller != "" && seller != trans.SellerName)
                    return false;

                buyer = trans.BuyerName;
                seller = trans.SellerName;

                total += trans.ItemPrice * trans.SaleQuantity;
            }

            AccountType account = AccountUtil.GetAccount(seller);
            if (account == null)
                return false;

            AddOrderCall addOrderCall = new AddOrderCall(account.SellerApiContext);
            OrderType orderType = new eBay.Service.Core.Soap.OrderType();
            orderType = new eBay.Service.Core.Soap.OrderType();
            orderType.CreatingUserRole = TradingRoleCodeType.Seller;
            orderType.PaymentMethods = new eBay.Service.Core.Soap.BuyerPaymentMethodCodeTypeCollection();
            orderType.PaymentMethods.Add(BuyerPaymentMethodCodeType.PayPal);
            orderType.Total = new eBay.Service.Core.Soap.AmountType();
            orderType.Total.Value = total;
            orderType.Total.currencyID = CurrencyCodeType.USD;
            orderType.TransactionArray = new eBay.Service.Core.Soap.TransactionTypeCollection();

            foreach (String tranId in transactionIds)
            {
                EbayTransactionType trans = EbayTransactionDAL.GetOneTransactonById(tranId);
                if (trans == null)
                    return false;

                TransactionType tranType = new TransactionType();
                tranType.Item = new ItemType();
                tranType.Item.ItemID = trans.ItemId;
                tranType.TransactionID = trans.EbayTransactionId;
                orderType.TransactionArray.Add(tranType);
            }

            String orderId = addOrderCall.AddOrder(orderType);

            return true;
        }
    }  // class EbayTransactionBiz

}
