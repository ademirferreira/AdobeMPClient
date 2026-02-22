using AdobeMPClient.Configuration;
using AdobeMPClient.Implementations;
using AdobeMPClient.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdobeMPClient.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAdobeClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AdobeSettings>(configuration.GetSection(AdobeSettings.SectionName));
        services.AddHttpClient<IAdobeClient, AdobeClient>("adobeClient");

        return services;
    }
}
