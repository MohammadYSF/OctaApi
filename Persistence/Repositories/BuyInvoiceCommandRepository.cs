using Application.Repositories;
using Domain.Invoice;
using OctaApi.Persistence.Contexts;
namespace Persistence.Repositories;
public class BuyInvoiceCommandRepository : IBuyInvoiceCommandRepository
{
    private readonly WriteDbContext _writeDbContext;

    public BuyInvoiceCommandRepository(WriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }

    public Task UpdateAsync(BuyInvoiceAggregate buyInvoiceAggregate)
    {
        _writeDbContext.BuyInvoices.Update(buyInvoiceAggregate);
        return Task.CompletedTask;
    }
}
