using ReSTerAvecMoi.Generics.Interfaces;

namespace ReSTerAvecMoi.Generics;

public class CrudServiceBase<TKey, TEntity, TIRepository>(TIRepository repository)
    : ICrudServiceBase<TKey, TEntity, TIRepository>
    where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    where TEntity : CrudEntityBase<TKey>
    where TIRepository : CrudRepositoryBase<TKey, TEntity>
{
    private readonly TIRepository _repository = repository;

    public async Task<Result<TEntity?>> Get(TKey id)
    {
        try
        {
            var entity = await _repository.Get(id);
            return Result<TEntity?>.Success(entity);
        }
        catch (Exception e)
        {
            return Result<TEntity?>.Failure(e);
        }
    }

    public async Task<Result<List<TEntity>>> GetAll()
    {
        try
        {
            var res = await _repository.GetAll();
            return Result<List<TEntity>>.Success(res);
        }
        catch (Exception e)
        {
            return Result<List<TEntity>>.Failure(e);
        }
    }

    public async Task<Result<TEntity>> Create(TEntity entity)
    {
        try
        {
            var res = await _repository.Create(entity);
            return Result<TEntity>.Success(res);
        }
        catch (Exception e)
        {
            return Result<TEntity>.Failure(e);
        }
    }

    public async Task<Result<TEntity>> Update(TEntity entity)
    {
        try
        {
            await _repository.Update(entity);
            return Result<TEntity>.Success(entity);
        }
        catch (Exception e)
        {
            return Result<TEntity>.Failure(e);
        }
    }

    public async Task<Result<TKey>> Delete(TKey id)
    {
        try
        {
            await _repository.Delete(id);
            return Result<TKey>.Success(id);
        }
        catch (Exception e)
        {
            return Result<TKey>.Failure(e);
        }
    }
}