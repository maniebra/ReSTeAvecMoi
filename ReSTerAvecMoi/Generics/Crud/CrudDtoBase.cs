namespace ReSTerAvecMoi.Generics;

/// <summary>
/// Base class for all Data Transfer Objects (DTOs) that wrap entities.
/// </summary>
public abstract class CrudDtoBase<TKey, TEntity>
    where TKey : IComparable<TKey>, IEquatable<TKey>
    where TEntity : CrudEntityBase<TKey>
{
    public TKey Id { get; set; } = default!;

    protected CrudDtoBase() { }

    protected CrudDtoBase(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        Id = entity.Id;
    }

    /// <summary>
    /// Converts the DTO to an entity.
    /// </summary>
    public abstract TEntity ToEntity();

    /// <summary>
    /// Converts a single entity to a DTO.
    /// </summary>
    public static CrudDtoBase<TKey, TEntity> FromEntity(TEntity entity)
    {
        throw new NotSupportedException(
            $"You must override {nameof(FromEntity)} in your derived DTO class."
        );
    }

    /// <summary>
    /// Converts a list of entities to DTOs.
    /// </summary>
    public static IEnumerable<TDto> FromEntities<TDto>(IEnumerable<TEntity>? entities, Func<TEntity, TDto> converter)
        where TDto : CrudDtoBase<TKey, TEntity>
    {
        if (entities == null)
            yield break;

        foreach (var entity in entities)
        {
            yield return converter(entity);
        }
    }
}

/// <summary>
/// Base DTO for "create" and "update" operations.
/// </summary>
public abstract class CrudModifyDtoBase<TKey, TEntity>
    : CrudDtoBase<TKey, TEntity>
    where TKey : IComparable<TKey>, IEquatable<TKey>
    where TEntity : CrudEntityBase<TKey>
{
    protected CrudModifyDtoBase() { }

    protected CrudModifyDtoBase(TEntity entity) : base(entity) { }
}

/// <summary>
/// Base DTO for "read" operations.
/// </summary>
public abstract class CrudReadDtoBase<TKey, TEntity>
    : CrudDtoBase<TKey, TEntity>
    where TKey : IComparable<TKey>, IEquatable<TKey>
    where TEntity : CrudEntityBase<TKey>
{
    protected CrudReadDtoBase() { }

    protected CrudReadDtoBase(TEntity entity) : base(entity) { }
}