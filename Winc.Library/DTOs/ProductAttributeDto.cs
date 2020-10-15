using System;
using System.Collections.Generic;
using System.Text;

namespace Winc.Library.DTOs
{
    public class ProductAttributeDto : ProductDto
    {
        public Guid BrandId { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
    }
}
