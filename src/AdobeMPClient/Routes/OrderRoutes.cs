using AdobeMPClient.Extensions;

namespace AdobeMPClient.Routes;

public static class OrderRoutes
{

    private const string OrdersTemplate = $"{AdobeApiVersion.V3}/customers/{{customerId}}/orders";
    private const string OrdersByIdTemplate = $"{AdobeApiVersion.V3}/customers/{{customerId}}/orders/{{orderId}}";

    public static string GetAll(string baseUrl, string customerId)
        => new RouteBuilder(OrdersTemplate)
            .WithRouteValue("customerId", customerId)
            .Build(baseUrl);

    public static string GetById(string baseUrl, string customerId, string orderId)
        => new RouteBuilder(OrdersByIdTemplate)
            .WithRouteValue("customerId", customerId)
            .WithRouteValue("orderId", orderId)
            .Build(baseUrl);

    public static string Create(string baseUrl, string customerId)
        => new RouteBuilder(OrdersTemplate)
            .WithRouteValue("customerId", customerId)
            .Build(baseUrl);

    public static string Update(string baseUrl, string customerId, string orderId)
        => new RouteBuilder(OrdersByIdTemplate)
            .WithRouteValue("customerId", customerId)
            .WithRouteValue("orderId", orderId)
            .Build(baseUrl);
}
