using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FootballBetting.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballBetting.Data.EntityConfigurations
{
    public class BetConfig : IEntityTypeConfiguration<Bet>
    {
        public void Configure(EntityTypeBuilder<Bet> builder)
        {
            builder.Property(x => x.Prediction).IsRequired();

            builder.HasOne(g => g.Game).WithMany(b => b.Bets).HasForeignKey(g => g.GameId);

            builder.HasOne(u => u.User).WithMany(b => b.Bets).HasForeignKey(u => u.UserId);
            
        }
    }
}
