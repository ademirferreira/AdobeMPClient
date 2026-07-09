using AdobeMPClient.Configuration;
using AdobeMPClient.Implementations;
using AdobeMPClient.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AdobeMPClient.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAdobeClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AdobeSettings>(configuration.GetSection(AdobeSettings.SectionName));
        services.AddSingleton<IValidateOptions<AdobeSettings>, AdobeSettingsValidator>();
        services.AddHttpClient("adobeIms");
        services.AddSingleton<IAdobeTokenProvider, AdobeTokenProvider>();
        services.AddHttpClient<IAdobeClient, AdobeClient>("adobeClient");

        return services;
    }
}
