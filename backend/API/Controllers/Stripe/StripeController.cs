using Domain.Models.Stripe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MySqlX.XDevAPI;
using Stripe;
using Stripe.Checkout;

namespace API.Controllers.Stripe
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController : ControllerBase
    {
        public string SessionId { get; set; }

        [HttpPost("create-checkout-session")]
        [AllowAnonymous]
        public ActionResult CreateCheckoutSession([FromBody] CheckoutSessionRequest request)
        {
            var domain = "http://localhost:5173";
            var options = new SessionCreateOptions
            {
                LineItems = request.LineItems.Select(item => new SessionLineItemOptions
                {
                    Price = item.PriceId, // Use the Price ID passed from the frontend
                    Quantity = item.Quantity
                }).ToList(),
                Mode = request.Mode, // "subscription"
                SuccessUrl = $"{domain}/success?session_id={{CHECKOUT_SESSION_ID}}",
                CancelUrl = $"{domain}",
            };
            var service = new SessionService();
            var session = service.Create(options);

            return new JsonResult(new { sessionId = session.Id, url = session.Url });
        }

        // Add this method to your StripeController
        [HttpGet("session/{sessionId}")]
        [AllowAnonymous]
        public ActionResult GetSessionDetails(string sessionId)
        {
            try
            {
                var service = new SessionService();
                var session = service.Get(sessionId);

                if (session == null)
                {
                    return NotFound("Session not found");
                }

                // Fetch line items for the session using the Session ID
                var lineItemService = new SessionLineItemService();
                var lineItemOptions = new SessionLineItemListOptions
                {
                    Limit = 10 // Adjust the limit as needed
                };
                var lineItems = lineItemService.List(sessionId, lineItemOptions);

                // Create a response object that includes the session details and line items
                var response = new
                {
                    customerdetails = session,
                    items = lineItems.Data.Select(item => new
                    {
                        item.Description,
                        item.Quantity,
                        item.AmountSubtotal,
                        item.AmountTotal
                    }).ToList()
                };

                // Return the combined data as JSON
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving session details: " + ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

    }
}