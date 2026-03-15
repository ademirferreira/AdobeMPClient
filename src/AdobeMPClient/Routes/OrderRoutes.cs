using AdobeMPClient.Extensions;
using AdobeMPClient.Models.Orders.Request;

namespace AdobeMPClient.Routes;

public static class OrderRoutes
{
    private const string Version = "v3";

    private const string OrdersTemplate = $"{Version}/customers/{{customerId}}/orders";
    private const string OrdersByIdTemplate = $"{Version}/customers/{{customerId}}/orders/{{orderId}}";

    public static string GetAll(string baseUrl, string customerId)
        => new RouteBuilder(OrdersTemplate)
            .WithRouteValue("customerId", customerId)
            .Build(baseUrl);

    public static string GetAll(string baseUrl, string customerId, GetOrderHistoryRequest? parameters)
    {
        var url = GetAll(baseUrl, customerId);

        if (parameters == null)
            return url;

        url = url.AddQueryParam("order-type", parameters.OrderType);
        url = url.AddQueryParam("reseller-id", parameters.ResellerId);
        url = url.AddQueryParam("status", parameters.Status);
        url = url.AddQueryParam("reference-order-id", parameters.ReferenceOrderId);
        url = url.AddQueryParam("offer-id", parameters.OfferId);
        url = url.AddQueryParam("start-date", parameters.StartDate);
        url = url.AddQueryParam("end-date", parameters.EndDate);
        url = url.AddQueryParam("limit", parameters.Limit);
        url = url.AddQueryParam("offset", parameters.Offset);
        url = url.AddQueryParam("fetch-price", parameters.FetchPrice);

        return url;
    }
}
