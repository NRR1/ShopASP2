using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopASP2.Domain.Entities;
using ShopASP2.Domain.Interfaces;
using ShopASP2.Infrastructure.Data;

namespace ShopASP2.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopASP2DBContext db;
        private readonly ILogger<ProductRepository> logger;
        public ProductRepository(ShopASP2DBContext db, ILogger<ProductRepository> logger)
        {
            this.db = db;
            this.logger = logger;
        }
        public async Task<IEnumerable<Product>> GetValues()
        {
            IEnumerable<Product> products = await db.Products.ToListAsync();
            logger.LogInformation("Попытка вернуть продукты(репозиторий)");
            try
            {
                return products;
            }
            catch(Exception ex)
            {
                logger.LogError("Ошибка вывода продуктов\nОписание ошибки:");
                logger.LogError(ex.Message.ToString());
                return null;
            }
        }

        public async Task<Product> GetValue(int id)
        {
            Product? product = await db.Products.FindAsync(id);
            logger.LogInformation($"Попытка вывести продукт по ID:{id}(репозиторий)");
            try
            {
                return product;
            }
            catch(Exception ex)
            {
                logger.LogError($"Ошибка вывода продукта по ID:{id}(репозиторий)");
                logger.LogError(ex.Message.ToString());
                return null;
            }
        }

        public async Task CreateValue(Product product)
        {
            await db.Products.AddAsync(product);
            logger.LogInformation("Попытка сохранить продукт(репозиторий)");
            try
            {
                await db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                logger.LogError($"Ошибка сохранения продукта. Описание ошибки:");
                logger.LogError(ex.Message.ToString());
            }
        }

        public async Task UpdateValue(Product product)
        {
            db.Products.Update(product);
            logger.LogInformation("Попытка обновить продукт(репозиторий)");
            try
            {
                await db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                logger.LogError("Ошибка обновления продукта");
                logger.LogError(ex.Message.ToString());
            }
        }

        public async Task DeleteValue(int id)
        {
            Task<Product> product = GetValue(id);
            db.Products.Remove(product.Result);
            logger.LogInformation("Попытка удалить продукт");
            try
            {
                await db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                logger.LogError("Ошибка удаления продукта");
                logger.LogError(ex.Message.ToString());
            }
        }
    }
}