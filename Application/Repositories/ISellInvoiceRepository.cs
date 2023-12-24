using Domain.SellInvoice;

namespace Application.Repositories;
public interface ISellInvoiceRepository
{
    Task<int> GenerateNewCodeAsync();
    Task DeleteAsync(SellInvoiceAggregate sellInvoiceAggregate);
    Task<SellInvoiceAggregate?> GetByIdAsync(Guid id);
    Task UpdateAsync(SellInvoiceAggregate sellInvoiceAggregate);
    Task AddAsync(SellInvoiceAggregate sellInvoiceAggregate);
    
}
