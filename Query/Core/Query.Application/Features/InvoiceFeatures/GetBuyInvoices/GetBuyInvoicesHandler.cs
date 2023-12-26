using MediatR;
using Query.Application.Repositories;
namespace Query.Application.Features.InvoiceFeatures.GetBuyInvoices;
public sealed class GetBuyInvoicesHandler : IRequestHandler<GetBuyInvoicesRequest, GetBuyInvoicesResponse>
{
    private readonly IBuyInvoiceQueryRepository _buyInvoiceQueryRepository;
    public GetBuyInvoicesHandler(IBuyInvoiceQueryRepository buyInvoiceQueryRepository)
    {
        _buyInvoiceQueryRepository = buyInvoiceQueryRepository;
    }
    public async Task<GetBuyInvoicesResponse> Handle(GetBuyInvoicesRequest request, CancellationToken cancellationToken)
    {
        var data = await _buyInvoiceQueryRepository.GetAsync();
        var response = new GetBuyInvoicesResponse(Data: data);
        return response;
    }
}
