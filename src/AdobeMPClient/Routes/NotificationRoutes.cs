using AdobeMPClient.Extensions;

namespace AdobeMPClient.Routes;

public static class NotificationRoutes
{
    private const string NotificationTemplate = $"{AdobeApiVersion.V3}/notifications";
    public static string Get(string baseUrl)
        => new RouteBuilder(NotificationTemplate)
            .Build(baseUrl);
}
