using AdobeMPClient.Configuration;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Common;
using AdobeMPClient.Models.Customer;
using AdobeMPClient.Models.Customer.Request;
using AdobeMPClient.Models.PendingLicenses;
using AdobeMPClient.Models.Subscriptions;
using AdobeMPClient.Models.Subscriptions.Request;
using AdobeMPClient.Routes;
using Duende.IdentityModel.Client;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            var response = await _httpClient.SendAsync(request, ct).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                var adobeError = await response.Content.ReadFromJsonAsync<Error>(cancellationToken: ct).ConfigureAwait(false)
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

    public async Task<Result<CustomerResponse>> GetCustomerAsync(string customerId, CancellationToken ct)
    {
        var token = await GetAccessTokenAsync().ConfigureAwait(false);

        var requestUri = CustomerRoutes.Get(_adobeSettings.BaseUrl, customerId);

        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.SetBearerToken(token.AccessToken!);
        SetHeaders(request);

        return await SendAsync<CustomerResponse>(request, ct).ConfigureAwait(false);
    }
    public async Task<Result<CustomerResponse>> CreateCustomerAsync(CreateCustomer createCustomer, CancellationToken ct = default)
    {
        var token = await GetAccessTokenAsync().ConfigureAwait(false);

        var requestUri = CustomerRoutes.Create(_adobeSettings.BaseUrl);

        var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
        request.SetBearerToken(token.AccessToken!);
        SetHeaders(request);

        request.Content = JsonContent.Create(createCustomer, options: JsonOptions);

        return await SendAsync<CustomerResponse>(request, ct).ConfigureAwait(false);
    }

   

    public async Task<Result<CustomerResponse>> UpdateCustomerAsync(string customerId, UpdateCustomer updateCustomer, CancellationToken ct = default)
    {
        var token = await GetAccessTokenAsync().ConfigureAwait(false);

        var requestUri = CustomerRoutes.Update(_adobeSettings.BaseUrl, customerId);

        var request = new HttpRequestMessage(HttpMethod.Patch, requestUri);
        request.SetBearerToken(token.AccessToken!);
        SetHeaders(request);

        request.Content = JsonContent.Create(updateCustomer, options: JsonOptions);

        return await SendAsync<CustomerResponse>(request, ct).ConfigureAwait(false);
    }

    public async Task<Result<PendingLicense>> GetCustomerOpenAcquisitionsAsync(string customerId, CancellationToken ct = default)
    {
        var token = await GetAccessTokenAsync().ConfigureAwait(false);
        var requestUri = CustomerRoutes.OpenAcquisitions(_adobeSettings.BaseUrl, customerId);

        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.SetBearerToken(token.AccessToken!);
        SetHeaders(request);

        return await SendAsync<PendingLicense>(request, ct).ConfigureAwait(false);
    }

    public async Task<Result<Subscriptions>> GetSubscriptionsAsync(string customerId, CancellationToken ct = default)
    {
        var token = await GetAccessTokenAsync().ConfigureAwait(false);
        var requestUri = SubscriptionRoutes.GetAll(_adobeSettings.BaseUrl, customerId);

        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.SetBearerToken(token.AccessToken!);
        SetHeaders(request);

        return await SendAsync<Subscriptions>(request, ct).ConfigureAwait(false);
    }

    public async Task<Result<Subscription>> GetSubscriptionByIdAsync(string customerId, string subscriptionId, CancellationToken ct = default)
    {
        var token = await GetAccessTokenAsync().ConfigureAwait(false);
        var requestUri = SubscriptionRoutes.GetById(_adobeSettings.BaseUrl, customerId, subscriptionId);

        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.SetBearerToken(token.AccessToken!);
        SetHeaders(request);

        return await SendAsync<Subscription>(request, ct).ConfigureAwait(false);
    }

    public async Task<Result<Subscription>> CreateSubscriptionAsync(string customerId, CreateSubscription createSubscription, CancellationToken ct = default)
    {
        var token = await GetAccessTokenAsync().ConfigureAwait(false);
        var requestUri = SubscriptionRoutes.Create(_adobeSettings.BaseUrl, customerId);
        var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
        request.SetBearerToken(token.AccessToken!);
        SetHeaders(request);
        request.Content = JsonContent.Create(createSubscription, options: JsonOptions);
        return await SendAsync<Subscription>(request, ct).ConfigureAwait(false);
    }
}
