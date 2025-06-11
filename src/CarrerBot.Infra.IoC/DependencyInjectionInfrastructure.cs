using CarrerBot.Domain._enums;
using Microsoft.Extensions.DependencyInjection;
using CarrerBot.Infra.Data.Providers;

namespace CarrerBot.Infra.IoC;

/// <summary>
/// Defines the <see cref="DependencyInjectionInfrastructure" />.
/// </summary>
public static class DependencyInjectionInfrastructure
{
    /// <summary>
    /// The AddInfrastructure.
    /// </summary>
    /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">The configuration<see cref="ServiceConfigurations"/>.</param>
    public static void AddInfrastructure(this IServiceCollection services, ServiceConfigurations configuration)
    {
        services.AddScoped<IDbProvider>(pvd => new DbProvider(
                new DbConnections(new Dictionary<DbSchemaEnum, string> {
                        { DbSchemaEnum.DataSystem, configuration.ConnectionStrings.Sql.DataSystem },
                }),
                DbTypeEnum.PostgreSQL
            )
        );
    }
}
