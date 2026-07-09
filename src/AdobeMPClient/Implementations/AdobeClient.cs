using AdobeMPClient.Configuration;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Common;
using Duende.IdentityModel.Client;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AdobeMPClient.Implementations;

public sealed class AdobeAuthenticationException(string message, Exception? inner = null)
    : Exception(message, inner);

public partial class AdobeClient(HttpClient httpClient, IOptions<AdobeSettings> options) : IAdobeClient
{
    private readonly AdobeSettings _adobeSettings = options.Value;

    private readonly SemaphoreSlim _tokenSemaphore = new(1, 1);

    private volatile TokenResponse? _currentToken;
    private long _tokenExpirationTicks;
    private const int TokenExpirationBufferSeconds = 30;
    private async Task<TokenResponse> GetAccessTokenAsync()
    {
        var expiration = new DateTime(Interlocked.Read(ref _tokenExpirationTicks), DateTimeKind.Utc);
        if (_currentToken != null && !string.IsNullOrEmpty(_currentToken.AccessToken) && DateTime.UtcNow < expiration)
        {
            return _currentToken;
        }

        await _tokenSemaphore.WaitAsync().ConfigureAwait(false);

        try
        {
            var expirationInner = new DateTime(Interlocked.Read(ref _tokenExpirationTicks), DateTimeKind.Utc);
            if (_currentToken != null
                && !string.IsNullOrEmpty(_currentToken.AccessToken)
                && DateTime.UtcNow < expirationInner)
            {
                return _currentToken;
            }

            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = $"{_adobeSettings.Ims}/ims/token/v3",
                ClientId = _adobeSettings.ApiKey,
                ClientSecret = _adobeSettings.ClientSecret,
                Scope = "openid,AdobeID,read_organizations"
            }).ConfigureAwait(false);

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
    private void SetHeaders(HttpRequestMessage request)
    {
        request.Headers.Add("x-api-key", _adobeSettings.ApiKey);
        request.Headers.Add("x-request-id", Guid.NewGuid().ToString());
        request.Headers.Add("x-correlation-id", Guid.NewGuid().ToString());
    }

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private async Task<Result<T>> SendAsync<T>(HttpRequestMessage request, CancellationToken ct)
    {
        try
        {
            var response = await httpClient.SendAsync(request, ct).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                var adobeError = await response.Content.ReadFromJsonAsync<Error>(JsonOptions, cancellationToken: ct).ConfigureAwait(false)
                               ?? new Error { Message = "Erro desconhecido" };

                return Result<T>.Failure(adobeError, (int)response.StatusCode);
            }
            var result = await response.Content.ReadFromJsonAsync<T>(JsonOptions, ct).ConfigureAwait(false);

            return Result<T>.Success(result!, (int)response.StatusCode);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (HttpRequestException ex)
        {
            var error = new Error { Message = $"Erro na requisição HTTP para Adobe: {ex.Message}" };
            return Result<T>.Failure(error, 503);
        }
        catch (JsonException ex)
        {
            var error = new Error { Message = $"Erro ao desserializar resposta da Adobe: {ex.Message}" };
            return Result<T>.Failure(error, 502);
        }
    }

}
