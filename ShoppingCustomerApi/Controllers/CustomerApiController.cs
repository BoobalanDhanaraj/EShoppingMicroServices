using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCustomerApi.Data;
using ShoppingCustomerApi.Model;
using ShoppingCustomerApi.Model.Dto;
using System.Text.RegularExpressions;

namespace ShoppingCustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly CusomerDbContext _db;
        private IMapper _mapper;
        private IConfiguration _config;

        public CustomerApiController(CusomerDbContext db, IMapper mapper, IConfiguration config)
        {
            _db = db;
            _mapper = mapper;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CustomerLoginDto customerDto)
        {
            var response = new ResponseDto();
            try
            {
                // Check for null or empty email
                if (string.IsNullOrEmpty(customerDto.Email))
                {
                    response.IsSuccess = false;
                    response.Message = "Email is required for login.";
                    return BadRequest(response);
                }

                // Validate email format
                if (!IsValidEmail(customerDto.Email))
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid email format.";
                    return BadRequest(response);
                }

                // Find the customer by email
                var customer = await _db.Customers.SingleOrDefaultAsync(c => c.Email == customerDto.Email);

                // Check if the customer exists
                if (customer != null)
                {
                    // Check if the password is correct
                    if (IsPasswordValid(customer, customerDto.Password))
                    {
                        // Map the customer entity to a DTO if needed
                        var customerResponseDto = _mapper.Map<CustomerLoginDto>(customer);

                        // Set the success response for correct login
                        response.Result = customerResponseDto;
                        response.Message = "Login successful";
                    }
                    else
                    {
                        // Set the unsuccessful response for incorrect password
                        response.IsSuccess = false;
                        response.Message = "Incorrect password";
                    }
                }
                else
                {
                    // Set the unsuccessful response for incorrect email
                    response.IsSuccess = false;
                    response.Message = "No account found with the provided email";
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                response.IsSuccess = false;
                response.Message = "Internal Server Error";
            }

            return Ok(response);
        }

        // Email validation using a simple regular expression
        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
            return Regex.IsMatch(email, emailPattern);
        }

        // Add your secure password hashing and validation logic here
        private bool IsPasswordValid(Customer customer, string enteredPassword)
        {
            // Implement secure password validation logic (e.g., using a password hashing library)
            // For simplicity, this example assumes plain-text passwords (not recommended for production)
            return customer.Password == enteredPassword;
        }

    }
}
