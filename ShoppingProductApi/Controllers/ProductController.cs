using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingProductApi.Data;
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
                    .Include(p => p.Seller)
                    .Include(p=>p.ProductImages)
                                        .Select(p => new ProductDto
                                        {
                                            ProductID = p.ProductID,
                                            ProductImage=p.ProductImages.ImageURLs,
                                            Name = p.Name,
                                            Price = p.Price,
                                            StockQuantity = p.StockQuantity,
                                            CategoryName = p.Subcategory.Category.CategoryName,
                                            SubcategoryName = p.Subcategory.SubCategoryName,
                                            SellerName = p.Seller.SellerName
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
    }
}
