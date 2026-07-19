using System.Linq.Expressions;

namespace CortexAI.Domain.Specifications;

public abstract class Specification<T> : ISpecification<T>
{
    private readonly List<Expression<Func<T, object?>>> _includes = [];

    public Expression<Func<T, bool>>? Criteria { get; private set; }
    public IReadOnlyCollection<Expression<Func<T, object?>>> Includes => _includes;
    public Expression<Func<T, object?>>? OrderBy { get; private set; }
    public bool AsNoTracking { get; private set; }

    protected void Where(Expression<Func<T, bool>> criteria) => Criteria = criteria;
    protected void Include(Expression<Func<T, object?>> includeExpression) => _includes.Add(includeExpression);
    protected void OrderByAscending(Expression<Func<T, object?>> orderByExpression) => OrderBy = orderByExpression;
    protected void UseNoTracking() => AsNoTracking = true;
}
