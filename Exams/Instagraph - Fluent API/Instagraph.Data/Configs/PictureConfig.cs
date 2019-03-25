using Instagraph.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagraph.Data.Configs
{
    public class PictureConfig : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(p => p.Users)
                .WithOne(u => u.ProfilePicture)
                .HasForeignKey(u => u.ProfilePictureId);

            builder.HasMany(p => p.Posts)
                .WithOne(po => po.Picture)
                .HasForeignKey(po => po.PictureId);
        }
    }
}
