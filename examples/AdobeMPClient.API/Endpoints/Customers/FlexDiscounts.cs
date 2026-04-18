using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Common;
using AdobeMPClient.Models.FlexDiscounts;
using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.API.Endpoints.Customers;

public class FlexDiscounts : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/customers/{customerId}/flex-discounts", async (
            [FromRoute] string customerId,
            [FromQuery] int? limit,
            [FromQuery] int? offset,
            IAdobeClient client,
            ILogger<FlexDiscounts> logger,
            CancellationToken ct = default) =>
        {
            logger.LogInformation("Getting flex discounts for customer {CustomerId}", customerId);

            var result = await client.GetCustomerFlexDiscountsAsync(customerId, limit, offset, ct);

            logger.LogInformation("Flex discounts for customer {CustomerId} retrieved successfully", customerId);
            return result.ToResult();
        })
        .WithName("GetCustomerFlexDiscounts")
        .WithTags("customers")
        .WithSummary("Get customer flex discounts")
        .WithDescription("Retrieves flex discounts for a customer by their unique identifier")
        .Produces<FlexDiscountResponse>(StatusCodes.Status200OK)
        .Produces<Error>(StatusCodes.Status400BadRequest)
        .Produces<Error>(StatusCodes.Status404NotFound)
        .Produces<Error>(StatusCodes.Status500InternalServerError);
    }
}
