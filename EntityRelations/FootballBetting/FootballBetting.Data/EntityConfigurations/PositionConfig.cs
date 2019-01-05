using Microsoft.EntityFrameworkCore;
using FootballBetting.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballBetting.Data.EntityConfigurations
{
    public class PositionConfig : IEntityTypeConfiguration<Position>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Position> builder)
        {
            builder.HasMany(p => p.Players).WithOne(p => p.Position).HasForeignKey(p => p.PlayerId);
        }
    }
}
