using AdobeMPClient.Extensions;

namespace AdobeMPClient.Routes;

public static class RecommendationRoutes
{
    private const string RecommendationsTemplate = $"{AdobeApiVersion.V3}/recommendations";

    public static string Fetch(string baseUrl)
        => new RouteBuilder(RecommendationsTemplate)
            .Build(baseUrl);
}
