using AdobeMPClient.Extensions;

namespace AdobeMPClient.Routes;

public static class MembershipRoutes
{
    private const string MembershipTemplate = $"{AdobeApiVersion.V3}/memberships";

    public static string Preview(string baseUrl, string membershipId)
        => new RouteBuilder($"{MembershipTemplate}/{{membershipId}}/offers")
            .WithRouteValue("membershipId", membershipId)
            .Build(baseUrl);
}
