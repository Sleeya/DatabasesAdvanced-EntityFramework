using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesDatabase.Data.EntityConfig
{
    public class StoreConfig : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.HasKey(x => x.StoreId);

            builder.Property(x => x.Name).HasMaxLength(80).IsUnicode();

            builder.HasMany(x => x.Sales).WithOne(x => x.Store).HasForeignKey(x => x.StoreId);
        }
    }
}
