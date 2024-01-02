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
        [HttpPost("AddAddress")]
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

        [HttpGet("{customerId}")]
        public IActionResult GetAddresses(int customerId)
        {
            var response = new ResponseDto();

            try
            {
                // Retrieve addresses for the given customer ID
                var addresses = _db.Addresses
                    .Where(a => a.CustomerID == customerId)
                    .ToList();

                if (addresses.Count == 0)
                {
                    // If the customer doesn't have any addresses, set Result to null
                    response.Result = null;
                    response.Message = "Customer has no addresses stored.";
                }
                else
                {
                    response.Result = addresses;
                    response.Message = "Addresses retrieved successfully";
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                response.IsSuccess = false;
                response.Message = "Failed to retrieve addresses";
            }

            return Ok(response);
        }
    }
}
