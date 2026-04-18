using AdobeMPClient.Extensions;

namespace AdobeMPClient.Routes;

public static class PriceListRoutes
{
    private const string PriceListTemplate = $"{AdobeApiVersion.V3}/pricelist";

    public static string Fetch(string baseUrl)
        => new RouteBuilder(PriceListTemplate)
            .Build(baseUrl);
}
