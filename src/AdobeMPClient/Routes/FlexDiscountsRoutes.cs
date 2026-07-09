using AdobeMPClient.Extensions;

namespace AdobeMPClient.Routes;

public static class FlexDiscountsRoutes
{
    private const string FlexDiscountTemplate = "{apiVersion}/flex-discounts";
    public static string Get(string apiVersion, string baseUrl)
        => new RouteBuilder(FlexDiscountTemplate)
            .WithRouteValue("apiVersion", apiVersion)
            .Build(baseUrl);
}
