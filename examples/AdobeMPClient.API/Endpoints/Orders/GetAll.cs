using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Orders.Request;
using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.API.Endpoints.Orders;

public class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/customers/{customerId}/orders", async (
            [FromRoute] string customerId,
            [AsParameters] GetOrderHistoryRequest parameters,
            IAdobeClient client,
            ILogger<GetAll> logger,
            CancellationToken ct = default) =>
        {
            logger.LogInformation("Getting subscriptions for customer {CustomerId}", customerId);

            var result = await client.GetOrderHistory(customerId, parameters, ct);

            logger.LogInformation("Subscriptions retrieved for customer {CustomerId}", customerId);
            return result.ToResult();
        })
        .WithName("GetOrderHistory")
        .WithTags("orders")
        .WithSummary("Get all customer orders")
        .WithDescription("Retrieves all orders for a customer")
        .Produces<AdobeMPClient.Models.Orders.OrderHistory>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}
