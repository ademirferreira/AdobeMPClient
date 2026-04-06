using AdobeMPClient.Models.Common;
using AdobeMPClient.Models.Customer;
using AdobeMPClient.Models.Customer.Request;
using AdobeMPClient.Models.PendingLicenses;
using AdobeMPClient.Routes;
using Duende.IdentityModel.Client;
using System.Net.Http.Json;

namespace AdobeMPClient.Implementations;

public partial class AdobeClient
{
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
}
