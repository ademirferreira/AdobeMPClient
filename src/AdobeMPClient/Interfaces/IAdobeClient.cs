using AdobeMPClient.Models.Common;
using AdobeMPClient.Models.Customer;
using AdobeMPClient.Models.Customer.Request;
using AdobeMPClient.Models.PendingLicenses;
using AdobeMPClient.Models.Subscriptions;
using AdobeMPClient.Models.Subscriptions.Request;

namespace AdobeMPClient.Interfaces;

public interface IAdobeClient
{
    Task<Result<CustomerResponse>> GetCustomerAsync(string customerId, CancellationToken ct = default);
    Task<Result<CustomerResponse>> CreateCustomerAsync(CreateCustomer createCustomer, CancellationToken ct = default);
    Task<Result<CustomerResponse>> UpdateCustomerAsync(string customerId, UpdateCustomer updateCustomer, CancellationToken ct = default);
    Task<Result<PendingLicense>> GetCustomerOpenAcquisitionsAsync(string customerId, CancellationToken ct = default);

    Task<Result<Subscriptions>> GetSubscriptionsAsync(string customerId, CancellationToken ct = default);
    Task<Result<Subscription>> GetSubscriptionByIdAsync(string customerId, string subscriptionId, CancellationToken ct = default);
    Task<Result<Subscription>> CreateSubscriptionAsync(string customerId, CreateSubscription createSubscription, CancellationToken ct = default);

}
