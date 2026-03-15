using AdobeMPClient.Extensions;

namespace AdobeMPClient.Routes;

public static class ResellerRoutes
{
    private const string ResellersTemplate = $"{AdobeApiVersion.V3}/resellers";
    private const string ResellerBaseTemplate = $"{AdobeApiVersion.V3}/resellers/{{resellerId}}";

    public static string Get(string baseUrl, string resellerId)
        => new RouteBuilder(ResellerBaseTemplate)
            .WithRouteValue("resellerId", resellerId)
            .Build(baseUrl);

    public static string GetAll(string baseUrl)
        => new RouteBuilder(ResellersTemplate)
            .Build(baseUrl);

    public static string Create(string baseUrl)
        => new RouteBuilder(ResellersTemplate)
            .Build(baseUrl);
}
