using AdobeMPClient.Extensions;

namespace AdobeMPClient.Routes;

public class CustomerRoutes
{
    private const string Version = "v3";

    private const string CustomerBaseTemplate = $"{Version}/customers/{{customerId}}";
    private const string CustomersTemplate = $"{Version}/customers";
    private const string OpenAcquisitionsTemplate = $"{Version}/customers/{{customerId}}/open-acquisitions";

    public static string Get(string baseUrl, string customerId)
        => new RouteBuilder(CustomerBaseTemplate)
            .WithRouteValue("customerId", customerId)
            .Build(baseUrl);

    public static string Create(string baseUrl)
        => new RouteBuilder(CustomersTemplate)
            .Build(baseUrl);

    public static string Update(string baseUrl, string customerId)
        => new RouteBuilder(CustomerBaseTemplate)
            .WithRouteValue("customerId", customerId)
            .Build(baseUrl);

    public static string OpenAcquisitions(string baseUrl, string customerId)
        => new RouteBuilder(OpenAcquisitionsTemplate)
            .WithRouteValue("customerId", customerId)
            .Build(baseUrl);
}
