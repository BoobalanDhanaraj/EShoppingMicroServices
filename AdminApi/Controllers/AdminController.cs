using AdminApi.Data;
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
            var isValid = ValidateAdminCredentials(loginDto.Email, loginDto.Password);

            if (!isValid)
            {
                return Unauthorized(new ResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid email or password."
                });
            }

            // You may want to generate a session token or use other authentication mechanisms

            return Ok(new ResponseDto
            {
                IsSuccess = true,
                Message = "Login successful."
            });
        }

        private bool ValidateAdminCredentials(string email, string password)
        {
            // Implement logic to validate admin credentials against your database
            // For example, query your database to check if the email and password match an admin account
            // Return true if valid, false otherwise

            var admin = _db.Admins.FirstOrDefault(a => a.Email == email && a.Password == password);
            return admin != null;
        }
    }
}
