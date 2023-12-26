using OctaApi.Application.Repositories;
using OctaApi.Persistence.Contexts;
namespace OctaApi.Persistence.Repositories;
public class CommandUnitOfWork : ICommandUnitOfWork
{
    private readonly WriteDbContext _writeDbContext;

    public CommandUnitOfWork(WriteDbContext context)
    {
        _writeDbContext = context;
    }
    public Task SaveAsync(CancellationToken cancellationToken)
    {
        return _writeDbContext.SaveChangesAsync(cancellationToken);
    }
}
