using Core.Domain.Entity;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContext;

public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Cart> Carts { get; set; } = null!;
    public DbSet<ProductCart> ProductCarts { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasKey(p => p.Id);
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<Cart>().HasKey(c => c.Id);
        

        modelBuilder.Entity<Cart>()
            .HasMany(c => c.ProductCarts)
            .WithOne(pc => pc.Cart)
            .HasForeignKey(pc => pc.CartId);

        modelBuilder.Entity<Product>()
            .HasMany(p => p.ProductCarts)
            .WithOne(pc => pc.Product)
            .HasForeignKey(pc => pc.ProductId);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}