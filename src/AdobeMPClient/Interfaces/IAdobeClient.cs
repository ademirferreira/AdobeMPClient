using AdobeMPClient.Models.Common;
using AdobeMPClient.Models.Customer;
using AdobeMPClient.Models.Customer.Request;
using AdobeMPClient.Models.FlexDiscounts;
using AdobeMPClient.Models.Memberships;
using AdobeMPClient.Models.Notifications;
using AdobeMPClient.Models.Orders;
using AdobeMPClient.Models.Orders.Request;
using AdobeMPClient.Models.PendingLicenses;
using AdobeMPClient.Models.PriceList;
using AdobeMPClient.Models.Recommendations;
using AdobeMPClient.Models.Reseller;
using AdobeMPClient.Models.Reseller.Request;
using AdobeMPClient.Models.Subscriptions;
using AdobeMPClient.Models.Subscriptions.Request;
using AdobeMPClient.Models.Transfers;

namespace AdobeMPClient.Interfaces;

public interface IAdobeClient
{
    Task<Result<CustomerResponse>> GetCustomerAsync(string customerId, CancellationToken ct = default);
    Task<Result<CustomerResponse>> CreateCustomerAsync(CreateCustomer createCustomer, CancellationToken ct = default);
    Task<Result<CustomerResponse>> UpdateCustomerAsync(string customerId, UpdateCustomer updateCustomer, CancellationToken ct = default);
    Task<Result<PendingLicense>> GetCustomerOpenAcquisitionsAsync(string customerId, CancellationToken ct = default);
    Task<Result<FlexDiscountResponse>> GetCustomerFlexDiscountsAsync(string customerId, int? limit, int? offset, CancellationToken ct = default);

    Task<Result<Subscriptions>> GetSubscriptionsAsync(string customerId, CancellationToken ct = default);
    Task<Result<Subscription>> GetSubscriptionByIdAsync(string customerId, string subscriptionId, CancellationToken ct = default);
    Task<Result<Subscription>> CreateSubscriptionAsync(string customerId, CreateSubscription createSubscription, CancellationToken ct = default);
    Task<Result<Subscription>> UpdateSubscriptionAsync(string customerId, string subscriptionId, UpdateSubscription updateSubscription, CancellationToken ct = default);
    Task<Result<Subscription>> RemoveFlexDiscountAsync(string customerId, string subscriptionId, CancellationToken ct = default);

    Task<Result<OrderHistory>> GetOrderHistoryAsync(string customerId, GetOrderHistoryRequest? parameters = null, CancellationToken ct = default);

    Task<Result<Order>> GetOrderByIdAsync(string customerId, string orderId, bool? fetchPrice = null, CancellationToken ct = default);
    Task<Result<Order>> CreateOrderAsync(string customerId, CreateOrder createOrder, bool? fetchPrice = null, CancellationToken ct = default);
    Task<Result<Order>> UpdateOrderAsync(string customerId, string orderId, UpdateOrder updateOrder, CancellationToken ct = default);

    Task<Result<Reseller>> GetResellerAsync(string resellerId, CancellationToken ct = default);
    Task<Result<Resellers>> GetResellersAsync(GetResellersList? parameters = null, CancellationToken ct = default);

    Task<Result<Reseller>> CreateResellerAsync(CreateReseller createReseller, CancellationToken ct = default);
    Task<Result<Reseller>> UpdateResellerAsync(string resellerId, UpdateReseller updateReseller, CancellationToken ct = default);

    Task<Result<RecommendationsResponse>> FetchRecommendationsAsync(FetchRecommendations fetchRecommendations, CancellationToken ct = default);

    Task<Result<NotificationResponse>> GetNotificationsAsync(NotificationRequest? parameters = null, CancellationToken ct = default);

    Task<Result<FlexDiscountResponse>> FetchFlexDiscountsAsync(FlexDiscountRequest? parameters = null, CancellationToken ct = default);

    Task<Result<PriceListResponse>> FetchPriceListAsync(PriceListRequest? parameters = null, int? limit = null, int? offset = null, CancellationToken ct = default);

    Task<Result<ResellerTransferResponse>> CreateResellerChangeAsync(ResellerTransferRequest resellerChangeRequest, CancellationToken ct = default);

    Task<Result<ResellerTransferDetails>> GetResellerTransferDetailsAsync(string transferId, CancellationToken ct = default);

    Task<Result<PreviewOffer>> PreviewTransfer(string membershipId, bool? ignoreOrderReturn = null, bool? expireOpenPas = null, CancellationToken ct = default);
    Task<Result<TransferResponse>> CreateTransfer(TransferRequest transferRequest, string membershipId, bool? ignoreOrderReturn = null, bool? expireOpenPas = null, CancellationToken ct = default);
}
