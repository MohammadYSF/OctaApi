using Command.Core.Application.Repositories;
using OctaShared.ReadModels;
namespace Command.Persistence.Repositories;

public class SellInvoiceRMRepository : ISellInvoiceRMRepository
{
    public Task<SellInvoiceRM?> GetBySellInvoiceId(Guid sellInvoiceId)
    {
        throw new NotImplementedException();
    }
}

