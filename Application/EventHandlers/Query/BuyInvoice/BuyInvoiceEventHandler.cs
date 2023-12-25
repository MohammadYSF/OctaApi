using Application.Common;
using Application.ReadModels;
using Application.Repositories.Query;
using Domain.BuyInvoice.Events;

namespace Application.EventHandlers.Query.BuyInvoice;
public class BuyInvoiceEventHandler :
    IEventHandler<BuyInvoiceCreatedEvent>
{
    private readonly IBuyInvoiceQueryRepository _buyInvoiceQueryRepository;
    private readonly IQueryUnitOfWork _queryUnitOfWork;
    public BuyInvoiceEventHandler(IBuyInvoiceQueryRepository buyInvoiceQueryRepository, IQueryUnitOfWork queryUnitOfWork)
    {
        _buyInvoiceQueryRepository = buyInvoiceQueryRepository;
        _queryUnitOfWork = queryUnitOfWork;
    }

    public async Task HandleAsync(BuyInvoiceCreatedEvent @event, CancellationToken cancellationToken)
    {
        BuyInvoiceRM buyInvoiceRM = new()
        {
            BuyInvoiceCode = @event.Code,
            BuyInvoiceCreateDate = @event.BuyDate,
            BuyInvoiceId = @event.BuyInvoiced,
            Id = Guid.NewGuid(),
            TotalPrice = 0
        };
        await _buyInvoiceQueryRepository.AddAsync(buyInvoiceRM);
        await _queryUnitOfWork.SaveAsync(cancellationToken);
    }
}
