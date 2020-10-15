using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Winc.Library.Entities;
using Winc.Shared.EntityFrameworkCore.Repository;

namespace Winc.Library.Repository.EFConfigurations
{
    public class ProductConfig : AuditEntityBaseConfig<Product>
    {
        public ProductConfig(): base("dbo")
        {
            
        }

        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar").IsRequired().HasMaxLength(50);
            builder.Property(x => x.IsAvailable).HasColumnName(@"IsAvailable").HasColumnType("bit").IsRequired();

            builder.Property(x => x.BrandSpecificProductAttributeId).HasColumnName(@"BrandSpecificProductAttributeId").HasColumnType("uniqueidentifier");

            // Foreign keys
            builder.HasOne(a => a.BrandSpecificProductAttribute).WithMany(b => b.Products).HasForeignKey(c => c.BrandSpecificProductAttributeId); // FK_LogDetails_Log


        }
    }
}
