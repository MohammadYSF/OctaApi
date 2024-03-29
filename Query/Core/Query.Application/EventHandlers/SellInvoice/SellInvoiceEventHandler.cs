﻿using OctaShared.Contracts;
using OctaShared.Events;
using OctaShared.ReadModels;
using Query.Application.Common.Exceptions;
using Query.Application.Repositories;
using System.Diagnostics.Tracing;
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

    private readonly IDistributedCacheService<CustomerRM> _customerRMCacheService;
    private readonly IDistributedCacheService<VehicleRM> _vehicleRMCacheService;
    private readonly IDistributedCacheService<CustomerVehicleRM> _customerVehicleRMCacheService;
    private readonly IDistributedCacheService<ServiceRM> _serviceRMCacheService;
    private readonly IDistributedCacheService<InventoryItemRM> _inventoryItemRMCacheService;
    private readonly IDistributedCacheService<SellInvoiceRM> _SellInvoiceRMCacheService;
    private readonly IDistributedCacheService<SellInvoicePaymentRM> _sellInvoicePaymentRMCacheService;
    private readonly IDistributedCacheService<SellInvoiceServiceRM> _sellInvoiceServiceRMCacheService;
    private readonly IDistributedCacheService<SellInvoiceInventoryItemRM> _sellInvoiceInventoryItemRMCacheService;
    public SellInvoiceEventHandler(ICustomerQueryRepository customerQueryRepository, IVehicleQueryRepository vehicleQueryRepository, ISellInvoiceQueryRepository sellInvoiceQueryRepository, IQueryUnitOfWork queryUnitOfWork, IServiceQueryRepository serviceQueryRepository, IInventoryItemQueryRepository inventoryItemQueryRepository, IDistributedCacheService<CustomerRM> customerRMCacheService, IDistributedCacheService<VehicleRM> vehicleRMCacheService, IDistributedCacheService<CustomerVehicleRM> customerVehicleRMCacheService, IDistributedCacheService<ServiceRM> serviceRMCacheService, IDistributedCacheService<InventoryItemRM> inventoryItemRMCacheService, IDistributedCacheService<SellInvoiceRM> sellInvoiceRMCacheService, IDistributedCacheService<SellInvoicePaymentRM> sellInvoicePaymentRMCacheService, IDistributedCacheService<SellInvoiceServiceRM> sellInvoiceServiceRMCacheService, IDistributedCacheService<SellInvoiceInventoryItemRM> sellInvoiceInventoryItemRMCacheService)
    {
        _customerQueryRepository = customerQueryRepository;
        _vehicleQueryRepository = vehicleQueryRepository;
        _sellInvoiceQueryRepository = sellInvoiceQueryRepository;
        _queryUnitOfWork = queryUnitOfWork;
        _serviceQueryRepository = serviceQueryRepository;
        _inventoryItemQueryRepository = inventoryItemQueryRepository;
        _customerRMCacheService = customerRMCacheService;
        _vehicleRMCacheService = vehicleRMCacheService;
        _customerVehicleRMCacheService = customerVehicleRMCacheService;
        _serviceRMCacheService = serviceRMCacheService;
        _inventoryItemRMCacheService = inventoryItemRMCacheService;
        _SellInvoiceRMCacheService = sellInvoiceRMCacheService;
        _sellInvoicePaymentRMCacheService = sellInvoicePaymentRMCacheService;
        _sellInvoiceServiceRMCacheService = sellInvoiceServiceRMCacheService;
        _sellInvoiceInventoryItemRMCacheService = sellInvoiceInventoryItemRMCacheService;
    }
    public async Task HandleAsync(SellInvoiceCreatedEvent @event)
    {
        SellInvoiceRM sellInvoiceRM;
        if (@event.CustomerId == Guid.Empty && @event.VehicleId == Guid.Empty)
        {
            // so Miscellaneous is created
            sellInvoiceRM = new()
            {
                Id = Guid.NewGuid(),
                ToPay = 0,
                ToPayWhenUsingBuyPrices = 0,
                Tax = 0,
                TotalPrice = 0,
                TotalPriceWhenUsingBuyPrices = 0,
                Discount = 0,
                SellInvoiceId = @event.SellInvoiceId,
                CustomerCode = string.Empty,
                CustomerName = string.Empty,
                SellInvoiceCode = @event.SellInvoiceCode,
                SellInvoiceDate = @event.CreatedDate,
                VehicleCode = string.Empty,
                VehicleName = string.Empty
            };
        }
        else
        {
            var customerRM = await _customerQueryRepository.GetByCustomerIdAsync(@event.CustomerId);
            if (customerRM == null) throw new ReadModelNotFoundException<CustomerRM>();
            VehicleRM? vehicleRM = await _vehicleQueryRepository.GetByVehicleIdAsync(@event.VehicleId);
            if (vehicleRM == null) throw new ReadModelNotFoundException<VehicleRM>();

            sellInvoiceRM = new()
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
        }
        await _sellInvoiceQueryRepository.AddAsync(sellInvoiceRM);
        await _queryUnitOfWork.SaveAsync(default);
        _SellInvoiceRMCacheService.Dirty();
    }

    public async Task HandleAsync(SellInvoiceDeletedEvent @event)
    {
        SellInvoiceRM? sellInvoiceRM = await _sellInvoiceQueryRepository.GetBySellInvoiceIdAsync(@event.SellInvoiceId);
        if (sellInvoiceRM == null) throw new ReadModelNotFoundException<SellInvoiceRM>();

        List<SellInvoicePaymentRM> sellInvoicePaymentRMs = await _sellInvoiceQueryRepository.GetSellInvoicePaymentRMsBySellInvoiceIdAsync(@event.SellInvoiceId);
        await _sellInvoiceQueryRepository.DeleteAsync(sellInvoicePaymentRMs);
        await _sellInvoiceQueryRepository.DeleteAsync(sellInvoiceRM);
        await _queryUnitOfWork.SaveAsync(default);
        _SellInvoiceRMCacheService.Dirty();
        _sellInvoicePaymentRMCacheService.Dirty();
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
            ServiceName = serviceRM.ServiceName
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
        _SellInvoiceRMCacheService.Dirty();
        _sellInvoiceServiceRMCacheService.Dirty();
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
            InventoryItemName = inventoryItemRM.InventoryItemName
        };
        SellInvoiceRM? sellInvoiceRM = await _sellInvoiceQueryRepository.GetBySellInvoiceIdAsync(@event.SellInvoiceId);
        sellInvoiceRM.TotalPrice += (long)(sellPrice * @event.Count);
        sellInvoiceRM.TotalPriceWhenUsingBuyPrices += (long)(buyPrice * @event.Count);
        sellInvoiceRM.ToPay += (long)(sellPrice * @event.Count);
        sellInvoiceRM.ToPayWhenUsingBuyPrices += (long)(buyPrice * @event.Count);
        await _sellInvoiceQueryRepository.UpdateAsync(sellInvoiceRM);
        await _sellInvoiceQueryRepository.AddAsync(sellInvoiceInventoryRM);
        await _queryUnitOfWork.SaveAsync(default);
        _SellInvoiceRMCacheService.Dirty();
        _sellInvoiceInventoryItemRMCacheService.Dirty();
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
        _SellInvoiceRMCacheService.Dirty();
        _sellInvoiceServiceRMCacheService.Dirty();


    }
    public async Task HandleAsync(InventoryItemRemovedFromSellInvoicecEvent @event)
    {
        SellInvoiceInventoryItemRM? sellInvoiceInventoryItemRM = await _sellInvoiceQueryRepository.GetSellInvoiceInventoryItemRMBySellInviceInventoryItemId(@event.SellInvoiceInventoryItemId);
        if (sellInvoiceInventoryItemRM == null) throw new ReadModelNotFoundException<SellInvoiceInventoryItemRM>();

        SellInvoiceRM? sellInvoiceRM = await _sellInvoiceQueryRepository.GetBySellInvoiceIdAsync(@event.SellInvoiceId);
        if (sellInvoiceRM == null) throw new ReadModelNotFoundException<SellInvoiceRM>();

        sellInvoiceRM.TotalPrice -= (long)(sellInvoiceInventoryItemRM.SellPrice * @event.Count);
        sellInvoiceRM.TotalPriceWhenUsingBuyPrices -= (long)(sellInvoiceInventoryItemRM.BuyPrice * @event.Count);
        sellInvoiceRM.ToPay -= (long)(sellInvoiceInventoryItemRM.SellPrice * @event.Count);
        sellInvoiceRM.ToPayWhenUsingBuyPrices -= (long)(sellInvoiceInventoryItemRM.BuyPrice * @event.Count);
        await _sellInvoiceQueryRepository.DeleteAsync(sellInvoiceInventoryItemRM);
        await _sellInvoiceQueryRepository.UpdateAsync(sellInvoiceRM);
        await _queryUnitOfWork.SaveAsync(default);
        _SellInvoiceRMCacheService.Dirty();
        _sellInvoiceInventoryItemRMCacheService.Dirty();

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
        _sellInvoicePaymentRMCacheService.Dirty();

    }
}
