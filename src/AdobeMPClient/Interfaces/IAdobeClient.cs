using AdobeMPClient.Models.Common;
using AdobeMPClient.Models.Customer;

namespace AdobeMPClient.Interfaces;

public interface IAdobeClient
{
    Task<Result<CustomerResponse>> GetCustomerAsync(string customerId, CancellationToken ct = default);
}
