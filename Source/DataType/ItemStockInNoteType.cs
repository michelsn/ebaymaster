using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EbayMaster
{
    public class ItemStockInNoteType
    {
        public int NoteId { get; set; }
        public String ItemSKU { get; set; }
        public String ItemTitle { get; set; }
        public String SourcingNoteId { get; set; }
        public int StockInNum { get; set; }
        public DateTime StockInDate { get; set; }
        public String Comment { get; set; }

        public ItemStockInNoteType()
        {
            
        }
    }
}
