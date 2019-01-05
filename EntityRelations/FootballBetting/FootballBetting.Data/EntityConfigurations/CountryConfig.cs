using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FootballBetting.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballBetting.Data.EntityConfigurations
{
    public class CountryConfig : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasMany(t => t.Towns).WithOne(c => c.Country).HasForeignKey(t => t.TownId);
        }
    }
}
