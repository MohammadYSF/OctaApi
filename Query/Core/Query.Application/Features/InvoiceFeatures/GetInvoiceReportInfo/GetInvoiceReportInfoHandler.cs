using MediatR;
using OctaShared.Contracts;
using OctaShared.ReadModels;
using Query.Application.Repositories;
namespace OctaApi.Application.Features.InvoiceFeatures.GetInvoiceReportInfo;

public sealed class GetInvoiceReportInfoHandler : IRequestHandler<GetInvoiceReportInfoRequest, GetInvoiceReportInfoResponse>
{
    private readonly ISellInvoiceQueryRepository _sellInvoiceQueryRepository;
    private readonly IDistributedCacheService<SellInvoiceRM> _sellInvoiceRMCacheService;
    private readonly IDistributedCacheService<SellInvoiceDescriptionRM> _sellInvoiceDescriptonRMCacheService;
    private readonly IDistributedCacheService<SellInvoiceServiceRM> _sellInvoiceServiceRMCacheService;
    private readonly IDistributedCacheService<SellInvoiceInventoryItemRM> _sellInvoiceInventoryItemRMCacheService;

    public GetInvoiceReportInfoHandler(ISellInvoiceQueryRepository sellInvoiceQueryRepository, IDistributedCacheService<SellInvoiceRM> sellInvoiceRMCacheService, IDistributedCacheService<SellInvoiceDescriptionRM> sellInvoiceDescriptonRMCacheService, IDistributedCacheService<SellInvoiceServiceRM> sellInvoiceServiceRMCacheService, IDistributedCacheService<SellInvoiceInventoryItemRM> sellInvoiceInventoryItemRMCacheService)
    {
        _sellInvoiceQueryRepository = sellInvoiceQueryRepository;
        _sellInvoiceRMCacheService = sellInvoiceRMCacheService;
        _sellInvoiceDescriptonRMCacheService = sellInvoiceDescriptonRMCacheService;
        _sellInvoiceServiceRMCacheService = sellInvoiceServiceRMCacheService;
        _sellInvoiceInventoryItemRMCacheService = sellInvoiceInventoryItemRMCacheService;
    }

    public async Task<GetInvoiceReportInfoResponse> Handle(GetInvoiceReportInfoRequest request, CancellationToken cancellationToken)
    {
        await _sellInvoiceQueryRepository.CheckCacheAsync();
        var sellInvoiceRM = _sellInvoiceRMCacheService.FindBy(a => a.SellInvoiceId == request.InvoiceId).FirstOrDefault();
        var sellInvoiceDescriptionRM = _sellInvoiceDescriptonRMCacheService.FindBy(a => a.SellInvoiceId == request.InvoiceId).FirstOrDefault();
        var sellInvoiceServices = _sellInvoiceServiceRMCacheService.FindBy(a => a.SellInvoiceId == request.InvoiceId);
        var sellInvoiceInventoryItems = _sellInvoiceInventoryItemRMCacheService.FindBy(a => a.SellInvoiceId == request.InvoiceId);
        //var sellInvoiceRM = await _sellInvoiceQueryRepository.GetBySellInvoiceIdAsync(request.InvoiceId);
        //var sellInvoiceDescriptionRM = await _sellInvoiceQueryRepository.GetSellInvoiceDescriptionRMBySellInvoiceId(request.InvoiceId);
        //var sellInvoiceServices = await _sellInvoiceQueryRepository.GetSellInvoiceServiceRMsBySellInvoiceId(request.InvoiceId);
        //var sellInvoiceInventoryItems = await _sellInvoiceQueryRepository.GetSellInvoiceInventoryItemRMsBySellInvoiceId(request.InvoiceId);
        var response = new GetInvoiceReportInfoResponse(sellInvoiceRM, sellInvoiceDescriptionRM, sellInvoiceServices, sellInvoiceInventoryItems);
        return response;
    }
}
