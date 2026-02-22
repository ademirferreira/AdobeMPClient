using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Common;

public class Error
{
    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("additionalDetails")]
    public List<string> AdditionalDetails { get; set; } = [];

    [JsonPropertyName("invalidFields")]
    public List<string> InvalidFields { get; set; } = [];
}

