using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Notifications;

namespace AdobeMPClient.API.Endpoints.Notifications;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/notifications", async ([AsParameters] NotificationRequest parameters, IAdobeClient client, ILogger<Get> logger, CancellationToken ct) =>
        {
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
