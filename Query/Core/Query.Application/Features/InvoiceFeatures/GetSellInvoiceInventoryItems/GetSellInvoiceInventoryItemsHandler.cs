using MediatR;
using Query.Application.Repositories;
namespace OctaApi.Application.Features.InvoiceFeatures.GetSellInvoiceInventoryItems;
public sealed class GetSellInvoiceInventoryItemsHandler : IRequestHandler<GetSellInvoiceInventoryItemsRequest, GetSellInvoiceInventoryItemsResponse>
{
    private readonly ISellInvoiceQueryRepository _sellInvoiceQueryRepository;

    public GetSellInvoiceInventoryItemsHandler(ISellInvoiceQueryRepository sellInvoiceQueryRepository)
    {
        _sellInvoiceQueryRepository = sellInvoiceQueryRepository;
    }
    public async Task<GetSellInvoiceInventoryItemsResponse> Handle(GetSellInvoiceInventoryItemsRequest request, CancellationToken cancellationToken)
    {
        var data = await _sellInvoiceQueryRepository.GetSellInvoiceInventoryItemRMsBySellInvoiceId(request.InvoiceId);
        var response = new GetSellInvoiceInventoryItemsResponse(Data: data);
        return response;
    }
}
