using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Reseller.Request;
using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.API.Endpoints.Resellers;

public class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/resellers", async (
            IAdobeClient client,
            [AsParameters] ResellersQuery query,
            ILogger<GetById> logger,
            CancellationToken ct = default) =>
        {
            logger.LogInformation("Getting resellers list");

            var parameters = new GetResellersList
            {
                Status = query.Status,
                Limit = query.Limit,
                OffSet = query.OffSet,
                CompanyName = query.CompanyName,
                SortBy = query.SortBy,
                OrderBy = query.OrderBy
            };

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

internal sealed record ResellersQuery
{
    [FromQuery(Name = "status")]
    public string[]? Status { get; init; }

    [FromQuery(Name = "limit")]
    public int? Limit { get; init; }

    [FromQuery(Name = "offset")]
    public int? OffSet { get; init; }

    [FromQuery(Name = "company-name")]
    public string? CompanyName { get; init; }

    [FromQuery(Name = "sort-by")]
    public string? SortBy { get; init; }

    [FromQuery(Name = "order-by")]
    public string? OrderBy { get; init; }
}
