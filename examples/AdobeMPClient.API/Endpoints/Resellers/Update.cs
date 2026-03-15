using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Reseller;
using AdobeMPClient.Models.Reseller.Request;
using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.API.Endpoints.Resellers;

public class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/api/resellers/{resellerId}", async (
            [FromRoute] string resellerId,
            [FromBody] UpdateReseller updateReseller,
            IAdobeClient adobeClient,
            ILogger<Update> logger,
            CancellationToken ct) =>
        {
            logger.LogInformation("Updating reseller {ResellerId}", resellerId);
            var result = await adobeClient.UpdateResellerAsync(resellerId, updateReseller, ct);
            logger.LogInformation("Reseller {ResellerId} updated successfully", resellerId);
            return result.ToResult();
        }).WithName("UpdateReseller")
        .WithTags("resellers")
        .WithSummary("Update a reseller's information")
        .WithDescription("Updates the information of an existing reseller. You can update the reseller's company profile and external reference ID.")
        .Accepts<UpdateReseller>("application/json")
        .Produces<Reseller>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}
