namespace AdobeMPClient.Models.Notifications;

public sealed record NotificationRequest
{
    public string? NotificationType { get; init; }
    public string? ResellerId { get; init; }
    public int? Limit { get; init; }
    public int? Offset { get; init; }
}
