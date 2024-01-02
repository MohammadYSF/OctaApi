using MediatR;
using Query.Application.ReadModels;
using Query.Application.Repositories;
namespace OctaApi.Application.Features.InvoiceFeatures.GetSellInvoiceInventoryItems;
public sealed class GetSellInvoiceInventoryItemsHandler : IRequestHandler<GetSellInvoiceInventoryItemsRequest, GetSellInvoiceInventoryItemsResponse>
{
    private readonly ISellInvoiceQueryRepository _sellInvoiceQueryRepository;
    private readonly IDistributedCacheService<SellInvoiceInventoryItemRM> _sellInvoiceInventoryItemRMCache;

    public GetSellInvoiceInventoryItemsHandler(ISellInvoiceQueryRepository sellInvoiceQueryRepository, IDistributedCacheService<SellInvoiceInventoryItemRM> sellInvoiceInventoryItemRMCache)
    {
        _sellInvoiceQueryRepository = sellInvoiceQueryRepository;
        _sellInvoiceInventoryItemRMCache = sellInvoiceInventoryItemRMCache;
    }
    public async Task<GetSellInvoiceInventoryItemsResponse> Handle(GetSellInvoiceInventoryItemsRequest request, CancellationToken cancellationToken)
    {
        await _sellInvoiceQueryRepository.CheckCacheAsync();
        var data = _sellInvoiceInventoryItemRMCache.FindBy(a => a.SellInvoiceId == request.InvoiceId);
        //var data = await _sellInvoiceQueryRepository.GetSellInvoiceInventoryItemRMsBySellInvoiceId(request.InvoiceId);
        var response = new GetSellInvoiceInventoryItemsResponse(Data: data);
        return response;
    }
}
