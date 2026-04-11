using AdobeMPClient.Models.Common;
using AdobeMPClient.Models.Recommendations;
using Duende.IdentityModel.Client;
using System.Net.Http.Json;

namespace AdobeMPClient.Implementations;

public partial class AdobeClient
{
    public async Task<Result<RecommendationsResponse>> FetchRecommendationsAsync(FetchRecommendations fetchRecommendations, CancellationToken ct = default)
    {
        var token = await GetAccessTokenAsync().ConfigureAwait(false);
        var requestUri = Routes.RecommendationRoutes.Fetch(_adobeSettings.BaseUrl);
        var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
        request.SetBearerToken(token.AccessToken!);
        SetHeaders(request);
        request.Content = JsonContent.Create(fetchRecommendations, options: JsonOptions);
        return await SendAsync<RecommendationsResponse>(request, ct).ConfigureAwait(false);
    }
}
