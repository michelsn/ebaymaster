using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EbayMaster
{
    public class DeliveryNoteType
    {
        public int DeliveryNoteId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public String DeliveryOrderIds { get; set; }
        public String DeliveryUser { get; set; }
        public double DeliveryFee { get; set; }
        public double DeliveryExtraFee { get; set; }
        public string DeliveryComment { get; set; }

        public DeliveryNoteType()
        {

        }
    }
}
