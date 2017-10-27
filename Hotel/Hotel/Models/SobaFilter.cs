using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class SobaFilter
    {

        public string SmestajName { get; set; }  
        public int? BrojKreveta { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }

    }
}