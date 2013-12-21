using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EbayMaster
{
    public class EbayMessageType
    {
        public int MessageId { get; set; }
        public String SellerName { get; set; }
        public String EbayMessageId { get; set; }
        public String MessageType { get; set; }
        public String QuestionType { get; set; }
        public bool IsRead { get; set; }

        public bool IsReplied { get; set; }
        public bool IsResponseEnabled { get; set; }
        public String ResponseURL { get; set; }
        public DateTime UserResponseDate { get; set; }
        public DateTime ReceiveDate { get; set; }

        public String RecipientUserId { get; set; }
        public String Sender { get; set; }
        public String Subject { get; set; }

        public bool IsHighPriority { get; set; }
        public String Content { get; set; }
        public String Text { get; set; }
        public String ExternalMessageId { get; set; }
        public long FolderId { get; set; }
        public String ItemID { get; set; }
        public String ItemTitle { get; set; }
        public DateTime ItemEndTime { get; set; }
        public String ListingStatus { get; set; }

        public EbayMessageType()
        {
        }

        public bool isValid()
        {
            return true;
        }
    }
}
