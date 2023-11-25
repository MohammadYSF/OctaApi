using AutoMapper;
using MediatR;
using OctaApi.Application.DomainModels;
using OctaApi.Application.Repositories;
using OctaApi.Domain.Models;
namespace OctaApi.Application.Features.CustomerFeatures.AddCustomer;
public class AddCustomerHandler : IRequestHandler<AddCustomerRequest, AddCustomerResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddCustomerHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AddCustomerResponse> Handle(AddCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = _mapper.Map<Customer>(request);
        var vehicles = request.VehicleDTOs.Select(dto => _mapper.Map<Vehicle>(dto)).ToList();
        int i = 0;
        foreach (var vehicle in vehicles)
        {
            vehicle.Id = Guid.NewGuid();
            vehicle.Code = await _customerRepository.GetNewVehicleCode() + i;
            vehicle.RegisterDate = DateTime.Now;
            i++;
        }
        customer.Code = await _customerRepository.GetNewCustomerCode();
        customer.Vehicles = vehicles;
        await _customerRepository.AddAsync(customer);
        await _unitOfWork.SaveAsync(cancellationToken);
        var response = new AddCustomerResponse(customer.Id, customer.Vehicles.Select(a => new VehicleDTO(a.Name, a.Plate, a.Color, a.Code.ToString())).ToList());
        return response;
    }
}
