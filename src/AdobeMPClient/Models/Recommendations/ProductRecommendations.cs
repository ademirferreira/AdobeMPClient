using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Recommendations;

public class ProductRecommendations
{
    [JsonPropertyName("upsells")]
    public List<RecommendationItem> Upsells { get; set; } = [];

    [JsonPropertyName("crossSells")]
    public List<RecommendationItem> CrossSells { get; set; } = [];

    [JsonPropertyName("addOns")]
    public List<RecommendationItem> AddOns { get; set; } = [];
}
