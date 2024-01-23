using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingProductApi.Data;
using ShoppingProductApi.Model;
using ShoppingProductApi.Model.dto;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace ShoppingProductApi.Controllers
{
    [ApiController]
    [Route("api/seller")]
    public class SellerController : Controller
    {
        private readonly ProductDbContext _db;

        public SellerController(ProductDbContext db)
        {
            _db = db;
        }

        [HttpPost("AddSeller")]
        public IActionResult AddSeller([FromBody] SellerDto sellerDto)
        {
            if (sellerDto == null)
            {
                return BadRequest(new ResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid input data."
                });
            }

            try
            {
                // Create a new Seller entity from the DTO
                var newSeller = new Seller
                {
                    SellerName = sellerDto.SellerName,
                    Email = sellerDto.Email,
                    PhoneNumber = sellerDto.PhoneNumber
                };

                // Add the new seller to the database
                _db.Sellers.Add(newSeller);
                _db.SaveChanges();

                // Return the newly added seller's ID or any other relevant information
                return Ok(new ResponseDto
                {
                    Result = new { SellerID = newSeller.SellerID },
                    IsSuccess = true,
                    Message = "Seller added successfully."
                });
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an error response
                return StatusCode(500, new ResponseDto
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}"
                });
            }
        }

        [HttpGet("GetSeller")]
        public IActionResult GetAllSellers()
        {
            try
            {
                // Retrieve all sellers from the database along with their associated products
                var sellersWithProducts = _db.Sellers
                    .Select(seller => new
                    {
                        seller.SellerID,
                        seller.SellerName,
                        seller.Email,
                        seller.PhoneNumber,
                        Products = _db.Products.Where(product => product.SellerID == seller.SellerID).ToList()
                    })
                    .ToList();

                // Return the list of sellers with associated products
                return Ok(new ResponseDto
                {
                    Result = sellersWithProducts,
                    IsSuccess = true,
                    Message = "Sellers with associated products retrieved successfully."
                });
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an error response
                return StatusCode(500, new ResponseDto
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}"
                });
            }
        }

        [HttpDelete("DeleteSeller")]
        public IActionResult DeleteSeller(int id)
        {
            try
            {
                // Find the seller by ID
                var seller = _db.Sellers.Find(id);

                // If the seller is not found, return a 404 Not Found response
                if (seller == null)
                {
                    return NotFound(new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "Seller not found."
                    });
                }

                // Remove the seller from the database
                _db.Sellers.Remove(seller);
                _db.SaveChanges();

                // Return a success response
                return Ok(new ResponseDto
                {
                    IsSuccess = true,
                    Message = "Seller deleted successfully."
                });
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an error response
                return StatusCode(500, new ResponseDto
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}"
                });
            }
        }

    }
}
