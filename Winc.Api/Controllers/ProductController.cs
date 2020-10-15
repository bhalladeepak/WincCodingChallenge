using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Winc.Library.DTOs;
using Winc.Library.Services;

namespace Winc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
         public ProductController(IProductService productService)
         {
             _productService = productService;
         }
        [HttpGet]
        [Route("GetProductList")]
        public async Task<IEnumerable<ProductDto>> GetProductListAsync()
        {
            try
            {
                var prodList = await _productService.GetProductListAsync();
                return prodList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        [HttpGet]
        [Route("GetProductListByBrand/{brandId} ")]
        public async Task<IEnumerable<ProductAttributeDto>> GetProductListByBrandAsync(string brandId)
        {
            try
            {
                var productAttribute = await _productService.GetProductListByBrandAsync(brandId);
                return productAttribute;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
