using Command.Core.Domain.SellInvoice;

namespace Command.Core.Application.Repositories;
public interface ISellInvoiceCommandRepository
{
    Task<int> GenerateNewCodeAsync();
    Task DeleteAsync(SellInvoiceAggregate sellInvoiceAggregate);
    Task<SellInvoiceAggregate?> GetByIdAsync(Guid id);
    Task UpdateAsync(SellInvoiceAggregate sellInvoiceAggregate);
    Task AddAsync(SellInvoiceAggregate sellInvoiceAggregate);
    
}
