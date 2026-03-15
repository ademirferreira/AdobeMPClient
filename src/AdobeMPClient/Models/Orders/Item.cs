using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Orders;

public class Item
{
    [JsonPropertyName("extLineItemNumber")]
    public int ExtLineItemNumber { get; set; }

    [JsonPropertyName("offerId")]
    public string OfferId { get; set; } = string.Empty;

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("subscriptionId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? SubscriptionId { get; set; }

    [JsonPropertyName("status")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Status { get; set; }

    [JsonPropertyName("currencyCode")]
    public string CurrencyCode { get; set; } = "USD";

    [JsonPropertyName("flexDiscountCodes")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? FlexDiscountCodes { get; set; }

    [JsonPropertyName("flexDiscounts")]
    public IEnumerable<Promotion> FlexDiscounts { get; set; } = Enumerable.Empty<Promotion>();

    [JsonPropertyName("proratedDays")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ProratedDays { get; set; }

    [JsonPropertyName("pricing")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Pricing? Pricing { get; set; }
}
