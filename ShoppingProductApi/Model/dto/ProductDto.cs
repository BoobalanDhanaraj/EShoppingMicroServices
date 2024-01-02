namespace ShoppingProductApi.Model.dto
{
    public class ProductDto
    {
        public int ProductID { get; set; }
        public string ProductImage { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string CategoryName { get; set; }
        public string SubcategoryName { get; set; }
        public string SellerName { get; set; }
    }
}
