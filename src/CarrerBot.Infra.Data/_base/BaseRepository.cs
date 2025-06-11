using Dapper;
using System.Data;
using System.Data.SqlClient;
using CarrerBot.Domain._enums;
using CarrerBot.Domain._base.Interface;
using CarrerBot.Infra.Data.Providers;

namespace CarrerBot.Infra._base;

public abstract class BaseRepository<TModel> : IBaseRepository<TModel>
{
    #region Properties
    private int _timeout = 120;
    public IDbProvider Provider { get; set; }
    public DbSchemaEnum Schema { get; set; } = DbSchemaEnum.DataSystem;
    protected string Query { get; set; }
    #endregion Properties

    #region Constructor
    public BaseRepository(IDbProvider provider, DbSchemaEnum schema = DbSchemaEnum.DataSystem)
    {
        Provider = provider;
        Schema = schema;
    }
    #endregion Constructor

    #region Contract Methods
    public virtual async Task<IEnumerable<TModel>> GetAll(int personId)
    {
        ValidateQuery();
        return await RetryOnTimeout(async dbConnection =>
        {
            var result = await dbConnection.QueryAsync<TModel>(Query, new { personId }, commandTimeout: _timeout);
            return result;
        });
    }

    public virtual async Task<TModel> GetById(long id, int personId)
    {
        ValidateQuery();
        return await RetryOnTimeout(async dbConnection =>
        {
            var result = await dbConnection.QueryFirstOrDefaultAsync<TModel>(Query, new { id, personId }, commandTimeout: _timeout);
            return result;
        });
    }

    public virtual async Task<int> Create(TModel model)
    {
        ValidateQuery();
        return await RetryOnTimeout(async dbConnection =>
        {
            var result = await dbConnection.QueryFirstOrDefaultAsync<int>(Query, model, commandTimeout: _timeout);
            return result;
        });
    }

    public virtual async Task<bool> UpdateById(TModel model)
    {
        ValidateQuery();
        return await RetryOnTimeout(async dbConnection =>
        {
            var affectedRows = await dbConnection.ExecuteAsync(Query, model, commandTimeout: _timeout);
            return affectedRows > 0;
        });
    }

    public virtual async Task<bool> SetDeletedById(int id, int personId)
    {
        ValidateQuery();
        return await RetryOnTimeout(async dbConnection =>
        {
            var affectedRows = await dbConnection.ExecuteAsync(Query, new { id, personId }, commandTimeout: _timeout);
            return affectedRows > 0;
        });
    }
    #endregion Contract Methods

    #region Methods
    protected async Task<T> RetryOnTimeout<T>(Func<IDbConnection, Task<T>> actionData)
    {
        try
        {
            using (IDbConnection dbConnection = Provider.Connect(Schema))
            {
                dbConnection.Open(); // Asynchronously open a connection to the database
                return await actionData(dbConnection); // Asynchronously execute actionData, which has been passed in as a Func<IDBConnection, Task<T>>
            }
        }
        catch (SqlException ex) when (ex.Number == -2) //Timeout
        {
            Thread.Sleep(1000);
            using (IDbConnection dbConnection = Provider.Connect(Schema))
            {
                dbConnection.Open();
                return await actionData(dbConnection);
            }
        }
        catch (SqlException ex) when (ex.Number == 1205) //Deadlock
        {
            try
            {
                Thread.Sleep(2000);
                using (IDbConnection dbConnection = Provider.Connect(Schema))
                {
                    dbConnection.Open();
                    return await actionData(dbConnection);
                }
            }
            catch (SqlException iex) when (iex.Number == 1205) //Deadlock
            {
                Thread.Sleep(3000);
                using (IDbConnection dbConnection = Provider.Connect(Schema))
                {
                    dbConnection.Open();
                    return await actionData(dbConnection);
                }
            }
        }
    }

    protected bool ValidateQuery()
    {
        if (!string.IsNullOrWhiteSpace(Query))
            return true;

        if (Query.Contains("INSERT ") && Query.Contains("OUTPUT"))
            return true;

        throw new Exception("Query can not be empty");
    }
    #endregion Methods
}
