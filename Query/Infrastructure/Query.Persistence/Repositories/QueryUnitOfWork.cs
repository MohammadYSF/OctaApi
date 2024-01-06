using Query.Application.Repositories;
using Query.Persistence.Contexts;

namespace Query.Persistence.Repositories;
public class QueryUnitOfWork : IQueryUnitOfWork
{
    private readonly QueryDbContext _queryDbContext;

    public QueryUnitOfWork(QueryDbContext queryDbContext)
    {
        _queryDbContext = queryDbContext;
    }

    public Task SaveAsync(CancellationToken cancellationToken)
    {
        return _queryDbContext.SaveChangesAsync(cancellationToken);
    }
}
