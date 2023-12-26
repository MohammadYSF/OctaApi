using Command.Core.Application.Repositories;
using MediatR;
namespace Command.Core.Application.Features.InvoiceFeatures.AddSellInvoicePayment;
public sealed class AddInvoicePaymentHandler : IRequestHandler<AddInvoicePaymentRequest, AddInvoicePaymentResponse>
{
    private readonly ICommandUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;
    private readonly ISellInvoiceCommandRepository _sellInvoiceCommandRepository;
    private readonly ISellInvoicePaymentRMRepository _sellInvoicePaymentRMRepository;
    private readonly ISellInvoiceRMRepository _sellInvoiceRMRepository;
    public AddInvoicePaymentHandler(ISellInvoiceCommandRepository sellInvoiceCommandRepository, ICommandUnitOfWork unitOfWork, ISellInvoicePaymentRMRepository sellInvoicePaymentRMRepository, IEventBus eventBus, ISellInvoiceRMRepository sellInvoiceRMRepository)
    {
        _sellInvoiceCommandRepository = sellInvoiceCommandRepository;
        _unitOfWork = unitOfWork;
        _sellInvoicePaymentRMRepository = sellInvoicePaymentRMRepository;
        _eventBus = eventBus;
        _sellInvoiceRMRepository = sellInvoiceRMRepository;
    }

    public async Task<AddInvoicePaymentResponse> Handle(AddInvoicePaymentRequest request, CancellationToken cancellationToken)
    {
        var sellInvoiceAggregate = await _sellInvoiceCommandRepository.GetByIdAsync(request.InvoiceId);
        var sellInvoicePaymentRMs = await _sellInvoicePaymentRMRepository.GetBySellInvoiceIdAsync(request.InvoiceId);
        var sellInvoiceRM = await _sellInvoiceRMRepository.GetBySellInvoiceId(request.InvoiceId);
        var total = sellInvoiceAggregate.UseBuyPrice ? sellInvoiceRM.TotalPriceWhenUsingBuyPrices : sellInvoiceRM.TotalPrice;
        long paidSoFar = sellInvoicePaymentRMs.Sum(a => a.PaidAmount);
        var dateTimeNow = DateTime.UtcNow;
        foreach (var item in request.TrackCodeAndAmountList)
        {
            sellInvoiceAggregate.Pay(item.Item2, dateTimeNow, item.Item1, paidSoFar , total);
            //todo:handle the exception here
        }
        foreach (var item in sellInvoiceAggregate.GetDomainEvents())
        {
            _eventBus.Publish(item);
        }
        await _sellInvoiceCommandRepository.UpdateAsync(sellInvoiceAggregate);
        await _unitOfWork.SaveAsync(cancellationToken);
        return new AddInvoicePaymentResponse(request.InvoiceId);
    }
}
