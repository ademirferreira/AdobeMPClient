using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.PriceList;

public sealed record PriceListResponse
{
    [JsonPropertyName("priceListMonth")]
    public string? PriceListMonth { get; init; }

    [JsonPropertyName("marketSegment")]
    public string? MarketSegment { get; init; }

    [JsonPropertyName("region")]
    public string? Region { get; init; }

    [JsonPropertyName("currency")]
    public string? Currency { get; init; }

    [JsonPropertyName("priceListType")]
    public string? PriceListType { get; init; }

    [JsonPropertyName("totalCount")]
    public int TotalCount { get; init; }

    [JsonPropertyName("count")]
    public int Count { get; init; }

    [JsonPropertyName("limit")]
    public int Limit { get; init; }

    [JsonPropertyName("offset")]
    public int Offset { get; init; }

    [JsonPropertyName("offers")]
    public IEnumerable<Offer>? Offers { get; init; }
}
