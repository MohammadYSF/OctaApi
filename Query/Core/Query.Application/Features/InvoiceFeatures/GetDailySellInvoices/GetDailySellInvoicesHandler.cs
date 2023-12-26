using MediatR;
using Query.Application.Repositories;
namespace OctaApi.Application.Features.InvoiceFeatures.GetDailySellInvoices;

public sealed record GetDailySellInvoicesHandler : IRequestHandler<GetDailySellInvoicesRequest, GetDailySellInvoicesResponse>
{
    private readonly ISellInvoiceQueryRepository _sellInvoiceQueryRepository;
    public GetDailySellInvoicesHandler(ISellInvoiceQueryRepository sellInvoiceQueryRepository)
    {
        _sellInvoiceQueryRepository = sellInvoiceQueryRepository;
    }

    public async Task<GetDailySellInvoicesResponse> Handle(GetDailySellInvoicesRequest request, CancellationToken cancellationToken)
    {
        var data = (await _sellInvoiceQueryRepository.GetAsync()).Where(a => a.SellInvoiceDate.Date == DateTime.Today.Date).ToList();

        var response = new GetDailySellInvoicesResponse(data);
        return response;
    }
}
