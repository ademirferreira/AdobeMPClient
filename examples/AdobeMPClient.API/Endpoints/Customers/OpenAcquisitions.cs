using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.PendingLicenses;
using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.API.Endpoints.Customers;

public class OpenAcquisitions : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/customers/{customerId}/open-acquisitions", async (
            [FromRoute] string customerId,
            IAdobeClient client,
            ILogger<OpenAcquisitions> logger,
            CancellationToken ct = default) =>
        {
            logger.LogInformation("Getting open acquisitions for customer {CustomerId}", customerId);

            var result = await client.GetCustomerOpenAcquisitionsAsync(customerId, ct);

            logger.LogInformation("Open acquisitions retrieved for customer {CustomerId}", customerId);
            return result.ToResult();
        })
        .WithName("GetCustomerOpenAcquisitions")
        .WithTags("customers")
        .WithSummary("Get customer open acquisitions")
        .WithDescription("Retrieves pending licenses/open acquisitions for a customer")
        .Produces<PendingLicense>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);
    }

}
