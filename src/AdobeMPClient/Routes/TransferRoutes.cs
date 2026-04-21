using AdobeMPClient.Extensions;


namespace AdobeMPClient.Routes;

public static class TransferRoutes
{
    private const string TransferTemplate = $"{AdobeApiVersion.V3}/transfers";

    public static string ResellerTransfer(string baseUrl)
        => new RouteBuilder(TransferTemplate)
            .Build(baseUrl);

}
