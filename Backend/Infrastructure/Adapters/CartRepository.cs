using Core.Domain.Entity;
using Core.Ports;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Adapters;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _context;

    public CartRepository(ApplicationDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<Cart?> GetCartByUserIdAsync(Guid userId)
    {
        return await _context.Carts
            .Include(p => p.ProductCarts)
            .ThenInclude(pc => pc.Product)
            .FirstOrDefaultAsync(p => p.UserId == userId);
    }
    
    public async Task AddProductToCartAsync(Guid userId, Guid productId, int quantity)
    {
        var cart = await GetCartByUserIdAsync(userId);
        if (cart == null)
        {
            cart = new Cart
            {
                UserId = userId,
                ProductCarts = new List<ProductCart>()
            };
            _context.Carts.Add(cart);
        }

        var existingProductCart = cart.ProductCarts.FirstOrDefault(pc => pc.ProductId == productId);
        if (existingProductCart != null)
        {
            existingProductCart.Quantity += quantity;
        }
        else
        {
            cart.ProductCarts.Add(new ProductCart
            {
                ProductId = productId,
                Quantity = quantity
            });
        }
        await _context.SaveChangesAsync();
    }
}