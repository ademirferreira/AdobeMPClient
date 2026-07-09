using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.API.Endpoints.Notifications;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/notifications", async ([AsParameters] NotificationsQuery query, IAdobeClient client, ILogger<Get> logger, CancellationToken ct) =>
        {
            var parameters = new NotificationRequest
            {
                NotificationType = query.NotificationType,
                ResellerId = query.ResellerId,
                Limit = query.Limit,
                Offset = query.Offset
            };

            logger.LogInformation("Received request to get notifications with parameters: {@Parameters}", parameters);

            var result = await client.GetNotificationsAsync(parameters, ct);

            return result.ToResult();

        }).WithName("GetNotifications")
            .WithTags("notifications")
            .WithSummary("Get notifications for the partner.")
            .WithDescription("Retrieves a list of notification type returns all customers under a given reseller who have at least one license created within the last 7 days for which order has not been placed yet by partner.")
            .Produces<NotificationResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}

internal sealed record NotificationsQuery
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
