namespace AdobeMPClient.Configuration;

public sealed class AdobeSettings
{
    public const string SectionName = "Adobe";
    public string Ims { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
}
