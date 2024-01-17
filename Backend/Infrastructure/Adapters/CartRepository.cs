using Core.Domain.Entity;
using Infrastructure.DbContext;
using Infrastructure.Ports;
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
        var cart = await _context.Carts.FirstOrDefaultAsync(p => p.SessionId == sessionId && p.IsPayd == false);
        if (cart == null)
        {
            cart = new Cart { SessionId = sessionId, IsPayd = false};
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        return cart;
    }

    public async Task<Cart> GetOrCreateCartForUserAsync(string userId)
    {
        var cart = await _context.Carts.FirstOrDefaultAsync(p => p.UserId == userId && p.IsPayd == false);
        if (cart == null)
        {
            cart = new Cart { UserId = userId, IsPayd = false };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        return cart;
    }
    

    public async Task AddProductToCartAsync(Guid cartId, Guid productId, int quantity)
    {
    var cart = await _context.Carts.Include(p => p.ProductCarts).FirstOrDefaultAsync(p => p.Id == cartId);
    if (cart == null || cart.IsPayd)
    {
        if (cart.UserId != null)
        {
            var newCart = new Cart
            {
                UserId = cart.UserId,
                IsPayd = false,
            };
            _context.Carts.Add(newCart);
            await _context.SaveChangesAsync();

            cartId = newCart.Id;
        }
        else
        {
            var newCart = new Cart
            {
                SessionId = cart.SessionId,
                IsPayd = false,
            };
            

            _context.Carts.Add(newCart);
            await _context.SaveChangesAsync();

            cartId = newCart.Id;
        }
    }
    var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
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
        return await _context.Carts.Include(p => p.ProductCarts).FirstOrDefaultAsync(p => p.SessionId == sessionId && p.IsPayd == false);
    }

    public async Task<Cart> GetCartByUserIdAsync(string userId)
    {
        return await _context.Carts.Include(p => p.ProductCarts).FirstOrDefaultAsync(p => p.UserId == userId && p.IsPayd == false);
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

    public async Task<List<Cart>> GetCartPaidByUserIdAsync(string userId)
    {
        return await _context.Carts.Include(p => p.ProductCarts).ThenInclude(pc => pc.Product).Where(p => p.UserId == userId && p.IsPayd == true).ToListAsync();
    }

    public async Task<List<Cart>> GetCartPaidBySessionIdAsync(string sessionId)
    {
        return await _context.Carts.Include(p => p.ProductCarts).ThenInclude(pc => pc.Product).Where(p => p.SessionId == sessionId && p.IsPayd == true).ToListAsync();
    }

    public async Task<bool> UpdateCartStatus(Guid cartId)
    {
        var cart = await _context.Carts.FindAsync(cartId);
        if (cart == null)
        {
            return false;
        }

        cart.IsPayd = true;
        _context.Carts.Update(cart);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<Guid?> GetUnpaidCartIdByUserIdOrSessionIdAsync(string userId)
    {
        Cart cart = null;

        if (!string.IsNullOrEmpty(userId))
        {
            cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId && !c.IsPayd);
        }

        return cart?.Id;
    }
    
    public async Task<Guid> GetCartIdByUserIdAsync(string userId)
    {
        var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
        return cart?.Id ?? Guid.Empty;
    }
    
    public async Task<Guid> GetCartIdBySessionIdAsync(string session)
    {
        var cart = await _context.Carts.FirstOrDefaultAsync(c => c.SessionId.ToString() == session);
        return cart?.Id ?? Guid.Empty;
    }
}