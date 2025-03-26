namespace ShopASP2.Application.DTO
{
    public class CartItemDTO
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtAdd { get; set; }
    }
}
