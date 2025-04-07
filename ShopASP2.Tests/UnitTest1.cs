using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using ShopASP2.Infrastructure.Repositories;

namespace ShopASP2.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task CreateOrderAsync_ShouldCreateOrder_WhenCartIsNotEmpty()
        {
            var dbContext = TestDbContextFactory.Create();

            var cartLoggerMock = new Mock<ILogger<CartItemRepository>>();
            var orderLoggerMock = new Mock<ILogger<OrderRepository>>()

            var cartRepo = new CartItemRepository(dbContext, cartLoggerMock.Object);
            var orderRepo = new OrderRepository(dbContext, orderLoggerMock.Object);

            int userid = 1;
        }
    }
}
