using OctaShared.ReadModels;

namespace Command.Core.Application.Repositories;
public interface ISellInvoiceRMRepository
{
    Task<SellInvoiceRM?> GetBySellInvoiceId(Guid sellInvoiceId);
}
