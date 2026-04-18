using AdobeMPClient.Extensions;

namespace AdobeMPClient.Routes;

public static class SubscriptionRoutes
{

    private const string SubscriptionsTemplate = $"{AdobeApiVersion.V3}/customers/{{customerId}}/subscriptions";
    private const string SubscriptionByIdTemplate = $"{AdobeApiVersion.V3}/customers/{{customerId}}/subscriptions/{{subscriptionId}}";

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

    public static string Update(string baseUrl, string customerId, string subscriptionId)
        => new RouteBuilder(SubscriptionByIdTemplate)
            .WithRouteValue("customerId", customerId)
            .WithRouteValue("subscriptionId", subscriptionId)
            .Build(baseUrl);

    public static string ResetDiscount(string baseUrl, string customerId, string subscriptionId)
        => new RouteBuilder(SubscriptionByIdTemplate)
            .WithRouteValue("customerId", customerId)
            .WithRouteValue("subscriptionId", subscriptionId)
            .Build(baseUrl);

}
