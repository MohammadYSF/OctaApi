using Application.Repositories;
using Domain.Customer;
using Microsoft.EntityFrameworkCore;
using OctaApi.Persistence.Contexts;
namespace Persistence.Repositories;
public class CustomerCommandRepository : ICustomerCommandRepository
{
    private readonly WriteDbContext _writeDbContext;
    public CustomerCommandRepository(WriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }
    public async Task AddAsync(CustomerAggregate customerAggregate)
    {
        await _writeDbContext.Customers.AddAsync(customerAggregate);
    }
    public async Task<int> GenerateNewCustomerCodeAsync()
    {
        List<int> usedCodes = await _writeDbContext.Vehicles.Select(a => a.Code.Value).ToListAsync();
        int min = usedCodes.Min();
        int max = usedCodes.Max();
        List<int> unUsedCodes = Enumerable.Range(min, max).Where(a => !usedCodes.Contains(a)).ToList();
        if (unUsedCodes.Count > 0) return unUsedCodes[0];
        return max + 1;
    }
}
