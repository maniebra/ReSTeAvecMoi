using ReSTeAvecMoi.Generics.Crud;

namespace ReSTeAvecMoi.RulesValidator;

public abstract class RulesChain<TKey, TEntity>
    where TKey : IEquatable<TKey>, IComparable<TKey>
    where TEntity : CrudEntityBase<TKey>
{
    public readonly List<string> Reasons = [];
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
        foreach (var rule in from rule in _rules
                 let compiledRule= rule.RuleExpression.Compile()
                 let result = compiledRule(entity)
                 where !result
                 select rule)
        {
            if (stopOnFirstFailure)
            {
                Reasons.Add(rule.Message ?? "Rule Failed");
                return false;
            }

            Reasons.Add(rule.Message ?? "Rule Failed");
        }

        return Reasons.Count <= 0;
    }
}