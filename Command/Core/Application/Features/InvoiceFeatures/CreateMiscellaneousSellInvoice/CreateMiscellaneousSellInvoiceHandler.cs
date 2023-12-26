using Command.Core.Application.Repositories;
using Command.Core.Domain.SellInvoice;
using MediatR;
namespace Command.Core.Application.Features.InvoiceFeatures.CreateMiscellaneousSellInvoice
{
    public sealed class CreateMiscellaneousSellInvoiceHandler : IRequestHandler<CreateMiscellaneousSellInvoiceRequest, CreateMiscellaneousSellInvoiceResponse>
    {
        private readonly ISellInvoiceCommandRepository _sellInvoiceRepository;
        private readonly ICommandUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;
        public CreateMiscellaneousSellInvoiceHandler(ICommandUnitOfWork unitOfWork, ISellInvoiceCommandRepository sellInvoiceRepository, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _sellInvoiceRepository = sellInvoiceRepository;
            _eventBus = eventBus;
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
            foreach (var item in aggregate.GetDomainEvents())
            {
                 _eventBus.Publish(item);
            }
            //var response = new CreateMiscellaneousSellInvoiceResponse(invoice.Id, invoice.Code);
            var response = new CreateMiscellaneousSellInvoiceResponse();
            return response;
        }
    }
}
