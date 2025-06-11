using CarrerBot.Domain._base;
using CarrerBot.Domain._base.Interface;
using CarrerBot.Application._base.Contracts;

namespace CarrerBot.Application._base;

public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseModel
{
    #region Properties
    private IBaseRepository<TEntity> _repository;
    #endregion Properties

    #region Constructor
    public BaseService(
        IBaseRepository<TEntity> repository
        )
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    #endregion Constructor

    #region Contract Methods
    public virtual async Task<Result<int>> Create(TEntity model, int userId)
    {
        model.CreatedBy = userId;
        var result = await _repository.Create(model);
        return result > 0
            ? Result<int>.SetSuccess(result)
            : Result<int>.SetFailed();
    }

    public virtual async Task<Result<IEnumerable<TEntity>>> GetAll(int userId)
    {
        var list = await _repository.GetAll(userId);
        return list.Count() > 0 ? Result<IEnumerable<TEntity>>.SetSuccess(list) : Result<IEnumerable<TEntity>>.SetFailed(SvcErrorType.NoResults);
    }

    public virtual async Task<Result<TEntity>> GetById(long id, int userId)
    {
        var model = await _repository.GetById(id, userId);
        if (model != null) return Result<TEntity>.SetSuccess(model);

        return Result<TEntity>.SetFailed(SvcErrorType.NoResults);
    }

    public virtual async Task<Result<bool>> SetDeletedById(int id, int userId)
    {
        var result = await _repository.SetDeletedById(id, userId);
        return result
            ? Result<bool>.SetSuccess(true)
            : Result<bool>.SetFailed();
    }

    public virtual async Task<Result<bool>> UpdateById(TEntity model, int userId)
    {
        model.UpdatedBy = userId;
        var result = await _repository.UpdateById(model);
        return result
            ? Result<bool>.SetSuccess(true)
            : Result<bool>.SetFailed();
    }
    #endregion Contract Methods
}
