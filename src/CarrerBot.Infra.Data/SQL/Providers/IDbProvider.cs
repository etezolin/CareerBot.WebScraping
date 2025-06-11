using System.Data;
using CarrerBot.Domain._enums;

namespace CarrerBot.Infra.Data.Providers;

public interface IDbProvider
{
    DbConnections Connections { get; }
    DbTypeEnum DbType { get; }
    IDbConnection Connect(DbSchemaEnum schema = DbSchemaEnum.DataSystem);
}
