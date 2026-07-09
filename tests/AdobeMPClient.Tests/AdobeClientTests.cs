using System.Net;
using System.Text;
using AdobeMPClient.Configuration;
using AdobeMPClient.Extensions;
using AdobeMPClient.Implementations;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Subscriptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AdobeMPClient.Tests;

public class AdobeClientTests
{
    private static (AdobeClient Client, FakeHttpMessageHandler Handler, IAdobeTokenProvider TokenProvider) CreateClient()
    {
        var handler = new FakeHttpMessageHandler();
        var httpClient = new HttpClient(handler);
        var settings = Options.Create(new AdobeSettings
        {
            Ims = "https://ims.test",
            BaseUrl = "https://api.test",
            ApiKey = "test-api-key",
            ClientSecret = "test-client-secret"
        });

        var tokenProvider = new AdobeTokenProvider(new FakeHttpClientFactory(handler), settings);

        return (new AdobeClient(httpClient, settings, tokenProvider), handler, tokenProvider);
    }

    private static HttpResponseMessage JsonResponse(HttpStatusCode statusCode, string json)
        => new(statusCode)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

    [Fact]
    public async Task GetSubscriptionsAsync_ReusesCachedToken_AcrossMultipleCalls()
    {
        var (client, handler, _) = CreateClient();
        handler.TokenResponder = _ => FakeHttpMessageHandler.TokenResponse(expiresIn: 3600);
        handler.ApiResponder = _ => JsonResponse(HttpStatusCode.OK, """{"totalCount":0,"items":[]}""");

        await client.GetSubscriptionsAsync("customer-1");
        await client.GetSubscriptionsAsync("customer-1");

        Assert.Equal(1, handler.TokenRequestCount);
        Assert.Equal(2, handler.ApiRequestCount);
    }

    [Fact]
    public async Task GetSubscriptionsAsync_RefreshesToken_WhenExpirationBufferAlreadyElapsed()
    {
        var (client, handler, _) = CreateClient();
        // expires_in menor que o buffer de 30s: a expiracao calculada ja fica no passado.
        handler.TokenResponder = _ => FakeHttpMessageHandler.TokenResponse(expiresIn: 10);
        handler.ApiResponder = _ => JsonResponse(HttpStatusCode.OK, """{"totalCount":0,"items":[]}""");

        await client.GetSubscriptionsAsync("customer-1");
        await client.GetSubscriptionsAsync("customer-1");

        Assert.Equal(2, handler.TokenRequestCount);
    }

    [Fact]
    public async Task GetSubscriptionsAsync_ThrowsAdobeAuthenticationException_WhenTokenRequestFails()
    {
        var (client, handler, _) = CreateClient();
        handler.TokenResponder = _ => FakeHttpMessageHandler.TokenErrorResponse();

        await Assert.ThrowsAsync<AdobeAuthenticationException>(
            () => client.GetSubscriptionsAsync("customer-1"));
    }

    [Fact]
    public async Task GetSubscriptionsAsync_ReturnsSuccess_WhenApiRespondsWithValidJson()
    {
        var (client, handler, _) = CreateClient();
        handler.TokenResponder = _ => FakeHttpMessageHandler.TokenResponse();
        handler.ApiResponder = _ => JsonResponse(HttpStatusCode.OK, """{"totalCount":1,"items":[]}""");

        var result = await client.GetSubscriptionsAsync("customer-1");

        Assert.True(result.IsSuccess);
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(1, result.Data!.TotalCount);
    }

    [Fact]
    public async Task GetSubscriptionsAsync_ReturnsFailure_WhenApiRespondsWithValidJsonError()
    {
        var (client, handler, _) = CreateClient();
        handler.TokenResponder = _ => FakeHttpMessageHandler.TokenResponse();
        handler.ApiResponder = _ => JsonResponse(
            HttpStatusCode.BadRequest,
            """{"code":"invalid_request","message":"Bad request"}""");

        var result = await client.GetSubscriptionsAsync("customer-1");

        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.StatusCode);
        Assert.NotNull(result.Error);
        Assert.Equal("invalid_request", result.Error!.Code);
        Assert.Equal("Bad request", result.Error.Message);
    }

    [Fact]
    public async Task GetSubscriptionsAsync_PreservesRealHttpStatus_WhenErrorBodyIsNotJson()
    {
        var (client, handler, _) = CreateClient();
        handler.TokenResponder = _ => FakeHttpMessageHandler.TokenResponse();
        handler.ApiResponder = _ => new HttpResponseMessage(HttpStatusCode.TooManyRequests)
        {
            Content = new StringContent("Too Many Requests", Encoding.UTF8, "text/plain")
        };

        var result = await client.GetSubscriptionsAsync("customer-1");

        Assert.False(result.IsSuccess);
        Assert.Equal(429, result.StatusCode);
        Assert.Contains("Too Many Requests", result.Error!.Message);
    }

    [Fact]
    public async Task GetSubscriptionsAsync_ReturnsServiceUnavailable_WhenHttpRequestFails()
    {
        var (client, handler, _) = CreateClient();
        handler.TokenResponder = _ => FakeHttpMessageHandler.TokenResponse();
        handler.ApiResponder = _ => throw new HttpRequestException("connection reset");

        var result = await client.GetSubscriptionsAsync("customer-1");

        Assert.False(result.IsSuccess);
        Assert.Equal(503, result.StatusCode);
    }

    [Fact]
    public async Task GetSubscriptionsAsync_ThrowsOperationCanceledException_WhenTokenAlreadyCancelled()
    {
        var (client, _, _) = CreateClient();
        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();

        await Assert.ThrowsAsync<TaskCanceledException>(
            () => client.GetSubscriptionsAsync("customer-1", cts.Token));
    }

    [Fact]
    public void Dispose_DoesNotThrow_EvenWhenCalledMultipleTimes()
    {
        var (_, _, tokenProvider) = CreateClient();
        var disposable = Assert.IsAssignableFrom<IDisposable>(tokenProvider);

        var exception = Record.Exception(() =>
        {
            disposable.Dispose();
            disposable.Dispose();
        });

        Assert.Null(exception);
    }

    [Fact]
    public void AddAdobeClient_ResolvesIAdobeTokenProvider_AsSingleton()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Adobe:Ims"] = "https://ims.test",
                ["Adobe:BaseUrl"] = "https://api.test",
                ["Adobe:ApiKey"] = "test-api-key",
                ["Adobe:ClientSecret"] = "test-client-secret"
            })
            .Build();

        var services = new ServiceCollection();
        services.AddAdobeClient(configuration);

        using var provider = services.BuildServiceProvider();

        var first = provider.GetRequiredService<IAdobeTokenProvider>();
        var second = provider.GetRequiredService<IAdobeTokenProvider>();

        Assert.Same(first, second);
    }
}
