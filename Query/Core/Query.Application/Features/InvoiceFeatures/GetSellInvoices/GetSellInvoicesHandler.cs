using MediatR;
using Query.Application.Repositories;
namespace OctaApi.Application.Features.InvoiceFeatures.GetSellInvoices;
public sealed class GetSellInvoicesHandler : IRequestHandler<GetSellInvoicesRequest, GetSellInvoicesResponse>
{
    private readonly ISellInvoiceQueryRepository _sellInvoiceQueryRepository;
    public GetSellInvoicesHandler(ISellInvoiceQueryRepository sellInvoiceQueryRepository)
    {
        _sellInvoiceQueryRepository = sellInvoiceQueryRepository;
    }
    public async Task<GetSellInvoicesResponse> Handle(GetSellInvoicesRequest request, CancellationToken cancellationToken)
    {
        var answer = await _sellInvoiceQueryRepository.GetAsync();
        var response = new GetSellInvoicesResponse(Data: answer);
        return response;
    }
}
