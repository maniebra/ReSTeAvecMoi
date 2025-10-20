using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ReSTeAvecMoi.Generics.Crud;

namespace ReSTeAvecMoi.RulesValidator;

public abstract class RulesChain<TKey, TEntity>
    where TKey : IEquatable<TKey>, IComparable<TKey>
    where TEntity : CrudEntityBase<TKey>
{
    public List<string> Reasons = [];
    private readonly List<Rule<TKey, TEntity>> _rules = [];

    protected RulesChain(Rule<TKey, TEntity>? rule)
    {
        if (rule != null)
            _rules.Add(rule);
    }

    protected RulesChain<TKey, TEntity> Add(Rule<TKey, TEntity> rule)
    {
        this._rules.Add(rule);
        return this;
    }

    public bool Check(TEntity entity, bool stopOnFirstFailure = false)
    {
        return _rules
            .Select(rule => rule.RuleExpression.Compile())
            .Select(compiledRule => compiledRule(entity))
            .Where(result => !result)
            .All(result => !stopOnFirstFailure);
    }
}