using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Notifications;

public class NotificationResponse
{
    [JsonPropertyName("limit")]
    public int Limit { get; set; }
    [JsonPropertyName("offset")]
    public int Offset { get; set; }
    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }
    [JsonPropertyName("count")]
    public int Count { get; set; }
    [JsonPropertyName("items")]
    public IEnumerable<Notification> Items { get; set; } = [];
}

public class Notification
{
    [JsonPropertyName("customerId")]
    public string CustomerId { get; set; } = string.Empty;
    [JsonPropertyName("resellerId")]
    public string ResellerId { get; set; } = string.Empty;
    [JsonPropertyName("notificationType")]
    public string NotificationType { get; set; } = string.Empty;
}