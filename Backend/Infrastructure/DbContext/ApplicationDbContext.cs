using Core.Domain.Entity;
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
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<Like> Likes { get; set; } = null!;
    public DbSet<ConfigurationBMX> ConfigurationsBMX { get; set; } = null!;



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasKey(p => p.Id);
        
        modelBuilder.Entity<User>().HasKey(u => u.Id);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(c => c.Id);
            
            entity.Property(p => p.UserId).IsRequired(false);
            
            entity.Property(p => p.SessionId).IsRequired(false);
            
            entity.HasMany(c => c.ProductCarts)
                .WithOne(pc => pc.Cart)
                .HasForeignKey(pc => pc.CartId);
        });
        
        modelBuilder.Entity<ProductCart>(entity =>
        {
            entity.HasKey(pc => pc.ProductCartId);
            entity.HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCarts)
                .HasForeignKey(pc => pc.ProductId);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(c => c.Id);
            
            entity.HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .IsRequired();
            
            entity.HasOne(c => c.Product)
                .WithMany()
                .HasForeignKey(c => c.ProductId)
                .IsRequired();
        });

        modelBuilder.Entity<Like>(entity =>
        {
            entity.HasKey(l => l.Id);

            entity.HasOne(l => l.User)
                .WithMany(u => u.Like)
                .HasForeignKey(l => l.UserId);
            
            entity.HasOne(l => l.Product)
                .WithMany(p => p.Like)
                .HasForeignKey(l => l.ProductId);
        });

        modelBuilder.Entity<ConfigurationBMX>(entity =>
        {
            entity.HasKey(c => c.ConfigurationId);

            entity.HasOne(c => c.User)
                .WithMany(u => u.Configurations)
                .HasForeignKey(c => c.UserId);
        });
    }
}