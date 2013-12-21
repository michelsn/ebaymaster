using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EbayMaster
{
    public class MessageTemplateCategoryType
    {
        public int CategoryId { get; set; }
        public int ParentCategoryId { get; set; }
        public String CategoryName { get; set; }
        public String CategoryDescription { get; set; }

        public MessageTemplateCategoryType()
        {

        }
    }
}
