namespace ShopASP2.Application.DTO
{
    public class OrderItemDTO
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtPurchase { get; set; }
        public string NameAtPurchase { get; set; }
        public decimal OrderFinalPrice { get; set; }
    }
}
