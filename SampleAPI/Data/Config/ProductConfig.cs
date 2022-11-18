using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace SampleAPI.Models.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder
                .HasKey(e => e.Id)
                .HasName("PK_Product");
            builder.Property(x => x.Id)
                .HasDefaultValue(Guid.NewGuid());
            builder.Property(x => x.Title)
                .IsRequired();
            builder.Property(x => x.Type)
                .IsRequired();
            builder.Property(x => x.Price)
                .IsRequired();
            builder.Property(x => x.Color)
                .IsRequired();

        }

    }
}



