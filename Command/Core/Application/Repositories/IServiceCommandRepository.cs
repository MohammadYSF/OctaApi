using Command.Core.Domain.Service;

namespace Command.Core.Application.Repositories;
public interface IServiceCommandRepository
{
    Task AddAsync(ServiceAggregate serviceAggregate);
    Task UpdateAsync(ServiceAggregate serviceAggregate);
    Task<int> GenerateNewCodeAsync();
    Task<ServiceAggregate?> GetByCodeAsync(int code);
    Task<ServiceAggregate?> GetByIdAsync(Guid id);
}
