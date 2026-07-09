using AdobeMPClient.Configuration;
using AdobeMPClient.Interfaces;
using Duende.IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace AdobeMPClient.Implementations;

public sealed class AdobeTokenProvider(IHttpClientFactory httpClientFactory, IOptions<AdobeSettings> options)
    : IAdobeTokenProvider, IDisposable
{
    private readonly AdobeSettings _adobeSettings = options.Value;

    private readonly SemaphoreSlim _tokenSemaphore = new(1, 1);

    private volatile TokenResponse? _currentToken;
    private long _tokenExpirationTicks;
    private const int TokenExpirationBufferSeconds = 30;

    public async Task<TokenResponse> GetAccessTokenAsync(CancellationToken ct)
    {
        var expiration = new DateTime(Interlocked.Read(ref _tokenExpirationTicks), DateTimeKind.Utc);
        if (_currentToken != null && !string.IsNullOrEmpty(_currentToken.AccessToken) && DateTime.UtcNow < expiration)
        {
            return _currentToken;
        }

        await _tokenSemaphore.WaitAsync(ct).ConfigureAwait(false);

        try
        {
            var expirationInner = new DateTime(Interlocked.Read(ref _tokenExpirationTicks), DateTimeKind.Utc);
            if (_currentToken != null
                && !string.IsNullOrEmpty(_currentToken.AccessToken)
                && DateTime.UtcNow < expirationInner)
            {
                return _currentToken;
            }

            var httpClient = httpClientFactory.CreateClient("adobeIms");
            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = $"{_adobeSettings.Ims}/ims/token/v3",
                ClientId = _adobeSettings.ApiKey,
                ClientSecret = _adobeSettings.ClientSecret,
                Scope = "openid,AdobeID,read_organizations"
            }, ct).ConfigureAwait(false);

            if (tokenResponse.IsError)
            {
                throw new AdobeAuthenticationException(
                    $"Falha na autenticação Adobe: {tokenResponse.Error} - {tokenResponse.ErrorDescription}",
                    tokenResponse.Exception);
            }

            _currentToken = tokenResponse;
            Interlocked.Exchange(
                ref _tokenExpirationTicks,
                DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn - TokenExpirationBufferSeconds).Ticks);

            return tokenResponse;
        }
        finally
        {
            _tokenSemaphore.Release();
        }
    }

    public void Dispose()
    {
        _tokenSemaphore.Dispose();
    }
}
