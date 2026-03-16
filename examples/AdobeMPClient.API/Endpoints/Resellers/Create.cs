using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Reseller;
using AdobeMPClient.Models.Reseller.Request;

namespace AdobeMPClient.API.Endpoints.Resellers;

public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/resellers", async (
            IAdobeClient client,
            CreateReseller request,
            ILogger<Create> logger,
            CancellationToken ct = default) =>
        {
            var result = await client.CreateResellerAsync(request, ct);

            logger.LogInformation("Creating a new reseller");
            return result
                .OnSuccess(data => logger.LogInformation("Reseller {ResellerId} created successfully", data.ResellerId))
                .OnFailure(error => logger.LogWarning("Failed to create reseller: {Error}", error?.Message))
                .ToResult(data => $"/api/resellers/{data.ResellerId}");
        }).WithName("CreateReseller")
            .WithTags("resellers")
            .WithSummary("Create a new reseller")
            .WithDescription("Creates a new reseller in the Adobe system")
            .Accepts<CreateReseller>("application/json")
            .Produces<Reseller>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}
