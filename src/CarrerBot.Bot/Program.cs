using Serilog;
using CarrerBot.Infra.IoC;

namespace CarrerBot.Bot;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            CreateHostBuilder(args).Build().Run();
        }
        catch (OperationCanceledException)
        {
            Log.Information("Application has been request to shutdown");
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application start-up failed");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
    public static IHostBuilder CreateHostBuilder(string[] args) =>

    Host.CreateDefaultBuilder(args)

    .UseWindowsService()
    .ConfigureLogging(host => host.AddSerilog())
    .ConfigureServices((hostContext, services) =>
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(hostContext.Configuration)
            .CreateLogger();
        Log.Information("Starting up");
        services.AddOptions();
        services.AddInfrastructure(hostContext.Configuration.GetSection("ServiceConfigurations").Get<ServiceConfigurations>());
        services.AddBusinessInjection(hostContext.Configuration.GetSection("ServiceConfigurations").Get<ServiceConfigurations>());
        services.AddHostedService<Startup>();
    })
    .UseSerilog();
}
