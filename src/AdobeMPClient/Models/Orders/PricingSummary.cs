using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Orders;

public class PricingSummary
{
    [JsonPropertyName("totalLineItemPartnerPrice")]
    public decimal TotalLineItemPartnerPrice { get; set; }
    [JsonPropertyName("currencyCode")]
    public string CurrencyCode { get; set; } = string.Empty;
}
