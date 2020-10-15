using System;
using System.Collections.Generic;
using System.Text;
using Winc.Shared.EntityFrameworkCore.Entities;

namespace Winc.Library.Entities
{
    public class BrandSpecificProductAttribute : AuditableEntity
    {
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public BrandSpecificProductAttribute()
        {
            Products = new List<Product>();
        }

    }
}
