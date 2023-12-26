using Application.Repositories;
using Domain.SellInvoice;
using MediatR;
using OctaApi.Application.Repositories;
namespace OctaApi.Application.Features.InvoiceFeatures.DeleteSellInvoice;

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
