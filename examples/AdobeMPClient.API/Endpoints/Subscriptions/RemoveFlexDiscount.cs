using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Common;
using AdobeMPClient.Models.Subscriptions.Request;
using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.API.Endpoints.Subscriptions
{
    public class RemoveFlexDiscount : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPatch("/api/customers/{customerId}/subscriptions/{subscriptionId}/reset-flex-discount", async (
            [FromRoute] string customerId,
            [FromRoute] string subscriptionId,
            IAdobeClient client,
            ILogger<Update> logger,
            CancellationToken ct = default) =>
            {
                logger.LogInformation("Removing flex discount from subscription {SubscriptionId} for customer {CustomerId}", subscriptionId, customerId);

                var result = await client.RemoveFlexDiscountAsync(customerId, subscriptionId, ct);

                logger.LogInformation("Flex discount removed from subscription {SubscriptionId} for customer {CustomerId}", subscriptionId, customerId);
                return result.ToResult();
            })
            .WithName("RemoveFlexDiscount")
            .WithTags("subscriptions")
            .WithSummary("Remove flex discount")
            .WithDescription("Updates an existing flex discount code for a subscription")
            .Accepts<UpdateSubscription>("application/json")
            .Produces<AdobeMPClient.Models.Subscriptions.Subscription>(StatusCodes.Status200OK)
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .Produces<Error>(StatusCodes.Status404NotFound)
            .Produces<Error>(StatusCodes.Status500InternalServerError);
        }
    }
}
