using Application.Core;

namespace Application.Repositories;
public interface ISellInvoicePaymentRMRepository
{
    Task<List<SellInvoicePaymentRM>> GetBySellInvoiceIdAsync(Guid sellInvoiceId);
}
