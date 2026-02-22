# AdobeMPClient

> 🚧 **ACTIVE DEVELOPMENT**
>
> This library is in early development. The API may change without prior notice until version 1.0.0.

[![Status: In Development](https://img.shields.io/badge/status-in%20development-orange)](https://github.com/seu-usuario/AdobeMPClient)
[![.NET](https://img.shields.io/badge/.NET-10.0%2B-purple)](https://dotnet.microsoft.com/)


A .NET SDK/wrapper for the Adobe Marketplace Partner API. This library provides strongly-typed models and a fluent client interface for interacting with Adobe's marketplace services, including customer management, orders, subscriptions, and notifications.

## Features

- ✅ **Strongly-typed models** - All API responses and requests are represented as strongly-typed C# classes
- ✅ **Result-based error handling** - Uses `Result<T>` pattern for explicit success/failure handling
- ✅ **Dependency Injection ready** - Built with `Microsoft.Extensions.DependencyInjection` in mind
- ✅ **Async/await support** - All operations are asynchronous with `CancellationToken` support
- ✅ **Token caching** - Automatic OAuth token management with thread-safe caching
- ✅ **Comprehensive error handling** - Handles HTTP errors, network exceptions, and JSON parsing errors
- ✅ **Well-organized models** - Models organized by domain (Customer, Orders, Subscriptions, etc.)

## Installation

PackageReference in your `.csproj`:

```xml
<PackageReference Include="AdobeMPClient" Version="1.0.0" />
```

## Quick Start

### 1. Configure Settings

Add your Adobe API credentials to `appsettings.json`:

```json
{
  "Adobe": {
    "BaseUrl": "https://partnersandbox-stage.adobe.io/",
    "ApiKey": "your-api-key",
    "ClientSecret": "your-client-secret",
    "Ims": "https://ims-na1-stg1.adobelogin.com",
    "ImsOrg": "your-ims-org"
  }
}
```
### Required Settings

- `BaseUrl`: Base URL of the Adobe Marketplace Partner API
- `ApiKey`: Your Adobe API key
- `ClientSecret`: Your Adobe client secret
- `Ims`: Identity Management Service URL
- `ImsOrg`: Your IMS organization ID (optional, may be required depending on your setup)

### 2. Register Services

In your `Program.cs` or `Startup.cs`:

```csharp
using AdobeMPClient.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Register Adobe Client
builder.Services.AddAdobeClient(builder.Configuration);

var app = builder.Build();
```

### 3. Use the Client

Inject `IAdobeClient` into your services:

```csharp
using AdobeMPClient.Interfaces;
using AdobeMPClient.Models.Common;
using AdobeMPClient.Models.Customer;

public class CustomerService
{
    private readonly IAdobeClient _adobeClient;

    public CustomerService(IAdobeClient adobeClient)
    {
        _adobeClient = adobeClient;
    }

    public async Task<CustomerResponse?> GetCustomerAsync(string customerId)
    {
        var result = await _adobeClient.GetCustomerAsync(customerId);

        return result.Match(
            (customer, statusCode) => customer,
            (error, statusCode) =>
            {
                Console.WriteLine($"Error: {error?.Message}");
                return null;
            }
        );
    }
}
```

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

For issues, questions, or contributions, please open an issue on GitHub.

## Related Links

- [Adobe Marketplace Partner API Documentation](https://developer.adobe.com/vipmp/docs/)