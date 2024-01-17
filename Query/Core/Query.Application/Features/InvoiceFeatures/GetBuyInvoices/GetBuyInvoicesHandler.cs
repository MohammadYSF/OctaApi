using MediatR;
using OctaShared.Contracts;
using OctaShared.ReadModels;
using Query.Application.Repositories;
namespace Query.Application.Features.InvoiceFeatures.GetBuyInvoices;
public sealed class GetBuyInvoicesHandler : IRequestHandler<GetBuyInvoicesRequest, GetBuyInvoicesResponse>
{
    private readonly IBuyInvoiceQueryRepository _buyInvoiceQueryRepository;
    private readonly IDistributedCacheService<BuyInvoiceRM> _buyInvoiceRMCacheService;

    public GetBuyInvoicesHandler(IBuyInvoiceQueryRepository buyInvoiceQueryRepository, IDistributedCacheService<BuyInvoiceRM> buyInvoiceRMCacheService)
    {
        _buyInvoiceQueryRepository = buyInvoiceQueryRepository;
        _buyInvoiceRMCacheService = buyInvoiceRMCacheService;
    }
    public async Task<GetBuyInvoicesResponse> Handle(GetBuyInvoicesRequest request, CancellationToken cancellationToken)
    {
        await _buyInvoiceQueryRepository.CheckCacheAsync();
        var data = _buyInvoiceRMCacheService.GetAll().ToList();
        //var data = await _buyInvoiceQueryRepository.GetAsync();
        var response = new GetBuyInvoicesResponse(Data: data);
        return response;
    }
}
