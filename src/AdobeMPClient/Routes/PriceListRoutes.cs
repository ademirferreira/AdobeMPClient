using AdobeMPClient.Extensions;

namespace AdobeMPClient.Routes;

public static class PriceListRoutes
{
    private const string PriceListTemplate = "{apiVersion}/pricelist";

    public static string Fetch(string apiVersion, string baseUrl)
        => new RouteBuilder(PriceListTemplate)
            .WithRouteValue("apiVersion", apiVersion)
            .Build(baseUrl);
}
