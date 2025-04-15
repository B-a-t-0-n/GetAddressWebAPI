using Microsoft.Extensions.DependencyInjection;
using Serilog.Events;
using Serilog;
using System.Reflection;
using GetAddressWebAPI.Options;
using GetAddressWebAPI.AddressServiceCommunication;
using GetAddressWebAPI.Features;

namespace GetAddressWebAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddLogger(
        this IServiceCollection services)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Debug()
            .Enrich.WithEnvironmentName()
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentUserName()
            .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
            .CreateLogger();

        services.AddSerilog();

        return services;
    }

    public static IServiceCollection AddDaDataService(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<DaDataOptions>(
           configuration.GetSection(DaDataOptions.DaData));

        services.AddHttpClient(DaDataOptions.DaDataClient, client =>
        {
            var daDataOptions = configuration.GetSection(DaDataOptions.DaData).Get<DaDataOptions>()
                ?? throw new ApplicationException("Missing DaData configuration");

            client.BaseAddress = new Uri(daDataOptions.Endpoint);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", $"Token {daDataOptions.AccessKey}");
            client.DefaultRequestHeaders.Add("X-Secret", daDataOptions.SecretKey);
        });

        services.AddScoped<IAddressService, AddressService>();

        return services;
    }

    public static IServiceCollection AddHandlers(
        this IServiceCollection services)
    {
        services.AddScoped<GetAddressHandler>();

        return services;
    }
}
