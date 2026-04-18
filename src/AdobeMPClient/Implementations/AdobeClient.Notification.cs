using AdobeMPClient.Extensions;
using AdobeMPClient.Models.Common;
using AdobeMPClient.Models.Notifications;
using AdobeMPClient.Routes;
using Duende.IdentityModel.Client;

namespace AdobeMPClient.Implementations;

public partial class AdobeClient
{
        public async Task<Result<NotificationResponse>> GetNotificationsAsync(NotificationRequest? parameters = null, CancellationToken ct = default)
        {
            var token = await GetAccessTokenAsync().ConfigureAwait(false);
    
            var requestUri = NotificationRoutes.Get(_adobeSettings.BaseUrl);
    
            if (parameters != null)
            {
                requestUri = requestUri
                .AddQueryParam("notification-type", parameters.NotificationType)
                .AddQueryParam("reseller-id", parameters.ResellerId)
                .AddQueryParam("limit", parameters.Limit)
                .AddQueryParam("offset", parameters.Offset);
            }
    
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.SetBearerToken(token.AccessToken!);
            SetHeaders(request);
    
            return await SendAsync<NotificationResponse>(request, ct).ConfigureAwait(false);
    }
}
