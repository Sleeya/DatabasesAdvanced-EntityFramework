﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HospitalDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalDatabase.Data.EntityConfiguration
{
    public class DiagnoseConfig : IEntityTypeConfiguration<Diagnose>
    {
        public void Configure(EntityTypeBuilder<Diagnose> builder)
        {
            builder.HasKey(x => x.DiagnoseId);

            builder.Property(x => x.Name).HasMaxLength(50).IsUnicode();

            builder.Property(x => x.Comments).HasMaxLength(250).IsUnicode();
        }
    }
}
