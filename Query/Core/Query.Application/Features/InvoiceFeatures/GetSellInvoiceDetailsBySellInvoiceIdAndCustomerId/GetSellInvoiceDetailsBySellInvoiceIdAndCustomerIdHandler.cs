﻿using MediatR;
using OctaShared.Contracts;
using OctaShared.ReadModels;
using Query.Application.Common.Exceptions;
using Query.Application.Repositories;
using OctaShared.DTOs.Request;
using OctaShared.DTOs.Response;
namespace Query.Application.Features.InvoiceFeatures.GetSellInvoiceDetailsBySellInvoiceIdAndCustomerId;

public class GetSellInvoiceDetailsBySellInvoiceIdAndCustomerIdHandler : IRequestHandler<GetSellInvoiceDetailsBySellInvoiceIdAndCustomerIdRequest, GetSellInvoiceDetailsBySellInvoiceIdAndCustomerIdResponse>
{
    private readonly ISellInvoiceQueryRepository _sellInvoiceQueryRepository;

    private readonly IDistributedCacheService<SellInvoiceRM> _sellInvoiceRMCache;
    private readonly IDistributedCacheService<SellInvoiceServiceRM> _sellInvoiceServiceRMCache;
    private readonly IDistributedCacheService<SellInvoiceInventoryItemRM> _sellInvoiceInventoryItemRMCache;

    public GetSellInvoiceDetailsBySellInvoiceIdAndCustomerIdHandler(IDistributedCacheService<SellInvoiceRM> sellInvoiceRMCache, IDistributedCacheService<SellInvoiceServiceRM> sellInvoiceServiceRMCache, IDistributedCacheService<SellInvoiceInventoryItemRM> sellInvoiceInventoryItemRMCache, ISellInvoiceQueryRepository sellInvoiceQueryRepository)
    {

        _sellInvoiceRMCache = sellInvoiceRMCache;
        _sellInvoiceServiceRMCache = sellInvoiceServiceRMCache;
        _sellInvoiceInventoryItemRMCache = sellInvoiceInventoryItemRMCache;
        _sellInvoiceQueryRepository = sellInvoiceQueryRepository;
    }


    public async Task<GetSellInvoiceDetailsBySellInvoiceIdAndCustomerIdResponse> Handle(GetSellInvoiceDetailsBySellInvoiceIdAndCustomerIdRequest request, CancellationToken cancellationToken)
    {
        await _sellInvoiceQueryRepository.CheckCacheAsync();

        var sellInvoiceRM = _sellInvoiceRMCache.FindBy(a => a.SellInvoiceId == request.SellInvoiceId);
        if (sellInvoiceRM == null || sellInvoiceRM.Count == 0)
            throw new ReadModelNotFoundException<SellInvoiceRM>();
        var sellInvoiceServiceRMs = _sellInvoiceServiceRMCache.FindBy(a => a.SellInvoiceId == request.SellInvoiceId);
        var sellInvoiceInventoryItemRMs = _sellInvoiceInventoryItemRMCache.FindBy(a => a.SellInvoiceId == request.SellInvoiceId);
        var response = new GetSellInvoiceDetailsBySellInvoiceIdAndCustomerIdResponse
        {
            SellInvoiceRM = sellInvoiceRM[0],
            sellInvoiceInventoryItemRMs = sellInvoiceInventoryItemRMs,
            sellInvoiceServiceRMs = sellInvoiceServiceRMs
        };
        return response;
    }
}
