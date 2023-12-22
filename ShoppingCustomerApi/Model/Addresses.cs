using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCustomerApi.Model
{
    public class Addresses
    {
        [Key]
        public int AddressID { get; set; }

        [ForeignKey("CustomerID")]
        public int CustomerID { get; set; }
        public string AddressType { get; set; }
        public string DoorNo { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

        public Customer Customer { get; set; }

    }
}
