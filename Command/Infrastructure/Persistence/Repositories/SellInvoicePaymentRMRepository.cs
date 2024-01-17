using Command.Core.Application.Repositories;
using OctaShared.ReadModels;
namespace Command.Persistence.Repositories;

public class SellInvoicePaymentRMRepository : ISellInvoicePaymentRMRepository
{
    public Task<List<SellInvoicePaymentRM>> GetBySellInvoiceIdAsync(Guid sellInvoiceId)
    {
        throw new NotImplementedException();
    }
}

