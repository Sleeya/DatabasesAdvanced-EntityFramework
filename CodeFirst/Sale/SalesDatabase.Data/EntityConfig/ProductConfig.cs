using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesDatabase.Data.EntityConfig
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.ProductId);

            builder.Property(x => x.Name).HasMaxLength(50).IsUnicode();

            builder.Property(x => x.Description).HasMaxLength(250).IsUnicode().HasDefaultValue("No description");

            builder.HasMany(x => x.Sales).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);
        }
    }
}
