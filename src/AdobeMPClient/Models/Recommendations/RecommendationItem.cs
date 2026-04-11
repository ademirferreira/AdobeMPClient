using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Recommendations;

public class RecommendationItem
{
    [JsonPropertyName("rank")]
    public int Rank { get; set; }

    [JsonPropertyName("product")]
    public Product Product { get; set; } = new();

    [JsonPropertyName("source")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Source? Source { get; set; }
}
