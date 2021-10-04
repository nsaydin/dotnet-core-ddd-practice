using Core.Domain.Campaign;
using Core.Domain.Order;
using Core.Domain.Product;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
    }
}