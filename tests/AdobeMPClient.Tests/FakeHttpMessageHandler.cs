using System.Net;
using System.Text;

namespace AdobeMPClient.Tests;

internal sealed class FakeHttpMessageHandler : HttpMessageHandler
{
    public int TokenRequestCount { get; private set; }
    public int ApiRequestCount { get; private set; }

    public Func<HttpRequestMessage, HttpResponseMessage>? TokenResponder { get; set; }
    public Func<HttpRequestMessage, HttpResponseMessage>? ApiResponder { get; set; }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (request.RequestUri!.AbsolutePath.Contains("/ims/token/"))
        {
            TokenRequestCount++;
            var responder = TokenResponder ?? (_ => TokenResponse());
            return Task.FromResult(responder(request));
        }

        ApiRequestCount++;
        if (ApiResponder is null)
            throw new InvalidOperationException("ApiResponder nao configurado para este teste.");

        return Task.FromResult(ApiResponder(request));
    }

    public static HttpResponseMessage TokenResponse(int expiresIn = 3600)
        => new(HttpStatusCode.OK)
        {
            Content = new StringContent(
                $$"""{"access_token":"fake-access-token","token_type":"Bearer","expires_in":{{expiresIn}}}""",
                Encoding.UTF8,
                "application/json")
        };

    public static HttpResponseMessage TokenErrorResponse()
        => new(HttpStatusCode.BadRequest)
        {
            Content = new StringContent(
                """{"error":"invalid_client","error_description":"Client authentication failed"}""",
                Encoding.UTF8,
                "application/json")
        };
}
