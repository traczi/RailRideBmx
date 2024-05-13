﻿using Core.Domain.Entity;
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
    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<Color> Colors { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Brand> Brands { get; set; } = null;



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<User>().HasKey(u => u.Id);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .IsRequired();

            entity.HasOne(p => p.Color)
                .WithMany()
                .HasForeignKey(p => p.ColorId)
                .IsRequired();

            entity.HasOne(p => p.Brand)
                .WithMany()
                .HasForeignKey(p => p.BrandId)
                .IsRequired();
        });
        
        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasKey(c => c.ColorId);
        });
        
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.CategoryId);
        });
        
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(b => b.BrandId);
        });
        
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(c => c.Id);
            
            entity.Property(p => p.UserId).IsRequired(false);
            
            entity.Property(p => p.SessionId).IsRequired(false);
            
            entity.HasMany(c => c.ProductCarts)
                .WithOne(pc => pc.Cart)
                .HasForeignKey(pc => pc.CartId);
            
            entity.HasOne(c => c.Address)
                .WithOne(a => a.Cart)
                .HasForeignKey<Address>(a => a.CartId);
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
                .WithMany(p => p.Comment)
                .HasForeignKey(c => c.ProductId)
                .IsRequired();
            
            entity.HasIndex(c => new { c.UserId, c.ProductId })
                .IsUnique();
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