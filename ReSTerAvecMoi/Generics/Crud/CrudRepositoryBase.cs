using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ReSTerAvecMoi.Exceptions;
using ReSTerAvecMoi.Generics.Interfaces;

namespace ReSTerAvecMoi.Generics.Crud;

public abstract class CrudRepositoryBase<TKey, TEntity>(DbContext context) : ICrudRepositoryBase<TKey, TEntity>
    where TKey : IComparable<TKey>, IEquatable<TKey>
    where TEntity : CrudEntityBase<TKey>
{
    private readonly DbContext _context = context;
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async Task<TEntity> Create(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task Update(TEntity entity)
    {
        var oldEntity = await _dbSet.FindAsync(entity.Id);
        if (oldEntity == null)
            throw new EntityNotFoundException<TEntity>();
        if (!oldEntity.Diff(entity))
            return;
        entity.UpdatedAt = DateTimeOffset.UtcNow;
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(TKey id)
    {
        var entity = await Get(id);
        await Delete(entity);
    }

    public async Task<TEntity> Get(TKey id)
    {
        var entity = await _dbSet.Where(c => c.Id.Equals(id)).SingleOrDefaultAsync();
        return entity ?? throw new EntityNotFoundException<TEntity>();
    }

    public async Task<List<TEntity>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<TEntity?> FindOne(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.Where(predicate).SingleOrDefaultAsync();
    }

    public async Task<List<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = await _dbSet.Where(predicate).ToListAsync();
        return entities;
    }
}