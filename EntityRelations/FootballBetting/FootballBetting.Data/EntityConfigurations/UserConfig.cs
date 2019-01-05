using Microsoft.EntityFrameworkCore;
using FootballBetting.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballBetting.Data.EntityConfigurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder.HasMany(b => b.Bets).WithOne(u => u.User).HasForeignKey(b => b.BetId);
        }
    }
}
