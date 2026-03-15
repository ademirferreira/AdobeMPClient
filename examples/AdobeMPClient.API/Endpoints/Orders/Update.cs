using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Orders;
using AdobeMPClient.Models.Orders.Request;
using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.API.Endpoints.Orders;

public class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/api/customers/{customerId}/orders/{orderId}", async (
                [FromRoute] string customerId,
                [FromRoute] string orderId,
                [FromBody] UpdateOrder externalReferenceId,
                IAdobeClient client,
                ILogger<Create> logger,
                CancellationToken ct = default) =>
        {
            logger.LogInformation("Updating order for customer {CustomerId}", customerId);

            var result = await client.UpdateOrderAsync(customerId, orderId, externalReferenceId, ct);

            logger.LogInformation("Order Updated successfully for customer {CustomerId}", customerId);
            return result.ToResult();
        })
            .WithName("UpdateOrder")
            .WithTags("orders")
            .WithSummary("Update an order")
            .WithDescription("Updates an order for a customer")
            .Accepts<string>("application/json")
            .Produces<Order>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}
