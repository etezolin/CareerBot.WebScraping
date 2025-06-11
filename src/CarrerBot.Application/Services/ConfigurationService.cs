using System.Globalization;
using CarrerBot.Application._base;
using CarrerBot.Domain.Configuration;

namespace CarrerBot.Application.Configuration;

public class ConfigurationService : BaseService<ConfigurationModel>, IConfigurationService
{
    private readonly IConfigurationRepository _repository;

    public ConfigurationService(IConfigurationRepository repository) : base(repository)
    {
        _repository = repository;
    }

    public async Task<Result<ConfigurationModel>> CanExecute(int id)
    {
        var result = await GetById(id, 1);
        if (result.Failed) return Result<ConfigurationModel>.SetFailed();
        try
        {
            var config = result.Value;
            var now = DateTime.Now;
            var startDateTime = DateTime.ParseExact(config.TimeStart, "HH:mm", CultureInfo.InvariantCulture);
            var endDateTime = DateTime.ParseExact(config.TimeEnd, "HH:mm", CultureInfo.InvariantCulture);

            if (!result.Value.IsActive) return Result<ConfigurationModel>.SetFailed(message: "Desativado");

            return now >= startDateTime && now <= endDateTime
                ? Result<ConfigurationModel>.SetSuccess(result.Value)
                : Result<ConfigurationModel>.SetFailed(message: $"{config.TimeStart} -> {config.TimeEnd}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<ConfigurationModel>.SetFailed(message: "Exception");
        }
    }

    public async Task<Result> UpdateLastExecution(int id)
    {
        var result = await _repository.UpdateLastExecution(id);
        return Result.SetResult(result);
    }
}
