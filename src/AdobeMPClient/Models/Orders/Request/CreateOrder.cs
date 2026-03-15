using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Orders.Request;

public sealed class CreateOrder
{
    [JsonPropertyName("orderType")]
    public string OrderType { get; set; } = string.Empty;
    [JsonPropertyName("referenceOrderId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull & JsonIgnoreCondition.WhenWritingDefault)]
    public string? ReferenceOrderId { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ExternalReferenceId { get; set; }
    [JsonPropertyName("currencyCode")]
    public string CurrencyCode { get; set; } = "USD";

    [JsonPropertyName("lineItems")]
    public List<Item> LineItems { get; set; } = new List<Item>();

    [JsonPropertyName("cancellingItems")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull & JsonIgnoreCondition.WhenWritingDefault)]
    public List<CancellingItem> CancellingItems { get; set; } = new List<CancellingItem>();
}
