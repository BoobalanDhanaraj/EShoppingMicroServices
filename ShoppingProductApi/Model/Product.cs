using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoppingProductApi.Model
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        [ForeignKey("Subcategory")]
        public int SubcategoryID { get; set; }

        [ForeignKey("Seller")]
        public int SellerID { get; set; }

        // Navigation properties
        public Subcategory Subcategory { get; set; }
        public Seller Seller { get; set; }
        public List<ProductImages> ProductImages { get; set; }
    }
}
