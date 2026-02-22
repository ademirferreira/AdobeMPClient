using AdobeMPClient.Models.Common;
using AdobeMPClient.Models.Customer;
using AdobeMPClient.Models.Customer.Request;

namespace AdobeMPClient.Interfaces;

public interface IAdobeClient
{
    Task<Result<CustomerResponse>> GetCustomerAsync(string customerId, CancellationToken ct = default);
    Task<Result<CustomerResponse>> CreateCustomerAsync(CreateCustomer createCustomer, CancellationToken ct = default);
    Task<Result<CustomerResponse>> UpdateCustomerAsync(string customerId, UpdateCustomer updateCustomer, CancellationToken ct = default);
}
