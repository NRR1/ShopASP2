using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP2.Application.DTO
{
    public class OrderDTO
    {
        public int OrderID { get; set; }
        public string UserID { get; set; }
        public DateTime CreateAt { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
