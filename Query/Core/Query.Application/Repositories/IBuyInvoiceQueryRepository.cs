
using Query.Application.ReadModels;

namespace Query.Application.Repositories;

public interface IBuyInvoiceQueryRepository
{
    Task<BuyInvoiceRM?> GetByBuyInvoiceId(Guid buyInvoiceId);
    Task AddAsync(BuyInvoiceRM buyInvoiceRM);
    Task<List<BuyInvoiceRM>> GetAsync();
    Task CheckCacheAsync();

}
