using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Benefits;

public class MinimumQuantity
{
    [JsonPropertyName("offerType")]
    public string OfferType { get; set; } = string.Empty;
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}
