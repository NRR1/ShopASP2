using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopASP2.Domain.Entities;
using ShopASP2.Infrastructure.Data;

namespace ShopASP2.Infrastructure.Repositories
{
    public class CartItemRepository
    {
        private readonly ShopASP2DBContext db;
        private readonly ILogger<CartItemRepository> logger;
        public CartItemRepository(ShopASP2DBContext db, ILogger<CartItemRepository> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        public async Task<IEnumerable<CartItem>> GetValues(string userid)
        {
            IEnumerable<CartItem> items = await db.CartItems.Include(p => p.Product).Where(uid => uid.UserID == userid).ToListAsync();
            return items;
        }


        public async Task ClearCart(string userid)
        {
            var items = db.CartItems.Where(uid => uid.UserID == userid);
            db.CartItems.RemoveRange(items);
            await db.SaveChangesAsync();
        }


        public async Task<CartItem> AddToCart(string userid, int productid, int quantity)
        {
            CartItem existingItem = await db.CartItems.FirstOrDefaultAsync(ci => ci.UserID == userid && ci.ProductID == productid);
            if(existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                Product product = await db.Products.FindAsync(productid);
                if(product == null)
                {
                    return null;
                }
                existingItem = new CartItem
                {
                    UserID = userid,
                    ProductID = productid,
                    Quantity = quantity,
                    PriceAtAdd = product.Cost
                };
                await db.CartItems.AddAsync(existingItem);
            }
            await db.SaveChangesAsync();
            return existingItem;
        }
    }
}
