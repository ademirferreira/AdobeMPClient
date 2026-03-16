using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Subscriptions;
using AdobeMPClient.Models.Subscriptions.Request;
using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.API.Endpoints.Subscriptions;

public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/customers/{customerId}/subscriptions", async (
            [FromRoute] string customerId,
            [FromBody] CreateSubscription createSubscription,
            IAdobeClient client,
            ILogger<Create> logger,
            CancellationToken ct = default) =>
        {
            logger.LogInformation("Creating subscription for customer {CustomerId}", customerId);

            var result = await client.CreateSubscriptionAsync(customerId, createSubscription, ct);

            return result
                .OnSuccess(data => logger.LogInformation("Subscription created successfully for customer {CustomerId}", customerId))
                .OnFailure(error => logger.LogError("Failed to create subscription for customer {CustomerId}. Error: {ErrorMessage}", customerId, error?.Message))
                .ToResult();
        })
            .WithName("CreateSubscription")
            .WithTags("subscriptions")
            .WithSummary("Create a new subscription")
            .WithDescription("Creates a new subscription for a customer")
            .Accepts<CreateSubscription>("application/json")
            .Produces<Subscription>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}
