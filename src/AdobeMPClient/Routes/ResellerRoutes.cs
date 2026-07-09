using AdobeMPClient.Extensions;

namespace AdobeMPClient.Routes;

public static class ResellerRoutes
{
    private const string ResellersTemplate = "{apiVersion}/resellers";
    private const string ResellerBaseTemplate = "{apiVersion}/resellers/{resellerId}";

    public static string Get(string apiVersion, string baseUrl, string resellerId)
        => new RouteBuilder(ResellerBaseTemplate)
            .WithRouteValue("apiVersion", apiVersion)
            .WithRouteValue("resellerId", resellerId)
            .Build(baseUrl);

    public static string GetAll(string apiVersion, string baseUrl)
        => new RouteBuilder(ResellersTemplate)
            .WithRouteValue("apiVersion", apiVersion)
            .Build(baseUrl);

    public static string Create(string apiVersion, string baseUrl)
        => new RouteBuilder(ResellersTemplate)
            .WithRouteValue("apiVersion", apiVersion)
            .Build(baseUrl);

    public static string Update(string apiVersion, string baseUrl, string resellerId)
        => new RouteBuilder(ResellerBaseTemplate)
            .WithRouteValue("apiVersion", apiVersion)
            .WithRouteValue("resellerId", resellerId)
            .Build(baseUrl);
}
