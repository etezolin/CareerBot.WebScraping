using System.Data;
using System.Data.SqlClient;
using CarrerBot.Domain._enums;
using Npgsql;

namespace CarrerBot.Infra.Data.Providers;

public class DbProvider : IDbProvider
{
    public DbConnections Connections { get; private set; }
    public DbTypeEnum DbType { get; private set; }

    public DbProvider(DbConnections connections, DbTypeEnum dbType)
    {
        Connections = connections;
        DbType = dbType;
    }

    public IDbConnection Connect(DbSchemaEnum schema = DbSchemaEnum.DataSystem)
    {
        switch (schema)
        {
            case DbSchemaEnum.DataSystem:
                DbType = DbTypeEnum.PostgreSQL;
                return new NpgsqlConnection(connectionString: Connections.GetConnection(schema));
            default:
                DbType = DbTypeEnum.SQLServer;
                return new SqlConnection(Connections.GetConnection(schema));
        }
    }
}

public class DbConnections
{
    public IDictionary<DbSchemaEnum, string> Schemas { get; private set; }

    public DbConnections(
       IDictionary<DbSchemaEnum, string> schemas
        )
    {
        Schemas = schemas;
    }

    public string GetConnection(DbSchemaEnum schema)
    {
        return Schemas[schema];
    }
}
