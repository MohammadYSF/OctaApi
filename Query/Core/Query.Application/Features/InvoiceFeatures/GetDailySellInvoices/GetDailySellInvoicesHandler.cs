using MediatR;
using Query.Application.ReadModels;
using Query.Application.Repositories;
namespace Query.Application.Features.InvoiceFeatures.GetDailySellInvoices;

public sealed record GetDailySellInvoicesHandler : IRequestHandler<GetDailySellInvoicesRequest, GetDailySellInvoicesResponse>
{
    private readonly ISellInvoiceQueryRepository _sellInvoiceQueryRepository;
    private readonly IDistributedCacheService<SellInvoiceRM> _sellInvoiceRMCacheService;

    public GetDailySellInvoicesHandler(ISellInvoiceQueryRepository sellInvoiceQueryRepository, IDistributedCacheService<SellInvoiceRM> sellInvoiceRMCacheService)
    {
        _sellInvoiceQueryRepository = sellInvoiceQueryRepository;
        _sellInvoiceRMCacheService = sellInvoiceRMCacheService;
    }

    public async Task<GetDailySellInvoicesResponse> Handle(GetDailySellInvoicesRequest request, CancellationToken cancellationToken)
    {
        await _sellInvoiceQueryRepository.CheckCacheAsync();
        var data = _sellInvoiceRMCacheService.FindBy(a => a.SellInvoiceDate.Date == DateTime.Today.Date).ToList();
        //var data = (await _sellInvoiceQueryRepository.GetAsync()).Where(a => a.SellInvoiceDate.Date == DateTime.Today.Date).ToList();

        var response = new GetDailySellInvoicesResponse(data);
        return response;
    }
}
