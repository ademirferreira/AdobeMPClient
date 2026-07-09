using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Common;
using AdobeMPClient.Models.FlexDiscounts;
using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.API.Endpoints.FlexDiscounts;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/flex-discounts", async ([AsParameters] FlexDiscountsQuery query, IAdobeClient client, CancellationToken ct) =>
        {
            var request = new FlexDiscountRequest
            {
                Categories = query.Categories,
                MarketSegment = query.MarketSegment,
                Country = query.Country,
                OfferIds = query.OfferIds,
                FlexDiscountCode = query.FlexDiscountCode,
                IncludeEligibleReusableDiscounts = query.IncludeEligibleReusableDiscounts,
                StartDate = query.StartDate,
                EndDate = query.EndDate,
                Limit = query.Limit,
                Offset = query.Offset
            };

            var result = await client.FetchFlexDiscountsAsync(request, ct).ConfigureAwait(false);
            return result.ToResult();
        }).WithName("GetFlexDiscounts").WithTags("flex discounts")
        .WithSummary("Fetches a list of available flex discounts based on the provided filters.")
        .WithDescription("This endpoint allows you to retrieve a list of flex discounts that match the specified criteria, such as categories, market segment, country, offer IDs, and more. You can also specify a date range and pagination parameters to control the results.")
        .Produces<FlexDiscountResponse>(StatusCodes.Status200OK)
        .Produces<Error>(StatusCodes.Status400BadRequest)
        .Produces<Error>(StatusCodes.Status500InternalServerError);
    }
}

internal sealed record FlexDiscountsQuery
{
    [FromQuery(Name = "categories")]
    public string? Categories { get; init; }

    [FromQuery(Name = "market-segment")]
    public string? MarketSegment { get; init; }

    [FromQuery(Name = "country")]
    public string? Country { get; init; }

    [FromQuery(Name = "offer-ids")]
    public string[]? OfferIds { get; init; }

    [FromQuery(Name = "flex-discount-code")]
    public string? FlexDiscountCode { get; init; }

    [FromQuery(Name = "include-eligible-reusable-discounts")]
    public bool? IncludeEligibleReusableDiscounts { get; init; }

    [FromQuery(Name = "start-date")]
    public string? StartDate { get; init; }

    [FromQuery(Name = "end-date")]
    public string? EndDate { get; init; }

    [FromQuery(Name = "limit")]
    public int? Limit { get; init; }

    [FromQuery(Name = "offset")]
    public int? Offset { get; init; }
}
