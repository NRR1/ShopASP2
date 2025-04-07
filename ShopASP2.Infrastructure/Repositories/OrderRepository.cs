using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopASP2.Domain.Entities;
using ShopASP2.Infrastructure.Data;

namespace ShopASP2.Infrastructure.Repositories
{
    public class OrderRepository
    {
        private readonly ShopASP2DBContext db;
        private readonly ILogger<OrderRepository> logger;
        public OrderRepository(ShopASP2DBContext db, ILogger<OrderRepository> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        public async Task<IEnumerable<Order>> GetValues()
        {
            IEnumerable<Order> orders = await db.Orders.Include(o => o.OrderItems).ThenInclude(p => p.Product).ToListAsync();
            return orders;
        }

        public async Task<IEnumerable<Order>> GetUserOrders(string userID)
        {
            IEnumerable<Order> userOrders = await db.Orders.Include(o => o.OrderItems).ThenInclude(p => p.Product).Where(u => u.UserID == userID).ToListAsync();
            return userOrders;
        }

        public async Task<Order> GetOrder(int id)
        {
            Order? order = await db.Orders.
                                Include(o => o.User).
                                Include(o => o.OrderItems).
                                ThenInclude(p => p.Product).
                                Where(oid => oid.OrderID == id).
                                FirstOrDefaultAsync();
            return order;
        }

        public async Task CreateOrderFromCart(string userId)
        {
            var cartItems = await db.CartItems.Include(p => p.Product)
                                              .Where(c => c.UserID == userId)
                                              .ToListAsync();
            if (!cartItems.Any())
                return;

            decimal totalAmount = 0;
            foreach (var item in cartItems)
            {
                if (item.Product.Quantity < item.Quantity)
                {
                    logger.LogWarning($"Недостаточно товара: {item.Product.Name}");
                    return; // или можно сделать частичный заказ
                }
                totalAmount += item.Product.Cost * item.Quantity;
            }

            var newOrder = new Order
            {
                UserID = userId,
                TotalAmount = totalAmount,
                OrderItems = new List<OrderItem>()
            };

            foreach (var item in cartItems)
            {
                item.Product.Quantity -= item.Quantity;
                db.Products.Update(item.Product);

                newOrder.OrderItems.Add(new OrderItem
                {
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    PriceAtPurchase = item.Product.Cost,
                    NameAtPurchase = item.Product.Name,
                    OrderFinalPrice = item.Product.Cost * item.Quantity
                });
            }

            await db.Orders.AddAsync(newOrder);
            await db.SaveChangesAsync();

            db.CartItems.RemoveRange(cartItems);
            await db.SaveChangesAsync();
        }


        

        public async Task CreateOrder(string userID, int productID, int quantity)
        {
            Product product = await db.Products.FindAsync(productID);
            if(product == null)
            {
                logger.LogError("Репозиторий(создание заказа->поиск продукта) вернул null");
                return;
            }
            if(product.Quantity < quantity)
            {
                logger.LogError("Заказано продукта больше чем на складе");
                return;
            }
            //Создаём объект Order
            Order newOrder = new Order()
            {
                UserID = userID,
                TotalAmount = quantity * product.Cost
            };
            
            product.Quantity -= quantity; //Обращаемся к объекту Product и уменьшаем количество товара
            db.Products.Update(product); //Сохраняем изменение в обновлении количества товара после заказа
            await db.Orders.AddAsync(newOrder); //Добавляем запись в таблицу Order, то есть создаём сам заказ
            await db.SaveChangesAsync(); //Сохраняем внесенные изменения


            //Создаём объект таблицы OrderItem
            OrderItem OP = new OrderItem()
            {
                OrderID = newOrder.OrderID,
                ProductID = productID,
                Quantity = quantity,
                PriceAtPurchase = product.Cost,
                NameAtPurchase = product.Name,
                OrderFinalPrice = newOrder.TotalAmount
            };

            //Сохраняем объект в таблицу OrderItem
            await db.OrderItems.AddAsync(OP);
            //Сохраняем изменения
            await db.SaveChangesAsync();
        }
    }
}
