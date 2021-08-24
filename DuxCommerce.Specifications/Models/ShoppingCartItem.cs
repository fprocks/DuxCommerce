namespace DuxCommerce.Specifications.Models
{
    public class ShoppingCartItem
    {
        public int Product { get; set; }
        public string ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal ItemTotal { get; set; }
    }
}