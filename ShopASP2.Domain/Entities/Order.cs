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
