using AutoMapper;
using MediatR;
using OctaApi.Application.Features.InventoryFeatures.UpdateService;
using OctaApi.Application.Repositories;
using OctaApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InventoryFeatures.UpdateInventoryItem
{
    public class UpdateInventoryItemHandler : IRequestHandler<UpdateInventoryItemRequest, UpdateInventoryItemResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInventoryItemRepository _inventoryItemRepository;
        private readonly IInventoryItemHistoryRepository _inventoryItemHistoryRepository;

        public UpdateInventoryItemHandler(IMapper mapper, IUnitOfWork unitOfWork, IInventoryItemRepository inventoryItemRepository, IInventoryItemHistoryRepository inventoryItemHistoryRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _inventoryItemRepository = inventoryItemRepository;
            _inventoryItemHistoryRepository = inventoryItemHistoryRepository;
        }

        public async Task<UpdateInventoryItemResponse> Handle(UpdateInventoryItemRequest request, CancellationToken cancellationToken)
        {
            InventoryItem? inventoryItem = await _inventoryItemRepository.GetByIdAsync(request.Id);
            if (inventoryItem == null)
                throw new Exception("");
            //var inventoryItemNew = _mapper.Map<InventoryItem>(request);
            //inventoryItem.Code = inventoryItem.Code;
            inventoryItem.SellPrice = request.SellPrice;
            inventoryItem.BuyPrice = request.BuyPrice;
            inventoryItem.Count = request.Count;
            inventoryItem.CountLowerBound = request.CountLowerBound;
            inventoryItem.Name = request.Name;            
            _inventoryItemRepository.Update(inventoryItem);
            var inventoryItemHistory = _mapper.Map<InventoryItemHistory>(inventoryItem);
            await _inventoryItemHistoryRepository.AddAsync(inventoryItemHistory);
            await _unitOfWork.SaveAsync(cancellationToken);

            var response = new UpdateInventoryItemResponse(inventoryItem.Id);
            return response;
        }
    }
}
