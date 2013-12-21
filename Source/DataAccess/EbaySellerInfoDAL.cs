using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EbayMaster.DataAccess
{
    public class EbaySellerInfoDAL
    {
        // Returns null if no record corresponds to itemID;
        public static EbaySellerInfoType GetOneSellerInfo(String sellerName)
        {
            if (sellerName == null || sellerName.Trim() == "")
                return null;

            String sql_getOneSellerInfo = String.Format("select * from [SellerInfo] where SellerName='{0}'", sellerName);

            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql_getOneSellerInfo);
            if (dt.Rows.Count == 0)
                return null;

            DataRow dr = dt.Rows[0];
            EbaySellerInfoType sellerInfo = new EbaySellerInfoType();

            sellerInfo.SellerInfoId = StringUtil.GetSafeInt(dr["SellerInfoId"]);
            sellerInfo.SellerName = sellerName;
            sellerInfo.AmountLimitRemaining = StringUtil.GetSafeDouble(dr["AmountLimitRemaining"]);
            sellerInfo.AmountLimitRemainingCurrencyID = StringUtil.GetSafeString(dr["AmountLimitRemainingCurrencyID"]);
            sellerInfo.AuctionBidCount = StringUtil.GetSafeInt(dr["AuctionBidCount"]);

            sellerInfo.AuctionSellingCount = StringUtil.GetSafeInt(dr["AuctionSellingCount"]);
            sellerInfo.QuantityLimitRemaining = StringUtil.GetSafeInt(dr["QuantityLimitRemaining"]);
            sellerInfo.SoldDurationInDays = StringUtil.GetSafeInt(dr["SoldDurationInDays"]);
            sellerInfo.TotalSoldCount = StringUtil.GetSafeInt(dr["TotalSoldCount"]);
            sellerInfo.TotalSoldValue = StringUtil.GetSafeDouble(dr["TotalSoldValue"]);

            sellerInfo.TotalSoldValueCurrencyID = StringUtil.GetSafeString(dr["TotalSoldValueCurrencyID"]);
            sellerInfo.AccountState = StringUtil.GetSafeString(dr["AccountState"]);
            sellerInfo.BankAccountInfo = StringUtil.GetSafeString(dr["BankAccountInfo"]);
            sellerInfo.CurrentBalance = StringUtil.GetSafeDouble(dr["CurrentBalance"]);
            sellerInfo.InvoiceBalance = StringUtil.GetSafeDouble(dr["InvoiceBalance"]);

            sellerInfo.InvoiceNewFee = StringUtil.GetSafeDouble(dr["InvoiceNewFee"]);
            sellerInfo.InvoicePayment = StringUtil.GetSafeDouble(dr["InvoicePayment"]);
            sellerInfo.LastAmountPaid = StringUtil.GetSafeDouble(dr["LastAmountPaid"]);

            return sellerInfo;
        }   // GetOneSellerInfo

        public static bool InsertOneSellerInfo(EbaySellerInfoType sellerInfo)
        {
            EbaySellerInfoType existedSellerInfo = GetOneSellerInfo(sellerInfo.SellerName);
            if (existedSellerInfo != null)
            {
                Logger.WriteSystemLog(string.Format("Seller info with seller name={0} alreay exists.", sellerInfo.SellerName));
                return false;
            }

            IDbCommand cmd = DataFactory.CreateCommand(null);
            cmd.CommandText = @"insert into [SellerInfo] (SellerName, AmountLimitRemaining, AmountLimitRemainingCurrencyID,"
                + "AuctionBidCount, AuctionSellingCount, QuantityLimitRemaining, SoldDurationInDays, TotalSoldCount,"
                + "TotalSoldValue, TotalSoldValueCurrencyID, AccountState, BankAccountInfo, CurrentBalance, InvoiceBalance,"
                + "InvoiceNewFee, InvoicePayment, LastAmountPaid) values ("
                + "@SellerName, @AmountLimitRemaining, @AmountLimitRemainingCurrencyID,"
                + "@AuctionBidCount, @AuctionSellingCount, @QuantityLimitRemaining, @SoldDurationInDays, @TotalSoldCount,"
                + "@TotalSoldValue, @TotalSoldValueCurrencyID, @AccountState, @BankAccountInfo, @CurrentBalance, @InvoiceBalance,"
                + "@InvoiceNewFee, @InvoicePayment, @LastAmountPaid)";

            DataFactory.AddCommandParam(cmd, "@SellerName", DbType.String, StringUtil.GetSafeString(sellerInfo.SellerName));
            DataFactory.AddCommandParam(cmd, "@AmountLimitRemaining", DbType.Double, StringUtil.GetSafeDouble(sellerInfo.AmountLimitRemaining));
            DataFactory.AddCommandParam(cmd, "@AmountLimitRemainingCurrencyID", DbType.String, StringUtil.GetSafeString(sellerInfo.AmountLimitRemainingCurrencyID));
            DataFactory.AddCommandParam(cmd, "@AuctionBidCount", DbType.Int32, StringUtil.GetSafeInt(sellerInfo.AuctionBidCount));
            DataFactory.AddCommandParam(cmd, "@AuctionSellingCount", DbType.Int32, StringUtil.GetSafeInt(sellerInfo.AuctionSellingCount));

            DataFactory.AddCommandParam(cmd, "@QuantityLimitRemaining", DbType.Int32, StringUtil.GetSafeInt(sellerInfo.QuantityLimitRemaining));
            DataFactory.AddCommandParam(cmd, "@SoldDurationInDays", DbType.Int32, StringUtil.GetSafeInt(sellerInfo.SoldDurationInDays));
            DataFactory.AddCommandParam(cmd, "@TotalSoldCount", DbType.Int32, StringUtil.GetSafeInt(sellerInfo.TotalSoldCount));
            DataFactory.AddCommandParam(cmd, "@TotalSoldValue", DbType.Double, StringUtil.GetSafeDouble(sellerInfo.TotalSoldValue));
            DataFactory.AddCommandParam(cmd, "@TotalSoldValueCurrencyID", DbType.String, StringUtil.GetSafeString(sellerInfo.TotalSoldValueCurrencyID));

            DataFactory.AddCommandParam(cmd, "@AccountState", DbType.String, StringUtil.GetSafeString(sellerInfo.AccountState));
            DataFactory.AddCommandParam(cmd, "@BankAccountInfo", DbType.String, StringUtil.GetSafeString(sellerInfo.BankAccountInfo));
            DataFactory.AddCommandParam(cmd, "@CurrentBalance", DbType.Double, StringUtil.GetSafeDouble(sellerInfo.CurrentBalance));
            DataFactory.AddCommandParam(cmd, "@InvoiceBalance", DbType.Double, StringUtil.GetSafeDouble(sellerInfo.InvoiceBalance));
            DataFactory.AddCommandParam(cmd, "@InvoiceNewFee", DbType.Double, StringUtil.GetSafeDouble(sellerInfo.InvoiceNewFee));

            DataFactory.AddCommandParam(cmd, "@InvoicePayment", DbType.Double, StringUtil.GetSafeDouble(sellerInfo.InvoicePayment));
            DataFactory.AddCommandParam(cmd, "@LastAmountPaid", DbType.Double, StringUtil.GetSafeDouble(sellerInfo.LastAmountPaid));

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
                Logger.WriteSystemLog(string.Format("Error inserting new seller info, seller name={0}, errorMsg={1}",
                    sellerInfo.SellerName, ex.Message));
                result = false;
            }
            finally
            {
                if (DataFactory.DbConnection.State == ConnectionState.Open)
                    DataFactory.DbConnection.Close();
            }

            return result;
        }   // InsertOneSellerInfo
    }
}
