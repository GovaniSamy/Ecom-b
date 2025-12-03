using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace Ecom.PL.Controllers
{
    [ApiController]
    [Route("api/stripe/webhook")]
    public class StripeWebhookController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly string _secret;

        public StripeWebhookController(IPaymentService paymentService, IConfiguration config)
        {
            _paymentService = paymentService;
            _secret = config["Stripe:WebhookSecret"];
        }

        [HttpPost]
        public async Task<IActionResult> Handle()
        {
            var json = await new StreamReader(Request.Body).ReadToEndAsync();

            Event stripeEvent;
            try
            {
                stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    _secret
                );
            }
            catch
            {
                return BadRequest(new { message = "Invalid Stripe signature" });
            }

            if (stripeEvent.Type == "checkout.session.completed")
            {
                var session = stripeEvent.Data.Object as Session;

                // 🔹 Safety check
                if (string.IsNullOrEmpty(session.ClientReferenceId))
                    return BadRequest(new { message = "Missing order reference" });

                if (!int.TryParse(session.ClientReferenceId, out int orderId))
                    return BadRequest(new { message = "Invalid order ID" });

                string paymentIntentId = session.PaymentIntentId;

                // 🔹 Business logic
                var result = await _paymentService.MarkPaymentPaidAsync(orderId, paymentIntentId);

                if (!result.IsSuccess)
                    return BadRequest(new { message = result.ErrorMessage });
            }

            // Stripe requires a 200 OK to stop retries
            return Ok(new { message = "Webhook processed" });
        }

    }
}
