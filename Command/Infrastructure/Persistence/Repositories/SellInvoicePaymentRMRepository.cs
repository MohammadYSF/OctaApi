using Command.Core.Application.Core;
using Command.Core.Application.Repositories;
namespace Command.Persistence.Repositories;

public class SellInvoicePaymentRMRepository : ISellInvoicePaymentRMRepository
{
    public Task<List<SellInvoicePaymentRM>> GetBySellInvoiceIdAsync(Guid sellInvoiceId)
    {
        throw new NotImplementedException();
    }
}

