using Microsoft.AspNetCore.Mvc;
using ShoppingProductApi.Model.dto;
using ShoppingProductApi.Model;
using ShoppingProductApi.Data;

namespace ShoppingProductApi.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ProductDbContext _db;

        public CategoriesController(ProductDbContext db)
        {
            _db = db;
        }

        [HttpPost("AddCategory")]
        public IActionResult AddCategory([FromBody] CategoryDto categoryDto)
        {
            var response = new ResponseDto();

            try
            {
                // Create a new category entity
                var newCategory = new Category
                {
                    CategoryName = categoryDto.CategoryName
                };

                // Add the category to the database
                _db.Categories.Add(newCategory);
                _db.SaveChanges();

                response.Result = newCategory;
                response.Message = "Category added successfully";
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

        [HttpGet("AllCategories")]
        public IActionResult GetAllCategories()
        {
            var response = new ResponseDto();

            try
            {
                // Retrieve all categories from the database along with names of associated sub-categories
                var categoriesWithSubcategoryNames = _db.Categories
                    .Select(category => new
                    {
                        category.CategoryID,
                        category.CategoryName,
                        SubcategoryNames = category.Subcategories.Select(subcategory => subcategory.SubCategoryName).ToList()
                    })
                    .ToList();

                response.Result = categoriesWithSubcategoryNames;
                response.Message = "Categories retrieved successfully";
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


        [HttpDelete("DeleteCategory")]
        public IActionResult RemoveCategory(int categoryId)
        {
            var response = new ResponseDto();

            try
            {
                // Find the category by ID
                var category = _db.Categories.Find(categoryId);

                if (category == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Category not found";
                    return NotFound(response);
                }

                // Remove the category from the database
                _db.Categories.Remove(category);
                _db.SaveChanges();

                response.Message = "Category removed successfully";
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
