using AdminApi.Data;
using AdminApi.Model;
using AdminApi.Model.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCustomerApi.Model.Dto;
using System.Text.RegularExpressions;

namespace AdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminDbContext _db;

        public AdminController(AdminDbContext db)
        {
            _db = db;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AdminLoginDto loginDto)
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
            {
                return BadRequest(new ResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid input data."
                });
            }

            // Validate admin credentials against the database
            var (isValid, username) = ValidateAdminCredentials(loginDto.Email, loginDto.Password);

            if (!isValid)
            {
                return Unauthorized(new ResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid email or password."
                });
            }

            // Retrieve the username from the database based on the provided email
            string retrievedUsername = GetUsernameByEmail(loginDto.Email);

            // You may want to generate a session token or use other authentication mechanisms

            return Ok(new ResponseDto
            {
                Result = new { Email = loginDto.Email, Username = retrievedUsername },
                IsSuccess = true,
                Message = "Login successful."
            });
        }

        // Validate admin credentials method
        private (bool, string) ValidateAdminCredentials(string email, string password)
        {
            // Implement logic to validate admin credentials against database
            // For example, query your database to check if the email and password match an admin account
            // Return a tuple of (isValid, username)

            var admin = _db.Admins.FirstOrDefault(a => a.Email == email && a.Password == password);

            if (admin != null)
            {
                // Valid credentials, return true and the username
                return (true, admin.UserName);
            }
            else
            {
                // Invalid credentials, return false and an empty string for the username
                return (false, string.Empty);
            }
        }

        // Method to retrieve the username from the database based on email
        private string GetUsernameByEmail(string email)
        {
            // Implement your database query logic here
            var admin = _db.Admins.FirstOrDefault(a => a.Email == email);

            // Check if the admin was found and return the username, or return an empty string if not found
            return admin != null ? admin.UserName : string.Empty;
        }
    }
}
