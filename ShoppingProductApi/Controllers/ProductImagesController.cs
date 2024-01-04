using Microsoft.AspNetCore.Mvc;
using ShoppingProductApi.Data;
using ShoppingProductApi.Model;
using ShoppingProductApi.Model.dto;

namespace ShoppingProductApi.Controllers
{
    [ApiController]
    [Route("api/productimages")]
    public class ProductImagesController : Controller
    {
        private readonly ProductDbContext _db;
        public ProductImagesController(ProductDbContext db)
        {
            _db = db;
        }
        [HttpGet("GetImage")]
        public IActionResult GetProductImages(int productId)
        {
            var response = new ResponseDto();

            try
            {
                // Get product images by productId
                var productImages = _db.ProductImages
                    .Where(pi => pi.ProductID == productId)
                    .Select(pi => pi.ImageURLs)
                    .ToList();

                response.Result = productImages;
                response.Message = "Product images retrieved successfully";
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

        [HttpPost("AddImage")]
        public IActionResult AddProductImages(int productId, [FromBody] List<string> imageUrls)
        {
            var response = new ResponseDto();

            try
            {
                // Check if the product exists
                var product = _db.Products.Find(productId);

                if (product == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Product not found";
                    return NotFound(response); // Return 404 Not Found with responseDto
                }

                // Create ProductImages entities and add them to the database
                var productImages = imageUrls.Select(url => new ProductImages
                {
                    ProductID = productId,
                    ImageURLs = url
                }).ToList();

                _db.ProductImages.AddRange(productImages);
                _db.SaveChanges();

                response.Message = "Product images added successfully";
                return Ok(response); // Return 200 OK with responseDto
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                response.IsSuccess = false;
                response.Message = "Internal Server Error";
                return StatusCode(500, response); // Return 500 Internal Server Error with responseDto
            }
        }

        [HttpDelete("DeleteImage")]
        public IActionResult DeleteProductImage(int imageId)
        {
            var response = new ResponseDto();

            try
            {
                // Check if the image exists
                var productImage = _db.ProductImages.Find(imageId);

                if (productImage == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Product image not found";
                    return NotFound(response); // Return 404 Not Found with responseDto
                }

                // Remove the image from the database
                _db.ProductImages.Remove(productImage);
                _db.SaveChanges();

                response.Message = "Product image deleted successfully";
                return Ok(response); // Return 200 OK with responseDto
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                response.IsSuccess = false;
                response.Message = "Internal Server Error";
                return StatusCode(500, response); // Return 500 Internal Server Error with responseDto
            }
        }

    }
}
