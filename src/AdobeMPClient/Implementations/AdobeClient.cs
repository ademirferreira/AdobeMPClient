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

public partial class AdobeClient(HttpClient httpClient, IOptions<AdobeSettings> options, IAdobeTokenProvider tokenProvider) : IAdobeClient
{
    private readonly AdobeSettings _adobeSettings = options.Value;

    private Task<TokenResponse> GetAccessTokenAsync(CancellationToken ct) => tokenProvider.GetAccessTokenAsync(ct);

    private void SetHeaders(HttpRequestMessage request)
    {
        var correlationId = System.Diagnostics.Activity.Current?.TraceId.ToString() ?? Guid.NewGuid().ToString();

        request.Headers.TryAddWithoutValidation("x-api-key", _adobeSettings.ApiKey);
        request.Headers.TryAddWithoutValidation("x-request-id", Guid.NewGuid().ToString());
        request.Headers.TryAddWithoutValidation("x-correlation-id", correlationId);
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
                Error adobeError;
                try
                {
                    adobeError = await response.Content.ReadFromJsonAsync<Error>(JsonOptions, cancellationToken: ct).ConfigureAwait(false)
                                 ?? new Error { Message = "Erro desconhecido" };
                }
                catch (JsonException)
                {
                    var raw = await response.Content.ReadAsStringAsync(ct).ConfigureAwait(false);
                    adobeError = new Error { Message = string.IsNullOrWhiteSpace(raw) ? "Erro desconhecido" : raw };
                }

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
