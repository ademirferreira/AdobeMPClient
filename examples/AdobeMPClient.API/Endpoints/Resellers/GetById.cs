using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Reseller;
using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.API.Endpoints.Resellers;

public class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/resellers/{resellerId}", async (
            [FromRoute] string resellerId,
            IAdobeClient client,
            ILogger<GetById> logger,
            CancellationToken ct = default) =>
        {
            logger.LogInformation("Getting customer {CustomerId}", resellerId);

            var result = await client.GetResellerAsync(resellerId, ct);

            logger.LogInformation("Customer {CustomerId} retrieved successfully", resellerId);
            return result.ToResult();
        })
        .WithName("GetResellerById")
        .WithTags("resellers")
        .WithSummary("Get reseller by ID")
        .WithDescription("Retrieves a reseller by their unique identifier")
        .Produces<Reseller>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}
