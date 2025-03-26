using ShopASP2.Domain.Entities;

namespace ShopASP2.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetValues();
        Task<Product> GetValue(int id);
        Task CreateValue(Product product);

        Task UpdateValue(Product product);
        Task DeleteValue(int id);
    }
}
