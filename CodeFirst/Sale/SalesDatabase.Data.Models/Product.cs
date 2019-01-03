using System;
using System.Collections.Generic;

namespace SalesDatabase.Data.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public double Quantity { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}
