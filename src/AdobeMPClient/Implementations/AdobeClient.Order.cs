using AdobeMPClient.Extensions;
using AdobeMPClient.Models.Common;
using AdobeMPClient.Models.Orders;
using AdobeMPClient.Models.Orders.Request;
using AdobeMPClient.Routes;
using Duende.IdentityModel.Client;
using System.Net.Http.Json;

namespace AdobeMPClient.Implementations;

public partial class AdobeClient
{
    public async Task<Result<OrderHistory>> GetOrderHistoryAsync(string customerId, GetOrderHistoryRequest? parameters = null, CancellationToken ct = default)
    {
        var token = await GetAccessTokenAsync().ConfigureAwait(false);

        var requestUri = OrderRoutes.GetAll(_adobeSettings.BaseUrl, customerId);

        if (parameters != null)
        {
            requestUri = requestUri
            .AddQueryParam("order-type", parameters.OrderType)
            .AddQueryParam("reseller-id", parameters.ResellerId)
            .AddQueryParam("status", parameters.Status)
            .AddQueryParam("reference-order-id", parameters.ReferenceOrderId)
            .AddQueryParam("offer-id", parameters.OfferId)
            .AddQueryParam("start-date", parameters.StartDate)
            .AddQueryParam("end-date", parameters.EndDate)
            .AddQueryParam("limit", parameters.Limit)
            .AddQueryParam("offset", parameters.OffSet)
            .AddQueryParam("fetch-price", parameters.FetchPrice);
        }

        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.SetBearerToken(token.AccessToken!);
        SetHeaders(request);

        return await SendAsync<OrderHistory>(request, ct).ConfigureAwait(false);
    }

    public async Task<Result<Order>> GetOrderByIdAsync(string customerId, string orderId, bool? fetchPrice = null, CancellationToken ct = default)
    {
        var token = await GetAccessTokenAsync().ConfigureAwait(false);

        var requestUri = OrderRoutes.GetById(_adobeSettings.BaseUrl, customerId, orderId)
            .AddQueryParam("fetch-price", fetchPrice);

        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.SetBearerToken(token.AccessToken!);
        SetHeaders(request);

        return await SendAsync<Order>(request, ct).ConfigureAwait(false);
    }

    public async Task<Result<Order>> CreateOrderAsync(string customerId, CreateOrder createOrder, CancellationToken ct = default)
    {
        var token = await GetAccessTokenAsync().ConfigureAwait(false);
        var requestUri = OrderRoutes.Create(_adobeSettings.BaseUrl, customerId);
        var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
        request.SetBearerToken(token.AccessToken!);
        SetHeaders(request);
        request.Content = JsonContent.Create(createOrder, options: JsonOptions);
        return await SendAsync<Order>(request, ct).ConfigureAwait(false);
    }

    public async Task<Result<Order>> UpdateOrderAsync(string customerId, string orderId, UpdateOrder updateOrder, CancellationToken ct = default)
    {
        var token = await GetAccessTokenAsync().ConfigureAwait(false);
        var requestUri = OrderRoutes.Update(_adobeSettings.BaseUrl, customerId, orderId);
        var request = new HttpRequestMessage(HttpMethod.Patch, requestUri);
        request.SetBearerToken(token.AccessToken!);
        SetHeaders(request);
        request.Content = JsonContent.Create(updateOrder, options: JsonOptions);
        return await SendAsync<Order>(request, ct).ConfigureAwait(false);
    }
}
