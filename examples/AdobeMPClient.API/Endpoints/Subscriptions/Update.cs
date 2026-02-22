using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Subscriptions.Request;
using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.API.Endpoints.Subscriptions;

public class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/api/customers/{customerId}/subscriptions/{subscriptionId}", async (
            [FromRoute] string customerId,
            [FromRoute] string subscriptionId,
            [FromBody] UpdateSubscription updateSubscription,
            IAdobeClient client,
            ILogger<Update> logger,
            CancellationToken ct = default) =>
        {
            logger.LogInformation("Updating subscription {SubscriptionId} for customer {CustomerId}", subscriptionId, customerId);

            var result = await client.UpdateSubscriptionAsync(customerId, subscriptionId, updateSubscription, ct);

            logger.LogInformation("Subscription {SubscriptionId} updated for customer {CustomerId}", subscriptionId, customerId);
            return result.ToResult();
        })
        .WithName("UpdateSubscription")
        .WithTags("subscriptions")
        .WithSummary("Update a subscription")
        .WithDescription("Updates an existing subscription by customer ID and subscription ID")
        .Accepts<UpdateSubscription>("application/json")
        .Produces<AdobeMPClient.Models.Subscriptions.Subscription>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}