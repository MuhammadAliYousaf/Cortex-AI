using CortexAI.Domain.Common;
using CortexAI.Domain.Repositories;
using CortexAI.Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace CortexAI.Infrastructure.Persistence;

public sealed class EfRepository<T>(ApplicationDbContext dbContext) : IRepository<T> where T : Entity
{
    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        dbContext.Set<T>().FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);

    public Task<T?> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default) =>
        SpecificationEvaluator.GetQuery(dbContext.Set<T>(), specification).FirstOrDefaultAsync(cancellationToken);

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T>? specification = null, CancellationToken cancellationToken = default)
    {
        var query = specification is null
            ? dbContext.Set<T>().AsQueryable()
            : SpecificationEvaluator.GetQuery(dbContext.Set<T>(), specification);

        return await query.ToListAsync(cancellationToken);
    }

    public Task AddAsync(T entity, CancellationToken cancellationToken = default) =>
        dbContext.Set<T>().AddAsync(entity, cancellationToken).AsTask();

    public void Update(T entity) => dbContext.Set<T>().Update(entity);
    public void Remove(T entity) => dbContext.Set<T>().Remove(entity);
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => dbContext.SaveChangesAsync(cancellationToken);
}
