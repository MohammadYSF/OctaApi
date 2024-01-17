using Command.Core.Application.Repositories;
using Command.Core.Domain.SellInvoice;
using MediatR;
using OctaShared.Contracts;
using OctaShared.DTOs.Request;
using OctaShared.DTOs.Response;
namespace Command.Core.Application.Features.InvoiceFeatures.CreateInvoice;
internal class CreateSellInvoiceHandler : IRequestHandler<CreateSellInvoiceRequest, CreateSellInvoiceResponse>
{
    private readonly ICommandUnitOfWork _unitOfWork;
    private readonly ISellInvoiceCommandRepository _sellInvoiceRepository;
    private readonly IEventBus _eventBus;
    public CreateSellInvoiceHandler(ICommandUnitOfWork unitOfWork, ISellInvoiceCommandRepository sellInvoiceRepository, IEventBus eventBus)
    {
        _unitOfWork = unitOfWork;
        _sellInvoiceRepository = sellInvoiceRepository;
        _eventBus = eventBus;
    }

    public async Task<CreateSellInvoiceResponse> Handle(CreateSellInvoiceRequest request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var createDate = DateTime.UtcNow;
        int code = await _sellInvoiceRepository.GenerateNewCodeAsync();
        var aggregate = SellInvoiceAggregate.Create(id, createDate, code, request.CustomerId, request.VehicleId);
        await _sellInvoiceRepository.AddAsync(aggregate);
        await _unitOfWork.SaveAsync(cancellationToken);
        foreach (var item in aggregate.GetDomainEvents())
        {
            _eventBus.Publish(item);
        }
        var response = new CreateSellInvoiceResponse();
        return response;
    }
}
