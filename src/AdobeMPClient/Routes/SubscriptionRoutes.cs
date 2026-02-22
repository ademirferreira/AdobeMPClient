using AdobeMPClient.Extensions;

namespace AdobeMPClient.Routes;

public static class SubscriptionRoutes
{
    private const string Version = "v3";

    private const string SubscriptionsTemplate = $"{Version}/customers/{{customerId}}/subscriptions";
    private const string SubscriptionByIdTemplate = $"{Version}/customers/{{customerId}}/subscriptions/{{subscriptionId}}";

    public static string GetAll(string baseUrl, string customerId)
        => new RouteBuilder(SubscriptionsTemplate)
            .WithRouteValue("customerId", customerId)
            .Build(baseUrl);

    public static string GetById(string baseUrl, string customerId, string subscriptionId)
        => new RouteBuilder(SubscriptionByIdTemplate)
            .WithRouteValue("customerId", customerId)
            .WithRouteValue("subscriptionId", subscriptionId)
            .Build(baseUrl);

    public static string Create(string baseUrl, string customerId)
        => new RouteBuilder(SubscriptionsTemplate)
            .WithRouteValue("customerId", customerId)
            .Build(baseUrl);

}
