using MediatR;
using Query.Application.ReadModels;
using Query.Application.Repositories;
namespace OctaApi.Application.Features.InvoiceFeatures.GetInvoiceById;
public sealed class GetInvoiceByIdHandler : IRequestHandler<GetInvoiceByIdRequest, GetInvoiceByIdResponse>
{
    private readonly ISellInvoiceQueryRepository _sellInvoiceQueryRepository;
    private readonly IDistributedCacheService<SellInvoiceRM> _sellInvoiceRMCacheService;
    private readonly IDistributedCacheService<SellInvoiceDescriptionRM> _sellInvoiceDescriptonRMCacheService;

    public GetInvoiceByIdHandler(ISellInvoiceQueryRepository sellInvoiceQueryRepository, IDistributedCacheService<SellInvoiceRM> sellInvoiceRMCacheService, IDistributedCacheService<SellInvoiceDescriptionRM> sellInvoiceDescriptonRMCacheService)
    {
        _sellInvoiceQueryRepository = sellInvoiceQueryRepository;
        _sellInvoiceRMCacheService = sellInvoiceRMCacheService;
        _sellInvoiceDescriptonRMCacheService = sellInvoiceDescriptonRMCacheService;
    }
    public async Task<GetInvoiceByIdResponse> Handle(GetInvoiceByIdRequest request, CancellationToken cancellationToken)
    {
        await _sellInvoiceQueryRepository.CheckCacheAsync();
        var sellInvoiceRM = _sellInvoiceRMCacheService.FindBy(a => a.SellInvoiceId == request.InvoiceId).FirstOrDefault();
        var sellInvoiceDescriptionRM = _sellInvoiceDescriptonRMCacheService.FindBy(a => a.SellInvoiceId == request.InvoiceId).FirstOrDefault();
        //var sellInvoiceRM = await _sellInvoiceQueryRepository.GetBySellInvoiceIdAsync(request.InvoiceId);
        //var sellInvoiceDescriptionRM = await _sellInvoiceQueryRepository.GetSellInvoiceDescriptionRMBySellInvoiceId(request.InvoiceId);
        var response = new GetInvoiceByIdResponse(sellInvoiceRM, sellInvoiceDescriptionRM);
        return response;
    }
}
