//using Command.Core.Application.Repositories;
//using OctaShared.Contracts;
//using OctaShared.Events;

//namespace Command.Application.EventHandlers.InventoryItem
//{
//    public class InventoryItemAggregateEventHandler : IEventHandler<InventoryItemRemovedFromSellInvoicecEvent>, IEventHandler<InventoryItemAddedToSellInvoiceEvent>
//    {
//        private readonly IInventoryItemCommandRepository _inventoryItemCommandRepository;
//        private readonly ICommandUnitOfWork _unitOfWork;
//        private readonly IEventBus _eventBus;
//        public InventoryItemAggregateEventHandler(IInventoryItemCommandRepository inventoryItemCommandRepository, ICommandUnitOfWork unitOfWork, IEventBus eventBus)
//        {
//            _inventoryItemCommandRepository = inventoryItemCommandRepository;
//            _unitOfWork = unitOfWork;
//            _eventBus = eventBus;
//        }
//        public async Task HandleAsync(InventoryItemAddedToSellInvoiceEvent @event)
//        {
//            var inventoryItemAggregate = await _inventoryItemCommandRepository.GetByIdAsync(@event.InventoryItemId);
//            inventoryItemAggregate.Use(@event.Count);
//            await _inventoryItemCommandRepository.UpdateAsync(inventoryItemAggregate);
//            await _unitOfWork.SaveAsync(default);
//        }
//        public async Task HandleAsync(InventoryItemRemovedFromSellInvoicecEvent @event)
//        {
//            var inventoryItemAggregate = await _inventoryItemCommandRepository.GetByIdAsync(@event.InventoryItemId);
//            inventoryItemAggregate.UnUse(@event.Count);
//            await _inventoryItemCommandRepository.UpdateAsync(inventoryItemAggregate);
//            await _unitOfWork.SaveAsync(default);
//        }
//    }
//}
