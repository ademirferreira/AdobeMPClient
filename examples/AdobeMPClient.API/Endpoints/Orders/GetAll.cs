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
            [AsParameters] OrderHistoryQuery query,
            IAdobeClient client,
            ILogger<GetAll> logger,
            CancellationToken ct = default) =>
        {
            logger.LogInformation("Getting orders for customer {CustomerId}", customerId);

            var parameters = new GetOrderHistoryRequest
            {
                OrderType = query.OrderType,
                ResellerId = query.ResellerId,
                Status = query.Status,
                ReferenceOrderId = query.ReferenceOrderId,
                OfferId = query.OfferId,
                StartDate = query.StartDate,
                EndDate = query.EndDate,
                Limit = query.Limit,
                OffSet = query.OffSet,
                FetchPrice = query.FetchPrice
            };

            var result = await client.GetOrderHistoryAsync(customerId, parameters, ct);

            logger.LogInformation("Orders retrieved for customer {CustomerId}", customerId);
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

internal sealed record OrderHistoryQuery
{
    [FromQuery(Name = "order-type")]
    public string? OrderType { get; init; }

    [FromQuery(Name = "reseller-id")]
    public string? ResellerId { get; init; }

    [FromQuery(Name = "status")]
    public string[]? Status { get; init; }

    [FromQuery(Name = "reference-order-id")]
    public string? ReferenceOrderId { get; init; }

    [FromQuery(Name = "offer-id")]
    public string? OfferId { get; init; }

    [FromQuery(Name = "start-date")]
    public string? StartDate { get; init; }

    [FromQuery(Name = "end-date")]
    public string? EndDate { get; init; }

    [FromQuery(Name = "limit")]
    public int? Limit { get; init; }

    [FromQuery(Name = "offset")]
    public int? OffSet { get; init; }

    [FromQuery(Name = "fetch-price")]
    public bool? FetchPrice { get; init; }
}
