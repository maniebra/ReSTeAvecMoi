using System.Linq.Expressions;
using ReSTeAvecMoi.Generics.Crud;

namespace ReSTeAvecMoi.RulesValidator;

public class Rule<TKey, TEntity>
where TKey : IEquatable<TKey>, IComparable<TKey>
where TEntity : CrudEntityBase<TKey>
{
    public required Expression<Func<TEntity, bool>> RuleExpression { get; set; }
    public string? Message { get; set; }
}