using OctaShared.ReadModels;

namespace Command.Core.Application.Repositories;
public interface ISellInvoicePaymentRMRepository
{
    Task<List<SellInvoicePaymentRM>> GetBySellInvoiceIdAsync(Guid sellInvoiceId);
}
