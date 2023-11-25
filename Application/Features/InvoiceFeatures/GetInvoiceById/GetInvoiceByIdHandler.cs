using MediatR;
using OctaApi.Application.Repositories;
using OctaApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InvoiceFeatures.GetInvoiceById
{
    public sealed class GetInvoiceByIdHandler : IRequestHandler<GetInvoiceByIdRequest, GetInvoiceByIdResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public GetInvoiceByIdHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<GetInvoiceByIdResponse> Handle(GetInvoiceByIdRequest request, CancellationToken cancellationToken)
        {
            Invoice? invoice =await  _invoiceRepository.GetById(request.InvoiceId);
            if (invoice == null)
            {
                throw new Exception("invoice not found!");
            }
            var response = new GetInvoiceByIdResponse(invoice.Id, invoice.Code.ToString(),invoice.UseBuyPrice.HasValue?invoice.UseBuyPrice.Value:false,invoice.Description);
            return response;
        }
    }
}
