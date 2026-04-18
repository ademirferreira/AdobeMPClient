using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.PriceList;
using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.API.Endpoints.PriceList;

public class Fetch : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/pricelist", async (
            PriceListRequest parameters, 
            [FromQuery]int? limit, 
            [FromQuery]int? offset, 
            CancellationToken ct, 
            IAdobeClient adobeClient) =>
        {
            var result = await adobeClient.FetchPriceListAsync(parameters, limit, offset, ct).ConfigureAwait(false);
            return result.ToResult();
        }).WithName("FetchPriceList")
        .WithTags("Price List")
        .WithSummary("Fetches the price list based on the provided parameters.")
        .WithDescription("Fetches the price list based on the provided parameters. This endpoint allows you to retrieve pricing information for Adobe products and services based on various criteria such as region, market segment, price list type, currency, and more.")
        .Produces<PriceListResponse>()
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}
