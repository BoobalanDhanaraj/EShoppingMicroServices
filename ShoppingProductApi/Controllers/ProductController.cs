﻿using AutoMapper;
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
                        ProductImages = p.ProductImages.Select(pi => pi.ImageURLs).ToList(), // Adjust here
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

        [HttpPost]
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


    }
}
