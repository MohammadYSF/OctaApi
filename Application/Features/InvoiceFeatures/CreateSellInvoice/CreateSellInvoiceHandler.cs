using MediatR;
using OctaApi.Application.Repositories;
using OctaApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InvoiceFeatures.CreateInvoice
{
    internal class CreateSellInvoiceHandler : IRequestHandler<CreateSellInvoiceRequest, CreateSellInvoiceResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSellInvoiceHandler(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork)
        {
            _invoiceRepository = invoiceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateSellInvoiceResponse> Handle(CreateSellInvoiceRequest request, CancellationToken cancellationToken)
        {
            Invoice invoice = new();
            invoice.Id = Guid.NewGuid();
            invoice.VehicleId = request.VehicleId;
            invoice.RegisterDate = DateTime.Now;
            invoice.Code = await _invoiceRepository.GetNewInvoiceCode();
            invoice.Type = Domain.Enums.InvoiceType.Sell;
            await _invoiceRepository.AddAsync(invoice);
            await _unitOfWork.SaveAsync(cancellationToken);
            var response = new CreateSellInvoiceResponse(invoice.Id, invoice.Code);
            return response;
        }
    }
}
