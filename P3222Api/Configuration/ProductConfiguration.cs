using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3222Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3222Api.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired(true).HasMaxLength(10);
            builder.Property(p => p.Price).HasDefaultValue(50).HasColumnType("decimal(18,2)");
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
           // builder.Property(p => p.CreateTime).HasDefaultValue(DateTime.UtcNow);
            builder.Property(p => p.CreateTime).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
