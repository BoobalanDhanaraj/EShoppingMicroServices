using Microsoft.EntityFrameworkCore;
using ShoppingCustomerApi.Model;
using System;

namespace ShoppingCustomerApi.Data {
    public class CusomerDbContext : DbContext
    {
        public CusomerDbContext(DbContextOptions<CusomerDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Addresses> Addresses { get; set; }
    }
}

