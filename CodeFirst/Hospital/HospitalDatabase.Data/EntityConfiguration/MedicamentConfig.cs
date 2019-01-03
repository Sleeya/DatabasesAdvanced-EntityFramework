using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HospitalDatabase.Data.Models;
using System;


namespace HospitalDatabase.Data.EntityConfiguration
{
    public class MedicamentConfig : IEntityTypeConfiguration<Medicament>
    {
        public void Configure(EntityTypeBuilder<Medicament> builder)
        {
            builder.HasKey(x => x.MedicamentId);

            builder.Property(x => x.Name).HasMaxLength(50).IsUnicode();

            builder.HasMany(x => x.Prescriptions).WithOne(x => x.Medicament).HasForeignKey(x => x.MedicamentId);
        }
    }
}
