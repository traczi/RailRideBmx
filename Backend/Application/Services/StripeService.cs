using Core.Domain.DTOs;
using Core.Domain.Entity;
using Core.Ports;
using Stripe;

namespace Application.Services;

public class StripeService: IStripeService
{
    
    private readonly ICartRepository _cartRepository;

    public StripeService(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;

    }
    public async Task<PaymentIntent> CreatePaymentIntentAsync(List<ProductDto> productDto, string userId)
    {
        var cartId = await _cartRepository.GetUnpaidCartIdByUserIdOrSessionIdAsync(userId);
        var paymentIntentService = new PaymentIntentService();
        var paymentIntent = await paymentIntentService.CreateAsync(new PaymentIntentCreateOptions
        {
            Amount = CalculateTotalAmount(productDto),
            Currency = "eur",
            Metadata = new Dictionary<string, string>
            {
                {"cartId", cartId.ToString() }
            }
        });

        return paymentIntent;
    }

    private long CalculateTotalAmount(List<ProductDto> productDto)
    {
        float total = 0;
        foreach (var product in productDto)
        {
            total += product.Price * product.CartQuantity;
        }

        long finalPrice = (long)(total * 100);
        Console.WriteLine("============================="+finalPrice);
        return finalPrice;
    }
}