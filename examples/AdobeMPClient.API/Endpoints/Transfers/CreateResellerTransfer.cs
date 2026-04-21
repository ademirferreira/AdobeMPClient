using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Transfers;
using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.API.Endpoints.Transfers;

public class CreateResellerTransfer : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/transfers/reseller-change", async (
            [FromBody] ResellerTransferRequest resellerTransferRequest,
            IAdobeClient adobeClient,
            ILogger<CreateResellerTransfer> logger,
            CancellationToken ct) =>
        {
            logger.LogInformation("Creating reseller transfer for reseller {ResellerId}", resellerTransferRequest.ResellerId);

            var result = await adobeClient.CreateResellerChangeAsync(resellerTransferRequest, ct);

            return result
                .OnSuccess(data => logger.LogInformation("Reseller transfer {TransferId} created successfully", data.TransferId))
                .OnFailure(error => logger.LogWarning("Failed to create reseller transfer: {Error}", error?.Message))
                .ToResult(data => $"/api/transfers/{data.TransferId}");
        })
            .WithName("CreateResellerTransfer")
            .WithTags("transfers")
            .WithSummary("Create a reseller transfer")
            .WithDescription("Creates a reseller transfer request in the Adobe system")
            .Accepts<ResellerTransferRequest>("application/json")
            .Produces<ResellerTransferResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}
