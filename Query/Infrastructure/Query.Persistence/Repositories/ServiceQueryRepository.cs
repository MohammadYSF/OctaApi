using Microsoft.EntityFrameworkCore;
using OctaShared.Contracts;
using OctaShared.ReadModels;
using Query.Application.Repositories;
using Query.Persistence.Contexts;
namespace Query.Persistence.Repositories;
public class ServiceQueryRepository : IServiceQueryRepository
{
    private readonly QueryDbContext _queryDbContext;
    private static readonly SemaphoreSlim Semaphore = new(1, 1);

    private readonly IDistributedCacheService<ServiceRM> _serviceRMCache;
    public ServiceQueryRepository(QueryDbContext queryDbContext, IDistributedCacheService<ServiceRM> serviceRMCache)
    {
        _queryDbContext = queryDbContext;
        _serviceRMCache = serviceRMCache;
    }
    public async Task CheckCacheAsync()
    {
        if (_serviceRMCache.Exists($"ids:{nameof(ServiceRM)}") == 0)
            await InitCacheAsync();
    }
    private async Task InitCacheAsync()
    {
        var exist = _serviceRMCache.Exists($"ids:{nameof(ServiceRM)}");
        if (exist == 1) return;
        await Semaphore.WaitAsync();
        try
        {
            var result = await _queryDbContext.ServiceRMs.AsNoTracking().ToListAsync();
            _serviceRMCache.Creates(result);
        }
        finally
        {
            Semaphore.Release();
        }
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
