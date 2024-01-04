namespace ShoppingProductApi.Model.dto
{
    public class AddProductDto
    {
        public int ProductID { get; set; }
        public string ProductImageUrl { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string CategoryId { get; set; }
        public string SubcategoryId { get; set; }
        public string SellerId { get; set; }
    }
}
