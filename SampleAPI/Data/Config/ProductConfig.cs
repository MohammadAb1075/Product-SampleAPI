using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleAPI.Entities;
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
                .IsRequired()
                .IsUnicode()
                .HasDefaultValueSql("NewId()");
                //.HasDefaultValue(Guid.NewGuid().ToString());
            builder.Property(x => x.Title)
                .IsRequired();
            builder.Property(x => x.Type)
                .IsRequired()
            ;
            builder.Property(x => x.Price)
                .IsRequired();
            builder.Property(x => x.Color)
                .IsRequired();

        }

    }
}



