using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HospitalDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalDatabase.Data.EntityConfiguration
{
    public class VisitationConfig : IEntityTypeConfiguration<Visitation>
    {
        public void Configure(EntityTypeBuilder<Visitation> builder)
        {
            builder.HasKey(x => x.VisitationId);

            builder.Property(x => x.Comments).HasMaxLength(250).IsUnicode();
        }
    }
}
