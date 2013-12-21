using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EbayMaster
{
    public class ItemSupplierType
    {
        public int ItemId { get; set; }
        public int SupplierId { get; set; }
        public String SourcingURL { get; set; }
        public double Price { get; set; }
        public double ShippingFee { get; set; }
        public String Comment { get; set; }
        public DateTime CreatedDate { get; set; }

        public ItemSupplierType()
        {

        }
    }
}
