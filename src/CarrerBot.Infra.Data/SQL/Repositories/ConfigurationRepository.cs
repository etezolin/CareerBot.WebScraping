using Dapper;
using CarrerBot.Infra._base;
using CarrerBot.Domain._enums;
using CarrerBot.Domain.Configuration;
using CarrerBot.Infra.Data.Providers;

namespace CarrerBot.Infra.Data.Repositories;

public class ConfigurationRepository : BaseRepository<ConfigurationModel>, IConfigurationRepository
{
    #region Properties
    private static string _queryGetById => @"select
                                                Id,
                                                Systemtype,
                                                Name,
                                                IsActive,
                                                IsRunning,
                                                SUBSTRING(timestart::TEXT, 1, 5) as TimeStart,
                                                SUBSTRING(timeend::TEXT, 1, 5) as TimeEnd,
                                                CreatedAt,
                                                LastExecution
                                            from carrer.systemconfig
                                            where 1 = 1
                                                and Id = @id";
    private static string _updateLastExecution => @"";

    #endregion Properties

    #region Constructor
    public ConfigurationRepository(IDbProvider provider, DbSchemaEnum schema = DbSchemaEnum.DataSystem) : base(provider)
    {
        Provider = provider;
        Schema = schema;
    }
    #endregion Constructor

    #region Contract Methods
    public async override Task<ConfigurationModel> GetById(long id, int personId)
    {
        Query = _queryGetById;
        return await base.GetById(id, personId);
    }

    public async Task<bool> UpdateLastExecution(int id)
    {
        return await RetryOnTimeout(async dbConnection =>
        {
            var result = await dbConnection.ExecuteAsync(_updateLastExecution, new { id }, commandTimeout: 120);
            return result == 1;
        });
    }
    #endregion Contract Methods
}
