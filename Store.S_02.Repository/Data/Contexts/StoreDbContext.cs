using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Store.S_02.Core.Entities;
using Store.S_02.Core.Entities.Order;

namespace Store.S_02.Repository.Data.Contexts;

public class StoreDbContext(DbContextOptions<StoreDbContext> options) : DbContext(options)
{
    // public StoreDbContext(DbContextOptions<StoreDbContext> options):base(options)
    // {
    // }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Products> Products { get; set; }
    public DbSet<ProductBrand> Brands { get; set; }
    public DbSet<ProductType> Type { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<OrderItem> OrderItem { get; set; }
    public DbSet<DelivertyMethod> DelivertyMethod { get; set; }
}