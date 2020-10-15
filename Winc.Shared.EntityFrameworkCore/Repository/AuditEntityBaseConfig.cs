using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Winc.Shared.EntityFrameworkCore.Entities;
using Winc.Shared.EntityFrameworkCore.Helper;

namespace Winc.Shared.EntityFrameworkCore.Repository
{
    public class AuditEntityBaseConfig<T> : IEntityTypeConfiguration<T> where T : AuditableEntity
    {
        public AuditEntityBaseConfig()
        {
            _schema = "dbo";
        }

        private readonly string _schema;
        public AuditEntityBaseConfig(string schema = "dbo")
        {
            _schema = schema;
        }

        public virtual void Configure(EntityTypeBuilder<T> entity)
        {
            var tableName = Inflector.Pluralize(typeof(T).Name).ToLower();

            var pk = typeof(T).Name + "Id";

            entity.ToTable(tableName, _schema);
            entity.HasKey(e => e.Id);


            entity.Property(e => e.Id).HasColumnName(pk).HasDefaultValueSql("(newsequentialid())");
            entity.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("uniqueidentifier").IsRequired();
            entity.Property(x => x.CreatedDate).HasColumnName(@"CreatedDate").HasColumnType("datetime").IsRequired();
            entity.Property(x => x.ModifiedBy).HasColumnName(@"ModifiedBy").HasColumnType("uniqueidentifier");
            entity.Property(x => x.ModifiedDate).HasColumnName(@"ModifiedDate").HasColumnType("datetime");

        }
    }
}
