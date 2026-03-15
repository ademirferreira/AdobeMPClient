using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Common;
using AdobeMPClient.Models.Customer.Request;
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
            return result.Match<IResult>(
                (data, statusCode) =>
                {
                    logger.LogInformation("Reseller {ResellerId} created successfully", data.ResellerId);
                    return TypedResults.Created($"/api/resellers/{data.ResellerId}", data);
                },
                (error, statusCode) =>
                {
                    logger.LogWarning("Failed to create reseller: {Error}", error?.Message);
                    var errorResult = Result<CreateCustomer>.Failure(error!, statusCode);
                    return errorResult.ToResult();
                }
            );
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
