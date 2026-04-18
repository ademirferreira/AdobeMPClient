using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.FlexDiscounts;

public class FlexDiscountOutcome
{
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("discountValues")]
    public List<DiscountValue> DiscountValues { get; init; } = [];
}
