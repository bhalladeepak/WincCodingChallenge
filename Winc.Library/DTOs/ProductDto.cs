using System;
using System.Collections.Generic;
using System.Text;

namespace Winc.Library.DTOs
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }

    }
}
