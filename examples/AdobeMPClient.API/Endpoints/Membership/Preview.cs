using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Common;
using AdobeMPClient.Models.Memberships;
using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.API.Endpoints.Membership;

public class Preview : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/memberships/{membershipId}/offers", async (
            [FromRoute] string membershipId,
            [FromQuery(Name = "ignore-order-return")] bool? ignoreOrderReturn,
            [FromQuery(Name = "expire-open-pas")] bool? expireOpenPas,
            IAdobeClient adobeClient,
            ILogger<Preview> Logger,
            CancellationToken ct) =>
        {
            Logger.LogInformation("Previewing transfer for membershipId: {MembershipId}", membershipId);
            var result = await adobeClient.PreviewTransfer(membershipId, ignoreOrderReturn, expireOpenPas, ct);
            return result.ToResult();
        })
            .WithName("PreviewOffers")
            .WithTags("Membership")
            .WithSummary("Previews a transfer for a given membership ID.")
            .WithDescription("Previews a transfer from LWS to Marketplace for a given membership ID. This endpoint allows you to see the potential offers that would be available for transfer without actually performing the transfer.")
            .Produces<PreviewOffer>(StatusCodes.Status200OK)
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .Produces<Error>(StatusCodes.Status404NotFound)
            .Produces<Error>(StatusCodes.Status500InternalServerError);
    }
}
