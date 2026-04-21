using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Customer;

public class Discount
{
    [JsonPropertyName("offerType")]
    public string OfferType { get; set; } = string.Empty;
    [JsonPropertyName("level")]
    public string Level { get; set; } = string.Empty;
    [JsonPropertyName("discountCode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DiscountCode { get; set; }
}
