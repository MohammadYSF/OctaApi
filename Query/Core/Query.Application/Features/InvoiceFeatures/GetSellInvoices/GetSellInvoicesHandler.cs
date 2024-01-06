using MediatR;
using Query.Application.ReadModels;
using Query.Application.Repositories;
namespace OctaApi.Application.Features.InvoiceFeatures.GetSellInvoices;
public sealed class GetSellInvoicesHandler : IRequestHandler<GetSellInvoicesRequest, GetSellInvoicesResponse>
{
    private readonly ISellInvoiceQueryRepository _sellInvoiceQueryRepository;
    private readonly IDistributedCacheService<SellInvoiceRM> _slelInvoiceRMCacheService;

    public GetSellInvoicesHandler(ISellInvoiceQueryRepository sellInvoiceQueryRepository, IDistributedCacheService<SellInvoiceRM>  distributedCacheService)
    {
        _sellInvoiceQueryRepository = sellInvoiceQueryRepository;
        _slelInvoiceRMCacheService = distributedCacheService;
    }
    public async Task<GetSellInvoicesResponse> Handle(GetSellInvoicesRequest request, CancellationToken cancellationToken)
    {
        await _sellInvoiceQueryRepository.CheckCacheAsync();
        var answer = _slelInvoiceRMCacheService.GetAll().ToList();
        //var answer = await _sellInvoiceQueryRepository.GetAsync();
        var response = new GetSellInvoicesResponse(Data: answer);
        return response;
    }
}
