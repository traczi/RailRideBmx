using Core.Domain.DTOs;
using Core.Domain.Entity;
using Stripe;

namespace Application;

public interface IStripeService
{
    Task<PaymentIntent> CreatePaymentIntentAsync(List<ProductDto> productDto, string userId);
}