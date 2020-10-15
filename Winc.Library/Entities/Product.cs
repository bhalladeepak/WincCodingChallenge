using System;
using System.Collections.Generic;
using System.Text;
using Winc.Shared.EntityFrameworkCore.Entities;

namespace Winc.Library.Entities
{
    public class Product : AuditableEntity
    {
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public Guid BrandSpecificProductAttributeId { get; set; }

        public virtual BrandSpecificProductAttribute BrandSpecificProductAttribute { get; set; }

    }
}
