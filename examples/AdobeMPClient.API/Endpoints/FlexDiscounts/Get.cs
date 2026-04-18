using AdobeMPClient.API.Extensions;
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Common;
using AdobeMPClient.Models.FlexDiscounts;

namespace AdobeMPClient.API.Endpoints.FlexDiscounts;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/flex-discounts", async ([AsParameters]FlexDiscountRequest request, IAdobeClient client, CancellationToken ct) =>
        {
            var result = await client.FetchFlexDiscountsAsync(request, ct).ConfigureAwait(false);
            return result.ToResult();
        }).WithName("GetFlexDiscounts").WithTags("Flex Discounts")
        .WithSummary("Fetches a list of available flex discounts based on the provided filters.")
        .WithDescription("This endpoint allows you to retrieve a list of flex discounts that match the specified criteria, such as categories, market segment, country, offer IDs, and more. You can also specify a date range and pagination parameters to control the results.")
        .Produces<FlexDiscountResponse>(StatusCodes.Status200OK)
        .Produces<Error>(StatusCodes.Status400BadRequest)
        .Produces<Error>(StatusCodes.Status500InternalServerError);
    }
}
