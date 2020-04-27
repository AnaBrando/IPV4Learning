using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infra.Config
{
    public class UserConfig : IEntityTypeConfiguration<Usuario>
    {

        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(u => u.ImageName)
                .IsRequired()
                .HasMaxLength(256);
           
       
            builder.Property(u => u.PhotoFile)
              .IsRequired()
              .HasMaxLength(256);

            builder.ToTable("AspNetUsers");
        }
    }
}

