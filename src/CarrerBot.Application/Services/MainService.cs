using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace CarrerBot.Application;

public class MainService : IMainService
{
    private readonly ILogger<MainService> _logger;
    private static readonly SemaphoreSlim semaphoreSlim = new(10);

    public MainService(ILogger<MainService> logger)
    {
        _logger = logger ?? throw new ArgumentException(nameof(logger));
    }
    public async Task<bool> StartBot()
    {
        try
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            //------
            Console.WriteLine($"Tempo Gasto: {stopwatch.Elapsed}");
            //Console.ReadLine();
        }
        catch (Exception ex)
        {
            _logger.LogError("{x}", ex.Message.ToString());
            Console.WriteLine();
        }
        return false;
    }
}
