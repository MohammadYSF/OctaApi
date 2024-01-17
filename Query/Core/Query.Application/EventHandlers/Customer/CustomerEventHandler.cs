using OctaShared.Contracts;
using OctaShared.Events;
using OctaShared.ReadModels;
using Query.Application.Repositories;
namespace Query.Application.EventHandlers.Customer;
public class CustomerEventHandler :
    IEventHandler<CustomerCreatedEvent>,
    IEventHandler<VehicleCreatedEvent>
{
    private readonly ICustomerQueryRepository _customerQueryRepository;
    private readonly IQueryUnitOfWork _queryUnitOfWork;
    private readonly IDistributedCacheService<CustomerRM> _customerRMCacheService;
    private readonly IDistributedCacheService<CustomerVehicleRM> _customerVehicleRMCacheService;
    private static readonly SemaphoreSlim Semaphore = new(1, 1);


    public CustomerEventHandler(ICustomerQueryRepository customerQueryRepository, IQueryUnitOfWork queryUnitOfWork, IDistributedCacheService<CustomerRM> customerRMCacheService, IDistributedCacheService<CustomerVehicleRM> customerVehicleRMCacheService)
    {
        _customerQueryRepository = customerQueryRepository;
        _queryUnitOfWork = queryUnitOfWork;
        _customerRMCacheService = customerRMCacheService;
        _customerVehicleRMCacheService = customerVehicleRMCacheService;
    }
    //private async Task InitCache()
    //{
    //    var exist = _customerRMCacheService.Exists($"ids:{nameof(CustomerRM)}");
    //    if (exist == 1) return;
    //    await Semaphore.WaitAsync();
    //    try
    //    {
    //        var result = await _customerQueryRepository.GetAsync();
    //        _customerRMCacheService.Creates(result);
    //    }
    //    finally
    //    {
    //        Semaphore.Release();
    //    }
    //}
    public async Task HandleAsync(CustomerCreatedEvent @event)
    {
        var customerRM = new CustomerRM
        {
            Id = Guid.NewGuid(),
            CustomerCode = @event.Code.ToString(),
            CustomerId = @event.CustomerId,
            CustomerPhoneNumber = @event.PhoneNumber,
            CustomerFirstName = @event.FirstName,
            CustomerLastName = @event.LastName,
        };
        await _customerQueryRepository.AddAsync(customerRM);
        await _queryUnitOfWork.SaveAsync(default);
        _customerRMCacheService.Dirty();
        _customerVehicleRMCacheService.Dirty();
        //_ = InitCache();


    }

    public async Task HandleAsync(VehicleCreatedEvent @event)
    {
        var customerVehicleRM = new CustomerVehicleRM
        {
            CustomerId = @event.CustomerId,
            VehicleId = @event.VehicleId,
            Id = Guid.NewGuid(),
            VehicleCode = @event.Code.ToString(),
            VehicleName = @event.Name,
            VehiclePlate = @event.Plate
        };
        await _customerQueryRepository.AddAsync(customerVehicleRM);
        await _queryUnitOfWork.SaveAsync(default);
        _customerRMCacheService.Dirty();
        _customerVehicleRMCacheService.Dirty();
        //_ = InitCache();
    }
}
