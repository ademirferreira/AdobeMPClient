using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.PriceList;

public sealed record PriceListRequest
{
    [JsonPropertyName("region")]
    public string? Region { get; init; }

    [JsonPropertyName("marketSegment")]
    public string? MarketSegment { get; init; }

    [JsonPropertyName("priceListType")]
    public string? PriceListType { get; init; }

    [JsonPropertyName("currency")]
    public string? Currency { get; init; }

    [JsonPropertyName("priceListMonth")]
    public string? PriceListMonth { get; init; }

    [JsonPropertyName("filters")]
    public Filters? Filters { get; init; }
    [JsonPropertyName("includeOfferAttributes")]
    public string[]? IncludeOfferAttributes { get; init; }
}
