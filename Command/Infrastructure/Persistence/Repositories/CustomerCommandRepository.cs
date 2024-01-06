using Command.Core.Application.Repositories;
using Command.Core.Domain.Customer;
using Command.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
namespace Command.Infrastructure.Persistence.Repositories;
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
        List<int> usedCodes = await _writeDbContext.Customers.Select(a => a.Code.Value).ToListAsync();
        if (usedCodes.Count == 0) return 1;
        int min = usedCodes.Min();
        int max = usedCodes.Max();
        List<int> unUsedCodes = Enumerable.Range(min, max).Where(a => !usedCodes.Contains(a)).ToList();
        if (unUsedCodes.Count > 0) return unUsedCodes[0];
        return max + 1;
    }
}
