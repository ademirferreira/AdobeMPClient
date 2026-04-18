using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.Models.FlexDiscounts;

public sealed record FlexDiscountRequest
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
