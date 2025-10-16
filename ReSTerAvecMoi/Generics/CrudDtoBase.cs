namespace ReSTerAvecMoi.Generics;

public abstract class CrudDtoBase<TKey, TEntity>
    where TKey : IComparable<TKey>, IEquatable<TKey>
    where TEntity : CrudEntityBase<TKey>
{
    public TKey Id { get; set; }

    protected CrudDtoBase(TEntity entity)
    {
        throw new NotImplementedException();
    }

    protected CrudDtoBase()
    {
        throw new NotImplementedException();
    }

    public abstract TEntity ToEntity();
}

public abstract class CrudModifyDtoBase<TKey, TEntity>
    : CrudDtoBase<TKey, TEntity>
    where TKey : IComparable<TKey>, IEquatable<TKey>
    where TEntity : CrudEntityBase<TKey>
{
}

public abstract class CrudReadDtoBase<TKey, TEntity> : CrudDtoBase<TKey, TEntity> 
    where TKey : IComparable<TKey>, IEquatable<TKey>
    where TEntity : CrudEntityBase<TKey>
{
}