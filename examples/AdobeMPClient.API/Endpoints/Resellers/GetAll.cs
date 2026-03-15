using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Reseller.Request;

namespace AdobeMPClient.API.Endpoints.Resellers;

public class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/resellers", async (
            IAdobeClient client,
            [AsParameters] GetResellersList parameters,
            ILogger<GetById> logger,
            CancellationToken ct = default) =>
        {
            logger.LogInformation("Getting resellers list");

            var result = await client.GetResellersAsync(parameters, ct);

            logger.LogInformation("Resellers list retrieved successfully");
            return result.ToResult();
        })
        .WithName("GetAllResellers")
        .WithTags("resellers")
        .WithSummary("Get all resellers")
        .WithDescription("Retrieves a list of resellers")
        .Produces<Models.Reseller.Resellers>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}