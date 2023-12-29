using MediatR;
using Query.Application.Common.Exceptions;
using Query.Application.ReadModels;
using Query.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Application.Features.InvoiceFeatures.GetSellInvoiceDetailsBySellInvoiceIdAndCustomerId;

public class GetSellInvoiceDetailsBySellInvoiceIdAndCustomerIdHandler : IRequestHandler<GetSellInvoiceDetailsBySellInvoiceIdAndCustomerIdRequest, GetSellInvoiceDetailsBySellInvoiceIdAndCustomerIdResponse>
{
    private readonly IDistributedCacheService<SellInvoiceRM> _sellInvoiceRMCache;
    private readonly IDistributedCacheService<SellInvoiceServiceRM> _sellInvoiceServiceRMCache;
    private readonly IDistributedCacheService<SellInvoiceInventoryItemRM> _sellInvoiceInventoryItemRMCache;

    public GetSellInvoiceDetailsBySellInvoiceIdAndCustomerIdHandler(IDistributedCacheService<SellInvoiceRM> sellInvoiceRMCache, IDistributedCacheService<SellInvoiceServiceRM> sellInvoiceServiceRMCache, IDistributedCacheService<SellInvoiceInventoryItemRM> sellInvoiceInventoryItemRMCache)
    {
        _sellInvoiceRMCache = sellInvoiceRMCache;
        _sellInvoiceServiceRMCache = sellInvoiceServiceRMCache;
        _sellInvoiceInventoryItemRMCache = sellInvoiceInventoryItemRMCache;
    }


    public Task<GetSellInvoiceDetailsBySellInvoiceIdAndCustomerIdResponse> Handle(GetSellInvoiceDetailsBySellInvoiceIdAndCustomerIdRequest request, CancellationToken cancellationToken)
    {
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
        return Task.FromResult(response);
    }
}
