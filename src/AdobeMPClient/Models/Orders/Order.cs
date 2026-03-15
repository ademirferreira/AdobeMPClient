using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Orders;

public class Order
{
    [JsonPropertyName("orderId")]
    public string OrderId { get; set; } = string.Empty;
    [JsonPropertyName("customerId")]
    public string CustomerId { get; set; } = string.Empty;

    [JsonPropertyName("externalReferenceId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ExternalReferenceId { get; set; }
    [JsonPropertyName("referenceOrderId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ReferenceOrderId { get; set; }
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
    [JsonPropertyName("currencyCode")]
    public string CurrencyCode { get; set; } = string.Empty;
    [JsonPropertyName("creationDate")]
    public DateTime CreationDate { get; set; }
    [JsonPropertyName("orderType")]
    public string OrderType { get; set; } = string.Empty;

    [JsonPropertyName("lineItems")]
    public IEnumerable<Item> LineItems { get; set; } = Enumerable.Empty<Item>();

    [JsonPropertyName("pricingSummary")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<PricingSummary>? PricingSummary { get; set; }
}
