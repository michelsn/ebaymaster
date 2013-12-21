using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EbayMaster
{
    public class SourcingNoteType
    {
        public int SourcingId { get; set; }
        public int SupplierId { get; set; }
        public String ItemSkuList { get; set; }
        public String ItemNumList { get; set; }
        public String ItemPriceList { get; set; }
        public double ExtraFee { get; set; }
        public double ShippingFee { get; set; }
        public double TotalFee { get; set; }
        public String Comment { get; set; }
        public DateTime SourcingDate { get; set; }

        public SourcingNoteType()
        {

        }
    }
}
