using Domain.Invoice;
namespace Application.Repositories;
public interface IBuyInvoiceCommandRepository
{
    Task UpdateAsync(BuyInvoiceAggregate buyInvoiceAggregate);
}
