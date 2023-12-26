using MediatR;
using Query.Application.Repositories;
namespace OctaApi.Application.Features.InvoiceFeatures.GetInvoiceReportInfo;

public sealed class GetInvoiceReportInfoHandler : IRequestHandler<GetInvoiceReportInfoRequest, GetInvoiceReportInfoResponse>
{
    private readonly ISellInvoiceQueryRepository _sellInvoiceQueryRepository;

    public GetInvoiceReportInfoHandler(ISellInvoiceQueryRepository sellInvoiceQueryRepository)
    {
        _sellInvoiceQueryRepository = sellInvoiceQueryRepository;
    }

    public async Task<GetInvoiceReportInfoResponse> Handle(GetInvoiceReportInfoRequest request, CancellationToken cancellationToken)
    {
        var sellInvoiceRM = await _sellInvoiceQueryRepository.GetBySellInvoiceIdAsync(request.InvoiceId);
        var sellInvoiceDescriptionRM = await _sellInvoiceQueryRepository.GetSellInvoiceDescriptionRMBySellInvoiceId(request.InvoiceId);
        var sellInvoiceServices = await _sellInvoiceQueryRepository.GetSellInvoiceServiceRMsBySellInvoiceId(request.InvoiceId);
        var sellInvoiceInventoryItems = await _sellInvoiceQueryRepository.GetSellInvoiceInventoryItemRMsBySellInvoiceId(request.InvoiceId);
        var response = new GetInvoiceReportInfoResponse(sellInvoiceRM, sellInvoiceDescriptionRM, sellInvoiceServices, sellInvoiceInventoryItems);
        return response;
    }
}
