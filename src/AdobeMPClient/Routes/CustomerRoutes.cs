using AdobeMPClient.Extensions;

namespace AdobeMPClient.Routes;

public class CustomerRoutes
{

    private const string CustomerBaseTemplate = $"{AdobeApiVersion.V3}/customers/{{customerId}}";
    private const string CustomersTemplate = $"{AdobeApiVersion.V3}/customers";
    private const string OpenAcquisitionsTemplate = $"{AdobeApiVersion.V3}/customers/{{customerId}}/open-acquisitions";
    private const string FlexDiscountTemplate = $"{AdobeApiVersion.V3}/customers/{{customerId}}/flex-discounts";

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

    public static string FlexDiscounts(string baseUrl, string customerId)
        => new RouteBuilder(FlexDiscountTemplate)
            .WithRouteValue("customerId", customerId)
            .Build(baseUrl);
}
