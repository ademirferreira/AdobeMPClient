using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Customer.Request;
using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.API.Endpoints.Customers;

public class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/api/customers/{customerId}", async (
            [FromRoute] string customerId,
            [FromBody] UpdateCustomer updateCustomer,
            IAdobeClient adobeClient,
            ILogger<Update> logger,
            CancellationToken ct) =>
        {
            logger.LogInformation("Updating customer {CustomerId}", customerId);

            var result = await adobeClient.UpdateCustomerAsync(customerId, updateCustomer, ct);

            logger.LogInformation("Customer {CustomerId} updated successfully", customerId);
            return result.ToResult();
        })
            .WithName("UpdateCustomer")
            .WithTags("customers")
            .WithSummary("Update a customer")
            .WithDescription("Updates an existing customer by their unique identifier")
            .Accepts<UpdateCustomer>("application/json")
            .Produces<AdobeMPClient.Models.Customer.CustomerResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}