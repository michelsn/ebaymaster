using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EbayMaster
{
    public class InventoryItemType
    {
        public int ItemId { get; set; }
        public int CategoryId { get; set; }
        public string ItemName { get; set; }
        public string ItemSKU { get; set; }
        public string ItemImagePath { get; set; }

        public int ItemStockShresholdNum { get; set; }
        public int ItemStockNum { get; set; }
        public string ItemSourcingInfo { get; set; }
        public string ItemSourcingURL { get; set; }
        public double ItemCost { get; set; }
        public double ItemWeight { get; set; }
        public string ItemCustomName { get; set; }
        public double ItemCustomWeight { get; set; }
        public double ItemCustomCost { get; set; }
        public DateTime ItemAddDateTime { get; set; }
        public string ItemNote { get; set; }

        public bool IsGroupItem { get; set; }
        public string SubItemSKUs { get; set; }

        public string ItemRefEnglishName { get; set; }
        public string ItemTermsList { get; set; }
        public double ItemRefPrice { get; set; }
        public string ItemMaterialEnglish { get; set; }
        public string ItemMaterial { get; set; }
        public string ItemSizeEnglish { get; set; }
        public string ItemSize { get; set; }

        public string ItemOtherInfo { get; set; }
        public string ItemRefURL { get; set; }
        public string ItemRefImageURL { get; set; }
        public double ItemShippingCost { get; set; }
        public double ItemLowestBuyItNowPrice { get; set; }
        public string ItemDescriptionEnglish { get; set; }

        public List<string> SubItemSKUList
        {
            get
            {
                if (IsGroupItem == false)
                    return null;

                List<string> skuList = new List<string>();
                if (SubItemSKUs == null || SubItemSKUs.Trim() == "")
                    return skuList;

                string[] splits = SubItemSKUs.Split(new char[] { '-' });
                foreach (string split in splits)
                    skuList.Add(split.Trim());

                return skuList;
            }
        }

        public InventoryItemType()
        {
        }
    }   // class InventoryItemType
}
