using Application.Repositories;
using AutoMapper;
using MediatR;
using OctaApi.Application.Repositories;
using OctaApi.Domain.InventoryItem;
using OctaApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InventoryFeatures.AddInventoryItem
{
    public class AddInventoryItemHandler : IRequestHandler<AddInventoryItemRequest, AddInventoryItemResponse>
    {
        private readonly IInventoryItemCommandRepository _inventoryItemRepository;
        private readonly IInventoryItemHistoryRepository _inventoryItemHistoryRepository;
        private readonly IMapper _mapper;
        private readonly ICommandUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;

        public AddInventoryItemHandler(IInventoryItemHistoryRepository inventoryItemHistoryRepository, IInventoryItemCommandRepository inventoryItemRepository, IMapper mapper, ICommandUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _inventoryItemHistoryRepository = inventoryItemHistoryRepository;
            _inventoryItemRepository = inventoryItemRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }

        public async Task<AddInventoryItemResponse> Handle(AddInventoryItemRequest request, CancellationToken cancellationToken)
        {
            int code = await _inventoryItemRepository.GenerateNewCodeAsync();
            //var inventoryItemToAdd = _mapper.Map<InventoryItem>(request);
            //int code = await _inventoryItemRepository.GetNewCode();
            var inventoryItemAggregate = InventoryItemَAggregate.Create(Guid.NewGuid(), request.Name, code);

            //inventoryItemToAdd.Code = code;
            await _inventoryItemRepository.AddAsync(inventoryItemAggregate);
            //await _inventoryItemRepository.AddAsync(inventoryItemToAdd);
            //var inventoryItemHistoryToAdd = _mapper.Map<InventoryItemHistory>(request);
            //inventoryItemHistoryToAdd.InventoryItemId = inventoryItemToAdd.Id;
            //inventoryItemHistoryToAdd.Code = code;
            //await _inventoryItemHistoryRepository.AddAsync(inventoryItemHistoryToAdd);
            //var response = new AddInventoryItemResponse(inventoryItemToAdd.Id);
            await _unitOfWork.SaveAsync(cancellationToken);
            foreach (var item in inventoryItemAggregate.GetDomainEvents())
            {
                await _eventBus.Publish(item);
            }
            //return response;
            return new AddInventoryItemResponse();
        }
    }
}
