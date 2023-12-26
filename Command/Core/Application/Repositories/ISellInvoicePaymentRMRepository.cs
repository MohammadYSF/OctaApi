
using Command.Core.Application.Core;

namespace Command.Core.Application.Repositories;
public interface ISellInvoicePaymentRMRepository
{
    Task<List<SellInvoicePaymentRM>> GetBySellInvoiceIdAsync(Guid sellInvoiceId);
}
