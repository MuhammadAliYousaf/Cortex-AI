using CortexAI.Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace CortexAI.Infrastructure.Persistence;

internal static class SpecificationEvaluator
{
    public static IQueryable<T> GetQuery<T>(IQueryable<T> query, ISpecification<T> specification) where T : class
    {
        if (specification.Criteria is not null)
        {
            query = query.Where(specification.Criteria);
        }

        query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

        if (specification.OrderBy is not null)
        {
            query = query.OrderBy(specification.OrderBy);
        }

        return specification.AsNoTracking ? query.AsNoTracking() : query;
    }
}
