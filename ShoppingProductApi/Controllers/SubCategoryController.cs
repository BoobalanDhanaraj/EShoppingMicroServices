using Microsoft.AspNetCore.Mvc;
using ShoppingProductApi.Model.dto;
using ShoppingProductApi.Model;
using ShoppingProductApi.Data;

namespace ShoppingProductApi.Controllers
{
    [ApiController]
    [Route("api/subcategories")]
    public class SubcategoriesController : ControllerBase
    {
        private readonly ProductDbContext _db; 

        public SubcategoriesController(ProductDbContext db)
        {
            _db = db;
        }

        [HttpPost("AddSubcategory")]
        public IActionResult AddSubcategory([FromBody] SubCategoryDto subcategoryDto)
        {
            var response = new ResponseDto();

            try
            {
                // Map DTO to entity
                var subcategory = new Subcategory
                {
                    SubCategoryName = subcategoryDto.SubCategoryName,
                    CategoryID = subcategoryDto.CategoryID
                };

                // Add to the database
                _db.Subcategories.Add(subcategory);
                _db.SaveChanges();

                response.Result = subcategory;
                response.Message = "Subcategory added successfully";

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                response.IsSuccess = false;
                response.Message = "Internal Server Error";
                return StatusCode(500, response);
            }
        }

        [HttpGet("AllSubcategories")]
        public IActionResult GetAllSubcategories()
        {
            var response = new ResponseDto();

            try
            {
                // Retrieve all subcategories from the database
                var subcategories = _db.Subcategories.ToList();

                var subcategoryDtos = subcategories.Select(s => new SubCategoryDto
                {
                    SubCategoryID = s.SubCategoryID,
                    SubCategoryName = s.SubCategoryName,
                    CategoryID = s.CategoryID
                }).ToList();

                response.Result = subcategoryDtos;
                response.Message = "Subcategories retrieved successfully";

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                response.IsSuccess = false;
                response.Message = "Internal Server Error";
                return StatusCode(500, response);
            }
        }

        [HttpDelete("DeleteSubcategory")]
        public IActionResult DeleteSubcategory(int id)
        {
            var response = new ResponseDto();

            try
            {
                // Find the subcategory by ID
                var subcategory = _db.Subcategories.Find(id);

                if (subcategory == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Subcategory not found";
                    return NotFound(response);
                }

                // Remove from the database
                _db.Subcategories.Remove(subcategory);
                _db.SaveChanges();

                response.Message = "Subcategory deleted successfully";

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                response.IsSuccess = false;
                response.Message = "Internal Server Error";
                return StatusCode(500, response);
            }
        }
    }

}
