using Command.Core.Application.Repositories;
using Command.Infrastructure.Persistence.Contexts;
namespace Command.Infrastructure.Persistence.Repositories;
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
