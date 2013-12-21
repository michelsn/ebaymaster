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
    public class EbayMessageBiz
    {
        // 
        // GetMyMessages 
        //  Returns information about the threaded messages sent to a user's My Messages mailbox. 
        //      http://developer.ebay.com/DevZone/XML/docs/Reference/eBay/GetMyMessages.html
        // For maximum efficiency, make sure your application can first call GetMyMessages 
        // with DetailLevel set to ReturnHeaders. 
        //
        //  With a detail level of ReturnHeaders or ReturnMessages,
        //  GetMyMessages only accepts 10 unique message ID values, per request.
        //

        // GetMemberMessages
        //      http://developer.ebay.com/DevZone/XML/docs/Reference/eBay/GetMemberMessages.html
        // You can get all the messages sent within a specific time range 
        // by providing StartCreationTime and EndCreationTime in your request. 
        // Or you can specify an item's ItemID to get messages about an item.

        // After calling GetMemberMessages, inspect the children of the MemberMessages container. 
        // For instance, if you want to know whether a message was previously answered, inspect the MessageStatus field.

        public static StringCollection GetAllMessageIds(AccountType account, DateTime startTime, DateTime endTime)
        {
            GetMyMessagesCall getMyMessageApiCall = new GetMyMessagesCall(account.SellerApiContext);
            getMyMessageApiCall.StartTime = startTime;
            getMyMessageApiCall.EndTime = endTime;

            DetailLevelCodeType[] detailLevels = new DetailLevelCodeType[] { DetailLevelCodeType.ReturnHeaders };
            getMyMessageApiCall.DetailLevelList = new DetailLevelCodeTypeCollection(detailLevels);

            StringCollection msgIds = new StringCollection();
            for (int folderId = 0; folderId <= 1; folderId++)
            {
                getMyMessageApiCall.FolderID = folderId;
                getMyMessageApiCall.GetMyMessages();
                MyMessagesMessageTypeCollection messages = getMyMessageApiCall.MessageList;

                foreach (MyMessagesMessageType msg in messages)
                {
                    msgIds.Add(msg.MessageID);
                }
            }

            return msgIds;
        }  // GetAllMessageIds

        public static List<EbayMessageType> GetAllMessageByIds(AccountType account, StringCollection msgIds)
        {
            List<EbayMessageType> messageList = new List<EbayMessageType>();

            if (msgIds.Count > 10)
            {
                Logger.WriteSystemLog("[GetAllMessageByIds]: can only get at most 10 messages once.");
                return messageList;
            }

            GetMyMessagesCall getMyMessageApiCall = new GetMyMessagesCall(account.SellerApiContext);
            getMyMessageApiCall.MessageIDList = msgIds;
            DetailLevelCodeType[] detailLevels = new DetailLevelCodeType[] { DetailLevelCodeType.ReturnMessages };
            getMyMessageApiCall.DetailLevelList = new DetailLevelCodeTypeCollection(detailLevels);
            getMyMessageApiCall.GetMyMessages();
            MyMessagesMessageTypeCollection messages = getMyMessageApiCall.MessageList;

            foreach (MyMessagesMessageType msg in messages)
            {
                // See if this message has already existed.
                String msgId = msg.MessageID;

                EbayMessageType tmpEbayMsg = EbayMessageDAL.GetOneMessage(msgId);
                if (tmpEbayMsg != null && tmpEbayMsg.EbayMessageId != "")
                {
                    Logger.WriteSystemLog(string.Format("[GetAllMessageByIds]: message with id={0} has already existed, skip and continue.", msgId));
                    continue;
                }

                EbayMessageType ebayMsg = new EbayMessageType();
                ebayMsg.EbayMessageId = msg.MessageID;
                ebayMsg.SellerName = account.ebayAccount;
                ebayMsg.MessageType = msg.MessageType.ToString();
                ebayMsg.QuestionType = msg.QuestionType.ToString();
                ebayMsg.IsRead = msg.Read;
                ebayMsg.IsReplied = msg.Replied;
                ebayMsg.IsResponseEnabled = msg.ResponseDetails != null ? msg.ResponseDetails.ResponseEnabled : false;
                ebayMsg.ResponseURL = msg.ResponseDetails != null ? msg.ResponseDetails.ResponseURL : "";
                ebayMsg.UserResponseDate = msg.ResponseDetails != null ? msg.ResponseDetails.UserResponseDate : DateTime.MinValue;
                ebayMsg.ReceiveDate = msg.ReceiveDate;
                //ebayMsg.RecipientUserId = msg.RecipientUserID;
                ebayMsg.RecipientUserId = msg.SendToName;
                ebayMsg.Sender = msg.Sender;
                ebayMsg.Subject = msg.Subject;
                ebayMsg.IsHighPriority = msg.HighPriority;
                // The message body. Plain text.
                ebayMsg.Content = msg.Content;
                // This field contains message content, and can contain a threaded message.
                // Max length: 2 megabytes in size. 
                ebayMsg.Text = msg.Text;
                ebayMsg.ExternalMessageId = msg.ExternalMessageID;
                ebayMsg.FolderId = msg.Folder != null ? msg.Folder.FolderID : -1;
                ebayMsg.ItemID = msg.ItemID;
                ebayMsg.ItemTitle = msg.ItemTitle;
                ebayMsg.ItemEndTime = msg.ItemEndTime;
                ebayMsg.ListingStatus = msg.ListingStatus.ToString();

                messageList.Add(ebayMsg);
            }

            return messageList;
        }  // GetAllMessageByIds

        // Get all messages between buyers and sellers.
        public static bool GetAllMessages(AccountType account, DateTime startTime, DateTime endTime)
        {
            GetMyMessagesCall getMyMessageApiCall = new GetMyMessagesCall(account.SellerApiContext);
            getMyMessageApiCall.StartTime = startTime;
            getMyMessageApiCall.EndTime = endTime;

            DetailLevelCodeType[] detailLevels = new DetailLevelCodeType[] { DetailLevelCodeType.ReturnHeaders };
            getMyMessageApiCall.DetailLevelList = new DetailLevelCodeTypeCollection(detailLevels);
            getMyMessageApiCall.GetMyMessages();
            MyMessagesMessageTypeCollection messages = getMyMessageApiCall.MessageList;

            foreach (MyMessagesMessageType msg in messages)
            {
                string msgId = msg.MessageID;
                GetMyMessagesCall getMyMessageApiCall2 = new GetMyMessagesCall(account.SellerApiContext);

                StringCollection msgIds = new StringCollection();
                msgIds.Add(msgId);
                getMyMessageApiCall2.MessageIDList = msgIds;
                detailLevels = new DetailLevelCodeType[] { DetailLevelCodeType.ReturnMessages };
                getMyMessageApiCall2.DetailLevelList = new DetailLevelCodeTypeCollection(detailLevels);
                getMyMessageApiCall2.GetMyMessages();

                MyMessagesMessageType msg2 = getMyMessageApiCall2.MessageList[0];

                EbayMessageType ebayMsg = new EbayMessageType();
                ebayMsg.EbayMessageId = msgId;
                ebayMsg.SellerName = account.ebayAccount;
                ebayMsg.MessageType = msg2.MessageType.ToString();
                ebayMsg.QuestionType = msg2.QuestionType.ToString();
                ebayMsg.IsRead = msg2.Read;
                ebayMsg.IsReplied = msg2.Replied;
                ebayMsg.IsResponseEnabled = msg2.ResponseDetails != null ? msg2.ResponseDetails.ResponseEnabled : false;
                ebayMsg.ResponseURL = msg2.ResponseDetails != null ? msg2.ResponseDetails.ResponseURL : "";
                ebayMsg.UserResponseDate = msg2.ResponseDetails != null ? msg2.ResponseDetails.UserResponseDate : DateTime.MinValue;
                ebayMsg.ReceiveDate = msg2.ReceiveDate;
                ebayMsg.RecipientUserId = msg2.RecipientUserID;
                ebayMsg.Sender = msg2.Sender;
                ebayMsg.Subject = msg2.Subject;
                ebayMsg.IsHighPriority = msg2.HighPriority;
                ebayMsg.Content = msg2.Content;
                ebayMsg.Text = msg2.Text;
                ebayMsg.ExternalMessageId = msg2.ExternalMessageID;
                ebayMsg.FolderId = msg2.Folder != null ? msg2.Folder.FolderID : -1;
                ebayMsg.ItemID = msg2.ItemID;
                ebayMsg.ItemTitle = msg2.ItemTitle;
                ebayMsg.ItemEndTime = msg2.ItemEndTime;
                ebayMsg.ListingStatus = msg2.ListingStatus.ToString();

                EbayMessageDAL.InsertOneMessage(ebayMsg);
            }

            return true;
        }  // GetAllMessages

        // Send a message to buyer.
        //  Note we can send a message to multiple users in single API call.
        //  See reference: AddMemberMessageAAQToPartner 
        //      http://developer.ebay.com/DevZone/XML/docs/Reference/eBay/AddMemberMessageAAQToPartner.html
        //
        public static bool SendMessageToBuyer(AccountType account, String buyerId, String itemId,
            String subject, String body, bool emailCopyToSender, QuestionTypeCodeType questionType)
        {
            if (account == null || account.SellerApiContext == null)
                return false;

            AddMemberMessageAAQToPartnerCall apiCall = new AddMemberMessageAAQToPartnerCall(account.SellerApiContext);
            apiCall.ItemID = itemId;
            apiCall.MemberMessage = new MemberMessageType();
            apiCall.MemberMessage.EmailCopyToSender = emailCopyToSender;
            apiCall.MemberMessage.QuestionType = questionType;
            StringCollection recipientIds = new StringCollection();
            recipientIds.Add(buyerId);
            apiCall.MemberMessage.RecipientID = recipientIds;
            apiCall.MemberMessage.Subject = "";
            apiCall.MemberMessage.Body = body;

            bool result = false;
            try
            {
                apiCall.AddMemberMessageAAQToPartner(apiCall.ItemID, apiCall.MemberMessage);
                result = true;
            }
            catch (System.Exception ex)
            {
                Logger.WriteSystemUserLog(string.Format("Error when sending message to buyer, msg={0}", ex.Message));
            }

            Logger.WriteSystemLog(String.Format("Successfully sent message to buyer = {0}", buyerId));
            return result;
        }   // SendMessageToBuyer

        //
        // AddMemberMessageRTQ 
        // http://developer.ebay.com/DevZone/XML/docs/Reference/eBay/AddMemberMessageRTQ.html
        public static bool ReplyBuyerMessage(AccountType account, String itemId, 
            String externalMessageId, String buyerId, String body)
        {
            if (account == null || account.SellerApiContext == null)
                return false;

            if (itemId == null)
                return false;

            if (body == null || body.Trim().Length == 0)
                return false;

            AddMemberMessageRTQCall apiCall = new AddMemberMessageRTQCall(account.SellerApiContext);
            apiCall.ItemID = itemId;
            apiCall.MemberMessage = new eBay.Service.Core.Soap.MemberMessageType();
            apiCall.MemberMessage.Body = body;
            apiCall.MemberMessage.DisplayToPublic = false;
            apiCall.MemberMessage.EmailCopyToSender = true;
            apiCall.MemberMessage.ParentMessageID = externalMessageId;

            StringCollection recipientIds = new StringCollection();
            recipientIds.Add(buyerId);
            apiCall.MemberMessage.RecipientID = recipientIds;
            apiCall.MemberMessage.Subject = "";

            bool result = false;
            try
            {
                apiCall.AddMemberMessageRTQ(apiCall.ItemID, apiCall.MemberMessage);
                result = true;
            }
            catch (System.Exception ex)
            {
                Logger.WriteSystemUserLog(string.Format("Error when sending message to buyer, msg={0}", ex.Message));
            }

            return result;
        }   // ReplyBuyerMessage
    }
}
