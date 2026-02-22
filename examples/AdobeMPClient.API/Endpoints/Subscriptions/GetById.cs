using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.API.Endpoints.Subscriptions;

public class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/customers/{customerId}/subscriptions/{subscriptionId}", async (
            [FromRoute] string customerId,
            [FromRoute] string subscriptionId,
            IAdobeClient client,
            ILogger<GetById> logger,
            CancellationToken ct = default) =>
        {
            logger.LogInformation("Getting subscription {SubscriptionId} for customer {CustomerId}", subscriptionId, customerId);

            var result = await client.GetSubscriptionByIdAsync(customerId, subscriptionId, ct);

            logger.LogInformation("Subscription {SubscriptionId} retrieved for customer {CustomerId}", subscriptionId, customerId);
            return result.ToResult();
        })
        .WithName("GetSubscriptionById")
        .WithTags("subscriptions")
        .WithSummary("Get subscription by ID")
        .WithDescription("Retrieves a specific subscription by customer ID and subscription ID")
        .Produces<AdobeMPClient.Models.Subscriptions.Subscription>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}
