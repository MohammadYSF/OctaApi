using Application.ReadModels;

namespace Application.Repositories.Query;

public interface IBuyInvoiceQueryRepository
{
    Task<BuyInvoiceRM?> GetByBuyInvoiceId(Guid buyInvoiceId);
    Task AddAsync(BuyInvoiceRM buyInvoiceRM);
    Task<List<BuyInvoiceRM>> GetAsync();
}
