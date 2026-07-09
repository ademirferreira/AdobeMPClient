using AdobeMPClient.Configuration;

namespace AdobeMPClient.Tests;

public class AdobeSettingsValidatorTests
{
    private static readonly AdobeSettingsValidator Validator = new();

    [Fact]
    public void Validate_Fails_WhenClientSecretIsEmpty()
    {
        var settings = new AdobeSettings
        {
            Ims = "https://ims.test",
            BaseUrl = "https://api.test",
            ApiKey = "test-api-key",
            ClientSecret = ""
        };

        var result = Validator.Validate(name: null, settings);

        Assert.True(result.Failed);
        Assert.Contains(result.Failures!, f => f.Contains(nameof(AdobeSettings.ClientSecret)));
    }

    [Fact]
    public void Validate_Fails_WhenImsIsNotAnAbsoluteUri()
    {
        var settings = new AdobeSettings
        {
            Ims = "not-a-uri",
            BaseUrl = "https://api.test",
            ApiKey = "test-api-key",
            ClientSecret = "test-client-secret"
        };

        var result = Validator.Validate(name: null, settings);

        Assert.True(result.Failed);
        Assert.Contains(result.Failures!, f => f.Contains(nameof(AdobeSettings.Ims)));
    }

    [Fact]
    public void Validate_Succeeds_WhenAllFieldsAreValid()
    {
        var settings = new AdobeSettings
        {
            Ims = "https://ims.test",
            BaseUrl = "https://api.test",
            ApiKey = "test-api-key",
            ClientSecret = "test-client-secret"
        };

        var result = Validator.Validate(name: null, settings);

        Assert.False(result.Failed);
    }
}
