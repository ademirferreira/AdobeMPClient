using AdobeMPClient.Extensions;

namespace AdobeMPClient.Routes;

public static class MembershipRoutes
{
    private const string MembershipTemplate = "{apiVersion}/memberships";

    public static string Preview(string apiVersion, string baseUrl, string membershipId)
        => new RouteBuilder($"{MembershipTemplate}/{{membershipId}}/offers")
            .WithRouteValue("apiVersion", apiVersion)
            .WithRouteValue("membershipId", membershipId)
            .Build(baseUrl);

    public static string CreateTransfer(string apiVersion, string baseUrl, string membershipId)
    => new RouteBuilder($"{MembershipTemplate}/{{membershipId}}/transfers")
        .WithRouteValue("apiVersion", apiVersion)
        .WithRouteValue("membershipId", membershipId)
        .Build(baseUrl);

    public static string TransferDetails(string apiVersion, string baseUrl, string membershipId, string transferId)
    => new RouteBuilder($"{MembershipTemplate}/{{membershipId}}/transfers/{{transferId}}")
        .WithRouteValue("apiVersion", apiVersion)
        .WithRouteValue("membershipId", membershipId)
        .WithRouteValue("transferId", transferId)
        .Build(baseUrl);
}
