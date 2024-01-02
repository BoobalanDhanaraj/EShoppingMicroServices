using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using ShoppingProductApi.Model;
using System;

namespace ShoppingProductApi.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext()
        {

        }
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Ms_Customer;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
            }
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductID= 1,
                Name="Realme 11 Pro",
                StockQuantity=25,
                SellerID=1,
                CategoryID=1,
            });
            modelBuilder.Entity<Seller>().HasData(new Seller
            {
                SellerID= 1,
                SellerName="Muralidharan",
                Email="murali@gmail.com",
                PhoneNumber="9786542875"
            });
            modelBuilder.Entity<ProductImages>().HasData(new ProductImages
            {
                ImageID= 1,
                ProductID= 1,
                ImageURLs= "https://i.gadgets360cdn.com/products/large/realme-11-pro-realme-db-1-661x800-1683725640.jpg",
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryID= 1,
                CategoryName="Electronics",
            });
            modelBuilder.Entity<Subcategory>().HasData(new Subcategory
            {
                SubCategoryID = 1,
                SubCategoryName="Mobile",
                CategoryID = 1,
            });
        }
    }
}

