using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Orders;
using AdobeMPClient.Models.Orders.Request;
using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.API.Endpoints.Orders;

public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/customers/{customerId}/orders", async (
                [FromRoute] string customerId,
                [FromBody] CreateOrder createOrder,
                IAdobeClient client,
                ILogger<Create> logger,
                CancellationToken ct = default) =>
            {
                logger.LogInformation("Creating order for customer {CustomerId}", customerId);

                var result = await client.CreateOrderAsync(customerId, createOrder, ct);

                logger.LogInformation("Order created successfully for customer {CustomerId}", customerId);
                return result.ToResult();
            })
            .WithName("CreateOrder")
            .WithTags("orders")
            .WithSummary("Create a new order")
            .WithDescription("Creates a new order for a customer")
            .Accepts<CreateOrder>("application/json")
            .Produces<Order>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}