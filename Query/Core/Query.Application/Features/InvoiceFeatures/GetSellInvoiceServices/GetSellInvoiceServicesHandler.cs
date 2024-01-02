using MediatR;
using Query.Application.ReadModels;
using Query.Application.Repositories;
namespace Query.Application.Features.InvoiceFeatures.GetSellInvoiceServices;
public sealed class GetSellInvoiceServicesHandler : IRequestHandler<GetSellInvoicecServicesRequest, GetSellInvoiceServicesResponse>
{
    private readonly ISellInvoiceQueryRepository _sellInvoiceQueryRepository;
    private readonly IDistributedCacheService<SellInvoiceServiceRM> _sellInvoiceServiceRMCacheService;

    public GetSellInvoiceServicesHandler(ISellInvoiceQueryRepository sellInvoiceQueryRepository, IDistributedCacheService<SellInvoiceServiceRM> sellInvoiceServiceRMCacheService)
    {
        _sellInvoiceQueryRepository = sellInvoiceQueryRepository;
        _sellInvoiceServiceRMCacheService = sellInvoiceServiceRMCacheService;
    }
    public async Task<GetSellInvoiceServicesResponse> Handle(GetSellInvoicecServicesRequest request, CancellationToken cancellationToken)
    {
        await _sellInvoiceQueryRepository.CheckCacheAsync();
        var data = _sellInvoiceServiceRMCacheService.FindBy(a => a.SellInvoiceId == request.InvoiceId);
        //var data = await _sellInvoiceQueryRepository.GetSellInvoiceServiceRMsBySellInvoiceId(request.InvoiceId);
        var response = new GetSellInvoiceServicesResponse(Data: data);
        return response;
    }
}
