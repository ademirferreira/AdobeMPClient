using AdobeMPClient.Extensions;

namespace AdobeMPClient.Routes;

public class CustomerRoutes
{
    private const string Version = "v3";

    private const string CustomerBaseTemplate = $"{Version}/customers/{{customerId}}";
    private const string CustomersTemplate = $"{Version}/customers";

    public static string Get(string baseUrl, string customerId)
        => new RouteBuilder(CustomerBaseTemplate)
            .WithRouteValue("customerId", customerId)
            .Build(baseUrl);
}
