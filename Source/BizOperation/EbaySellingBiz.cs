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
    public class EbaySellingBiz
    {
        #region GetMyeBaySelling - Get active/sold listing

        //
        // GetMyeBaySelling
        //  Use this call to return information from the All Selling section of the authenticated user's My eBay account. 
        //      http://developer.ebay.com/DevZone/XML/docs/Reference/eBay/GetMyeBaySelling.html
        //

        public static List<EbayActiveListingType> GetMyActiveListing(AccountType account)
        {
            if (account.SellerApiContext == null)
            {
                Logger.WriteSystemLog("Invalid API context in GetMyActiveListing.");
                return null;
            }

            GetMyeBaySellingCall getMyeBaySellingApiCall = new GetMyeBaySellingCall(account.SellerApiContext);
            DetailLevelCodeType[] detailLevels = new DetailLevelCodeType[] { DetailLevelCodeType.ReturnAll };
            getMyeBaySellingApiCall.DetailLevelList = new DetailLevelCodeTypeCollection(detailLevels);

            getMyeBaySellingApiCall.SellingSummary = new eBay.Service.Core.Soap.ItemListCustomizationType();
            getMyeBaySellingApiCall.SellingSummary.Include = true;

            getMyeBaySellingApiCall.ActiveList = new eBay.Service.Core.Soap.ItemListCustomizationType();
            getMyeBaySellingApiCall.ActiveList.Include = true;
            getMyeBaySellingApiCall.ActiveList.IncludeNotes = true;
            //getMyeBaySellingApiCall.ActiveList.ListingType = ListingTypeCodeType.FixedPriceItem;
            getMyeBaySellingApiCall.ActiveList.Pagination = new PaginationType();
            getMyeBaySellingApiCall.ActiveList.Pagination.EntriesPerPage = 25; // max 200 default 25
            getMyeBaySellingApiCall.ActiveList.Pagination.PageNumber = 1;

            

            //getMyeBaySellingApiCall.BidList = new ItemListCustomizationType();
            //getMyeBaySellingApiCall.BidList.Include = true;
            //getMyeBaySellingApiCall.BidList.Pagination = new PaginationType();
            //getMyeBaySellingApiCall.BidList.Pagination.EntriesPerPage = 25;
            //getMyeBaySellingApiCall.BidList.Pagination.PageNumber = 1;

            getMyeBaySellingApiCall.GetMyeBaySelling();

            AmountType amountLimitRemaining = getMyeBaySellingApiCall.Summary.AmountLimitRemaining;
            long quantityLimitRemaining = getMyeBaySellingApiCall.Summary.QuantityLimitRemaining;

            PaginatedItemArrayType paginatedItemArray = getMyeBaySellingApiCall.ActiveListReturn;

            if (paginatedItemArray == null || paginatedItemArray.ItemArray == null)
                return null;

            List<EbayActiveListingType> activeListings = new List<EbayActiveListingType>();
            foreach (ItemType item in paginatedItemArray.ItemArray)
            {
                EbayActiveListingType activeListing = new EbayActiveListingType();
                activeListing.ListId = 0;   // to be set
                activeListing.SellerName = account.ebayAccount;
                activeListing.ItemID = StringUtil.GetSafeString(item.ItemID);
                activeListing.Title = StringUtil.GetSafeString(item.Title);
                if (item.ListingTypeSpecified)
                {
                    if (ListingTypeCodeType.FixedPriceItem == item.ListingType)
                        activeListing.ListingType = "FixedPriceItem";
                    else if (ListingTypeCodeType.Auction == item.ListingType ||
                            ListingTypeCodeType.Chinese == item.ListingType)
                        activeListing.ListingType = "Auction";
                    else
                        activeListing.ListingType = "Unknown";
                }
                else
                {
                    activeListing.ListingType = "Unknown";
                }
                if (item.PictureDetails != null)
                    activeListing.GalleryURL = StringUtil.GetSafeString(item.PictureDetails.GalleryURL);
                else
                    activeListing.GalleryURL = "";

                if (item.BiddingDetails != null)
                {
                    activeListing.QuantityBid = StringUtil.GetSafeInt(item.BiddingDetails.QuantityBid);
                    if (item.BiddingDetails.MaxBid != null)
                        activeListing.MaxBid = StringUtil.GetSafeDouble(item.BiddingDetails.MaxBid.Value);
                    else
                        activeListing.MaxBid = 0.0;
                }
                else
                {
                    activeListing.QuantityBid = 0;
                    activeListing.MaxBid = 0.0;
                }

                if (item.StartPrice!=null)
                {
                    activeListing.StartPrice = StringUtil.GetSafeDouble(item.StartPrice.Value);
                }
                else
                {
                    activeListing.StartPrice = 0.0;
                }
                

                if (item.BuyItNowPrice != null)
                {
                    activeListing.BuyItNowPrice = StringUtil.GetSafeDouble(item.BuyItNowPrice.Value);
                    activeListing.CurrencyID = StringUtil.GetSafeString(item.BuyItNowPrice.currencyID);
                }
                else
                {
                    activeListing.BuyItNowPrice = 0.0;
                    activeListing.CurrencyID = "";
                }

                if (item.ListingDetails != null)
                {
                    activeListing.StartTime = StringUtil.GetSafeDateTime(item.ListingDetails.StartTime);
                    activeListing.EndTime = StringUtil.GetSafeDateTime(item.ListingDetails.EndTime);
                    activeListing.ViewItemURL = StringUtil.GetSafeString(item.ListingDetails.ViewItemURL);
                    
                }

                activeListing.ListDuration = StringUtil.GetSafeInt(StringUtil.GetSafeListDurationDays(item.ListingDuration));
                activeListing.PrivateListing = StringUtil.GetSafeBool(item.PrivateListing);
                activeListing.Quantity = StringUtil.GetSafeInt(item.Quantity);
                activeListing.QuantityAvailable = StringUtil.GetSafeInt(item.QuantityAvailable);
                if (item.SellingStatus != null)
                {
                    if (item.SellingStatus.ListingStatus == ListingStatusCodeType.Active)
                        activeListing.SellingStatus = "Active";
                    else if (item.SellingStatus.ListingStatus == ListingStatusCodeType.Completed)
                        activeListing.SellingStatus = "Completed";
                    else if (item.SellingStatus.ListingStatus == ListingStatusCodeType.Ended)
                        activeListing.SellingStatus = "Ended";
                    else
                        activeListing.SellingStatus = "Unknown";

                    if (item.SellingStatus.BidCountSpecified)
                        activeListing.BidCount = item.SellingStatus.BidCount;
                    else
                        activeListing.BidCount = 0;

                    if (item.SellingStatus.BidderCountSpecified)
                        activeListing.BidderCount = (int)item.SellingStatus.BidderCount;
                    else
                        activeListing.BidderCount = 0;

                    if (item.SellingStatus.CurrentPrice != null)
                        activeListing.CurrentPrice = item.SellingStatus.CurrentPrice.Value;
                    else
                        activeListing.CurrentPrice = 0.0;
                }
                else
                {
                    activeListing.SellingStatus = "Unknown";
                }
                activeListing.SKU = StringUtil.GetSafeString(item.SKU);
                activeListing.TimeLeft = StringUtil.GetSafeString(item.TimeLeft);
                activeListing.WatchCount = StringUtil.GetSafeInt(item.WatchCount);

                activeListings.Add(activeListing);

            }

            Logger.WriteSystemLog(string.Format("Successfully called GetMyActiveListing, returns {0} entries", activeListings.Count));
            return activeListings;
        } // GetMyActiveListing


        public static string GetMyEbaySelling(AccountType account)
        {
            if (account.SellerApiContext == null)
                return null;

            GetMyeBaySellingCall getMyeBaySellingApiCall = new GetMyeBaySellingCall(account.SellerApiContext);
            DetailLevelCodeType[] detailLevels = new DetailLevelCodeType[] { DetailLevelCodeType.ReturnSummary };
            getMyeBaySellingApiCall.DetailLevelList = new DetailLevelCodeTypeCollection(detailLevels);

            getMyeBaySellingApiCall.SellingSummary = new eBay.Service.Core.Soap.ItemListCustomizationType();
            getMyeBaySellingApiCall.SellingSummary.Include = true;

            getMyeBaySellingApiCall.ActiveList = new eBay.Service.Core.Soap.ItemListCustomizationType();
            getMyeBaySellingApiCall.ActiveList.Include = true;
            getMyeBaySellingApiCall.ActiveList.IncludeNotes = true;
            getMyeBaySellingApiCall.ActiveList.ListingType = ListingTypeCodeType.FixedPriceItem;
            getMyeBaySellingApiCall.ActiveList.Pagination = new PaginationType();
            getMyeBaySellingApiCall.ActiveList.Pagination.EntriesPerPage = 10;
            getMyeBaySellingApiCall.ActiveList.Pagination.PageNumber = 1;

            getMyeBaySellingApiCall.GetMyeBaySelling();

            AmountType amountLimitRemaining = getMyeBaySellingApiCall.Summary.AmountLimitRemaining;
            long quantityLimitRemaining = getMyeBaySellingApiCall.Summary.QuantityLimitRemaining;

            PaginatedItemArrayType paginatedItemArray = getMyeBaySellingApiCall.ActiveListReturn;

            return null;
        }

        #endregion
    }
}
