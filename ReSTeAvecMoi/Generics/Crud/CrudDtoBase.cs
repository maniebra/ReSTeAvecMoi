namespace ReSTeAvecMoi.Generics.Crud;

/// <summary>
/// Base class for all Data Transfer Objects (DTOs) that wrap entities.
/// </summary>
public abstract class CrudDtoBase<TKey, TEntity>
    where TKey : IComparable<TKey>, IEquatable<TKey>
    where TEntity : CrudEntityBase<TKey>
{ }

/// <summary>
/// Base DTO for "create" and "update" operations.
/// </summary>
public abstract class CrudModifyDtoBase<TKey, TEntity>
    where TKey : IComparable<TKey>, IEquatable<TKey>
    where TEntity : CrudEntityBase<TKey>
{
    protected CrudModifyDtoBase() { }
}

/// <summary>
/// Base DTO for "read" operations.
/// </summary>
public abstract class CrudReadDtoBase<TKey, TEntity>
    where TKey : IComparable<TKey>, IEquatable<TKey>
    where TEntity : CrudEntityBase<TKey>
{
    public TKey Id { get; set; } = default!; 
    
    protected CrudReadDtoBase() { }
}