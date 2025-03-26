using AutoMapper;
using Microsoft.Extensions.Logging;
using ShopASP2.Application.DTO;
using ShopASP2.Application.Interfaces;
using ShopASP2.Domain.Entities;
using ShopASP2.Domain.Interfaces;

namespace ShopASP2.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper mapper;
        private readonly IProductRepository repository;
        private readonly ILogger<ProductService> logger;
        public ProductService(IMapper mapper, IProductRepository repository, ILogger<ProductService> logger)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.repository = repository;
        }
        
        public async Task<IEnumerable<ProductDTO>> GetValues()
        {
            IEnumerable<Product> products = await repository.GetValues();
            try
            {
                logger.LogInformation("Попытка вернуть все продукты в сервисе");
                return mapper.Map<IEnumerable<ProductDTO>>(products);
            }
            catch(Exception ex)
            {
                logger.LogError("Ошибка вывода продуктов в сервисе. Описание:");
                logger.LogError(ex.Message.ToString());
                return null;
            }
        }

        public async Task<ProductDTO> GetValue(int id)
        {
            Product product = await repository.GetValue(id);
            try
            {
                logger.LogInformation($"Попытка вернуть продукт по ID {id}");
                return mapper.Map<ProductDTO>(product);
            }
            catch(Exception ex)
            {
                logger.LogError("Ошибка поиска продукта в сервисе. Описание:");
                logger.LogError(ex.Message.ToString());
                return null;
            }
        }

        public async Task CreateValue(ProductDTO product)
        {
            Product newproduct = mapper.Map<Product>(product);
            try
            {
                logger.LogInformation("Попытка сохранить продукт в сервисе");
                await repository.CreateValue(newproduct);
            }
            catch(Exception ex)
            {
                logger.LogError("Ошибка добавления продукта в сервисе. Описание:");
                logger.LogError(ex.Message.ToString());
            }
        }

        public async Task UpdateValue(ProductDTO product)
        {
            Product updProduct = mapper.Map<Product>(product);
            try
            {
                logger.LogInformation("Попытка обновить продукт в сервисе");
                await repository.UpdateValue(updProduct);
            }
            catch(Exception ex)
            {
                logger.LogError("Ошибка обновления продукта в сервисе. Описание");
                logger.LogError(ex.Message.ToString());
            }
        }

        public async Task DeleteValue(int id)
        {
            try
            {
                await repository.DeleteValue(id);
                logger.LogInformation("Попытка удалить продукт в сервисе");
            }
            catch(Exception ex)
            {
                logger.LogError("Ошибка удаления продукта в сервисе. Описание:");
                logger.LogError(ex.Message.ToString());
            }
        }
    }
}
