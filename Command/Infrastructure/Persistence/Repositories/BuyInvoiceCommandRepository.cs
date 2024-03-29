﻿using Command.Core.Application.Repositories;
using Command.Core.Domain.Invoice;
using Command.Infrastructure.Persistence.Contexts;
namespace Command.Infrastructure.Persistence.Repositories;
public class BuyInvoiceCommandRepository : IBuyInvoiceCommandRepository
{
    private readonly WriteDbContext _writeDbContext;

    public BuyInvoiceCommandRepository(WriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }
    public async Task CreateAsync(BuyInvoiceAggregate buyInvoiceAggregate)
    {
        await _writeDbContext.BuyInvoices.AddAsync(buyInvoiceAggregate);
    }
    public Task UpdateAsync(BuyInvoiceAggregate buyInvoiceAggregate)
    {
        _writeDbContext.BuyInvoices.Update(buyInvoiceAggregate);
        return Task.CompletedTask;
    }
}
