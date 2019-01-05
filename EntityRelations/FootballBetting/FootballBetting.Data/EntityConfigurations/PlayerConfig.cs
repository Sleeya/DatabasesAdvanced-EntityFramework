using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FootballBetting.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballBetting.Data.EntityConfigurations
{
    public class PlayerConfig : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(x => x.PlayerId);

            builder.HasOne(t => t.Team).WithMany(p => p.Players).HasForeignKey(t => t.TeamId);

            builder.HasOne(p => p.Position).WithMany(p => p.Players).HasForeignKey(p => p.PositionId);

            builder.HasMany(g => g.PlayerStatistics).WithOne(x => x.Player).HasForeignKey(x => x.GameId);
        }
    }
}
