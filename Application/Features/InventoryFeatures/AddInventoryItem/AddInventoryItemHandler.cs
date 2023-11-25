using AutoMapper;
using MediatR;
using OctaApi.Application.Repositories;
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
        private readonly IInventoryItemRepository _inventoryItemRepository;
        private readonly IInventoryItemHistoryRepository _inventoryItemHistoryRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AddInventoryItemHandler(IInventoryItemHistoryRepository inventoryItemHistoryRepository, IInventoryItemRepository inventoryItemRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _inventoryItemHistoryRepository = inventoryItemHistoryRepository;
            _inventoryItemRepository = inventoryItemRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<AddInventoryItemResponse> Handle(AddInventoryItemRequest request, CancellationToken cancellationToken)
        {
            var inventoryItemToAdd = _mapper.Map<InventoryItem>(request);
            int code = await _inventoryItemRepository.GetNewCode();
            inventoryItemToAdd.Code = code;
            await _inventoryItemRepository.AddAsync(inventoryItemToAdd);
            var inventoryItemHistoryToAdd = _mapper.Map<InventoryItemHistory>(request);
            inventoryItemHistoryToAdd.InventoryItemId = inventoryItemToAdd.Id;
            inventoryItemHistoryToAdd.Code = code;
            await _inventoryItemHistoryRepository.AddAsync(inventoryItemHistoryToAdd);
            var response = new AddInventoryItemResponse(inventoryItemToAdd.Id);
            await _unitOfWork.SaveAsync(cancellationToken);
            return response;
        }
    }
}
