using Microsoft.EntityFrameworkCore;
using Query.Application.ReadModels;
using Query.Application.Repositories;
using Query.Persistence.Contexts;

namespace Query.Persistence.Repositories;
public class VehicleQueryRepository : IVehicleQueryRepository
{
    private readonly QueryDbContext _queryDbContext;
    private static readonly SemaphoreSlim Semaphore = new(1, 1);

    private readonly IDistributedCacheService<VehicleRM> _vehicleRMCache;
    public VehicleQueryRepository(QueryDbContext queryDbContext, IDistributedCacheService<VehicleRM> vehicleRMCache)
    {
        _queryDbContext = queryDbContext;
        _vehicleRMCache = vehicleRMCache;
    }
    public async Task CheckCacheAsync()
    {
        if (_vehicleRMCache.Exists($"ids:{nameof(VehicleRM)}") == 0)
            await InitCacheAsync();
    }
    private async Task InitCacheAsync()
    {
        var exist = _vehicleRMCache.Exists($"ids:{nameof(VehicleRM)}");
        if (exist == 1) return;
        await Semaphore.WaitAsync();
        try
        {
            var result = await _queryDbContext.VehicleRMs.AsNoTracking().ToListAsync();
            _vehicleRMCache.Creates(result);
        }
        finally
        {
            Semaphore.Release();
        }
    }
    public async Task<List<VehicleRM>> GetAsync()
    {
        return await _queryDbContext.VehicleRMs.ToListAsync();
    }

    public Task<VehicleRM?> GetByVehicleCodeAsync(string code)
    {
        throw new NotImplementedException();
    }

    public async Task<VehicleRM?> GetByVehicleIdAsync(Guid vehicleId)
    {
        return await _queryDbContext.VehicleRMs.FirstOrDefaultAsync(a => a.VehicleId == vehicleId);
    }
}
