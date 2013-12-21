using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace EbayMaster
{
    public class EbayListingDAL
    {
        // Get all listing count.
        //  Returns the listing count.
        public static int GetListingCount()
        {
            int listingsCnt = 0;
            String sql = "select count(*) from [ActiveListing]";
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql);

            try
            {
                object obj = dt.Rows[0][0];
                if (obj != null)
                    Int32.TryParse(obj.ToString(), out listingsCnt);
            }
            catch (System.Exception)
            {

            }
            return listingsCnt;
        }

        public static DataTable GetPagedActiveListings(int pageNum, int pageSize)
        {
            String pagedOrderFormatSql = "select * from [ActiveListing] where ListId "
                    + "in (select top {0} sub.ListId from ("
                    + " select top {1} ListId,StartTime from [ActiveListing] order by StartTime desc"
                    + ") [sub] order by sub.StartTime) order by StartTime desc";
            String pagedOrderSql = String.Format(pagedOrderFormatSql, pageSize, pageNum * pageSize);
            DataTable dt = DataFactory.ExecuteSqlReturnTable(pagedOrderSql);
            return dt;
        }

        public static DataTable GetAllActiveListings()  
        {
            String sql_getAllTransactions = "select * from [ActiveListing]  order by StartTime";
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql_getAllTransactions);
            return dt;
        }

        // Returns null if no record corresponds to itemID;
        public static EbayActiveListingType GetOneActiveListing(String itemID)
        {
            if (itemID == null || itemID.Trim().Length == 0)
                return null;

            String sql_getOneActiveListing = String.Format("select * from [ActiveListing] where ItemID='{0}'", itemID);

            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql_getOneActiveListing);
            if (dt.Rows.Count == 0)
                return null;

            DataRow dr = dt.Rows[0];
            EbayActiveListingType activeListing = new EbayActiveListingType();
            activeListing.ListId = StringUtil.GetSafeInt(dr["ListId"]);
            activeListing.SellerName = StringUtil.GetSafeString(dr["SellerName"]);
            activeListing.ItemID = itemID;
            activeListing.Title = StringUtil.GetSafeString(dr["Title"]);
            activeListing.ListingType = StringUtil.GetSafeString(dr["ListingType"]);
            activeListing.GalleryURL = StringUtil.GetSafeString(dr["GalleryURL"]);
            activeListing.QuantityBid = StringUtil.GetSafeInt(dr["QuantityBid"]);
            activeListing.MaxBid = StringUtil.GetSafeDouble(dr["MaxBid"]);
            activeListing.StartPrice = StringUtil.GetSafeDouble(dr["StartPrice"]);
            activeListing.BuyItNowPrice = StringUtil.GetSafeDouble(dr["BuyItNowPrice"]);
            activeListing.CurrencyID = StringUtil.GetSafeString(dr["CurrencyID"]);
            activeListing.StartTime = StringUtil.GetSafeDateTime(dr["StartTime"]);
            activeListing.EndTime = StringUtil.GetSafeDateTime(dr["EndTime"]);
            activeListing.ViewItemURL = StringUtil.GetSafeString(dr["ViewItemURL"]);
            activeListing.ListDuration = StringUtil.GetSafeInt(dr["ListDuration"]);
            activeListing.PrivateListing = StringUtil.GetSafeBool(dr["PrivateListing"]);
            activeListing.Quantity = StringUtil.GetSafeInt(dr["Quantity"]);
            activeListing.QuantityAvailable = StringUtil.GetSafeInt(dr["QuantityAvailable"]);
            activeListing.SellingStatus = StringUtil.GetSafeString(dr["SellingStatus"]);
            activeListing.SKU = StringUtil.GetSafeString(dr["SKU"]);
            activeListing.TimeLeft = StringUtil.GetSafeString(dr["TimeLeft"]);
            activeListing.WatchCount = StringUtil.GetSafeInt(dr["WatchCount"]);
            activeListing.BidCount = StringUtil.GetSafeInt(dr["BidCount"]);
            activeListing.BidderCount = StringUtil.GetSafeInt(dr["BidCount"]);
            activeListing.CurrentPrice = StringUtil.GetSafeDouble(dr["BidCount"]);
            activeListing.FVF = StringUtil.GetSafeDouble(dr["BidCount"]);
            activeListing.ImagePath = StringUtil.GetSafeString(dr["ImagePath"]);

            return activeListing;
        }

        public static bool DeleteAllListings()
        {
            String sql = string.Format("delete * from [ActiveListing]");
            DataFactory.ExecuteSql(sql);
            return true;
        }

        public static bool DeleteOneListing(String itemID)
        {
            bool result = false;

            String sql = string.Format("delete from [ActiveListing] where ItemID='{0}'", itemID);
            try
            {
                DataFactory.ExecuteSql(sql);
                result = true;
            }
            catch (System.Exception ex)
            {
                Logger.WriteSystemLog(String.Format("Delete listing with ItemID={0} failed, error msg={1}", itemID, ex.Message));
            }
           
            return result;
        } // DeleteOneListing

        public static bool UpdateActingListImagePath(String listingId, String imagePath)
        {
            bool result = false;

            IDbCommand cmd = DataFactory.CreateCommand(null);
            cmd.CommandText = @"Update [ActiveListing] set ImagePath=@ImagePath where ListId=@ListId";

            DataFactory.AddCommandParam(cmd, "@ImagePath", DbType.String, StringUtil.GetSafeString(imagePath));
            DataFactory.AddCommandParam(cmd, "@ListId", DbType.Int32, listingId);

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

        public static bool InsertOrUpdateOneActiveListing(EbayActiveListingType activeListing)
        {
            EbayActiveListingType existedListing = GetOneActiveListing(activeListing.ItemID);
            if (existedListing != null)
            {
                UpdateOneActiveListing(existedListing.ListId, activeListing);
            }
            else
            {
                InsertOneActiveListing(activeListing);
            }

            return true;
        }

        public static bool UpdateOneActiveListing(int listId, EbayActiveListingType activeListing)
        {
            bool result = false;

            IDbCommand cmd = DataFactory.CreateCommand(null);
            cmd.CommandText = @"Update [ActiveListing] set ItemID=@ItemID, Title=@Title, ListingType=@ListingType,"
                + "GalleryURL=@GalleryURL, QuantityBid=@QuantityBid, MaxBid=@MaxBid, StartPrice=@StartPrice,"
                + "BuyItNowPrice=@BuyItNowPrice, CurrencyID=@CurrencyID, StartTime=@StartTime, EndTime=@EndTime,"
                + "ViewItemURL=@ViewItemURL, ListDuration=@ListDuration, PrivateListing=@PrivateListing,"
                + "Quantity=@Quantity, QuantityAvailable=@QuantityAvailable, SellingStatus=@SellingStatus,"
                + "SKU=@SKU, TimeLeft=@TimeLeft, WatchCount=@WatchCount, BidCount=@BidCount, BidderCount=@BidderCount,"
                + "CurrentPrice=@CurrentPrice, FVF=@FVF, ImagePath=@ImagePath where ListId=@ListId";

            DataFactory.AddCommandParam(cmd, "@ItemID", DbType.String, StringUtil.GetSafeString(activeListing.ItemID));
            DataFactory.AddCommandParam(cmd, "@Title", DbType.String, StringUtil.GetSafeString(activeListing.Title));
            DataFactory.AddCommandParam(cmd, "@ListingType", DbType.String, StringUtil.GetSafeString(activeListing.ListingType));
            DataFactory.AddCommandParam(cmd, "@GalleryURL", DbType.String, StringUtil.GetSafeString(activeListing.GalleryURL));
            DataFactory.AddCommandParam(cmd, "@QuantityBid", DbType.Int32, StringUtil.GetSafeInt(activeListing.QuantityBid));

            DataFactory.AddCommandParam(cmd, "@MaxBid", DbType.Double, StringUtil.GetSafeDouble(activeListing.MaxBid));
            DataFactory.AddCommandParam(cmd, "@StartPrice", DbType.Double, StringUtil.GetSafeDouble(activeListing.StartPrice));
            DataFactory.AddCommandParam(cmd, "@BuyItNowPrice", DbType.Double, StringUtil.GetSafeDouble(activeListing.BuyItNowPrice));
            DataFactory.AddCommandParam(cmd, "@CurrencyID", DbType.String, StringUtil.GetSafeString(activeListing.CurrencyID));
            DataFactory.AddCommandParam(cmd, "@StartTime", DbType.DateTime, StringUtil.GetSafeDateTime(activeListing.StartTime));

            DataFactory.AddCommandParam(cmd, "@EndTime", DbType.DateTime, StringUtil.GetSafeDateTime(activeListing.EndTime));
            DataFactory.AddCommandParam(cmd, "@ViewItemURL", DbType.String, StringUtil.GetSafeString(activeListing.ViewItemURL));
            DataFactory.AddCommandParam(cmd, "@ListDuration", DbType.Int32, StringUtil.GetSafeInt(activeListing.ListDuration));
            DataFactory.AddCommandParam(cmd, "@PrivateListing", DbType.Boolean, StringUtil.GetSafeBool(activeListing.PrivateListing));
            DataFactory.AddCommandParam(cmd, "@Quantity", DbType.Int32, StringUtil.GetSafeInt(activeListing.Quantity));

            DataFactory.AddCommandParam(cmd, "@QuantityAvailable", DbType.Int32, StringUtil.GetSafeInt(activeListing.QuantityAvailable));
            DataFactory.AddCommandParam(cmd, "@SellingStatus", DbType.String, StringUtil.GetSafeString(activeListing.SellingStatus));
            DataFactory.AddCommandParam(cmd, "@SKU", DbType.String, StringUtil.GetSafeString(activeListing.SKU));
            DataFactory.AddCommandParam(cmd, "@TimeLeft", DbType.String, StringUtil.GetSafeString(activeListing.TimeLeft));
            DataFactory.AddCommandParam(cmd, "@WatchCount", DbType.Int32, StringUtil.GetSafeInt(activeListing.WatchCount));

            DataFactory.AddCommandParam(cmd, "@BidCount", DbType.Int32, StringUtil.GetSafeInt(activeListing.BidCount));
            DataFactory.AddCommandParam(cmd, "@BidderCount", DbType.Int32, StringUtil.GetSafeInt(activeListing.BidderCount));
            DataFactory.AddCommandParam(cmd, "@CurrentPrice", DbType.Double, StringUtil.GetSafeDouble(activeListing.CurrentPrice));
            DataFactory.AddCommandParam(cmd, "@FVF", DbType.Double, StringUtil.GetSafeDouble(activeListing.FVF));
            DataFactory.AddCommandParam(cmd, "@ImagePath", DbType.String, StringUtil.GetSafeString(activeListing.ImagePath));

            DataFactory.AddCommandParam(cmd, "@ListId", DbType.Int32, listId);

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

        public static bool InsertOneActiveListing(EbayActiveListingType activeListing)
        {
            EbayActiveListingType existedActiveListing = GetOneActiveListing(activeListing.ItemID);
            if (existedActiveListing != null)
            {
                Logger.WriteSystemLog(string.Format("Active listing with itemId={0} alreay exists.", activeListing.ItemID));
                return false;
            }

            IDbCommand cmd = DataFactory.CreateCommand(null);
            cmd.CommandText = @"insert into [ActiveListing] (ItemID, SellerName, Title, ListingType, GalleryURL, QuantityBid,MaxBid,"
                + "StartPrice, BuyItNowPrice, CurrencyID, StartTime, EndTime, ViewItemURL, ListDuration, PrivateListing,"
                + "Quantity, QuantityAvailable, SellingStatus, SKU, TimeLeft, WatchCount, BidCount, BidderCount, CurrentPrice, FVF, ImagePath) values ("
                + "@ItemID, @SellerName, @Title, @ListingType, @GalleryURL, @QuantityBid, @MaxBid, @StartPrice, @BuyItNowPrice,"
                + "@CurrencyID, @StartTime, @EndTime, @ViewItemURL, @ListDuration, @PrivateListing,"
                + "@Quantity, @QuantityAvailable, @SellingStatus, @SKU, @TimeLeft, @WatchCount, @BidCount, @BidderCount, @CurrentPrice, @FVF, @ImagePath)";

            DataFactory.AddCommandParam(cmd, "@ItemID", DbType.String, StringUtil.GetSafeString(activeListing.ItemID));
            DataFactory.AddCommandParam(cmd, "@SellerName", DbType.String, StringUtil.GetSafeString(activeListing.SellerName));
            DataFactory.AddCommandParam(cmd, "@Title", DbType.String, StringUtil.GetSafeString(activeListing.Title));
            DataFactory.AddCommandParam(cmd, "@ListingType", DbType.String, StringUtil.GetSafeString(activeListing.ListingType));
            DataFactory.AddCommandParam(cmd, "@GalleryURL", DbType.String, StringUtil.GetSafeString(activeListing.GalleryURL));
            DataFactory.AddCommandParam(cmd, "@QuantityBid", DbType.Int32, StringUtil.GetSafeInt(activeListing.QuantityBid));

            DataFactory.AddCommandParam(cmd, "@MaxBid", DbType.Double, StringUtil.GetSafeDouble(activeListing.MaxBid));
            DataFactory.AddCommandParam(cmd, "@StartPrice", DbType.Double, StringUtil.GetSafeDouble(activeListing.StartPrice));
            DataFactory.AddCommandParam(cmd, "@BuyItNowPrice", DbType.Double, StringUtil.GetSafeDouble(activeListing.BuyItNowPrice));
            DataFactory.AddCommandParam(cmd, "@CurrencyID", DbType.String, StringUtil.GetSafeString(activeListing.CurrencyID));
            DataFactory.AddCommandParam(cmd, "@StartTime", DbType.DateTime, StringUtil.GetSafeDateTime(activeListing.StartTime));

            DataFactory.AddCommandParam(cmd, "@EndTime", DbType.DateTime, StringUtil.GetSafeDateTime(activeListing.EndTime));
            DataFactory.AddCommandParam(cmd, "@ViewItemURL", DbType.String, StringUtil.GetSafeString(activeListing.ViewItemURL));
            DataFactory.AddCommandParam(cmd, "@ListDuration", DbType.Int32, StringUtil.GetSafeInt(activeListing.ListDuration));
            DataFactory.AddCommandParam(cmd, "@PrivateListing", DbType.Boolean, StringUtil.GetSafeBool(activeListing.PrivateListing));
            DataFactory.AddCommandParam(cmd, "@Quantity", DbType.Int32, StringUtil.GetSafeInt(activeListing.Quantity));

            DataFactory.AddCommandParam(cmd, "@QuantityAvailable", DbType.Int32, StringUtil.GetSafeInt(activeListing.QuantityAvailable));
            DataFactory.AddCommandParam(cmd, "@SellingStatus", DbType.String, StringUtil.GetSafeString(activeListing.SellingStatus));
            DataFactory.AddCommandParam(cmd, "@SKU", DbType.String, StringUtil.GetSafeString(activeListing.SKU));
            DataFactory.AddCommandParam(cmd, "@TimeLeft", DbType.String, StringUtil.GetSafeString(activeListing.TimeLeft));
            DataFactory.AddCommandParam(cmd, "@WatchCount", DbType.Int32, StringUtil.GetSafeInt(activeListing.WatchCount));

            DataFactory.AddCommandParam(cmd, "@BidCount", DbType.Int32, StringUtil.GetSafeInt(activeListing.BidCount));
            DataFactory.AddCommandParam(cmd, "@BidderCount", DbType.Int32, StringUtil.GetSafeInt(activeListing.BidderCount));
            DataFactory.AddCommandParam(cmd, "@CurrentPrice", DbType.Double, StringUtil.GetSafeDouble(activeListing.CurrentPrice));
            DataFactory.AddCommandParam(cmd, "@FVF", DbType.Double, StringUtil.GetSafeDouble(activeListing.FVF));
            DataFactory.AddCommandParam(cmd, "@ImagePath", DbType.String, StringUtil.GetSafeString(activeListing.ImagePath));

            bool result = false;
            int newItemId = 0;

            try
            {
                if (DataFactory.DbConnection.State == ConnectionState.Closed)
                    DataFactory.DbConnection.Open();
                cmd.ExecuteNonQuery();

                IDbCommand cmdNewID = DataFactory.CreateCommand("SELECT @@IDENTITY");

                // Retrieve the Auto number and store it in the CategoryID column.
                object obj = cmdNewID.ExecuteScalar();
                Int32.TryParse(obj.ToString(), out newItemId);
                result = newItemId > 0;
            }
            catch (DataException ex)
            {
                // Write to log here.
                Logger.WriteSystemLog(string.Format("Error inserting new active listing, itemID={0}, errorMsg={1}",
                    activeListing.ItemID, ex.Message));
                result = false;
            }
            finally
            {
                if (DataFactory.DbConnection.State == ConnectionState.Open)
                    DataFactory.DbConnection.Close();
            }

            return result;
        }   // InsertOneActiveListing
    }
}