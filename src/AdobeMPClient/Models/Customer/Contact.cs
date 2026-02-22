using System.Text.Json.Serialization;

namespace AdobeMPClient.Models.Customer;

public class Contact
{
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = string.Empty;
    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = string.Empty;
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
    [JsonPropertyName("phoneNumber")]
    public string? PhoneNumber { get; set; }
}
