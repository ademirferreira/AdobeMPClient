using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Recommendations;
using Microsoft.AspNetCore.Mvc;

namespace AdobeMPClient.API.Endpoints.Recommendations;

public class Fetch : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/recommendations", async (
            [FromBody] FetchRecommendations fetchRecommendations,
            IAdobeClient adobeClient,
            ILogger<Fetch> logger,
            CancellationToken ct) =>
        {
            logger.LogInformation("Fetching recommendations for customer {CustomerId}", fetchRecommendations.CustomerId);
            var result = await adobeClient.FetchRecommendationsAsync(fetchRecommendations, ct);
            return result
                .OnSuccess(data => logger.LogInformation("Recommendations fetched successfully for customer {CustomerId}", fetchRecommendations.CustomerId))
                .OnFailure(error => logger.LogWarning("Failed to fetch recommendations for customer {CustomerId}: {Error}", fetchRecommendations.CustomerId, error?.Message))
                .ToResult();
        })
            .WithName("FetchRecommendations")
            .WithTags("recommendations")
            .WithSummary("Fetch recommendations for a customer")
            .WithDescription("Fetches personalized recommendations for a given customer based on their profile and behavior")
            .Accepts<FetchRecommendations>("application/json")
            .Produces<RecommendationsResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}
