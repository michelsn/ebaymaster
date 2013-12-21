using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EbayMaster
{
    public class EbaySellerInfoType
    {
        public int SellerInfoId;
        public string SellerName;
        public double AmountLimitRemaining;
        public string AmountLimitRemainingCurrencyID;
        public int AuctionBidCount;
        public int AuctionSellingCount;
        public int QuantityLimitRemaining;
        public int SoldDurationInDays;
        public int TotalSoldCount;
        public double TotalSoldValue;
        public string TotalSoldValueCurrencyID;
        public string AccountState;
        public string BankAccountInfo;
        public double CurrentBalance;
        public double InvoiceBalance;
        public double InvoiceNewFee;
        public double InvoicePayment;
        public double LastAmountPaid;

        public EbaySellerInfoType()
        {

        }
    }
}
