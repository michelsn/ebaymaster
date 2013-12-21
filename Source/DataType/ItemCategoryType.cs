using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EbayMaster
{
    public class ItemCategoryType
    {
        public int CategoryId { get; set; }
        public int ParentCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategorySkuPrefix { get; set; }

        public ItemCategoryType()
        {

        }


    }
}
