using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FootballBetting.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballBetting.Data.EntityConfigurations
{
    public class ColorConfig : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.HasKey(x => x.ColorId);

            builder.HasMany(x => x.PrimaryKitTeams).WithOne(x => x.PrimaryKitColor).HasForeignKey(x => x.TeamId);
            builder.HasMany(x => x.SecondaryKitTeams).WithOne(x => x.SecondaryKitColor).HasForeignKey(x => x.TeamId);
        }
    }
}
