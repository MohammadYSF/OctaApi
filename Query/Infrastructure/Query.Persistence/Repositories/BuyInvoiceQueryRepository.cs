using Microsoft.EntityFrameworkCore;
using Query.Application.ReadModels;
using Query.Application.Repositories;
using Query.Persistence.Contexts;

namespace Query.Persistence.Repositories;

public class BuyInvoiceQueryRepository : IBuyInvoiceQueryRepository
{
    private readonly QueryDbContext _queryDbContext;

    public BuyInvoiceQueryRepository(QueryDbContext queryDbContext)
    {
        _queryDbContext = queryDbContext;
    }

    public async Task AddAsync(BuyInvoiceRM buyInvoiceRM)
    {
        await _queryDbContext.BuyInvoiceRMs.AddAsync(buyInvoiceRM);
    }

    public async Task<List<BuyInvoiceRM>> GetAsync()
    {
        return await _queryDbContext.BuyInvoiceRMs.ToListAsync();
    }

    public async Task<BuyInvoiceRM?> GetByBuyInvoiceId(Guid buyInvoiceId)
    {
        return await _queryDbContext.BuyInvoiceRMs.FirstOrDefaultAsync(a=> a.BuyInvoiceId == buyInvoiceId);
    }
}
