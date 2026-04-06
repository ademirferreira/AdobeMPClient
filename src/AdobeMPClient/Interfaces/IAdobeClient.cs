using AdobeMPClient.Models.Common;
using AdobeMPClient.Models.Customer;
using AdobeMPClient.Models.Customer.Request;
using AdobeMPClient.Models.Orders;
using AdobeMPClient.Models.Orders.Request;
using AdobeMPClient.Models.PendingLicenses;
using AdobeMPClient.Models.Reseller;
using AdobeMPClient.Models.Reseller.Request;
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
    Task<Result<Subscription>> UpdateSubscriptionAsync(string customerId, string subscriptionId, UpdateSubscription updateSubscription, CancellationToken ct = default);

    Task<Result<OrderHistory>> GetOrderHistoryAsync(string customerId, GetOrderHistoryRequest? parameters = null, CancellationToken ct = default);

    Task<Result<Order>> GetOrderByIdAsync(string customerId, string orderId, bool? fetchPrice = null, CancellationToken ct = default);
    Task<Result<Order>> CreateOrderAsync(string customerId, CreateOrder createOrder, bool? fetchPrice = null, CancellationToken ct = default);
    Task<Result<Order>> UpdateOrderAsync(string customerId, string orderId, UpdateOrder updateOrder, CancellationToken ct = default);

    Task<Result<Reseller>> GetResellerAsync(string resellerId, CancellationToken ct = default);
    Task<Result<Resellers>> GetResellersAsync(GetResellersList? parameters = null, CancellationToken ct = default);

    Task<Result<Reseller>> CreateResellerAsync(CreateReseller createReseller, CancellationToken ct = default);
    Task<Result<Reseller>> UpdateResellerAsync(string resellerId, UpdateReseller updateReseller, CancellationToken ct = default);
}
