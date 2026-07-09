using AdobeMPClient.Extensions;

namespace AdobeMPClient.Routes;

public static class OrderRoutes
{

    private const string OrdersTemplate = "{apiVersion}/customers/{customerId}/orders";
    private const string OrdersByIdTemplate = "{apiVersion}/customers/{customerId}/orders/{orderId}";

    public static string GetAll(string apiVersion, string baseUrl, string customerId)
        => new RouteBuilder(OrdersTemplate)
            .WithRouteValue("apiVersion", apiVersion)
            .WithRouteValue("customerId", customerId)
            .Build(baseUrl);

    public static string GetById(string apiVersion, string baseUrl, string customerId, string orderId)
        => new RouteBuilder(OrdersByIdTemplate)
            .WithRouteValue("apiVersion", apiVersion)
            .WithRouteValue("customerId", customerId)
            .WithRouteValue("orderId", orderId)
            .Build(baseUrl);

    public static string Create(string apiVersion, string baseUrl, string customerId)
        => new RouteBuilder(OrdersTemplate)
            .WithRouteValue("apiVersion", apiVersion)
            .WithRouteValue("customerId", customerId)
            .Build(baseUrl);

    public static string Update(string apiVersion, string baseUrl, string customerId, string orderId)
        => new RouteBuilder(OrdersByIdTemplate)
            .WithRouteValue("apiVersion", apiVersion)
            .WithRouteValue("customerId", customerId)
            .WithRouteValue("orderId", orderId)
            .Build(baseUrl);
}
