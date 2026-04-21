using AdobeMPClient.Extensions;


namespace AdobeMPClient.Routes;

public static class TransferRoutes
{
    private const string TransferTemplate = $"{AdobeApiVersion.V3}/transfers";

    public static string ResellerTransfer(string baseUrl)
        => new RouteBuilder(TransferTemplate)
            .Build(baseUrl);

    public static string GetReserllerTransfer(string baseUrl, string transferId)
        => new RouteBuilder($"{TransferTemplate}/{{transferId}}")
        .WithRouteValue("transferId", transferId)
            .Build(baseUrl);

}
