using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Subscriptions.Request;

public class CreateSubscription
{
    [JsonPropertyName("offerId")]
    public string OfferId { get; set; } = string.Empty;
    [JsonPropertyName("autoRenewal")]
    public AutoRenewal AutoRenewal { get; set; } = new AutoRenewal();
}
