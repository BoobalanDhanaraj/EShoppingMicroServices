﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShoppingProductApi.Data;

#nullable disable

namespace ShoppingProductApi.Migrations
{
    [DbContext(typeof(ProductDbContext))]
    partial class ProductDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ShoppingProductApi.Model.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryID = 1,
                            CategoryName = "Electronics"
                        });
                });

            modelBuilder.Entity("ShoppingProductApi.Model.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductID"));

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SellerID")
                        .HasColumnType("int");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("int");

                    b.HasKey("ProductID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("SellerID");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductID = 1,
                            CategoryID = 1,
                            Name = "Realme 11 Pro",
                            Price = 0m,
                            SellerID = 1,
                            StockQuantity = 25
                        });
                });

            modelBuilder.Entity("ShoppingProductApi.Model.ProductImages", b =>
                {
                    b.Property<int>("ImageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImageID"));

                    b.Property<string>("ImageURLs")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.HasKey("ImageID");

                    b.HasIndex("ProductID");

                    b.ToTable("ProductImages");

                    b.HasData(
                        new
                        {
                            ImageID = 1,
                            ImageURLs = "https://i.gadgets360cdn.com/products/large/realme-11-pro-realme-db-1-661x800-1683725640.jpg",
                            ProductID = 1
                        });
                });

            modelBuilder.Entity("ShoppingProductApi.Model.Seller", b =>
                {
                    b.Property<int>("SellerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SellerID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SellerID");

                    b.ToTable("Sellers");

                    b.HasData(
                        new
                        {
                            SellerID = 1,
                            Email = "murali@gmail.com",
                            PhoneNumber = "9786542875",
                            SellerName = "Muralidharan"
                        });
                });

            modelBuilder.Entity("ShoppingProductApi.Model.Subcategory", b =>
                {
                    b.Property<int>("SubCategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubCategoryID"));

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<string>("SubCategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubCategoryID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Subcategories");

                    b.HasData(
                        new
                        {
                            SubCategoryID = 1,
                            CategoryID = 1,
                            SubCategoryName = "Mobile"
                        });
                });

            modelBuilder.Entity("ShoppingProductApi.Model.Product", b =>
                {
                    b.HasOne("ShoppingProductApi.Model.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShoppingProductApi.Model.Seller", "Seller")
                        .WithMany("Products")
                        .HasForeignKey("SellerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("ShoppingProductApi.Model.ProductImages", b =>
                {
                    b.HasOne("ShoppingProductApi.Model.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ShoppingProductApi.Model.Subcategory", b =>
                {
                    b.HasOne("ShoppingProductApi.Model.Category", "Category")
                        .WithMany("Subcategories")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ShoppingProductApi.Model.Category", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("Subcategories");
                });

            modelBuilder.Entity("ShoppingProductApi.Model.Seller", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
