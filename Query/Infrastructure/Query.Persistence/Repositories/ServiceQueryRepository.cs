using Microsoft.EntityFrameworkCore;
using Query.Application.ReadModels;
using Query.Application.Repositories;
using Query.Persistence.Contexts;
namespace Query.Persistence.Repositories;
public class ServiceQueryRepository : IServiceQueryRepository
{
    private readonly QueryDbContext _queryDbContext;

    public ServiceQueryRepository(QueryDbContext queryDbContext)
    {
        _queryDbContext = queryDbContext;
    }

    public async Task AddAsync(ServiceRM service)
    {
        await _queryDbContext.ServiceRMs.AddAsync(service);
    }

    public async Task<List<ServiceRM>> GetAsync()
    {
        return await _queryDbContext.ServiceRMs.ToListAsync();
    }

    public async Task<List<ServiceRM>> GetByServiceIdAsync(Guid serviceId)
    {
        return await _queryDbContext.ServiceRMs.Where(a => a.ServiceId == serviceId).ToListAsync();
    }

    public Task UpdateAsync(ServiceRM service)
    {
        _queryDbContext.ServiceRMs.Update(service);
        return Task.CompletedTask;
    }
}
