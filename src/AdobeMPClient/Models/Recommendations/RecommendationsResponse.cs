using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Recommendations;

public class RecommendationsResponse
{
    [JsonPropertyName("productRecommendations")]
    public ProductRecommendations ProductRecommendations { get; set; } = new();
}
