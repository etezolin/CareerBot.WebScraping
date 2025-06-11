using System.Diagnostics;
using CarrerBot.Application;
using CarrerBot.Application.Configuration;
using CarrerBot.Infra.IoC;

namespace CarrerBot.Bot;

public class Startup : BackgroundService
{
    #region Properties
    private static IConfigurationService _configurationService;
    private readonly IMainService _mainService;
    private readonly ServiceConfigurations _serviceConfigurations;
    private static ILogger<Startup> _logger;
    private DateTime _lastExecution;
    #endregion Properties

    #region Constructor
    public Startup(
        ServiceConfigurations serviceConfigurations,
        IConfigurationService configurationService,
        IMainService mainService,
        ILogger<Startup> logger
        )
    {
        _serviceConfigurations = serviceConfigurations ?? throw new ArgumentNullException(nameof(serviceConfigurations));
        _mainService = mainService ?? throw new ArgumentNullException(nameof(mainService));
        _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    #endregion Constructor

    #region Contract Methods
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(3 * 1000, stoppingToken);
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var canContinue = await CanContinue(stoppingToken);
                if (!canContinue) continue;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("===========================");
                _logger.LogInformation("Processamento Iniciado");
                Console.ResetColor();

                var watch = Stopwatch.StartNew();
                _logger.LogInformation("Iniciando a teste MultiThreads...");

                Console.WriteLine();

                var result = await _mainService.StartBot();

                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;

                //await _configurationService.UpdateLastExecution(_serviceConfigurations.SystemConfigId);

                _logger.LogInformation($"Processamento Finalizado - {elapsedMs / 1000} segundos");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("===========================");
                Console.ResetColor();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro na Worker");
            }
            finally
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("===========================");
                _logger.LogInformation("Próxima execução permitida dia {Date}", DateTime.Now.AddSeconds(_serviceConfigurations.SecondsTimeout).ToString("dd/MM/yyyy HH:mm:ss"));
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("===========================");
                Console.ResetColor();
                await Task.Delay(_serviceConfigurations.SecondsTimeout * 1000, stoppingToken);
            }
        }
    }
    #endregion Contract Methods

    #region Private Methods
    private async Task<bool> CanContinue(CancellationToken stoppingToken)
    {
        try
        {
            var botProceed = await _configurationService.CanExecute(_serviceConfigurations.SystemConfigId);

            if (botProceed.Failed)
            {
                await PrintSystemConfigError(stoppingToken, botProceed.Message);
                return false;
            }
            // else
            // {
            //     if (botProceed.Value.LastExecution == null) _lastExecution = DateTime.MinValue;
            //     else _lastExecution = ((DateTime)botProceed.Value.LastExecution).AddHours(-3);
            // }
            // if (_lastExecution.Date == DateTime.Now.Date)
            // {
            //     _logger.LogInformation("Processamento já realizado na data de hoje");
            //     return false;
            // }
        }
        catch (OperationCanceledException)
        {
            return false;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Erro ao buscar SystemConfig {_serviceConfigurations.SystemConfigId} no banco");
            await Task.Delay(1800 * 1000, stoppingToken);
            return false;
        }

        return true;
    }

    private async Task<bool> PrintSystemConfigError(CancellationToken stoppingToken, string msg)
    {
        var isToday = _lastExecution.Date == DateTime.Now.Date;
        var nextExecution = isToday ? DateTime.Now.AddDays(1) : DateTime.Now;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("===========================");
        Console.ResetColor();
        _logger.LogWarning("Parado por SystemConfig: {SystemConfigId}", _serviceConfigurations.SystemConfigId);
        if (msg == "Desativado") _logger.LogWarning($"Bot está desativado");
        else _logger.LogWarning("Próxima execução permitida dia {Date}. {msg}", nextExecution.ToString("dd/MM/yyyy HH:mm:ss"), msg);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("===========================");
        Console.ResetColor();
        await Task.Delay(60 * 1000, stoppingToken);
        return true;
    }
    #endregion Private Methods
}
