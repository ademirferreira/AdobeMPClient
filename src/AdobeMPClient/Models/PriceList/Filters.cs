using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.PriceList;

public sealed record Filters
{
    [JsonPropertyName("offerId")]
    public string? OfferId { get; init; }

    [JsonPropertyName("productFamily")]
    public string? ProductFamily { get; init; }

    [JsonPropertyName("firstOrderDate")]
    public string? FirstOrderDate { get; init; }

    [JsonPropertyName("lastOrderDate")]
    public string? LastOrderDate { get; init; }

    [JsonPropertyName("discountCode")]
    public string? DiscountCode { get; init; }
}
