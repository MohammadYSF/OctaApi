using Application.Repositories;
using Domain.SellInvoice;
using MediatR;
using OctaApi.Application.Features.InvoiceFeatures.CreateInvoice;
using OctaApi.Application.Repositories;
using OctaApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InvoiceFeatures.CreateMiscellaneousSellInvoice
{
    public sealed class CreateMiscellaneousSellInvoiceHandler : IRequestHandler<CreateMiscellaneousSellInvoiceRequest, CreateMiscellaneousSellInvoiceResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ISellInvoiceRepository _sellInvoiceRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateMiscellaneousSellInvoiceHandler(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork, ISellInvoiceRepository sellInvoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
            _unitOfWork = unitOfWork;
            _sellInvoiceRepository = sellInvoiceRepository;
        }

        public async Task<CreateMiscellaneousSellInvoiceResponse> Handle(CreateMiscellaneousSellInvoiceRequest request, CancellationToken cancellationToken)
        {
            Guid id = Guid.NewGuid();
            DateTime createDate = DateTime.Now;
            int code = await _sellInvoiceRepository.GenerateNewCodeAsync();
            var aggregate = SellInvoiceAggregate.CreateMiscellaneous(id, createDate, code);
            //Invoice invoice = new();
            //invoice.Id = Guid.NewGuid();
            //invoice.CustomerId= Guid.Parse("e7ee7b39-c393-4c5a-85f9-35568138943b");
            //invoice.RegisterDate = DateTime.Now;
            //invoice.Code = await _invoiceRepository.GetNewInvoiceCode();
            //invoice.Type = Domain.Enums.InvoiceType.Sell;
            //await _invoiceRepository.AddAsync(invoice);
            await _sellInvoiceRepository.UpdateAsync(aggregate);
            await _unitOfWork.SaveAsync(cancellationToken);
            //var response = new CreateMiscellaneousSellInvoiceResponse(invoice.Id, invoice.Code);
            var response = new CreateMiscellaneousSellInvoiceResponse();
            return response;
        }
    }
}
