namespace Command.Core.Application.Repositories
{
    public interface ICommandUnitOfWork
    {
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
