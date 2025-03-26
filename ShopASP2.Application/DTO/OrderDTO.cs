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
