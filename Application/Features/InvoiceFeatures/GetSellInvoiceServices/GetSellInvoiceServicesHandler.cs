using MediatR;
using OctaApi.Application.Features.InvoiceFeatures.GetSellInvoiceInventoryItems;
using OctaApi.Application.Features.InvoiceFeatures.GetSellInvoices;
using OctaApi.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InvoiceFeatures.GetSellInvoiceServices
{
    public sealed class GetSellInvoiceServicesHandler : IRequestHandler<GetSellInvoicecServicesRequest, GetSellInvoiceServicesResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public GetSellInvoiceServicesHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<GetSellInvoiceServicesResponse> Handle(GetSellInvoicecServicesRequest request, CancellationToken cancellationToken)
        {

            var data = await _invoiceRepository.GetSellInvoicesServicesAsync(request.InvoiceId);
            var answer = data.Select((a, i) => new GetSellInvoiceServices_DTO
          (
              Code: a.Service.Code.ToString(),
              ServiceId: a.ServiceId,
              InvoiceServiceId: a.Id,
              RowNumber: i + 1,
              ServiceName: a.Service.Name,
              Count: 1, //todo
              UnitPrice: a.Price,
              TotalPrice: a.Price
          )).ToList();
            var response = new GetSellInvoiceServicesResponse(Data: answer);
            return response;

        }
    }
}
