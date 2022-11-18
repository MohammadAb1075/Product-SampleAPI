using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleAPI.Models;

namespace SampleAPI.Data.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.Id)
                        .IsRequired()
                        .ValueGeneratedNever();
            builder.Property(e => e.Username)
                                .IsRequired()
                                .HasMaxLength(256);
            builder.Property(e => e.Password)
                                .IsRequired()
                                .HasMaxLength(256);
            builder.Property(e => e.IsActive)
                                .IsRequired()
                                .HasDefaultValue(1);
            }
    }
}
