using System.Linq.Expressions;
using ReSTeAvecMoi.Generics.Crud;

namespace ReSTeAvecMoi.Generics.Interfaces;

public interface ICrudServiceBase<TKey, TEntity, TIRepository>
where TKey : IComparable<TKey>, IEquatable<TKey>
where TEntity : CrudEntityBase<TKey>
where TIRepository : ICrudRepositoryBase<TKey, TEntity>
{
    public Task<Result<TEntity?>> Get(TKey id);
    public Task<Result<List<TEntity>>> GetAll();
    public Task<Result<List<TEntity>>> FindAll(Expression<Func<TEntity, bool>> predicate);
    public Task<Result<TEntity?>> FindOne(Expression<Func<TEntity, bool>> predicate);
    public Task<Result<TEntity>> Create(TEntity entity);
    public Task<Result<TEntity>> Update(TEntity entity);
    public Task<Result<TKey>> Delete(TKey id);
}