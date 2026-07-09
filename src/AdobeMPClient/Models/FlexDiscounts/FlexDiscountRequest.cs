namespace AdobeMPClient.Models.FlexDiscounts;

public sealed record FlexDiscountRequest
{
    public string? Categories { get; init; }
    public string? MarketSegment { get; init; }
    public string? Country { get; init; }
    public string[]? OfferIds { get; init; }
    public string? FlexDiscountCode { get; init; }
    public bool? IncludeEligibleReusableDiscounts { get; init; }
    public string? StartDate { get; init; }
    public string? EndDate { get; init; }
    public int? Limit { get; init; }
    public int? Offset { get; init; }
}
