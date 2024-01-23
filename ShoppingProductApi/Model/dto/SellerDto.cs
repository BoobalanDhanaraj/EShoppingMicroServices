using System.ComponentModel.DataAnnotations;

namespace ShoppingProductApi.Model.dto
{
    public class SellerDto
    {
        public string SellerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
