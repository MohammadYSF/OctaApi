namespace Query.Application.Repositories;
public interface IQueryUnitOfWork
{
    Task SaveAsync(CancellationToken cancellationToken);
}
