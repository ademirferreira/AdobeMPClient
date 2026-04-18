using AdobeMPClient.Extensions;

namespace AdobeMPClient.Routes;

public static class FlexDiscountsRoutes
{
    private const string FlexDiscountTemplate = $"{AdobeApiVersion.V3}/flex-discounts";
    public static string Get(string baseUrl)
        => new RouteBuilder(FlexDiscountTemplate)
            .Build(baseUrl);
}
