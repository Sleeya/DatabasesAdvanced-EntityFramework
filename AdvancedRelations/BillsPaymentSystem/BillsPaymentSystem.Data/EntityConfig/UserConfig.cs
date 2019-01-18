using BillsPaymentSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillsPaymentSystem.Data.EntityConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.UserId);

            builder.Property(x => x.FirstName)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(80)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.Password)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsRequired();
        }
    }
}
