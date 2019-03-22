using Microsoft.EntityFrameworkCore;
using ProductsShop.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsShop.Data.EntityConfig
{
    internal class CategoryProductConfiguration : IEntityTypeConfiguration<CategoryProduct>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CategoryProduct> builder)
        {
            builder.HasKey(x => new {x.CategoryId, x.ProductId});
        }
    }
}
