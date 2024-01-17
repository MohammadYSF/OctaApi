using Command.Core.Application.Common.Exceptions;
using Command.Core.Application.Repositories;
using Command.Core.Domain.SellInvoice;
using MediatR;
using OctaShared.Contracts;
namespace Command.Core.Application.Features.InvoiceFeatures.DeleteSellInvoice;
public sealed class DeleteSellInvoiceHandler : IRequestHandler<DeleteSellInvoiceRequest, DeleteSellInvoiceResponse>
{
    private readonly ICommandUnitOfWork _unitOfWork;
    private readonly ISellInvoiceCommandRepository _sellInvoiceRepository;
    private readonly IEventBus _eventBus;
    public DeleteSellInvoiceHandler(ICommandUnitOfWork unitOfWork, ISellInvoiceCommandRepository sellInvoiceRepository, IEventBus eventBus)
    {
        _unitOfWork = unitOfWork;
        _sellInvoiceRepository = sellInvoiceRepository;
        _eventBus = eventBus;
    }

    public async Task<DeleteSellInvoiceResponse> Handle(DeleteSellInvoiceRequest request, CancellationToken cancellationToken)
    {
        SellInvoiceAggregate? sellInvoiceAggregate = await _sellInvoiceRepository.GetByIdAsync(request.Id);
        if (sellInvoiceAggregate == null)
            throw new AggregateNotFoundException<SellInvoiceAggregate>($"{nameof(SellInvoiceAggregate)} with id {request.Id} not found !");
        sellInvoiceAggregate?.Delete();
        await _sellInvoiceRepository.DeleteAsync(sellInvoiceAggregate!);

        await _unitOfWork.SaveAsync(cancellationToken);
        foreach (var item in sellInvoiceAggregate.GetDomainEvents())
        {
            _eventBus.Publish(item);
        }
        return new DeleteSellInvoiceResponse(request.Id);
    }
}
