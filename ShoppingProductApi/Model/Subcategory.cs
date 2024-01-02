using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoppingProductApi.Model
{
    public class Subcategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubCategoryID { get; set; }

        [Required]
        public string SubCategoryName { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        // Navigation property
        public Category Category { get; set; }
    }
}
