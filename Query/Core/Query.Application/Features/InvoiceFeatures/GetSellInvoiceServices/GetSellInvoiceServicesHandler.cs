﻿using MediatR;
using OctaShared.Contracts;
using OctaShared.ReadModels;
using OctaShared.DTOs.Request;
using OctaShared.DTOs.Response;
using Query.Application.Repositories;
namespace Query.Application.Features.InvoiceFeatures.GetSellInvoiceServices;
public sealed class GetSellInvoiceServicesHandler : IRequestHandler<GetSellInvoicecServicesRequest, GetSellInvoiceServicesResponse>
{
    private readonly ISellInvoiceQueryRepository _sellInvoiceQueryRepository;
    private readonly IDistributedCacheService<SellInvoiceServiceRM> _sellInvoiceServiceRMCacheService;

    public GetSellInvoiceServicesHandler(ISellInvoiceQueryRepository sellInvoiceQueryRepository, IDistributedCacheService<SellInvoiceServiceRM> sellInvoiceServiceRMCacheService)
    {
        _sellInvoiceQueryRepository = sellInvoiceQueryRepository;
        _sellInvoiceServiceRMCacheService = sellInvoiceServiceRMCacheService;
    }
    public async Task<GetSellInvoiceServicesResponse> Handle(GetSellInvoicecServicesRequest request, CancellationToken cancellationToken)
    {
        await _sellInvoiceQueryRepository.CheckCacheAsync();
        var data = _sellInvoiceServiceRMCacheService.GetAll().Where(a => a.SellInvoiceId == request.InvoiceId).ToList();
        //var data = await _sellInvoiceQueryRepository.GetSellInvoiceServiceRMsBySellInvoiceId(request.InvoiceId);
        var response = new GetSellInvoiceServicesResponse(Data: data);
        return response;
    }
}
