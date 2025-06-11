using CarrerBot.Application._base;
using CarrerBot.Domain.Configuration;
using CarrerBot.Application._base.Contracts;

namespace CarrerBot.Application.Configuration;

public interface IConfigurationService : IBaseService<ConfigurationModel>
{
    Task<Result<ConfigurationModel>> CanExecute(int id);
    Task<Result> UpdateLastExecution(int id);
}
