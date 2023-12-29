using Query.Application.Common.Exceptions;
using Query.Application.Core;
using Query.Application.Events.SellInvoice;
using Query.Application.ReadModels;
using Query.Application.Repositories;
namespace Query.Application.EventHandlers.SellInvoice;
public class SellInvoiceEventHandler :
    IEventHandler<SellInvoiceCreatedEvent>
    , IEventHandler<SellInvoiceDeletedEvent>
    , IEventHandler<ServiceAddedToSellInvoiceEvent>
    , IEventHandler<InventoryItemAddedToSellInvoiceEvent>
    , IEventHandler<ServiceRemovedFromSellInvoiceEvent>
    , IEventHandler<InventoryItemRemovedFromSellInvoicecEvent>
    , IEventHandler<SellInvoicePaymentCreatedEvent>
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
    public async Task HandleAsync(SellInvoiceCreatedEvent @event)
    {
        var customerRM = await _customerQueryRepository.GetByCustomerIdAsync(@event.CustomerId);
        if (customerRM == null) throw new ReadModelNotFoundException<CustomerRM>();
        VehicleRM? vehicleRM = await _vehicleQueryRepository.GetByVehicleIdAsync(@event.VehicleId);
        if (vehicleRM == null) throw new ReadModelNotFoundException<VehicleRM>();

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
        await _queryUnitOfWork.SaveAsync(default);
    }

    public async Task HandleAsync(SellInvoiceDeletedEvent @event)
    {
        SellInvoiceRM? sellInvoiceRM = await _sellInvoiceQueryRepository.GetBySellInvoiceIdAsync(@event.SellInvoiceId);
        if (sellInvoiceRM == null) throw new ReadModelNotFoundException<SellInvoiceRM>();

        List<SellInvoicePaymentRM> sellInvoicePaymentRMs = await _sellInvoiceQueryRepository.GetSellInvoicePaymentRMsBySellInvoiceIdAsync(@event.SellInvoiceId);
        await _sellInvoiceQueryRepository.DeleteAsync(sellInvoicePaymentRMs);
        await _sellInvoiceQueryRepository.DeleteAsync(sellInvoiceRM);
        await _queryUnitOfWork.SaveAsync(default);
    }

    public async Task HandleAsync(ServiceAddedToSellInvoiceEvent @event)
    {
        ServiceRM? serviceRM = (await _serviceQueryRepository.GetByServiceIdAsync(@event.ServiceId)).FirstOrDefault(a => !a.ToDate.HasValue);
        string serviceCode = serviceRM.ServiceCode;
        SellInvoiceServiceRM? sellInvoiceServiceRM = new()
        {
            DefaultPrice = serviceRM.ServiceDefaultPrice,
            Price = @event.Price,
            Id = @event.SellInvoiceServiceId,
            SellInvoiceId = @event.SellInvoiceId,
            ServiceCode = serviceCode,
            ServiceId = @event.ServiceId,

        };
        SellInvoiceRM? sellInvoiceRM = await _sellInvoiceQueryRepository.GetBySellInvoiceIdAsync(@event.SellInvoiceId);
        if (sellInvoiceRM == null) throw new ReadModelNotFoundException<SellInvoiceRM>();

        sellInvoiceRM.TotalPrice += @event.Price;
        sellInvoiceRM.ToPay += @event.Price;
        sellInvoiceRM.TotalPriceWhenUsingBuyPrices += @event.Price;
        sellInvoiceRM.ToPayWhenUsingBuyPrices += @event.Price;
        await _sellInvoiceQueryRepository.UpdateAsync(sellInvoiceRM);
        await _sellInvoiceQueryRepository.AddAsync(sellInvoiceServiceRM);
        await _queryUnitOfWork.SaveAsync(default);
    }

    public async Task HandleAsync(InventoryItemAddedToSellInvoiceEvent @event)
    {
        InventoryItemRM? inventoryItemRM = (await _inventoryItemQueryRepository.GetByInventoryItemIdAsync(@event.InventoryItemId)).FirstOrDefault(a => !a.ToDate.HasValue);
        if (inventoryItemRM == null) throw new ReadModelNotFoundException<InventoryItemRM>();

        long buyPrice = inventoryItemRM.InventoryItemBuyPrice;
        long sellPrice = inventoryItemRM.InventoryItemSellPrice;
        SellInvoiceInventoryItemRM? sellInvoiceInventoryRM = new()
        {
            Id = @event.SellInvoiceInventoryItemId,
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
        await _queryUnitOfWork.SaveAsync(default);
    }

    public async Task HandleAsync(ServiceRemovedFromSellInvoiceEvent @event)
    {
        SellInvoiceServiceRM? sellInvoiceServiceRM = await _sellInvoiceQueryRepository.GetSellInvoiceServiceRMBySellInvoicecServiceId(@event.SellInvoiceServiceId);
        if (sellInvoiceServiceRM == null) throw new ReadModelNotFoundException<SellInvoiceServiceRM>();

        SellInvoiceRM? sellInvoiceRM = await _sellInvoiceQueryRepository.GetBySellInvoiceIdAsync(@event.SellInvoiceId);
        if (sellInvoiceRM == null) throw new ReadModelNotFoundException<SellInvoiceRM>();

        sellInvoiceRM.TotalPrice -= sellInvoiceServiceRM.Price;
        sellInvoiceRM.TotalPriceWhenUsingBuyPrices -= sellInvoiceServiceRM.Price;
        sellInvoiceRM.ToPay -= sellInvoiceServiceRM.Price;
        sellInvoiceRM.ToPayWhenUsingBuyPrices -= sellInvoiceServiceRM.Price;
        await _sellInvoiceQueryRepository.DeleteAsync(sellInvoiceServiceRM);
        await _sellInvoiceQueryRepository.UpdateAsync(sellInvoiceRM);
        await _queryUnitOfWork.SaveAsync(default);
    }
    public async Task HandleAsync(InventoryItemRemovedFromSellInvoicecEvent @event)
    {
        SellInvoiceInventoryItemRM? sellInvoiceInventoryItemRM = await _sellInvoiceQueryRepository.GetSellInvoiceInventoryItemRMBySellInviceInventoryItemId(@event.SellInvoiceInventoryItemId);
        if (sellInvoiceInventoryItemRM == null) throw new ReadModelNotFoundException<SellInvoiceInventoryItemRM>();

        SellInvoiceRM? sellInvoiceRM = await _sellInvoiceQueryRepository.GetBySellInvoiceIdAsync(@event.SellInvoiceId);
        if (sellInvoiceRM == null) throw new ReadModelNotFoundException<SellInvoiceRM>();

        sellInvoiceRM.TotalPrice -= sellInvoiceInventoryItemRM.SellPrice;
        sellInvoiceRM.TotalPriceWhenUsingBuyPrices -= sellInvoiceInventoryItemRM.BuyPrice;
        sellInvoiceRM.ToPay -= sellInvoiceInventoryItemRM.SellPrice;
        sellInvoiceRM.ToPayWhenUsingBuyPrices -= sellInvoiceInventoryItemRM.BuyPrice;
        await _sellInvoiceQueryRepository.DeleteAsync(sellInvoiceInventoryItemRM);
        await _sellInvoiceQueryRepository.UpdateAsync(sellInvoiceRM);
        await _queryUnitOfWork.SaveAsync(default);
        throw new NotImplementedException();
    }

    public async Task HandleAsync(SellInvoicePaymentCreatedEvent @event)
    {
        var sellInvoicePaymentRM = new SellInvoicePaymentRM
        {
            Id = Guid.NewGuid(),
            PaidAmount = @event.Amount,
            SellInvoiceId = @event.SellInvoiceId
        };
        await _sellInvoiceQueryRepository.AddAsync(sellInvoicePaymentRM);
        await _queryUnitOfWork.SaveAsync(default);
    }
}
