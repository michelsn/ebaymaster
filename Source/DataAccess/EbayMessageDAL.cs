using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Sql;

namespace EbayMaster
{
    public class EbayMessageDAL
    {
        public static DataTable GetAllMessagesTable()
        {
            String sql_getAllMessages = "select * from [Message]";
            DataTable dt = DbAccess.ExecuteSqlReturnTable(sql_getAllMessages);
            return dt;
        }

        public static DataTable GetPagedMessageTable(int pageNum, int pageSize)
        {
            String pagedMsgFormatSql = "select * from [Message] where MessageId "
              + "in (select top {0} sub.MessageId from ("
              + " select top {1} MessageId,ReceiveDate from [Message] order by ReceiveDate desc"
               + ") [sub] order by sub.ReceiveDate) order by ReceiveDate desc";
            String pagedMsgSql = String.Format(pagedMsgFormatSql, pageSize, pageNum * pageSize);
            DataTable dt = DataFactory.ExecuteSqlReturnTable(pagedMsgSql);
            return dt;
        }

        public static int GetMessageCount()
        {
            int count = 0;

            String sql = "select count(*) from [Message]";
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql);

            try
            {
                object obj = dt.Rows[0][0];
                if (obj != null)
                    Int32.TryParse(obj.ToString(), out count);
            }
            catch (System.Exception)
            {

            }
            return count;
        }

        // Use with care.
        public static DataTable GetAllTransactionMessages(String buyerId, String sellerId, String itemId)
        {
            String sql = String.Format("select * from [Message] where (Sender='{0}' and RecipientUserId='{1}' and ItemId='{2}') or (Sender='{1}' and RecipientUserId='{0}' and ItemId='{2}') order by ReceiveDate desc",
                buyerId, sellerId, itemId);
            DataTable dt = DbAccess.ExecuteSqlReturnTable(sql);
            return dt;
        }

        // Should be used in most cases.
        public static DataTable GetAllTransactionMessagesCompact(String buyerId, String sellerId, String itemId)
        {
            String sql = String.Format("select MessageId, EbayMessageId, IsRead, IsReplied, ReceiveDate, Sender, RecipientUserId, Subject, ItemID from [Message] where (Sender='{0}' and RecipientUserId='{1}' and ItemId='{2}') or (Sender='{1}' and RecipientUserId='{0}' and ItemId='{2}') order by ReceiveDate desc",
                buyerId, sellerId, itemId);
            DataTable dt = DbAccess.ExecuteSqlReturnTable(sql);
            return dt;
        }

        public static TransactionMessageStatus GetTransactionMessageStatus(String buyerId, String sellerId, String itemId)
        {
            TransactionMessageStatus messageType = TransactionMessageStatus.Invalid;

            DataTable dtMsg = GetAllTransactionMessagesCompact(buyerId, sellerId, itemId);
            if (dtMsg.Rows.Count == 0)
            {
                messageType = TransactionMessageStatus.NoMessage;
                return messageType;
            }

            bool buyerReplied = false;
            foreach (DataRow dr in dtMsg.Rows)
            {
                bool buyerSentMsg = false;

                String sender = dr["Sender"].ToString();
                if (sender == buyerId)
                {
                    buyerSentMsg = true;
                    buyerReplied = true;
                }

                bool isRead = StringUtil.GetSafeBool(dr["IsRead"]);
                bool isReplied = StringUtil.GetSafeBool(dr["IsReplied"]);
                if (buyerSentMsg)
                {
                    if (!isReplied)
                        return TransactionMessageStatus.BuyerRepliedSellerNotReplied;
                }
            }

            if (buyerReplied)
                messageType = TransactionMessageStatus.BuyerRepliedSellerReplied;
            else
                messageType = TransactionMessageStatus.SellerInquired;

            return messageType;
        }

        public static DataTable GetAllUserMessages(String senderId)
        {
            String sql_getAllMessages = "select * from [Message] where Sender='" + senderId + "' order by ReceiveDate desc";
            DataTable dt = DbAccess.ExecuteSqlReturnTable(sql_getAllMessages);
            return dt;
        }

        public static DataTable GetUserTransactionMessageTable(String senderId, String itemId)
        {
            String sql_getAllMessages = String.Format("select * from [Message] where Sender='{0}' and ItemId='{1}' order by ReceiveDate desc",
                senderId, itemId);
            DataTable dt = DbAccess.ExecuteSqlReturnTable(sql_getAllMessages);
            return dt;
        }

        // obsoleted
        public static EbayMessageType GetUserTransactionMessage(String senderId, String itemId)
        {
            String sql_getAllMessages = "select * from [Message] where Sender='" + senderId + "' and ItemId='" + itemId + "' order by ReceiveDate desc";
            DataTable dt = DataFactory.ExecuteSqlReturnTable(sql_getAllMessages);
            if (dt.Rows.Count == 0)
                return null;

            DataRow dr = dt.Rows[0];
            EbayMessageType msg = new EbayMessageType();
            msg.EbayMessageId = dr["EbayMessageId"].ToString();
            msg.IsRead = (bool)dr["IsRead"];
            msg.Sender = dr["Sender"].ToString();
            msg.Subject = dr["Sender"].ToString();
            msg.ItemID = dr["Sender"].ToString();
            // ZHI_TODO:
            return msg;
        }

        public static bool MarkMessageAsReplied(String ebayMessageId)
        {
            String sql = string.Format("update [Message] set IsReplied=true where EbayMessageId='{0}'", ebayMessageId);
            DataFactory.ExecuteSql(sql);
            return true;
        }

        public static EbayMessageType GetOneMessage(String ebayMessageId)
        {
            String sql_getAllMessages = String.Format("select * from [Message] where EbayMessageId='{0}'", ebayMessageId);
            DataTable dt = DbAccess.ExecuteSqlReturnTable(sql_getAllMessages);
            if (dt.Rows.Count == 0)
                return null;

            DataRow dr = dt.Rows[0];
            EbayMessageType msg = new EbayMessageType();
            msg.MessageId = StringUtil.GetSafeInt(dr["MessageId"]);
            msg.SellerName = StringUtil.GetSafeString(dr["SellerName"]);
            msg.EbayMessageId = StringUtil.GetSafeString(dr["EbayMessageId"]);
            msg.MessageType = StringUtil.GetSafeString(dr["MessageType"]);
            msg.QuestionType = StringUtil.GetSafeString(dr["QuestionType"]);
            msg.IsRead = StringUtil.GetSafeBool(dr["IsRead"]);
            msg.IsReplied = StringUtil.GetSafeBool(dr["IsReplied"]);
            msg.IsResponseEnabled = StringUtil.GetSafeBool(dr["IsResponseEnabled"]);
            msg.ResponseURL = StringUtil.GetSafeString(dr["ResponseURL"]);
            msg.UserResponseDate = StringUtil.GetSafeDateTime(dr["UserResponseDate"]);
            msg.ReceiveDate = StringUtil.GetSafeDateTime(dr["ReceiveDate"]);
            msg.Sender = StringUtil.GetSafeString(dr["Sender"]);
            msg.RecipientUserId = StringUtil.GetSafeString(dr["RecipientUserId"]);
            msg.Subject = StringUtil.GetSafeString(dr["Subject"]);
            msg.Content = StringUtil.GetSafeString(dr["MessageContent"]);
            msg.Text = StringUtil.GetSafeString(dr["MessageText"]);
            msg.ExternalMessageId = StringUtil.GetSafeString(dr["ExternalMessageId"]);
            msg.FolderId = StringUtil.GetSafeInt(dr["FolderId"]);
            msg.ItemID = StringUtil.GetSafeString(dr["ItemID"]);
            msg.ItemTitle = StringUtil.GetSafeString(dr["ItemTitle"]);
            // ZHI_TODO:

            return msg;
        }  // GetOneMessage

        public static List<EbayMessageType> GetAllMessages()
        {
            return null;
        }

        public static bool InsertOneMessage(EbayMessageType msg)
        {
            if (!msg.isValid())
                return false;

            OleDbCommand cmd = new OleDbCommand(null, DbAccess.DbConn);
            cmd.CommandText = @"Insert into [Message] (EbayMessageId, SellerName, MessageType, QuestionType, IsRead, IsReplied, "
                + "IsResponseEnabled, ResponseURL, UserResponseDate, ReceiveDate, RecipientUserId,"
                + "Sender, Subject, IsHighPriority, MessageContent, MessageText, "
                + "ExternalMessageId, FolderId, ItemID, ItemTitle, ItemEndTime, ListingStatus) values ("
                + " @EbayMessageId, @SellerName, @MessageType, @QuestionType, @IsRead, @IsReplied, "
                + " @IsResponseEnabled, @ResponseURL, @UserResponseDate, @ReceiveDate, @RecipientUserId,"
                + " @Sender, @Subject, @IsHighPriority, @MessageContent, @MessageText, "
                + " @ExternalMessageId, @FolderId, @ItemID, @ItemTitle, @ItemEndTime, @ListingStatus)";

            cmd.Parameters.Add("@EbayMessageId", OleDbType.VarChar).Value = StringUtil.GetSafeString(msg.EbayMessageId);
            cmd.Parameters.Add("@SellerName", OleDbType.VarChar).Value = StringUtil.GetSafeString(msg.SellerName);
            cmd.Parameters.Add("@MessageType", OleDbType.VarChar).Value = StringUtil.GetSafeString(msg.MessageType);
            cmd.Parameters.Add("@QuestionType", OleDbType.VarChar).Value = StringUtil.GetSafeString(msg.QuestionType);
            cmd.Parameters.Add("@IsRead", OleDbType.Boolean).Value = msg.IsRead;
            cmd.Parameters.Add("@IsReplied", OleDbType.Boolean).Value = msg.IsReplied;

            cmd.Parameters.Add("@IsResponseEnabled", OleDbType.Boolean).Value = msg.IsResponseEnabled;
            cmd.Parameters.Add("@ResponseURL", OleDbType.VarChar).Value = StringUtil.GetSafeString(msg.ResponseURL);
            cmd.Parameters.Add("@UserResponseDate", OleDbType.Date).Value = StringUtil.GetSafeDateTime(msg.UserResponseDate);
            cmd.Parameters.Add("@ReceiveDate", OleDbType.Date).Value = StringUtil.GetSafeDateTime(msg.ReceiveDate);
            cmd.Parameters.Add("@RecipientUserId", OleDbType.VarChar).Value = StringUtil.GetSafeString(msg.RecipientUserId);

            cmd.Parameters.Add("@Sender", OleDbType.VarChar).Value = StringUtil.GetSafeString(msg.Sender);
            cmd.Parameters.Add("@Subject", OleDbType.VarChar).Value = StringUtil.GetSafeString(msg.Subject);
            cmd.Parameters.Add("@IsHighPriority", OleDbType.Boolean).Value = msg.IsHighPriority;
            cmd.Parameters.Add("@MessageContent", OleDbType.LongVarChar).Value = StringUtil.GetSafeString(msg.Content);
            cmd.Parameters.Add("@MessageText", OleDbType.LongVarChar).Value = StringUtil.GetSafeString(msg.Text);

            cmd.Parameters.Add("@ExternalMessageId", OleDbType.VarChar).Value = StringUtil.GetSafeString(msg.ExternalMessageId);
            cmd.Parameters.Add("@FolderId", OleDbType.BigInt).Value = msg.FolderId;
            cmd.Parameters.Add("@ItemID", OleDbType.VarChar).Value = StringUtil.GetSafeString(msg.ItemID);
            cmd.Parameters.Add("@ItemTitle", OleDbType.VarChar).Value = StringUtil.GetSafeString(msg.ItemTitle);
            cmd.Parameters.Add("@ItemEndTime", OleDbType.Date).Value = StringUtil.GetSafeDateTime(msg.ItemEndTime);
            cmd.Parameters.Add("@ListingStatus", OleDbType.VarChar).Value = StringUtil.GetSafeString(msg.ListingStatus);

            bool result = false;
            try
            {
                if (DbAccess.DbConn.State == ConnectionState.Closed)
                    DbAccess.DbConn.Open();
                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (OleDbException ex)
            {
                // Write to log here.
                Logger.WriteSystemLog(string.Format("Error : {0}", ex.Message));
                result = false;
            }
            finally
            {
                if (DbAccess.DbConn.State == ConnectionState.Open)
                    DbAccess.DbConn.Close();
            }

            return result;
        } // InsertOneMessage
    }
}
