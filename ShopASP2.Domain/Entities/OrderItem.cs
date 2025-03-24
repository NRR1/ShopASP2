using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP2.Domain.Entities
{
    public class OrderItem
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public virtual Order Order { get; set; }
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtPurchase { get; set; }
        public string NameAtPurchase { get; set; }
        public decimal OrderFinalPrice { get; set; }
    }
}
