namespace CarrerBot.Application._base.Contracts;

public interface IBaseService<TEntity>
{
    Task<Result<IEnumerable<TEntity>>> GetAll(int userId = 1);
    Task<Result<TEntity>> GetById(long id, int userId = 1);
    Task<Result<int>> Create(TEntity model, int userId = 1);
    Task<Result<bool>> UpdateById(TEntity model, int userId = 1);
    Task<Result<bool>> SetDeletedById(int id, int userId = 1);
}
