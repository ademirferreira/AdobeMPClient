using AdobeMPClient.Configuration;
using AdobeMPClient.Interfaces;
using Duende.IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace AdobeMPClient.Implementations;

public class AdobeClient(HttpClient httpClient, IOptions<AdobeSettings> options) : IAdobeClient
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly AdobeSettings _adobeSettings = options.Value;

    private readonly SemaphoreSlim _tokenSemaphore = new(1, 1);

    private TokenResponse? _currentToken;
    private DateTime _tokenExpiration;

    private async Task<TokenResponse> GetAccessTokenAsync()
    {
        if (_currentToken != null && !string.IsNullOrEmpty(_currentToken.AccessToken) && DateTime.UtcNow < _tokenExpiration)
        {
            return _currentToken;
        }

        await _tokenSemaphore.WaitAsync().ConfigureAwait(false);

        try
        {
            if (_currentToken != null && !string.IsNullOrEmpty(_currentToken.AccessToken) && DateTime.UtcNow < _tokenExpiration)
            {
                return _currentToken;
            }

            var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = $"{_adobeSettings.Ims}/ims/token/v3",
                ClientId = _adobeSettings.ApiKey,
                ClientSecret = _adobeSettings.ClientSecret,
                Scope = "openid,AdobeID,read_organizations"
            }).ConfigureAwait(false);

            if (tokenResponse.IsError)
            {
                throw new Exception($"Falha na autenticação Adobe: {tokenResponse.Error} - {tokenResponse.ErrorDescription}", tokenResponse.Exception);
            }

            _currentToken = tokenResponse;
            _tokenExpiration = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn);

            return tokenResponse;
        }
        finally
        {
            _tokenSemaphore.Release();
        }
    }
}
