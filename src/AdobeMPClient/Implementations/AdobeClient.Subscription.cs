using AdobeMPClient.Extensions;
using AdobeMPClient.Models.Common;
using AdobeMPClient.Models.Subscriptions;
using AdobeMPClient.Models.Subscriptions.Request;
using AdobeMPClient.Routes;
using Duende.IdentityModel.Client;
using System.Net.Http.Json;

namespace AdobeMPClient.Implementations;

public partial class AdobeClient
{
    public async Task<Result<Subscriptions>> GetSubscriptionsAsync(string customerId, CancellationToken ct = default)
    {
        var token = await GetAccessTokenAsync().ConfigureAwait(false);
        var requestUri = SubscriptionRoutes.GetAll(_adobeSettings.BaseUrl, customerId);

        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.SetBearerToken(token.AccessToken!);
        SetHeaders(request);

        return await SendAsync<Subscriptions>(request, ct).ConfigureAwait(false);
    }

    public async Task<Result<Subscription>> GetSubscriptionByIdAsync(string customerId, string subscriptionId, CancellationToken ct = default)
    {
        var token = await GetAccessTokenAsync().ConfigureAwait(false);
        var requestUri = SubscriptionRoutes.GetById(_adobeSettings.BaseUrl, customerId, subscriptionId);

        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.SetBearerToken(token.AccessToken!);
        SetHeaders(request);

        return await SendAsync<Subscription>(request, ct).ConfigureAwait(false);
    }

    public async Task<Result<Subscription>> CreateSubscriptionAsync(string customerId, CreateSubscription createSubscription, CancellationToken ct = default)
    {
        var token = await GetAccessTokenAsync().ConfigureAwait(false);
        var requestUri = SubscriptionRoutes.Create(_adobeSettings.BaseUrl, customerId);
        var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
        request.SetBearerToken(token.AccessToken!);
        SetHeaders(request);
        request.Content = JsonContent.Create(createSubscription, options: JsonOptions);
        return await SendAsync<Subscription>(request, ct).ConfigureAwait(false);
    }

    public async Task<Result<Subscription>> UpdateSubscriptionAsync(string customerId, string subscriptionId, UpdateSubscription updateSubscription, CancellationToken ct = default)
    {
        var token = await GetAccessTokenAsync().ConfigureAwait(false);
        var requestUri = SubscriptionRoutes.Update(_adobeSettings.BaseUrl, customerId, subscriptionId);
        var request = new HttpRequestMessage(HttpMethod.Patch, requestUri);

        request.SetBearerToken(token.AccessToken!);
        SetHeaders(request);

        request.Content = JsonContent.Create(updateSubscription, options: JsonOptions);
        return await SendAsync<Subscription>(request, ct).ConfigureAwait(false);
    }

    public async Task<Result<Subscription>> RemoveFlexDiscountAsync(string customerId, string subscriptionId, CancellationToken ct = default)
    {
        var token = await GetAccessTokenAsync().ConfigureAwait(false);
        var requestUri = SubscriptionRoutes.ResetDiscount(_adobeSettings.BaseUrl, customerId, subscriptionId)
            .AddQueryParam("reset-flex-discount-codes", true);
        
        var request = new HttpRequestMessage(HttpMethod.Patch, requestUri);

        request.SetBearerToken(token.AccessToken!);
        SetHeaders(request);

        request.Content = JsonContent.Create(new {}, options: JsonOptions);
        return await SendAsync<Subscription>(request, ct).ConfigureAwait(false);
    }
}
