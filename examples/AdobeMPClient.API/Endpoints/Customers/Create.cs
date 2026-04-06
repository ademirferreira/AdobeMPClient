using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Customer;
using AdobeMPClient.Models.Customer.Request;
using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.API.Endpoints.Customers;

public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/customers", async (
            [FromBody] CreateCustomer createCustomer,
            IAdobeClient adobeClient,
            ILogger<Create> logger,
            CancellationToken ct) =>
        {
            logger.LogInformation("Creating new customer");

            var result = await adobeClient.CreateCustomerAsync(createCustomer, ct);

            return result
                .OnSuccess(data => logger.LogInformation("Customer {CustomerId} created successfully", data.CustomerId))
                .OnFailure(error => logger.LogWarning("Failed to create customer: {Error}", error?.Message))
                .ToResult(data => $"/api/customers/{data.CustomerId}");
        })
            .WithName("CreateCustomer")
            .WithTags("customers")
            .WithSummary("Create a new customer")
            .WithDescription("Creates a new customer in the Adobe system")
            .Accepts<CreateCustomer>("application/json")
            .Produces<CustomerResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}