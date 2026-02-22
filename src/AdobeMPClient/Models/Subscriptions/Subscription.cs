using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Subscriptions
{
    public class Subscription
    {
        [JsonPropertyName("subscriptionId")]
        public string SubscriptionId { get; set; } = string.Empty;

        [JsonPropertyName("offerId")]
        public string OfferId { get; set; } = string.Empty;

        [JsonPropertyName("currentQuantity")]
        public int CurrentQuantity { get; set; }

        [JsonPropertyName("usedQuantity")]
        public int UsedQuantity { get; set; }

        [JsonPropertyName("autoRenewal")]
        public AutoRenewal AutoRenewal { get; set; } = new();

        [JsonPropertyName("creationDate")]
        public DateTime CreationDate { get; set; }

        [JsonPropertyName("renewalDate")]
        public DateTime RenewalDate { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("currencyCode")]
        public string CurrencyCode { get; set; } = string.Empty;

        [JsonPropertyName("allowedActions")]
        public IEnumerable<string> AllowedActions { get; set; } = [];
    }

}
