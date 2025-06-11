using CarrerBot.Application;
using CarrerBot.Domain.Configuration;
using CarrerBot.Application.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CarrerBot.Infra.Data.Repositories;

namespace CarrerBot.Infra.IoC;
/// <summary>
/// Defines the <see cref="DependencyInjectionBusiness" />.
/// </summary>
public static class DependencyInjectionBusiness
{
    /// <summary>
    /// The AddInfrastructure.
    /// </summary>
    /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">The configuration<see cref="ServiceConfigurations"/>.</param>
    public static void AddBusinessInjection(this IServiceCollection services, ServiceConfigurations configuration)
    {
        services.AddTransient(_ => configuration);
        services.AddTransient<IMainService, MainService>();
        services.AddTransient<IConfigurationService, ConfigurationService>();
        services.AddTransient<IConfigurationRepository, ConfigurationRepository>();
        services.AddTransient<IServiceCollection>(o => services);
    }
}
