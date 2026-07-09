using AdobeMPClient.Extensions;

namespace AdobeMPClient.Routes;

public class CustomerRoutes
{

    private const string CustomerBaseTemplate = "{apiVersion}/customers/{customerId}";
    private const string CustomersTemplate = "{apiVersion}/customers";
    private const string OpenAcquisitionsTemplate = "{apiVersion}/customers/{customerId}/open-acquisitions";
    private const string FlexDiscountTemplate = "{apiVersion}/customers/{customerId}/flex-discounts";

    public static string Get(string apiVersion, string baseUrl, string customerId)
        => new RouteBuilder(CustomerBaseTemplate)
            .WithRouteValue("apiVersion", apiVersion)
            .WithRouteValue("customerId", customerId)
            .Build(baseUrl);

    public static string Create(string apiVersion, string baseUrl)
        => new RouteBuilder(CustomersTemplate)
            .WithRouteValue("apiVersion", apiVersion)
            .Build(baseUrl);

    public static string Update(string apiVersion, string baseUrl, string customerId)
        => new RouteBuilder(CustomerBaseTemplate)
            .WithRouteValue("apiVersion", apiVersion)
            .WithRouteValue("customerId", customerId)
            .Build(baseUrl);

    public static string OpenAcquisitions(string apiVersion, string baseUrl, string customerId)
        => new RouteBuilder(OpenAcquisitionsTemplate)
            .WithRouteValue("apiVersion", apiVersion)
            .WithRouteValue("customerId", customerId)
            .Build(baseUrl);

    public static string FlexDiscounts(string apiVersion, string baseUrl, string customerId)
        => new RouteBuilder(FlexDiscountTemplate)
            .WithRouteValue("apiVersion", apiVersion)
            .WithRouteValue("customerId", customerId)
            .Build(baseUrl);
}
