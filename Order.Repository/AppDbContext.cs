using Microsoft.EntityFrameworkCore;

namespace Order.Repository
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Order> Orders { get; set; }
    }
}