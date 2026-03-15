using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Orders;

public class OrderHistory
{
    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }
    [JsonPropertyName("count")]
    public int Count { get; set; }
    [JsonPropertyName("offset")]
    public int Offset { get; set; }
    [JsonPropertyName("limit")]
    public int Limit { get; set; }
    [JsonPropertyName("items")]
    public IEnumerable<Order> Items { get; set; } = Enumerable.Empty<Order>();
}
