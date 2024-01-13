using System.Security.Claims;
using Application;
using Core.Domain.DTOs;
using Core.Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace RailRideBMXHexagonale.Controllers;

public class StripeController : ApiController
{
    private readonly IStripeService _stripeService;
    private readonly ICartService _cartService;
    const string endpointSecret = "whsec_bf85cfeee7b1e157291956a0f096d8c3ee3a0e3882fff74de064c03aa5d17057";
    private const string SessionName = "UserSessionId";

    public StripeController(IStripeService stripeService,ICartService cartService)
    {
        _stripeService = stripeService;
        _cartService = cartService;
    }

    [HttpPost("CreatePaymentIntent")]   
    public async Task<IActionResult> CreatePaymentIntent()
    {
        var cart = new List<ProductDto>();
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Console.WriteLine("=============" +userId);
        if (string.IsNullOrEmpty(userId))
        {
            HttpContext.Request.Cookies.TryGetValue(SessionName, out var sessionId);
            cart = await _cartService.GetProductInCartAsync(sessionId);
        }
        else
        {
            cart = await _cartService.GetProductInCartByUserAsync(userId);
        }
        var paymentIntent = await _stripeService.CreatePaymentIntentAsync(cart, userId);

        return Ok(new { clientSecret = paymentIntent.ClientSecret });
    }
    
    [HttpPost]
    [Route("Webhook")]
    public async Task<IActionResult> WebHook()
    {
        
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        try
        {
            var stripeEvent = EventUtility.ConstructEvent(json,
                Request.Headers["Stripe-Signature"], endpointSecret);

            if (stripeEvent.Type == Events.PaymentIntentSucceeded)
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                var cartId = Guid.Parse(paymentIntent.Metadata["cartId"]);
                Console.WriteLine("======================" + cartId);
                await _cartService.UpdateCartStatus(cartId);
                Console.WriteLine("PaymentIntent was successful!");
            }
            else
            {
                Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
            }

            return Ok();
        }
        catch (StripeException)
        {
            return BadRequest();
        }
    }
}