using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace ShoppingProductApi.Model
{
    public class ProductImages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageID { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set; }

        public string ImageURLs { get; set; }

        // Navigation property
        public Product Product { get; set; }
    }
}
