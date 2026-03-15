using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Orders;

public class Pricing
{
    [JsonPropertyName("partnerPrice")]
    public decimal PartnerPrice { get; set; }
    [JsonPropertyName("discountedPartnerPrice")]
    public decimal DiscountedPartnerPrice { get; set; }
    [JsonPropertyName("netPartnerPrice")]
    public decimal NetPartnerPrice { get; set; }
    [JsonPropertyName("lineItemPartnerPrice")]
    public decimal LineItemPartnerPrice { get; set; }
}
