using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Common;
using AdobeMPClient.Models.Transfers;

namespace AdobeMPClient.API.Endpoints.Transfers;

public class TransferDetail : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/transfers/{transferId}", async (
            string transferId,
            IAdobeClient adobeClient,
            ILogger<TransferDetail> logger,
            CancellationToken ct) =>
        {
            logger.LogInformation("Getting details for transfer {TransferId}", transferId);
            var result = await adobeClient.GetResellerTransferDetailsAsync(transferId, ct);
            return result.ToResult();
                
        }).WithName("GetResellerTransfer")
            .WithTags("transfers")
            .WithSummary("Get reseller transfer details")
            .WithDescription("Retrieves the details of a reseller transfer request in the Adobe system")
            .Produces<ResellerTransferDetails>(StatusCodes.Status200OK)
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .Produces<Error>(StatusCodes.Status404NotFound)
            .Produces<Error>(StatusCodes.Status500InternalServerError);
    }
}
