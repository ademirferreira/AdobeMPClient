using AdobeMPClient.Models.Common;
using AdobeMPClient.Models.Transfers;
using AdobeMPClient.Routes;
using Duende.IdentityModel.Client;
using System.Net.Http.Json;

namespace AdobeMPClient.Implementations;

public partial class AdobeClient
{
    public async Task<Result<ResellerTransferResponse>> CreateResellerChangeAsync(ResellerTransferRequest resellerChangeRequest, CancellationToken ct = default)
    {
        var token = await GetAccessTokenAsync().ConfigureAwait(false);
        var requestUri = TransferRoutes.ResellerTransfer(_adobeSettings.BaseUrl);

        var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
        request.SetBearerToken(token.AccessToken!);
        SetHeaders(request);
        request.Content = JsonContent.Create(resellerChangeRequest, options: JsonOptions);
        return await SendAsync<ResellerTransferResponse>(request, ct).ConfigureAwait(false);
    }
}

