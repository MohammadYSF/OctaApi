using Microsoft.EntityFrameworkCore;
using OctaShared.Contracts;
using OctaShared.ReadModels;
using Query.Application.Repositories;
using Query.Persistence.Contexts;
namespace Query.Persistence.Repositories;
public class CustomerQueryRepository : ICustomerQueryRepository
{
    private readonly QueryDbContext _queryDbContext;
    private static readonly SemaphoreSlim Semaphore = new(1, 1);

    private readonly IDistributedCacheService<CustomerRM> _customerRMCache;
    private readonly IDistributedCacheService<CustomerVehicleRM> _customerVehicleRMCache;
    public CustomerQueryRepository(QueryDbContext queryDbContext, IDistributedCacheService<CustomerRM> customerRMCache, IDistributedCacheService<CustomerVehicleRM> customerVehicleRMCache)
    {
        _queryDbContext = queryDbContext;
        _customerRMCache = customerRMCache;
        _customerVehicleRMCache = customerVehicleRMCache;
    }
    public async Task CheckCacheAsync()
    {
        if (_customerRMCache.Exists($"ids:{nameof(CustomerRM)}") == 0)
            await InitCacheAsync1();
        if (_customerVehicleRMCache.Exists($"ids:{nameof(CustomerVehicleRM)}") == 0)
            await InitCacheAsync2();
    }
    private async Task InitCacheAsync2()
    {
        var exist = _customerVehicleRMCache.Exists($"ids:{nameof(CustomerVehicleRM)}");
        if (exist == 1) return;
        await Semaphore.WaitAsync();
        try
        {
            var result = await _queryDbContext.CustomerVehicleRMs.AsNoTracking().ToListAsync();
            _customerVehicleRMCache.Creates(result);
        }
        finally
        {
            Semaphore.Release();
        }
    }
    private async Task InitCacheAsync1()
    {
        var exist = _customerRMCache.Exists($"ids:{nameof(CustomerRM)}");
        if (exist == 1) return;
        await Semaphore.WaitAsync();
        try
        {
            var result = await _queryDbContext.CustomerRMs.AsNoTracking().ToListAsync();
            _customerRMCache.Creates(result);
        }
        finally
        {
            Semaphore.Release();
        }
    }
    public async Task AddAsync(CustomerVehicleRM customerVehicleRM)
    {
        await _queryDbContext.CustomerVehicleRMs.AddAsync(customerVehicleRM);
    }

    public async Task AddAsync(CustomerRM customerRM)
    {
        await _queryDbContext.CustomerRMs.AddAsync(customerRM);
    }

    public async Task<List<CustomerRM>> GetAsync()
    {
        return await _queryDbContext.CustomerRMs.AsNoTracking().ToListAsync();
    }

    public async Task<CustomerRM?> GetByCustomerCodeAsync(string customerCode)
    {
        return await _queryDbContext.CustomerRMs.Where(a => a.CustomerCode == customerCode).FirstOrDefaultAsync();
    }

    public async Task<CustomerRM?> GetByCustomerIdAsync(Guid customerId)
    {
        return await _queryDbContext.CustomerRMs.AsNoTracking().FirstOrDefaultAsync(a => a.CustomerId == customerId);
    }

    public Task UpdateAsync(CustomerRM customerRM)
    {
        _queryDbContext.CustomerRMs.Update(customerRM);
        return Task.CompletedTask;
    }
}
