using AdobeMPClient.Extensions;

namespace AdobeMPClient.Routes;

public static class SubscriptionRoutes
{

    private const string SubscriptionsTemplate = "{apiVersion}/customers/{customerId}/subscriptions";
    private const string SubscriptionByIdTemplate = "{apiVersion}/customers/{customerId}/subscriptions/{subscriptionId}";

    public static string GetAll(string apiVersion, string baseUrl, string customerId)
        => new RouteBuilder(SubscriptionsTemplate)
            .WithRouteValue("apiVersion", apiVersion)
            .WithRouteValue("customerId", customerId)
            .Build(baseUrl);

    public static string GetById(string apiVersion, string baseUrl, string customerId, string subscriptionId)
        => new RouteBuilder(SubscriptionByIdTemplate)
            .WithRouteValue("apiVersion", apiVersion)
            .WithRouteValue("customerId", customerId)
            .WithRouteValue("subscriptionId", subscriptionId)
            .Build(baseUrl);

    public static string Create(string apiVersion, string baseUrl, string customerId)
        => new RouteBuilder(SubscriptionsTemplate)
            .WithRouteValue("apiVersion", apiVersion)
            .WithRouteValue("customerId", customerId)
            .Build(baseUrl);

    public static string Update(string apiVersion, string baseUrl, string customerId, string subscriptionId)
        => new RouteBuilder(SubscriptionByIdTemplate)
            .WithRouteValue("apiVersion", apiVersion)
            .WithRouteValue("customerId", customerId)
            .WithRouteValue("subscriptionId", subscriptionId)
            .Build(baseUrl);

    public static string ResetDiscount(string apiVersion, string baseUrl, string customerId, string subscriptionId)
        => new RouteBuilder(SubscriptionByIdTemplate)
            .WithRouteValue("apiVersion", apiVersion)
            .WithRouteValue("customerId", customerId)
            .WithRouteValue("subscriptionId", subscriptionId)
            .Build(baseUrl);

}
