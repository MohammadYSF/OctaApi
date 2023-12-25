using Application.ReadModels;
namespace Application.Repositories.Query;
public interface IServiceQueryRepository
{
    Task<List<ServicecRM>> GetByServiceIdAsync(Guid serviceId);
    Task<List<ServicecRM>> GetAsync();
    Task AddAsync(ServicecRM service);
    Task UpdateAsync(ServicecRM service);
}
