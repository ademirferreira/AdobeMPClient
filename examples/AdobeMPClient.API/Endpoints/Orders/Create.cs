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
                [FromQuery(Name = "fetch-price")] bool? fetchPrice,
                IAdobeClient client,
                ILogger<Create> logger,
                CancellationToken ct = default) =>
            {
                logger.LogInformation("Creating order for customer {CustomerId}", customerId);

                var result = await client.CreateOrderAsync(customerId, createOrder, fetchPrice, ct);

                return result
                    .OnSuccess(data => logger.LogInformation("Order created successfully for customer {CustomerId}", customerId))
                    .OnFailure(error => logger.LogError("Failed to create order for customer {CustomerId}. Error: {ErrorMessage}", customerId, error?.Message))
                    .ToResult();
            })
            .WithName("CreateOrder")
            .WithTags("orders")
            .WithSummary("Create a new order")
            .WithDescription("Creates a new order for a customer")
            .Accepts<CreateOrder>("application/json")
            .Produces<Order>(StatusCodes.Status202Accepted)
            .Produces<Order>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}