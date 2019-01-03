using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HospitalDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalDatabase.Data.EntityConfiguration
{
    public class PatientMedicamentConfig : IEntityTypeConfiguration<PatientMedicament>
    {
        public void Configure(EntityTypeBuilder<PatientMedicament> builder)
        {
            builder.HasKey(x => new { x.PatientId, x.MedicamentId });
        }
    }
}
