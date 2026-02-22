using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Customer;
using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.API.Endpoints.Customers;

public class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/customers/{customerId}", async (
            [FromRoute] string customerId,
            IAdobeClient client,
            ILogger<GetById> logger,
            CancellationToken ct = default) =>
        {
            logger.LogInformation("Getting customer {CustomerId}", customerId);

            var result = await client.GetCustomerAsync(customerId, ct);

            logger.LogInformation("Customer {CustomerId} retrieved successfully", customerId);
            return result.ToResult();
        })
        .WithName("GetCustomerById")
        .WithTags("customers")
        .WithSummary("Get customer by ID")
        .WithDescription("Retrieves a customer by their unique identifier")
        .Produces<CustomerResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}
