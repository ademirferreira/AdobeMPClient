using AdobeMPClient.Extensions;


namespace AdobeMPClient.Routes;

public static class TransferRoutes
{
    private const string TransferTemplate = "{apiVersion}/transfers";

    public static string ResellerTransfer(string apiVersion, string baseUrl)
        => new RouteBuilder(TransferTemplate)
            .WithRouteValue("apiVersion", apiVersion)
            .Build(baseUrl);

    public static string GetReserllerTransfer(string apiVersion, string baseUrl, string transferId)
        => new RouteBuilder($"{TransferTemplate}/{{transferId}}")
        .WithRouteValue("apiVersion", apiVersion)
        .WithRouteValue("transferId", transferId)
            .Build(baseUrl);

}
