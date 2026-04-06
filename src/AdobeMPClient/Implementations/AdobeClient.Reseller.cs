using AdobeMPClient.Extensions;
using AdobeMPClient.Models.Common;
using AdobeMPClient.Models.Reseller;
using AdobeMPClient.Models.Reseller.Request;
using AdobeMPClient.Routes;
using Duende.IdentityModel.Client;
using System.Net.Http.Json;

namespace AdobeMPClient.Implementations;

public partial class AdobeClient
{
    public async Task<Result<Reseller>> GetResellerAsync(string resellerId, CancellationToken ct = default)
    {
        var token = await GetAccessTokenAsync().ConfigureAwait(false);

        var requestUri = ResellerRoutes.Get(_adobeSettings.BaseUrl, resellerId);

        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.SetBearerToken(token.AccessToken!);
        SetHeaders(request);

        return await SendAsync<Reseller>(request, ct).ConfigureAwait(false);
    }

    public async Task<Result<Resellers>> GetResellersAsync(GetResellersList? parameters, CancellationToken ct = default)
    {
        var token = await GetAccessTokenAsync().ConfigureAwait(false);
        var requestUri = ResellerRoutes.GetAll(_adobeSettings.BaseUrl);

        if (parameters != null)
        {
            requestUri = requestUri
            .AddQueryParam("status", parameters.Status)
            .AddQueryParam("limit", parameters.Limit)
            .AddQueryParam("offset", parameters.OffSet)
            .AddQueryParam("company-name", parameters.CompanyName)
            .AddQueryParam("sort-by", parameters.SortBy)
            .AddQueryParam("order-by", parameters.OrderBy);
        }
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.SetBearerToken(token.AccessToken!);
        SetHeaders(request);
        return await SendAsync<Resellers>(request, ct).ConfigureAwait(false);
    }

    public async Task<Result<Reseller>> CreateResellerAsync(CreateReseller createReseller, CancellationToken ct = default)
    {
        var token = await GetAccessTokenAsync().ConfigureAwait(false);
        var requestUri = ResellerRoutes.Create(_adobeSettings.BaseUrl);

        var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
        request.SetBearerToken(token.AccessToken!);
        SetHeaders(request);
        request.Content = JsonContent.Create(createReseller, options: JsonOptions);
        return await SendAsync<Reseller>(request, ct).ConfigureAwait(false);
    }

    public async Task<Result<Reseller>> UpdateResellerAsync(string resellerId, UpdateReseller updateReseller, CancellationToken ct = default)
    {
        var token = await GetAccessTokenAsync().ConfigureAwait(false);
        var requestUri = ResellerRoutes.Update(_adobeSettings.BaseUrl, resellerId);

        var request = new HttpRequestMessage(HttpMethod.Patch, requestUri);
        request.SetBearerToken(token.AccessToken!);
        SetHeaders(request);
        request.Content = JsonContent.Create(updateReseller, options: JsonOptions);
        return await SendAsync<Reseller>(request, ct).ConfigureAwait(false);
    }
}
