using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EbayMaster
{
    public class EbayActiveListingType
    {
        public int ListId { get; set; }
        public String SellerName { get; set; }
        public String ItemID { get; set; }
        public String Title { get; set; }
        public String ListingType { get; set; }
        public String GalleryURL { get; set; }
        public int QuantityBid { get; set; }
        public double MaxBid { get; set; }
        public double StartPrice { get; set; }
        public double BuyItNowPrice { get; set; }
        public String CurrencyID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public String ViewItemURL { get; set; }
        public int ListDuration { get; set; }
        public bool PrivateListing { get; set; }
        public int Quantity { get; set; }
        public int QuantityAvailable { get; set; }
        public String SellingStatus { get; set; }
        public String SKU { get; set; }
        public String TimeLeft { get; set; }
        public int WatchCount { get; set; }
        public int BidCount { get; set; }
        public int BidderCount { get; set; }
        public double CurrentPrice { get; set; }
        public double FVF { get; set; }
        public String ImagePath { get; set; }

        public EbayActiveListingType()
        {

        }

        // Input: P16DT1H15M2S
        // Output: 16 days/1 hour
        public String getTimeLeftStr(String timeLeft)
        {
            String[] subStrs = timeLeft.Split(new char[] {'P', 'p', 'T', 't' });
            return null;
        }
    }
}
