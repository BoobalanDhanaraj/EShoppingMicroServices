using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingProductApi.Data;
using ShoppingProductApi.Model;
using ShoppingProductApi.Model.dto;

namespace ShoppingProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ProductDbContext _db;
        private IMapper _mapper;

        public ProductController(ProductDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet("ProductList")]
        public IActionResult GetAllProducts()
        {
            var response = new ResponseDto();
            try
            {
                var products = _db.Products
                    .Include(p => p.Subcategory)
                    .ThenInclude(s => s.Category) // Include the category within subcategory
                    .Include(p => p.Seller)
                    .Include(p => p.ProductImages)
                    .Select(p => new ProductDto
                    {
                        ProductID = p.ProductID,
                        ProductImages = p.ProductImages.Select(pi => pi.ImageURLs).ToList(), 
                        Name = p.Name,
                        Price = p.Price,
                        StockQuantity = p.StockQuantity,
                        Category = p.Subcategory.Category.CategoryName,
                        Subcategory = p.Subcategory.SubCategoryName,
                        Seller = p.Seller.SellerName
                    })
                    .ToList();

                    response.Result = products;
                    response.Message = "Products retrieved successfully";
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

        [HttpPost("AddProduct")]
        public IActionResult AddProduct([FromBody] AddProductDto newProductDto)
        {
            var response = new ResponseDto();

            try
            {
                // Validate required properties
                if (newProductDto == null ||
                    string.IsNullOrEmpty(newProductDto.Name) ||
                    newProductDto.Price <= 0 ||
                    newProductDto.StockQuantity < 0 ||
                    string.IsNullOrEmpty(newProductDto.SubcategoryId) ||
                    string.IsNullOrEmpty(newProductDto.SellerId))
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid or missing input. Please provide valid data for all required fields.";
                    return BadRequest(response);
                }

                // Map the DTO to the Product entity
                var newProduct = new Product
                {
                    Name = newProductDto.Name,
                    Price = newProductDto.Price,
                    StockQuantity = newProductDto.StockQuantity,
                    SubcategoryID = int.Parse(newProductDto.SubcategoryId),
                    SellerID = int.Parse(newProductDto.SellerId),
                    // Other properties...
                };

                // Add the new product to the database
                _db.Products.Add(newProduct);
                _db.SaveChanges();

                // Add image URLs to the ProductImages table
                if (newProductDto.ProductImageUrl != null && newProductDto.ProductImageUrl.Any())
                {
                    var productImages = newProductDto.ProductImageUrl.Select(url => new ProductImages
                    {
                        ProductID = newProduct.ProductID,
                        ImageURLs = url.ToString()
                    }).ToList();

                    _db.ProductImages.AddRange(productImages);
                    _db.SaveChanges();
                }

                response.Result = newProduct;
                response.Message = "Product added successfully";
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                response.IsSuccess = false;
                response.Message = "Failed to add product";
            }

            return Ok(response);
        }

        [HttpPut("EditProduct")]
        public IActionResult EditProduct(int productId, [FromBody] EditProductDto editProductDto)
        {
            var response = new ResponseDto();

            try
            {
                // Find the product by ID
                var existingProduct = _db.Products.Find(productId);

                if (existingProduct == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Product not found";
                    return NotFound(response);
                }

                // Update the product properties
                existingProduct.Name = editProductDto.Name ?? existingProduct.Name;
                existingProduct.Price = editProductDto.Price > 0 ? editProductDto.Price : existingProduct.Price;
                existingProduct.StockQuantity = editProductDto.StockQuantity >= 0 ? editProductDto.StockQuantity : existingProduct.StockQuantity;

                // Save changes to the database
                _db.SaveChanges();

                response.Result = existingProduct;
                response.Message = "Product updated successfully";
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                response.IsSuccess = false;
                response.Message = "Failed to update product";
            }

            return Ok(response);
        }

        [HttpGet("GetProductDetails")]
        public IActionResult GetProductDetails(int productId)
        {
            var response = new ResponseDto();

            try
            {
                // Find the product by ID
                var product = _db.Products
                                    .Include(p => p.Subcategory)
                                    .ThenInclude(s => s.Category)
                                    .Include(p => p.Seller)
                                    .Include(p => p.ProductImages)
                                    .FirstOrDefault(p => p.ProductID == productId);

                if (product == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Product not found";
                    return NotFound(response);
                }

                // Map the product entity to a DTO if needed
                var productDto = new ProductDto
                {
                    ProductID = product.ProductID,
                    ProductImages = product.ProductImages?.Select(pi => pi.ImageURLs).ToList(),
                    Name = product.Name,
                    Price = product.Price,
                    StockQuantity = product.StockQuantity,
                    Category = product.Subcategory?.Category?.CategoryName,
                    Subcategory = product.Subcategory?.SubCategoryName,
                    Seller = product.Seller?.SellerName
                };



                response.Result = productDto;
                response.Message = "Product details retrieved successfully";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
                // Log the exception or handle it appropriately
                response.IsSuccess = false;
                response.Message = "Failed to retrieve product details";
            }

            return Ok(response);
        }

        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct(int productId)
        {
            var response = new ResponseDto();

            try
            {
                // Find the product by ID
                var product = _db.Products.Find(productId);

                // Check if the product exists
                if (product == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Product not found";
                    return NotFound(response); // Return 404 Not Found with responseDto
                }

                // Remove the product from the database
                _db.Products.Remove(product);
                _db.SaveChanges();

                response.Message = "Product deleted successfully";
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
