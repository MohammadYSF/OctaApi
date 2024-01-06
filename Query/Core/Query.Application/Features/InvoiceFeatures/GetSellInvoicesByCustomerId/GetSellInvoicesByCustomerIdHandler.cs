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
    private readonly ISellInvoiceQueryRepository _sellInvoiceQueryRepository;


    public GetSellInvoicesByCustomerIdHandler(IDistributedCacheService<SellInvoiceRM> sellInvoiceRMCache, ISellInvoiceQueryRepository sellInvoiceQueryRepository)
    {
        _sellInvoiceRMCache = sellInvoiceRMCache;
        _sellInvoiceQueryRepository = sellInvoiceQueryRepository;
    }

    public async Task<GetSellInvoicesByCustomerIdResponse> Handle(GetSellInvoicesByCustomerIdRequest request, CancellationToken cancellationToken)
    {
        await _sellInvoiceQueryRepository.CheckCacheAsync();
        var data = _sellInvoiceRMCache.FindBy(a => a.CustomerCode == request.CustomerCode);
        return new GetSellInvoicesByCustomerIdResponse
        {
            SellInvoiceRMs = data,
            TotalCount = data.Count
        };
    }
}
