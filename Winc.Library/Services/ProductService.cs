using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winc.Library.DTOs;
using Winc.Library.Entities;
using Winc.Library.Repository;
using Winc.Shared.EntityFrameworkCore.Services;

namespace Winc.Library.Services
{
    public interface IProductService : IGenericService
    {
        Task<IEnumerable<ProductDto>> GetProductListAsync();
        Task<IEnumerable<ProductAttributeDto>> GetProductListByBrandAsync(Guid brandId);
    }
    public class ProductService : GenericService, IProductService
    {
        private IWincRepository _wincRepository;
        public ProductService(IWincRepository wincRepository) : base(wincRepository)
        {
            _wincRepository = wincRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetProductListAsync()
        {
            var prodList = new List<ProductDto>();

            var products = await _wincRepository.GetAllAsync<Product>();
            foreach (var prod in products)
            {
                var p = new ProductDto
                {
                    ProductId = prod.Id,
                    Name = prod.Name,
                    IsAvailable = prod.IsAvailable
                };
                prodList.Add(p);
            }

            return prodList;
        }

        public async Task<IEnumerable<ProductAttributeDto>> GetProductListByBrandAsync(Guid brandId)
        {
            var prod = from p in _wincRepository.Query<Product>()
                       join bspa in _wincRepository.Query<BrandSpecificProductAttribute>() on p.BrandSpecificProductAttributeId equals bspa.Id
                       where bspa.Id == brandId && p.IsAvailable == true
                    select new ProductAttributeDto
                    {
                        ProductId = p.Id,
                        Name = p.Name,
                        IsAvailable = p.IsAvailable,
                        BrandId = bspa.Id,
                        Description = bspa.Description,
                        Image = bspa.Image,
                        Price = bspa.Price
                    };


            return prod;
        }

    }
}
