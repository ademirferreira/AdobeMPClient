using Duende.IdentityModel.Client;

namespace AdobeMPClient.Interfaces;

public interface IAdobeTokenProvider
{
    Task<TokenResponse> GetAccessTokenAsync(CancellationToken ct);
}
