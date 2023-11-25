using AutoMapper;
using OctaApi.Application.Features.InventoryFeatures.DeleteService;
using OctaApi.Application.Repositories;
using OctaApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InventoryFeatures.DeleteInventoryItem
{
    public class DeleteInventoryItemHandler
    {
        private readonly IInventoryItemRepository  _inventoryItemRepository;
        private readonly IInventoryItemHistoryRepository  _inventoryItemHistoryRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteInventoryItemHandler(IInventoryItemRepository serviceRepository, IInventoryItemHistoryRepository serviceHistoryRepository, IMapper mapper)
        {
            _inventoryItemRepository = serviceRepository;
            _inventoryItemHistoryRepository = serviceHistoryRepository;
            _mapper = mapper;
        }

        public async Task<DeleteInventoryItemResponse> Handle(DeleteServiceRequest request, CancellationToken cancellationToken)
        {
            var inventoryItem = await _inventoryItemRepository.GetByCodeAsync(request.Code);
            if (inventoryItem == null)
                throw new Exception(""); //todo
            inventoryItem.IsActive = false;
            var inventoryItemHistory = _mapper.Map<InventoryItemHistory>(inventoryItem);
            _inventoryItemRepository.Update(inventoryItem);
            await _inventoryItemHistoryRepository.AddAsync(inventoryItemHistory);
            await _unitOfWork.SaveAsync(cancellationToken);
            var response = new DeleteInventoryItemResponse(inventoryItem.Id);
            return response;
        }
    }
}
