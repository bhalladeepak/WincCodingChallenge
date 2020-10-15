using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Winc.Library.Entities;
using Winc.Shared.EntityFrameworkCore.Repository;

namespace Winc.Library.Repository.EFConfigurations
{
    public class BrandSpecificProductAttributeConfig : AuditEntityBaseConfig<BrandSpecificProductAttribute>
    {
        public BrandSpecificProductAttributeConfig() : base("dbo")
        {
            
        }

        public override void Configure(EntityTypeBuilder<BrandSpecificProductAttribute> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Description).HasColumnName(@"Description").HasColumnType("nvarchar").IsRequired().HasMaxLength(200);
            builder.Property(x => x.Image).HasColumnName(@"Image").HasColumnType("nvarchar").IsRequired().HasMaxLength(200);
            builder.Property(x => x.Price).HasColumnName(@"Price").HasColumnType("decimal").IsRequired();
            
        }
    }
}
