using Microsoft.Extensions.Options;

namespace AdobeMPClient.Configuration;

public sealed class AdobeSettingsValidator : IValidateOptions<AdobeSettings>
{
    public ValidateOptionsResult Validate(string? name, AdobeSettings options)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(options.Ims) || !Uri.TryCreate(options.Ims, UriKind.Absolute, out _))
        {
            errors.Add($"{nameof(AdobeSettings.Ims)} deve ser uma URI absoluta valida.");
        }

        if (string.IsNullOrWhiteSpace(options.BaseUrl) || !Uri.TryCreate(options.BaseUrl, UriKind.Absolute, out _))
        {
            errors.Add($"{nameof(AdobeSettings.BaseUrl)} deve ser uma URI absoluta valida.");
        }

        if (string.IsNullOrWhiteSpace(options.ApiKey))
        {
            errors.Add($"{nameof(AdobeSettings.ApiKey)} nao pode ser vazio.");
        }

        if (string.IsNullOrWhiteSpace(options.ClientSecret))
        {
            errors.Add($"{nameof(AdobeSettings.ClientSecret)} nao pode ser vazio.");
        }

        return errors.Count == 0
            ? ValidateOptionsResult.Success
            : ValidateOptionsResult.Fail(errors);
    }
}
