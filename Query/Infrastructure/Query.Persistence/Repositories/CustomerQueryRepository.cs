using Microsoft.EntityFrameworkCore;
using Query.Application.ReadModels;
using Query.Application.Repositories;
using Query.Persistence.Contexts;
namespace Query.Persistence.Repositories;
public class CustomerQueryRepository : ICustomerQueryRepository
{
    private readonly QueryDbContext _queryDbContext;
    private static readonly SemaphoreSlim Semaphore = new(1, 1);

    private readonly IDistributedCacheService<CustomerRM> _customerRMCache;
    public CustomerQueryRepository(QueryDbContext queryDbContext, IDistributedCacheService<CustomerRM> customerRMCache)
    {
        _queryDbContext = queryDbContext;
        _customerRMCache = customerRMCache;
        //if (_customerRMCache.Exists($"ids:{nameof(CustomerRM)}") == 0)
        //    _ = InitCache();
    }

    private async Task InitCache()
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

    public Task<CustomerRM?> GetByCustomerCodeAsync(string customerCode)
    {
        throw new NotImplementedException();
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
