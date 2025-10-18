using System.Linq.Expressions;
using ReSTeAvecMoi.Generics.Crud;

namespace ReSTeAvecMoi.Generics.Interfaces;

public interface ICrudRepositoryBase<in TKey, TEntity>
where TKey : IEquatable<TKey>, IComparable<TKey>
where TEntity : CrudEntityBase<TKey>
{
    public Task<TEntity> Create(TEntity entity);
    public Task Update(TEntity entity);
    public Task Delete(TEntity entity);
    public Task Delete(TKey id);
    public Task<TEntity> Get(TKey id);
    public Task<List<TEntity>> GetAll();
    public Task<TEntity?> FindOne(Expression<Func<TEntity, bool>> predicate);
    public Task<List<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate);
}