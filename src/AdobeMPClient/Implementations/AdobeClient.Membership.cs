using AdobeMPClient.Extensions;
using AdobeMPClient.Models.Common;
using AdobeMPClient.Models.Memberships;
using AdobeMPClient.Routes;
using Duende.IdentityModel.Client;
using System.Net.Http.Json;

namespace AdobeMPClient.Implementations;

public partial class AdobeClient
{
    public async Task<Result<PreviewOffer>> PreviewTransferAsync(string membershipId, bool? ignoreOrderReturn = null, bool? expireOpenPas = null, CancellationToken ct = default)
    {
        var token = await GetAccessTokenAsync(ct).ConfigureAwait(false);
        var requestUri = MembershipRoutes.Preview(_adobeSettings.ApiVersion, _adobeSettings.BaseUrl, membershipId);

        requestUri = requestUri
            .AddQueryParam("ignore-order-return", ignoreOrderReturn)
            .AddQueryParam("expire-open-pas", expireOpenPas);

        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.SetBearerToken(token.AccessToken!);
        SetHeaders(request);
        return await SendAsync<PreviewOffer>(request, ct).ConfigureAwait(false);
    }

    public async Task<Result<TransferResponse>> CreateTransferAsync(TransferRequest transferRequest, string membershipId, bool? ignoreOrderReturn = null, bool? expireOpenPas = null, CancellationToken ct = default)
    {
        var token = await GetAccessTokenAsync(ct).ConfigureAwait(false);
        var requestUri = MembershipRoutes.CreateTransfer(_adobeSettings.ApiVersion, _adobeSettings.BaseUrl, membershipId);

        requestUri = requestUri
            .AddQueryParam("ignore-order-return", ignoreOrderReturn)
            .AddQueryParam("expire-open-pas", expireOpenPas);

        var request = new HttpRequestMessage(HttpMethod.Post, requestUri)
        {
            Content = JsonContent.Create(transferRequest, options: JsonOptions)
        };
        request.SetBearerToken(token.AccessToken!);
        SetHeaders(request);
        return await SendAsync<TransferResponse>(request, ct).ConfigureAwait(false);
    }

    public async Task<Result<TransferResponse>> GetTransferAsync(string membershipId, string transferId, CancellationToken ct = default)
    {
        var token = await GetAccessTokenAsync(ct).ConfigureAwait(false);
        var requestUri = MembershipRoutes.TransferDetails(_adobeSettings.ApiVersion, _adobeSettings.BaseUrl, membershipId, transferId);
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.SetBearerToken(token.AccessToken!);
        SetHeaders(request);
        return await SendAsync<TransferResponse>(request, ct).ConfigureAwait(false);
    }
}