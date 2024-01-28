using Command.Core.Domain.Invoice;

namespace Command.Core.Application.Repositories;
public interface IBuyInvoiceCommandRepository
{
    Task UpdateAsync(BuyInvoiceAggregate buyInvoiceAggregate);
    Task CreateAsync(BuyInvoiceAggregate buyInvoiceAggregate);

}
