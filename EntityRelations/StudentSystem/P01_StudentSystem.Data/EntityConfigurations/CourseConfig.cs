using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_StudentSystem.Data.EntityConfigurations
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(x => x.CourseId);

            builder.Property(x => x.Name).HasMaxLength(80).IsUnicode().IsRequired();
            builder.Property(x => x.Description).IsUnicode().IsRequired(false);
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            builder.Property(x => x.Price).IsRequired();

            builder.HasMany(x => x.Resources).WithOne(x => x.Course).HasForeignKey(x => x.CourseId);
            builder.HasMany(x => x.HomeworkSubmissions).WithOne(x => x.Course).HasForeignKey(x => x.CourseId);
        }
    }
}
