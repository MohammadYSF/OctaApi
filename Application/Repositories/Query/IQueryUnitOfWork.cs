namespace Application.Repositories.Query;
public interface IQueryUnitOfWork
{
    Task SaveAsync(CancellationToken cancellationToken);
}
