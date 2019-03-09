using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesTransaction.Data.Entity;

namespace TesTransaction.Models.Transactions
{
    public class TrSearchViewModel
    {
        public string Product { get; set; }

        public IList<PRODUCT> Products { get; set; }

        public string Price { get; set; }

        public string Image { get; set; }

        public string Method { get; set; }

        public string Argument { get; set; }

        public IList<BRAND> Brands { get; set; }

        public IList<HERO> Heros { get; set; }

        public IList<AGE> Ages { get; set; }

        public IList<CATEGORY> Cats { get; set; }
    }
}