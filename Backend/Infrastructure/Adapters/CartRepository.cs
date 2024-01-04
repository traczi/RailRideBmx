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

    public async Task<Cart> GetOrCreateCartForSessionAsync(string sessionId)
    {
        var cart = await _context.Carts.FirstOrDefaultAsync(p => p.SessionId == sessionId);
        if (cart == null)
        {
            cart = new Cart { SessionId = sessionId };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        return cart;
    }

    public async Task<Cart> GetOrCreateCartForUserAsync(string userId)
    {
        var cart = await _context.Carts.FirstOrDefaultAsync(p => p.UserId == userId);
        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        return cart;
    }
    

    public async Task AddProductToCartAsync(Guid cartId, Guid productId, int quantity)
    {
        var cart = await _context.Carts.Include(p => p.ProductCarts).FirstOrDefaultAsync(p => p.Id == cartId);
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        if (cart != null)
        {
            var productCart =
                await _context.ProductCarts.FirstOrDefaultAsync(pc =>
                    pc.CartId == cartId && pc.ProductId == productId);

            int maxQuantity = product.Quantity;
            if (productCart != null)
            {
                productCart.Quantity = Math.Min(productCart.Quantity + quantity, maxQuantity);
            }
            else
            {
                var newProductCart = new ProductCart
                {
                    CartId = cartId,
                    ProductId = productId,
                    Quantity = quantity
                };
                _context.ProductCarts.Add(newProductCart);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine( "L'erreur est =" + e.InnerException.Message);
            }
        }
    }

    public async Task TransferCartFromSessionToUserAsync(string sessionId, string userId)
    {
        var cart = await _context.Carts.FirstOrDefaultAsync(p => p.SessionId == sessionId);
        if (cart != null)
        {
            cart.UserId = userId;
            cart.SessionId = null;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Cart> GetCartBySessionIdAsync(string sessionId)
    {
        return await _context.Carts.Include(p => p.ProductCarts).FirstOrDefaultAsync(p => p.SessionId == sessionId);
    }

    public async Task<Cart> GetCartByUserIdAsync(string userId)
    {
        return await _context.Carts.Include(p => p.ProductCarts).FirstOrDefaultAsync(p => p.UserId == userId);
    }
    public async Task<List<Product>> GetProductInCartAsync(Guid cartId)
    {
        var productIds = _context.ProductCarts.Where(pc => pc.CartId == cartId).Select(pc => pc.ProductId).ToList();
        
        var products = await _context.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();
        return products;
    }
    public async Task UpdateProductQuantityAsync(Guid cartId, Guid productId, int newQuantity)
    {
        var productCart = await _context.ProductCarts.FirstOrDefaultAsync(pc => pc.CartId == cartId && pc.ProductId == productId);
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

        if (productCart != null && product != null)
        {
            productCart.Quantity = newQuantity > product.Quantity ? product.Quantity : newQuantity;
            await _context.SaveChangesAsync();
        }
        else
        {
        }
    }


}