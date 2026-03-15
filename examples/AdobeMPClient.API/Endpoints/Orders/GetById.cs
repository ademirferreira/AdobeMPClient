using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.API.Endpoints.Orders;

public class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/customers/{customerId}/orders/{orderId}", async (
            [FromRoute] string customerId,
            [FromRoute] string orderId,
            [FromQuery(Name = "fetch-price")] bool? fetchPrice,
            IAdobeClient client,
            ILogger<GetById> logger,
            CancellationToken ct = default) =>
        {
            logger.LogInformation("Getting order {OrderId} for customer {CustomerId}", orderId, customerId);

            var result = await client.GetOrderByIdAsync(customerId, orderId, fetchPrice, ct);

            logger.LogInformation("order {OrderId} retrieved for customer {CustomerId}", orderId, customerId);
            return result.ToResult();
        })
        .WithName("GetOrderById")
        .WithTags("orders")
        .WithSummary("Get order by ID")
        .WithDescription("Retrieves a specific order by customer ID and order ID")
        .Produces<AdobeMPClient.Models.Orders.Order>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}
