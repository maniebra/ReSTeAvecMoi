namespace ReSTeAvecMoi.Exceptions;

public class EntityNotFoundException<TEntity> : Exception
    where TEntity : class
{

    public EntityNotFoundException(string message) : base(message) { }

    public EntityNotFoundException() : base($"No such {nameof(TEntity)}!") {}
}
