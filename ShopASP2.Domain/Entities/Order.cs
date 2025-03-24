using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP2.Domain.Entities
{
    public class Order
    {
        public int OrderID { get; set; }
        public string UserID { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual User User { get; set; }
    }

}
