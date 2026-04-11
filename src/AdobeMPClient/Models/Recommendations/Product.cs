using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Recommendations;

public class Product
{
    [JsonPropertyName("baseOfferId")]
    public string BaseOfferId { get; set; } = string.Empty;
}
