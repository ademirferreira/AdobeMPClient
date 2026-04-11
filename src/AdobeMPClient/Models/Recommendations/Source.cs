using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Recommendations;

public class Source
{
    [JsonPropertyName("sourceType")]
    public string SourceType { get; set; } = string.Empty;

    [JsonPropertyName("offerIds")]
    public List<string> OfferIds { get; set; } = [];
}
