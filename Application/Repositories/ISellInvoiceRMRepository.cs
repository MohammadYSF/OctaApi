using Application.ReadModels;

namespace Application.Repositories;
public interface ISellInvoiceRMRepository
{
    Task<SellInvoiceRM?> GetBySellInvoiceId(Guid sellInvoiceId);
}
