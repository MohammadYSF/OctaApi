using MediatR;
using Query.Application.Core;
using Query.Application.ReadModels;
using Query.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Application.Features.InvoiceFeatures.GetSellInvoicesByCustomerId;

public sealed class GetSellInvoicesByCustomerIdHandler : IRequestHandler<GetSellInvoicesByCustomerIdRequest, GetSellInvoicesByCustomerIdResponse>
{
    private readonly IDistributedCacheService<SellInvoiceRM> _sellInvoiceRMCache;

    public GetSellInvoicesByCustomerIdHandler(IDistributedCacheService<SellInvoiceRM> sellInvoiceRMCache)
    {
        _sellInvoiceRMCache = sellInvoiceRMCache;
    }

    public Task<GetSellInvoicesByCustomerIdResponse> Handle(GetSellInvoicesByCustomerIdRequest request, CancellationToken cancellationToken)
    {
        var data = _sellInvoiceRMCache.FindBy(a => a.CustomerCode == request.CustomerCode);
        return Task.FromResult(new GetSellInvoicesByCustomerIdResponse
        {
            SellInvoiceRMs = data,
            TotalCount = data.Count
        });
    }
}
