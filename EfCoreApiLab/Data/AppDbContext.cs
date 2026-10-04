using EfCoreApiLab.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreApiLab.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
}