using AdobeMPClient.Extensions;

namespace AdobeMPClient.Routes;

public static class RecommendationRoutes
{
    private const string RecommendationsTemplate = "{apiVersion}/recommendations";

    public static string Fetch(string apiVersion, string baseUrl)
        => new RouteBuilder(RecommendationsTemplate)
            .WithRouteValue("apiVersion", apiVersion)
            .Build(baseUrl);
}
