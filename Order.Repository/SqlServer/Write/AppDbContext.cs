using Microsoft.EntityFrameworkCore;
using Order.Domain.Write;

namespace Repository.Write;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Order.Domain.Write.Order> Orders { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Product> Products { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().Property(x => x.Id).ValueGeneratedNever();


        base.OnModelCreating(modelBuilder);
    }
}