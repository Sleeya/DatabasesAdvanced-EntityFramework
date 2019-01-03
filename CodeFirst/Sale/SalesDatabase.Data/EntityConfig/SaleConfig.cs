using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesDatabase.Data.EntityConfig
{
    public class SaleConfig : IEntityTypeConfiguration<Sale>
    {
        
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(x => x.SaleId);

            builder.HasOne(x => x.Product).WithMany(x => x.Sales).HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.Customer).WithMany(x => x.Sales).HasForeignKey(x => x.CustomerId);
            builder.HasOne(x => x.Store).WithMany(x => x.Sales).HasForeignKey(x => x.StoreId);
        }
    }
}
