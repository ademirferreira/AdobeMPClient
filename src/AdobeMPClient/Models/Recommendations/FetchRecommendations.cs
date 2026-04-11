using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Recommendations;

public class FetchRecommendations
{
    [JsonPropertyName("recommendationContext")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RecommendationContext { get; set; } = string.Empty;
    [JsonPropertyName("customerId")]
    public string CustomerId { get; set; } = string.Empty;
    [JsonPropertyName("country")]
    public string? Country { get; set; }
    [JsonPropertyName("language")]
    public string? Language { get; set; }
    [JsonPropertyName("offers")]
    public IEnumerable<Offers>? Offers { get; set; }
}

public class Offers
{
    [JsonPropertyName("offerId")]
    public string OfferId { get; set; } = string.Empty;
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}