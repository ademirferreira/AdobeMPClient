namespace AdobeMPClient.Models.Orders.Request;

public sealed record UpdateOrder
{
    public string ExternalReferenceId { get; init; } = string.Empty;
}
