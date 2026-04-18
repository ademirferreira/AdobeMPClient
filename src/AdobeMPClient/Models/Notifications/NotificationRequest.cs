

using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.Models.Notifications;

public sealed record NotificationRequest
{
    [FromQuery(Name = "notification-type")]
    public string? NotificationType { get; init; }

    [FromQuery(Name = "reseller-id")]
    public string? ResellerId { get; init; }

    [FromQuery(Name = "limit")]
    public int? Limit { get; init; }

    [FromQuery(Name = "offset")]
    public int? Offset { get; init; }
}
