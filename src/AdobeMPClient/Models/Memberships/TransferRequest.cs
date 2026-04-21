namespace AdobeMPClient.Models.Memberships;

public sealed record TransferRequest
{
    public string ResellerId { get; init; } = string.Empty;
}
