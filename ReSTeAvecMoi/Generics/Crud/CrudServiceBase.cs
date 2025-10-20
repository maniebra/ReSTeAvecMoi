using System.Linq.Expressions;
using ReSTeAvecMoi.Generics.Interfaces;
using Serilog;

namespace ReSTeAvecMoi.Generics.Crud;

public class CrudServiceBase<TKey, TEntity, TIRepository>(TIRepository repository, ILogger logger)
    : ICrudServiceBase<TKey, TEntity, TIRepository>
    where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    where TEntity : CrudEntityBase<TKey>
    where TIRepository : ICrudRepositoryBase<TKey, TEntity>
{
    
    private readonly ILogger _logger = logger;
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
            _logger.Error(e, "Failed to get entity");
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
            _logger.Error(e, "Failed to get entities");
            return Result<List<TEntity>>.Failure(e);
        }
    }

    public async Task<Result<List<TEntity>>> FindAll(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var res = await _repository.FindAll(predicate);
            return Result<List<TEntity>>.Success(res);
        }
        catch (Exception e)
        {
            _logger.Error(e, "Failed to find all matching entities");
            return Result<List<TEntity>>.Failure(e);
        }
    }

    public async Task<Result<TEntity?>> FindOne(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var res = await _repository.FindOne(predicate);
            return Result<TEntity?>.Success(res);
        }
        catch (Exception e)
        {
            _logger.Error(e, "Failed to find a matching entity");
            return Result<TEntity?>.Failure(e);
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
            _logger.Error(e, "Failed to create entity");
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
            _logger.Error(e, "Failed to update entity");
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
            _logger.Error(e, "Failed to delete entity");
            return Result<TKey>.Failure(e);
        }
    }
}