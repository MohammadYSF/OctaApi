using OctaApi.Application.DomainModels;
namespace OctaApi.Application.Features.CustomerFeatures.AddCustomer;

public sealed record AddCustomerResponse(Guid CustomerId , List<VehicleDTO> VehicleDTOs);    
