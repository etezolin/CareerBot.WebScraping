using CarrerBot.Domain._base.Interface;

namespace CarrerBot.Domain.Configuration;

public interface IConfigurationRepository : IBaseRepository<ConfigurationModel>
{
    Task<bool> UpdateLastExecution(int id);
}
