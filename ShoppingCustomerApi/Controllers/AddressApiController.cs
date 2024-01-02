using Microsoft.AspNetCore.Mvc;
using ShoppingCustomerApi.Data;
using ShoppingCustomerApi.Model.Dto;
using ShoppingCustomerApi.Model;

namespace ShoppingCustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressApiController : Controller
    {
        private readonly CusomerDbContext _db;

        public AddressApiController(CusomerDbContext db)
        {
            _db = db;
        }
        [HttpPost]
        public IActionResult AddAddress([FromBody] AddressesDto addressDto)
        {
            var response = new ResponseDto();

            try
            {
                // Create a new Address entity from the DTO
                var address = new Addresses
                {
                    CustomerID = addressDto.CustomerID,
                    AddressType = addressDto.AddressType,
                    DoorNo = addressDto.DoorNo,
                    StreetAddress = addressDto.StreetAddress,
                    City = addressDto.City,
                    State = addressDto.State,
                    ZipCode = addressDto.ZipCode,
                    Country = addressDto.Country
                };

                // Add the address to the database
                _db.Addresses.Add(address);
                _db.SaveChanges();

                response.Result = address;
                response.Message = "Address added successfully";
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                response.IsSuccess = false;
                response.Message = "Failed to add address";
            }

            return Ok(response);
        }
    }
}
