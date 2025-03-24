using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP2.Domain.Entities
{
    public class CartItem
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtAdd { get; set; }
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}
