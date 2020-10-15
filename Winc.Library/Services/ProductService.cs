using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Winc.Library.DTOs;
using Winc.Library.Repository;
using Winc.Shared.EntityFrameworkCore.Services;

namespace Winc.Library.Services
{
    public interface IProductService : IGenericService
    {
        Task<IEnumerable<ProductDto>> GetProductListAsync();
        Task<IEnumerable<ProductAttributeDto>> GetProductListByBrandAsync(string brandId);
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

            return prodList;
        }

        public async Task<IEnumerable<ProductAttributeDto>> GetProductListByBrandAsync(string brandId)
        {
            var prodAttributeList = new List<ProductAttributeDto>();

            return prodAttributeList;
        }

    }
}
