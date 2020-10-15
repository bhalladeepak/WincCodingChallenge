using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Winc.Library.DbConfigurations;
using Winc.Library.Entities;
using Winc.Library.Repository.EFConfigurations;

namespace Winc.Library.Repository
{

    public class WincDbContext : DbContext
    {
        public WincDbContext(DbContextOptions<WincDbContext> dbOptions) : base(dbOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new BrandSpecificProductAttributeConfig());

            base.OnModelCreating(modelBuilder);

        }

        //all tables display under Entities folder i.e Entity set
        #region Tables

        public DbSet<Product> Products { get; set; }
        public DbSet<BrandSpecificProductAttribute> BrandSpecificProductAttributes { get; set; }


        #endregion
    }
}
