using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Orders;

public class CancellingItem
{
    [JsonPropertyName("extLineItemNumber")]
    public int ExtLineItemNumber { get; set; }
    [JsonPropertyName("referenceLineItemNumber")]
    public int ReferenceLineItemNumber { get; set; }
    [JsonPropertyName("subscriptionId")]
    public string SubscriptionId { get; set; } = string.Empty;
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}
