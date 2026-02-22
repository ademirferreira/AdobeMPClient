using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.API.Endpoints.Subscriptions;

public class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/customers/{customerId}/subscriptions", async (
            [FromRoute] string customerId,
            IAdobeClient client,
            ILogger<GetAll> logger,
            CancellationToken ct = default) =>
        {
            logger.LogInformation("Getting subscriptions for customer {CustomerId}", customerId);

            var result = await client.GetSubscriptionsAsync(customerId, ct);

            logger.LogInformation("Subscriptions retrieved for customer {CustomerId}", customerId);
            return result.ToResult();
        })
        .WithName("GetAllSubscriptions")
        .WithTags("subscriptions")
        .WithSummary("Get all customer subscriptions")
        .WithDescription("Retrieves all subscriptions for a customer")
        .Produces<AdobeMPClient.Models.Subscriptions.Subscriptions>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}