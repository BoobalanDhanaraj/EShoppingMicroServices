using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using ShoppingCustomerApi.Model;
using System;

namespace ShoppingCustomerApi.Data {
    public class CusomerDbContext : DbContext
    {
        public CusomerDbContext()
        {
            
        }
        public CusomerDbContext(DbContextOptions<CusomerDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Ms_Customer;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
            }
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Addresses> Addresses { get; set; }
    }
}

