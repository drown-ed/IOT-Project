using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AladinBookSearch.Model
{
    public class BookItem
    {
        public string ItemId { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Cover { get; set; }
        public string Author { get; set; }
        public string PubDate { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public string PriceSales { get; set; }
    }
}
