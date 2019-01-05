using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FootballBetting.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballBetting.Data.EntityConfigurations
{
    public class PlayerStatisticConfig : IEntityTypeConfiguration<PlayerStatistic>
    {
        public void Configure(EntityTypeBuilder<PlayerStatistic> builder)
        {
            

            builder.HasKey(x => new { x.PlayerId, x.GameId });

            builder.HasOne(p => p.Player).WithMany(p => p.PlayerStatistics).HasForeignKey(p => p.PlayerId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(g => g.Game).WithMany(p=>p.PlayerStatistics).HasForeignKey(p => p.GameId);

        }
    }
}
