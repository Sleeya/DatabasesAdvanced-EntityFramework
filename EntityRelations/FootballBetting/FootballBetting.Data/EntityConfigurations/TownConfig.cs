using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FootballBetting.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballBetting.Data.EntityConfigurations
{
    public class TownConfig : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> builder)
        {
            builder.HasMany(t => t.Teams).WithOne(t => t.Town).HasForeignKey(t => t.TeamId);

            builder.HasOne(c => c.Country).WithMany(t => t.Towns).HasForeignKey(c => c.CountryId);
        }
    }
}
