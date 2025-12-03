using Ecom.BLL.ModelVM.Order;
using Stripe.Checkout;

public class StripeService : IStripeService
{
    public async Task<string> CreateCheckoutSessionAsync(GetOrderVM order)
    {
        var options = new SessionCreateOptions
        {
            Mode = "payment",

            // REQUIRED so webhook can find the order
            ClientReferenceId = order.Id.ToString(),

            SuccessUrl = $"http://localhost:4200/order/success/{order.Id}?session_id={{CHECKOUT_SESSION_ID}}",
            CancelUrl = "http://localhost:4200/order/cancel",

            LineItems = order.Items.Select(item => new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(item.UnitPrice * 100),
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.ProductTitle
                    }
                },
                Quantity = item.Quantity
            }).ToList()
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);

        return session.Url;
    }

}