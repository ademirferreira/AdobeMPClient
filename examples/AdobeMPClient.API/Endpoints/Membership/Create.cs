using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Common;
using AdobeMPClient.Models.Memberships;
using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.API.Endpoints.Membership;

public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/memberships/{membershipId}/transfers", async (
            [FromRoute] string membershipId,
            [FromBody] TransferRequest request,
            [FromQuery(Name = "ignore-order-return")] bool? ignoreOrderReturn,
            [FromQuery(Name = "expire-open-pas")] bool? expireOpenPas,
            IAdobeClient adobeClient,
            ILogger<Create> Logger,
            CancellationToken ct) =>
        {
            Logger.LogInformation("Initiating transfer for membershipId: {MembershipId} to resellerId: {ResellerId}", membershipId, request.ResellerId);
            var result = await adobeClient.CreateTransfer(request, membershipId, ignoreOrderReturn, expireOpenPas, ct);
            return result.ToResult();
        })
            .WithName("CreateTransfer")
            .WithTags("Membership")
            .WithSummary("Initiates a transfer for a given membership ID.")
            .WithDescription("Initiates a transfer from LWS to Marketplace for a given membership ID. This endpoint performs the actual transfer of the membership to the specified reseller.")
            .Produces<TransferResponse>(StatusCodes.Status202Accepted)
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .Produces<Error>(StatusCodes.Status404NotFound)
            .Produces<Error>(StatusCodes.Status500InternalServerError);
    }
}
