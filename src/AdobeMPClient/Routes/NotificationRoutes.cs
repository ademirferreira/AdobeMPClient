using AdobeMPClient.Extensions;

namespace AdobeMPClient.Routes;

public static class NotificationRoutes
{
    private const string NotificationTemplate = "{apiVersion}/notifications";
    public static string Get(string apiVersion, string baseUrl)
        => new RouteBuilder(NotificationTemplate)
            .WithRouteValue("apiVersion", apiVersion)
            .Build(baseUrl);
}
