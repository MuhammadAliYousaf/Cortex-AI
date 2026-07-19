using System.Linq.Expressions;

namespace CortexAI.Domain.Specifications;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    IReadOnlyCollection<Expression<Func<T, object?>>> Includes { get; }
    Expression<Func<T, object?>>? OrderBy { get; }
    bool AsNoTracking { get; }
}
