using Application.ReadModels;
using Microsoft.EntityFrameworkCore;
using Query.Application.Repositories;
using Query.Persistence.Contexts;
namespace Query.Persistence.Repositories;
public class CustomerQueryRepository : ICustomerQueryRepository
{
    private readonly QueryDbContext _queryDbContext;

    public CustomerQueryRepository(QueryDbContext queryDbContext)
    {
        _queryDbContext = queryDbContext;
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
        return await _queryDbContext.CustomerRMs.ToListAsync();
    }

    public async Task<CustomerRM?> GetByCustomerIdAsync(Guid customerId)
    {
        return await _queryDbContext.CustomerRMs.FirstOrDefaultAsync(a => a.CustomerId == customerId);
    }

    public Task UpdateAsync(CustomerRM customerRM)
    {
        _queryDbContext.CustomerRMs.Update(customerRM);
        return Task.CompletedTask;
    }
}
