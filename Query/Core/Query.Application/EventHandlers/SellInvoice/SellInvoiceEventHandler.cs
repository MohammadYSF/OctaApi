using Application.Common;
using Application.ReadModels;
using Domain.SellInvoice.Events;
using Query.Application.Repositories;
namespace Query.Application.EventHandlers.SellInvoice;
public class SellInvoiceEventHandler :
    IEventHandler<SellInvoiceCreatedEvent>
    , IEventHandler<SellInvoiceDeletedEvent>
    , IEventHandler<ServiceAddedToSellInvoiceEvent>
    , IEventHandler<InventoryItemAddedToSellInvoiceEvent>
    , IEventHandler<ServiceRemovedFromSellInvoiceEvent>,
    IEventHandler<InventoryItemRemovedFromSellInvoicecEvent>
{
    private readonly IInventoryItemQueryRepository _inventoryItemQueryRepository;
    private readonly IServiceQueryRepository _serviceQueryRepository;
    private readonly ICustomerQueryRepository _customerQueryRepository;
    private readonly IVehicleQueryRepository _vehicleQueryRepository;
    private readonly ISellInvoiceQueryRepository _sellInvoiceQueryRepository;
    private readonly IQueryUnitOfWork _queryUnitOfWork;
    public SellInvoiceEventHandler(ICustomerQueryRepository customerQueryRepository, IVehicleQueryRepository vehicleQueryRepository, ISellInvoiceQueryRepository sellInvoiceQueryRepository, IQueryUnitOfWork queryUnitOfWork, IServiceQueryRepository serviceQueryRepository, IInventoryItemQueryRepository inventoryItemQueryRepository)
    {
        _customerQueryRepository = customerQueryRepository;
        _vehicleQueryRepository = vehicleQueryRepository;
        _sellInvoiceQueryRepository = sellInvoiceQueryRepository;
        _queryUnitOfWork = queryUnitOfWork;
        _serviceQueryRepository = serviceQueryRepository;
        _inventoryItemQueryRepository = inventoryItemQueryRepository;
    }
    public async Task HandleAsync(SellInvoiceCreatedEvent @event, CancellationToken cancellationToken)
    {
        var customerRM = await _customerQueryRepository.GetByCustomerIdAsync(@event.CustomerId);
        VehicleRM? vehicleRM = await _vehicleQueryRepository.GetByVehicleIdAsync(@event.VehicleId);
        var sellInvoiceRM = new SellInvoiceRM
        {
            Id = Guid.NewGuid(),
            ToPay = 0,
            ToPayWhenUsingBuyPrices = 0,
            Tax = 0,
            TotalPrice = 0,
            TotalPriceWhenUsingBuyPrices = 0,
            Discount = 0,
            SellInvoiceId = @event.SellInvoiceId,
            CustomerCode = customerRM?.CustomerCode,
            CustomerName = customerRM?.CustomerName,
            SellInvoiceCode = @event.SellInvoiceCode,
            SellInvoiceDate = @event.CreatedDate,
            VehicleCode = vehicleRM?.VehicleCode,
            VehicleName = vehicleRM?.VehicleName
        };
        await _sellInvoiceQueryRepository.AddAsync(sellInvoiceRM);
        await _queryUnitOfWork.SaveAsync(cancellationToken);
    }

    public async Task HandleAsync(SellInvoiceDeletedEvent @event, CancellationToken cancellationToken)
    {
        SellInvoiceRM? sellInvoiceRM = await _sellInvoiceQueryRepository.GetBySellInvoiceIdAsync(@event.SellInvoiceId);
        await _sellInvoiceQueryRepository.DeleteAsync(sellInvoiceRM);
        await _queryUnitOfWork.SaveAsync(cancellationToken);
    }

    public async Task HandleAsync(ServiceAddedToSellInvoiceEvent @event, CancellationToken cancellationToken)
    {
        ServicecRM? serviceRM = (await _serviceQueryRepository.GetByServiceIdAsync(@event.ServiceId)).FirstOrDefault(a => !a.ToDate.HasValue);
        string serviceCode = serviceRM.ServiceCode;
        SellInvoiceServiceRM? sellInvoiceServiceRM = new()
        {
            DefaultPrice = serviceRM.ServiceDefaultPrice,
            Price = @event.Price,
            Id = Guid.NewGuid(),
            SellInvoiceId = @event.SellInvoiceId,
            ServiceCode = serviceCode,
            ServiceId = @event.ServiceId,

        };
        SellInvoiceRM? sellInvoiceRM = await _sellInvoiceQueryRepository.GetBySellInvoiceIdAsync(@event.SellInvoiceId);
        sellInvoiceRM.TotalPrice += @event.Price;
        sellInvoiceRM.ToPay += @event.Price;
        sellInvoiceRM.TotalPriceWhenUsingBuyPrices += @event.Price;
        sellInvoiceRM.ToPayWhenUsingBuyPrices += @event.Price;
        await _sellInvoiceQueryRepository.UpdateAsync(sellInvoiceRM);
        await _sellInvoiceQueryRepository.AddAsync(sellInvoiceServiceRM);
        await _queryUnitOfWork.SaveAsync(cancellationToken);
    }

    public async Task HandleAsync(InventoryItemAddedToSellInvoiceEvent @event, CancellationToken cancellationToken)
    {
        InventoryItemRM? inventoryItemRM = (await _inventoryItemQueryRepository.GetByInventoryItemIdAsync(@event.InventoryItemId)).FirstOrDefault(a => !a.ToDate.HasValue);
        long buyPrice = inventoryItemRM.InventoryItemBuyPrice;
        long sellPrice = inventoryItemRM.InventoryItemSellPrice;
        SellInvoiceInventoryItemRM? sellInvoiceInventoryRM = new()
        {
            Id = Guid.NewGuid(),
            BuyPrice = buyPrice,
            SellPrice = sellPrice,
            Count = @event.Count,
            SellInvoiceId = @event.SellInvoiceId,
            InventoryItemCode = inventoryItemRM.InventoryItemCode,
            InventoryItemId = @event.InventoryItemId,
        };
        SellInvoiceRM? sellInvoiceRM = await _sellInvoiceQueryRepository.GetBySellInvoiceIdAsync(@event.SellInvoiceId);
        sellInvoiceRM.TotalPrice += sellPrice;
        sellInvoiceRM.TotalPriceWhenUsingBuyPrices += buyPrice;
        sellInvoiceRM.ToPay += sellPrice;
        sellInvoiceRM.ToPayWhenUsingBuyPrices += buyPrice;
        await _sellInvoiceQueryRepository.UpdateAsync(sellInvoiceRM);
        await _sellInvoiceQueryRepository.AddAsync(sellInvoiceInventoryRM);
        await _queryUnitOfWork.SaveAsync(cancellationToken);
    }

    public async Task HandleAsync(ServiceRemovedFromSellInvoiceEvent @event, CancellationToken cancellationToken)
    {
        SellInvoiceServiceRM? sellInvoiceServiceRM = await _sellInvoiceQueryRepository.GetSellInvoiceServiceRMBySellInvoicecServiceId(@event.SellInvoiceServiceId);
        SellInvoiceRM? sellInvoiceRM = await _sellInvoiceQueryRepository.GetBySellInvoiceIdAsync(@event.SellInvoiceId);
        sellInvoiceRM.TotalPrice -= sellInvoiceServiceRM.Price;
        sellInvoiceRM.TotalPriceWhenUsingBuyPrices -= sellInvoiceServiceRM.Price;
        sellInvoiceRM.ToPay -= sellInvoiceServiceRM.Price;
        sellInvoiceRM.ToPayWhenUsingBuyPrices -= sellInvoiceServiceRM.Price;
        await _sellInvoiceQueryRepository.DeleteAsync(sellInvoiceServiceRM);
        await _sellInvoiceQueryRepository.UpdateAsync(sellInvoiceRM);
        await _queryUnitOfWork.SaveAsync(cancellationToken);
    }

    public async Task HandleAsync(InventoryItemRemovedFromSellInvoicecEvent @event, CancellationToken cancellationToken)
    {
        SellInvoiceInventoryItemRM? sellInvoiceInventoryItemRM = await _sellInvoiceQueryRepository.GetSellInvoiceInventoryItemRMBySellInviceInventoryItemId(@event.SellInvoiceInventoryItemId);
        SellInvoiceRM? sellInvoiceRM = await _sellInvoiceQueryRepository.GetBySellInvoiceIdAsync(@event.SellInvoiceId);
        sellInvoiceRM.TotalPrice -= sellInvoiceInventoryItemRM.SellPrice;
        sellInvoiceRM.TotalPriceWhenUsingBuyPrices -= sellInvoiceInventoryItemRM.BuyPrice;
        sellInvoiceRM.ToPay -= sellInvoiceInventoryItemRM.SellPrice;
        sellInvoiceRM.ToPayWhenUsingBuyPrices -= sellInvoiceInventoryItemRM.BuyPrice;
        await _sellInvoiceQueryRepository.DeleteAsync(sellInvoiceInventoryItemRM);
        await _sellInvoiceQueryRepository.UpdateAsync(sellInvoiceRM);
        await _queryUnitOfWork.SaveAsync(cancellationToken);
        throw new NotImplementedException();
    }
}
