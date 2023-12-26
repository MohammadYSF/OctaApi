using MediatR;
using Query.Application.Repositories;
namespace Query.Application.Features.InvoiceFeatures.GetSellInvoiceServices;
public sealed class GetSellInvoiceServicesHandler : IRequestHandler<GetSellInvoicecServicesRequest, GetSellInvoiceServicesResponse>
{
    private readonly ISellInvoiceQueryRepository _sellInvoiceQueryRepository;
    public GetSellInvoiceServicesHandler(ISellInvoiceQueryRepository sellInvoiceQueryRepository)
    {
        _sellInvoiceQueryRepository = sellInvoiceQueryRepository;
    }
    public async Task<GetSellInvoiceServicesResponse> Handle(GetSellInvoicecServicesRequest request, CancellationToken cancellationToken)
    {

        var data = await _sellInvoiceQueryRepository.GetSellInvoiceServiceRMsBySellInvoiceId(request.InvoiceId);
        var response = new GetSellInvoiceServicesResponse(Data: data);
        return response;
    }
}
