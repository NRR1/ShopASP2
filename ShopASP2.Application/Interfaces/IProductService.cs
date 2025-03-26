using ShopASP2.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP2.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetValues();
        Task<ProductDTO> GetValue(int id);
        Task CreateValue(ProductDTO product);
        Task UpdateValue(ProductDTO product);
        Task DeleteValue(int id);
    }
}
