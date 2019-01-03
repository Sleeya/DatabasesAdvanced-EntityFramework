using System;
using System.Collections.Generic;
using System.Text;

namespace SalesDatabase.Data.Models
{
    public class Store
    {
        public int StoreId { get; set; }

        public string Name { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}
