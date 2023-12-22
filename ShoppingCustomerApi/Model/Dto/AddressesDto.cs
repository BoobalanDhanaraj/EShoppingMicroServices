

namespace ShoppingCustomerApi.Model.Dto
{
    public class AddressesDto
    {
        public int AddressID { get; set; }
        public int CustomerID { get; set; }
        public string AddressType { get; set; }
        public string DoorNo { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }
}
