using Command.Core.Application.ReadModels;
using Command.Core.Application.Repositories;
namespace Command.Persistence.Repositories;

public class SellInvoiceRMRepository : ISellInvoiceRMRepository
{
    public Task<SellInvoiceRM?> GetBySellInvoiceId(Guid sellInvoiceId)
    {
        throw new NotImplementedException();
    }
}

