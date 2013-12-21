using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EbayMaster
{
    //
    // Enum to indicate the transaction message status.
    //      Invalid                     - Status not specified.
    //      NoMessage                   - There is no message on a specific transaction.
    //      SellerInquired              - Seller sent message on a transaction but buyer didn't reply.
    //      BuyerRepliedSellerReplied   - There are messages on a specific transaction, and every buyer message has been replied.
    //      BuyerRepliedSellerNotReplied- There are messages on a specific transaction, however some buyer messages didn't get replied.
    //
    public enum TransactionMessageStatus
    {
        Invalid = -1,
        NoMessage = 0,
        SellerInquired = 1,
        BuyerRepliedSellerReplied = 2,
        BuyerRepliedSellerNotReplied = 3
    }

    public class EbayTransactionType
    {
        public int TransactionId { get; set; }
        public string SellerName { get; set; }
        public String OrderId { get; set; }
        // OrderLineItemId can be used to uniquely identify an ebay transaction.
        public String OrderLineItemId { get; set; }
        public String EbayTransactionId { get; set; }
        public int EbayRecordId { get; set; }

        public String BuyerId { get; set; }
        public int BuyerRating { get; set; }
        public string BuyerCountryEbayCode { get; set; }
        public string BuyerCountry4PXCode { get; set; }
        public String BuyerCountry { get; set; }

        public string BuyerCompanyName { get; set; }
        public string BuyerName { get; set; }
        public string BuyerStateOrProvince { get; set; }
        public string BuyerCity { get; set; }
        public string BuyerTel { get; set; }
        public string BuyerMail { get; set; }
        public string BuyerPostalCode { get; set; }

        public String BuyerAddress { get; set; }
        public string BuyerAddressCompact { get; set; }
        public string BuyerAddressLine1 { get; set;  }
        public string BuyerAddressLine2 { get; set; }
        public String BuyerPayPal { get; set; }

        public String ItemId { get; set; }
        public String ItemTitle { get; set; }
        public String ItemSKU { get; set; }
        public double ItemPrice { get; set; }
        public int SaleQuantity { get; set; }
        public double SalePrice { get; set; }
        public double TotalPrice { get; set; }
        public string CurrencyId { get; set; }
        public DateTime SaleDate { get; set; }
        public DateTime SaleDateCN { get; set; }

        public bool IsPaid { get; set; }
        public DateTime PaidDate { get; set; }
        public bool IsShipped { get; set; }
        public DateTime ShippedDate { get; set; }
        public String ShippingServiceCode { get; set; }
        public String ShippingService { get; set; }
        public String ShippingTrackingNo { get; set; }
        public double ShippingCost { get; set; }
        public double FinalValueFee { get; set; }
        public double PayPalFee { get; set; }

        public bool IsReceived { get; set; }
        public bool IsBuyerLeftFeedback { get; set; }
        public bool IsSellerLeftFeedback { get; set; }
        public bool IsNeedAttention { get; set; }
        public TransactionMessageStatus MessageStatus { get; set; }
        public bool IsContactedBuyer { get; set; }
        public DateTime LastContactedBuyerDate { get; set; }
        public bool IsResendReplacement { get; set; }
        public string UserComment { get; set; }
        public bool IsDelivered { get; set; }
        public int DeliveryNoteId { get; set; }

        public EbayTransactionType()
        {
        }

        public bool IsValid()
        {
            return true;
        }

        public void dump()
        {
            string dumpStr = string.Format("TransactionId={0},SellerName={1},OrderId={2}, OrderLineItemId={3}, EbayTransactionId={4}, BuyerId={5}, BuyerRating={6},\r\n",
                TransactionId, SellerName, OrderId, OrderLineItemId, EbayTransactionId, BuyerId, BuyerRating);
            dumpStr += string.Format("BuyerCountryEbayCode={0},BuyerCountry4PXCode={1}, BuyerCountry={2}, BuyerCompanyName={3}, BuyerName={4}, BuyerStateOrProvince={5},\r\n",
                BuyerCountryEbayCode, BuyerCountry4PXCode, BuyerCountry, BuyerCompanyName, BuyerName, BuyerStateOrProvince);
            dumpStr += string.Format("BuyerCity={0}, BuyerTel={1}, BuyerMail={2}, BuyerPostalCode={3}, BuyerAddress={4}, BuyerAddressCompact={5},\r\n",
                BuyerCity, BuyerTel, BuyerMail, BuyerPostalCode, BuyerAddress, BuyerAddressCompact);
            dumpStr += string.Format("BuyerPayPal={0}, ItemId={1}, ItemTitle={2}, ItemSKU={3}, ItemPrice={4}, SaleQuantity={5},\r\n",
                BuyerPayPal, ItemId, ItemTitle, ItemSKU, ItemPrice, SaleQuantity);
            dumpStr += string.Format("SalePrice={0}, TotalPrice={1}, CurrencyId={2}, SaleDate={3}, IsPaid={4}, PaidDate={5},\r\n",
                SalePrice, TotalPrice, CurrencyId, SaleDate, IsPaid, PaidDate);
            dumpStr += string.Format("IsShipped={0}, ShippedDate={1}, ShippingServiceCode={2}, ShippingService={3}, ShippingCost={4}, FinalValueFee={5},\r\n",
                IsShipped, ShippedDate, ShippingServiceCode, ShippingService, ShippingCost, FinalValueFee);
            dumpStr += string.Format("PayPalFee={0}, IsReceived={1}, IsBuyerLeftFeedback={2}, IsSellerLeftFeedback={3}, IsNeedAttention={4}, IsContactedBuyer={5},\r\n",
                PayPalFee, IsReceived, IsBuyerLeftFeedback, IsSellerLeftFeedback, IsNeedAttention, IsContactedBuyer);
            dumpStr += string.Format("LastContactedBuyerDate={0}, IsResendReplacement={1}, UserComment={2}",
                LastContactedBuyerDate, IsResendReplacement, UserComment);

            Logger.WriteSystemLog(dumpStr);
        }
    }
}
