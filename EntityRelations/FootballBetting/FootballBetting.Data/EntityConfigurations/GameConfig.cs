using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FootballBetting.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballBetting.Data.EntityConfigurations
{
    public class GameConfig : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasOne(t => t.HomeTeam).WithMany(g => g.HomeGames).HasForeignKey(t=> t.HomeTeamId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.AwayTeam).WithMany(g => g.AwayGames).HasForeignKey(t => t.AwayTeamId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.PlayerStatistics).WithOne(g => g.Game).HasForeignKey(p => p.PlayerId);

            builder.HasMany(b => b.Bets).WithOne(g => g.Game).HasForeignKey(b => b.BetId);
        }
    }
}
