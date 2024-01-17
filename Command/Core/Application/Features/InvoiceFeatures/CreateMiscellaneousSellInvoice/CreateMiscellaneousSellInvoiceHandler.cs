using Command.Core.Application.Repositories;
using Command.Core.Domain.SellInvoice;
using MediatR;
using OctaShared.Contracts;
using OctaShared.DTOs.Request;
using OctaShared.DTOs.Response;
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
            await _sellInvoiceRepository.UpdateAsync(aggregate);
            await _unitOfWork.SaveAsync(cancellationToken);
            foreach (var item in aggregate.GetDomainEvents())
            {
                _eventBus.Publish(item);
            }

            var response = new CreateMiscellaneousSellInvoiceResponse();
            return response;
        }
    }
}
