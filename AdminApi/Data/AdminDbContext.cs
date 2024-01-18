using Microsoft.EntityFrameworkCore;
using AdminApi.Model;

namespace AdminApi.Data
{
    public class AdminDbContext : DbContext
    {
        public AdminDbContext()
        {

        }
        public AdminDbContext(DbContextOptions<AdminDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Ms_Customer;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
            }
        }

        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Admin>().HasData(new Admin
            {
                AdminID = 1,
                UserName = "Antony Xavier",
                Email = "admin1@gmail.com",
                Password = "abcd",
            });
        }
    }
}
