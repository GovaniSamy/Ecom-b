using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class StripeController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IStripeService _stripeService;

    public StripeController(IOrderService orderService, IStripeService stripeService)
    {
        _orderService = orderService;
        _stripeService = stripeService;
    }

    [HttpPost("create-session/{orderId}")]
    public async Task<IActionResult> CreateSession(int orderId)
    {
        // 1️⃣ Get the order from DB
        var orderResult = await _orderService.GetByIdAsync(orderId);

        if (!orderResult.IsSuccess || orderResult.Result == null)
            return NotFound("Order not found");

        // 2️⃣ Create Stripe session
        var sessionUrl = await _stripeService.CreateCheckoutSessionAsync(orderResult.Result);

        // 3️⃣ Return URL to Angular
        return Ok(new { url = sessionUrl });
    }
}
