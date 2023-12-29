using System.ComponentModel.DataAnnotations;

namespace ShoppingCustomerApi.Model.Dto
{
    public class CustomerSignUpDto
    {
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }
}
