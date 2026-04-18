using AdobeMPClient.Extensions;
using AdobeMPClient.Models.Common;
using AdobeMPClient.Models.FlexDiscounts;
using AdobeMPClient.Routes;
using Duende.IdentityModel.Client;

namespace AdobeMPClient.Implementations;

public partial class AdobeClient
{
    public async Task<Result<FlexDiscountResponse>> FetchFlexDiscountsAsync(FlexDiscountRequest? parameters = null, CancellationToken ct = default)
    {
        var token = await GetAccessTokenAsync().ConfigureAwait(false);
        var requestUri = FlexDiscountsRoutes.Get(_adobeSettings.BaseUrl);
        if (parameters != null)
        {
            requestUri = requestUri
                .AddQueryParam("categories", parameters.Categories)
                .AddQueryParam("market-segment", parameters.MarketSegment)
                .AddQueryParam("country", parameters.Country)
                .AddQueryParam("offer-ids", parameters.OfferIds)
                .AddQueryParam("flex-discount-code", parameters.FlexDiscountCode)
                .AddQueryParam("include-eligible-reusable-discounts", parameters.IncludeEligibleReusableDiscounts)
                .AddQueryParam("start-date", parameters.StartDate)
                .AddQueryParam("end-date", parameters.EndDate)
                .AddQueryParam("limit", parameters.Limit)
                .AddQueryParam("offset", parameters.Offset);
        }
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.SetBearerToken(token.AccessToken!);
        SetHeaders(request);
        return await SendAsync<FlexDiscountResponse>(request, ct).ConfigureAwait(false);
    }

}
