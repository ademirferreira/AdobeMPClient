using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Common;
using AdobeMPClient.Models.Memberships;
using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.API.Endpoints.Membership;

public class TransferDetails : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/memberships/{membershipId}/transfers/{transferId}", async (
            [FromRoute]string membershipId, 
            [FromRoute]string transferId, 
            IAdobeClient adobeClient,
            CancellationToken ct) =>
        {
            var result = await adobeClient.GetTransfer(membershipId, transferId, ct);
            return result.ToResult();
        }).WithName("GetTransferDetails")
          .WithTags("Memberships")
          .WithSummary("Get transfer details for a specific membership transfer.")
          .WithDescription("Retrieves the details of a specific transfer associated with a membership, including the transfer status and related information.")
          .Produces<TransferResponse>(StatusCodes.Status200OK)
          .Produces<Error>(StatusCodes.Status400BadRequest)
          .Produces<Error>(StatusCodes.Status404NotFound)
          .Produces<Error>(StatusCodes.Status500InternalServerError);
    }
}
