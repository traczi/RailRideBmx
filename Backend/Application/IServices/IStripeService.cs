using Core.DTOs;
using Stripe;

namespace Application.IServices;

public interface IStripeService
{
    Task<PaymentIntent> CreatePaymentIntentAsync(List<ProductDto> productDto, string userId);
}