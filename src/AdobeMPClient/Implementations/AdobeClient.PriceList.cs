using AdobeMPClient.Extensions;
using AdobeMPClient.Models.Common;
using AdobeMPClient.Models.PriceList;
using AdobeMPClient.Routes;
using Duende.IdentityModel.Client;
using System.Net.Http.Json;

namespace AdobeMPClient.Implementations;

public partial class AdobeClient
{
    public async Task<Result<PriceListResponse>> FetchPriceListAsync(PriceListRequest? parameters = null, int? limit = null, int? offset = null, CancellationToken ct = default)
    {
        var token = await GetAccessTokenAsync().ConfigureAwait(false);
        var requestUri = PriceListRoutes.Fetch(_adobeSettings.BaseUrl);

        if (parameters != null)
        {
            requestUri = requestUri.AddQueryParam("limit", limit)
                                   .AddQueryParam("offset", offset);
            //requestUri = requestUri.AddQueryParam("region", parameters.Region)
            //.AddQueryParam("market-segment", parameters.MarketSegment)
            //.AddQueryParam("price-list-type", parameters.PriceListType)
            //.AddQueryParam("currency", parameters.Currency)
            //.AddQueryParam("price-list-month", parameters.PriceListMonth)
            //.AddQueryParam("include-offer-attributes", string.Join(",", parameters.IncludeOfferAttributes ?? Array.Empty<string>()));
        }

        var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
        request.SetBearerToken(token.AccessToken!);
        SetHeaders(request);


        request.Content = JsonContent.Create(parameters, options: JsonOptions);


        return await SendAsync<PriceListResponse>(request, ct).ConfigureAwait(false);
    }
}

