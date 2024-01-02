using Query.Application.Core;
using Query.Application.Events.BuyInvoice;
using Query.Application.ReadModels;
using Query.Application.Repositories;

namespace Query.Application.EventHandlers.BuyInvoice;
public class BuyInvoiceEventHandler :
    IEventHandler<BuyInvoiceCreatedEvent>
{
    private readonly IBuyInvoiceQueryRepository _buyInvoiceQueryRepository;
    private readonly IQueryUnitOfWork _queryUnitOfWork;
    private readonly IDistributedCacheService<BuyInvoiceRM> _buyInvoiceRMCacheService;

    public BuyInvoiceEventHandler(IBuyInvoiceQueryRepository buyInvoiceQueryRepository, IQueryUnitOfWork queryUnitOfWork, IDistributedCacheService<BuyInvoiceRM> buyInvoiceRMCacheService)
    {
        _buyInvoiceQueryRepository = buyInvoiceQueryRepository;
        _queryUnitOfWork = queryUnitOfWork;
        _buyInvoiceRMCacheService = buyInvoiceRMCacheService;
    }

    public async Task HandleAsync(BuyInvoiceCreatedEvent @event)
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
        await _queryUnitOfWork.SaveAsync(default);
        _buyInvoiceRMCacheService.Dirty();
    }
}
