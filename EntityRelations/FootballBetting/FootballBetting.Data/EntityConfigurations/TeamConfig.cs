using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FootballBetting.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballBetting.Data.EntityConfigurations
{
    public class TeamConfig : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasOne(x => x.PrimaryKitColor).WithMany(x => x.PrimaryKitTeams).HasForeignKey(x => x.PrimaryKitColorId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.SecondaryKitColor).WithMany(x => x.SecondaryKitTeams).HasForeignKey(x => x.SecondaryKitColorId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Town).WithMany(t => t.Teams).HasForeignKey(x => x.TownId);

            builder.HasMany(x => x.HomeGames).WithOne(x => x.HomeTeam).HasForeignKey(x => x.GameId);
            builder.HasMany(x => x.AwayGames).WithOne(x => x.AwayTeam).HasForeignKey(x => x.GameId);

            builder.HasMany(p => p.Players).WithOne(t => t.Team).HasForeignKey(p => p.PlayerId);
        }
    }
}
