using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.EntityConfigurations
{
    public class HomeworkConfig : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            builder.HasKey(x => x.HomeworkId);

            builder.Property(x => x.Content).IsUnicode(false).IsRequired();
            builder.Property(x => x.ContentType).IsRequired();
            builder.Property(x => x.SubmissionTime).IsRequired();

            builder.HasOne(x => x.Student).WithMany(x => x.HomeworkSubmissions).HasForeignKey(x => x.HomeworkId);
            builder.HasOne(x => x.Course).WithMany(x => x.HomeworkSubmissions).HasForeignKey(x => x.HomeworkId);
        }
    }
}
