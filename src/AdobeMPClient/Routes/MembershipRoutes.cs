using AdobeMPClient.Extensions;

namespace AdobeMPClient.Routes;

public static class MembershipRoutes
{
    private const string MembershipTemplate = $"{AdobeApiVersion.V3}/memberships";

    public static string Preview(string baseUrl, string membershipId)
        => new RouteBuilder($"{MembershipTemplate}/{{membershipId}}/offers")
            .WithRouteValue("membershipId", membershipId)
            .Build(baseUrl);

    public static string CreateTransfer(string baseUrl, string membershipId)
    => new RouteBuilder($"{MembershipTemplate}/{{membershipId}}/transfers")
        .WithRouteValue("membershipId", membershipId)
        .Build(baseUrl);

    public static string TransferDetails(string baseUrl, string membershipId, string transferId)
    => new RouteBuilder($"{MembershipTemplate}/{{membershipId}}/transfers/{{transferId}}")
        .WithRouteValue("membershipId", membershipId)
        .WithRouteValue("transferId", transferId)
        .Build(baseUrl);
}
