namespace CarrerBot.Domain._base.Interface;

public interface IBaseRepository<TEntity>
{
    Task<IEnumerable<TEntity>> GetAll(int userId);
    Task<TEntity> GetById(long id, int userId);
    Task<int> Create(TEntity Model);
    Task<bool> UpdateById(TEntity Model);
    Task<bool> SetDeletedById(int id, int userId);
}
