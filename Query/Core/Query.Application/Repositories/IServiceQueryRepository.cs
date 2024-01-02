using Query.Application.ReadModels;

namespace Query.Application.Repositories;
public interface IServiceQueryRepository
{
    Task<List<ServiceRM>> GetByServiceIdAsync(Guid serviceId);
    Task<List<ServiceRM>> GetAsync();
    Task AddAsync(ServiceRM service);
    Task UpdateAsync(ServiceRM service);
    Task CheckCacheAsync();

}
