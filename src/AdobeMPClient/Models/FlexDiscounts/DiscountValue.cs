using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.FlexDiscounts;

public class DiscountValue
{
    [JsonPropertyName("country")]
    public string? Country { get; init; }

    [JsonPropertyName("currency")]
    public string? Currency { get; init; }

    [JsonPropertyName("value")]
    public decimal Value { get; init; }
}
