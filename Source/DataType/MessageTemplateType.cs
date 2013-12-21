using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EbayMaster
{
    public class MessageTemplateType
    {
        public int TemplateId { get; set; }
        public int TemplateCategoryId { get; set; }
        public String TemplateName { get; set; }
        public String TemplateContent { get; set; }

        public MessageTemplateType()
        {

        }
    }
}
