﻿using OctaShared.Contracts;
using OctaShared.Events;
using OctaShared.Events.Events.InventoryItem;
using OctaShared.ReadModels;
using Query.Application.Common.Exceptions;
using Query.Application.Repositories;
namespace Query.Application.EventHandlers.InventoryItem;
public class InventoryItemEventHandler :
    IEventHandler<InventoryItemCreatedEvent>
    , IEventHandler<InventoryItemUpdatedEvent>
    , IEventHandler<InventoryItemBoughtEvent>
    , IEventHandler<InventoryItemAddedToSellInvoiceEvent>
    , IEventHandler<InventoryItemRemovedFromSellInvoicecEvent>
    , IEventHandler<InventoryItemDeletedEvent>


{
    private readonly IQueryUnitOfWork _unitOfWork;
    private readonly IInventoryItemQueryRepository _inventoryItemQueryRepository;
    private readonly IDistributedCacheService<InventoryItemRM> _inventoryItemRMCacheService;
    public InventoryItemEventHandler(IQueryUnitOfWork unitOfWork, IInventoryItemQueryRepository inventoryItemQueryRepository, IDistributedCacheService<InventoryItemRM> inventoryItemRMCacheService)
    {
        _unitOfWork = unitOfWork;
        _inventoryItemQueryRepository = inventoryItemQueryRepository;
        _inventoryItemRMCacheService = inventoryItemRMCacheService;
    }

    public async Task HandleAsync(InventoryItemCreatedEvent @event)
    {
        var inventoryItemRM = new InventoryItemRM
        {
            FromDate = @event.CreateDateTime,
            InventoryItemName = @event.Name,
            Id = Guid.NewGuid(),
            InventoryItemBuyPrice = 0,
            InventoryItemCode = @event.Code.ToString(),
            InventoryItemCount = 0,
            InventoryItemId = @event.InventoryItemId,
            InventoryItemSellPrice = 0,
            ToDate = null
        };
        await _inventoryItemQueryRepository.AddAsync(inventoryItemRM);
        await _unitOfWork.SaveAsync(default);
        _inventoryItemRMCacheService.Dirty();
    }

    public async Task HandleAsync(InventoryItemUpdatedEvent @event)
    {
        try
        {
            InventoryItemRM? prevRM = (await _inventoryItemQueryRepository.GetByInventoryItemIdAsync(@event.InventoryItemId)).FirstOrDefault(a => !a.ToDate.HasValue);
            if (prevRM == null) throw new ReadModelNotFoundException<InventoryItemRM>();
            prevRM.ToDate = @event.UpdateDate;
            var inventoryItemRM = new InventoryItemRM
            {
                Id = Guid.NewGuid(),
                InventoryItemBuyPrice = @event.NewBuyPrice,
                InventoryItemSellPrice = @event.NewSellPrice,
                FromDate = @event.UpdateDate,
                InventoryItemCode = @event.Code.ToString(),
                InventoryItemCount = @event.NewCount,
                InventoryItemId = @event.InventoryItemId,
                InventoryItemName = @event.NewName,
                IsDeleted = false,
                ToDate = null
            };
            await _inventoryItemQueryRepository.UpdateAsync(prevRM);
            await _inventoryItemQueryRepository.AddAsync(inventoryItemRM);
            await _unitOfWork.SaveAsync(default);
            _inventoryItemRMCacheService.Dirty();

        }
        catch (Exception e)
        {
            //todo:handle the errors
        }

    }

    public async Task HandleAsync(InventoryItemBoughtEvent @event)
    {
        var updateDate = @event.Date;
        InventoryItemRM? prevRM = (await _inventoryItemQueryRepository.GetByInventoryItemIdAsync(@event.InventoryItemId)).FirstOrDefault(a => !a.ToDate.HasValue);
        if (prevRM == null) throw new ReadModelNotFoundException<InventoryItemRM>();
        prevRM.ToDate = updateDate;
        var inventoryItemRM = new InventoryItemRM
        {
            Id = Guid.NewGuid(),
            InventoryItemBuyPrice = @event.BuyPrice,
            InventoryItemSellPrice = @event.SellPrice,
            FromDate = updateDate,
            InventoryItemCode = @event.Code.ToString(),
            InventoryItemCount = @event.Count,
            InventoryItemId = @event.InventoryItemId,
            InventoryItemName = @event.Name,
            IsDeleted = false,
            ToDate = null
        };
        await _inventoryItemQueryRepository.UpdateAsync(prevRM);
        await _inventoryItemQueryRepository.AddAsync(inventoryItemRM);
        await _unitOfWork.SaveAsync(default);
        _inventoryItemRMCacheService.Dirty();

    }

    public async Task HandleAsync(InventoryItemAddedToSellInvoiceEvent @event)
    {
        InventoryItemRM? prevRM = (await _inventoryItemQueryRepository.GetByInventoryItemIdAsync(@event.InventoryItemId)).FirstOrDefault(a => !a.ToDate.HasValue);
        if (prevRM == null) throw new ReadModelNotFoundException<InventoryItemRM>();
        prevRM.InventoryItemCount -= @event.Count;
        await _inventoryItemQueryRepository.UpdateAsync(prevRM);
        await _unitOfWork.SaveAsync(default);
        _inventoryItemRMCacheService.Dirty();

    }

    public async Task HandleAsync(InventoryItemRemovedFromSellInvoicecEvent @event)
    {
        InventoryItemRM? prevRM = (await _inventoryItemQueryRepository.GetByInventoryItemIdAsync(@event.InventoryItemId)).FirstOrDefault(a => !a.ToDate.HasValue);
        if (prevRM == null) throw new ReadModelNotFoundException<InventoryItemRM>();
        prevRM.InventoryItemCount += @event.Count;
        await _inventoryItemQueryRepository.UpdateAsync(prevRM);
        await _unitOfWork.SaveAsync(default);
        _inventoryItemRMCacheService.Dirty();
    }

    public async Task HandleAsync(InventoryItemDeletedEvent @event)
    {
        InventoryItemRM? inventoryItemRM = (await _inventoryItemQueryRepository.GetByInventoryItemIdAsync(@event.InventoryItemId)).FirstOrDefault(a => !a.ToDate.HasValue);
        if (inventoryItemRM == null) throw new ReadModelNotFoundException<InventoryItemRM>();
        inventoryItemRM.IsDeleted = true;
        await _inventoryItemQueryRepository.UpdateAsync(inventoryItemRM);
        await _unitOfWork.SaveAsync(default);
        _inventoryItemRMCacheService.Dirty();

    }
}
