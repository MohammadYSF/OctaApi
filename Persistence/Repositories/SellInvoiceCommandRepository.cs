using Application.Repositories;
using Domain.SellInvoice;
using Microsoft.EntityFrameworkCore;
using OctaApi.Persistence.Contexts;
namespace Persistence.Repositories;
public class SellInvoiceCommandRepository : ISellInvoiceCommandRepository
{
    private readonly WriteDbContext _writeDbContext;

    public SellInvoiceCommandRepository(WriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }
    public async Task AddAsync(SellInvoiceAggregate sellInvoiceAggregate)
    {
        await _writeDbContext.SellInvoices.AddAsync(sellInvoiceAggregate);
    }

    public Task DeleteAsync(SellInvoiceAggregate sellInvoiceAggregate)
    {
        _writeDbContext.SellInvoices.Remove(sellInvoiceAggregate);
        return Task.CompletedTask;
    }

    public async Task<int> GenerateNewCodeAsync()
    {
        List<int> usedCodes = await _writeDbContext.SellInvoices.Select(a => a.Code.Value).ToListAsync();
        int min = usedCodes.Min();
        int max = usedCodes.Max();
        List<int> unUsedCodes = Enumerable.Range(min, max).Where(a => !usedCodes.Contains(a)).ToList();
        if (unUsedCodes.Count > 0) return unUsedCodes[0];
        return max + 1;
    }

    public async Task<SellInvoiceAggregate?> GetByIdAsync(Guid id)
    {

        return await _writeDbContext.SellInvoices.FirstOrDefaultAsync(a => a.Id == id);
    }

    public Task UpdateAsync(SellInvoiceAggregate sellInvoiceAggregate)
    {
        _writeDbContext.SellInvoices.Update(sellInvoiceAggregate);
        return Task.CompletedTask;
    }
}
