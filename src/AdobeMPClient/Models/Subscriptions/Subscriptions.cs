using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Subscriptions
{
    public class Subscriptions
    {
        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }
        [JsonPropertyName("items")]
        public IEnumerable<Subscription> Items { get; set; } = Enumerable.Empty<Subscription>();
    }
}
