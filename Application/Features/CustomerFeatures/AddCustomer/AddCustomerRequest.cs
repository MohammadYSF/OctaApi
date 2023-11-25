using MediatR;
using OctaApi.Application.DomainModels;

namespace OctaApi.Application.Features.CustomerFeatures.AddCustomer;

public sealed record AddCustomerRequest(string FirstName , string LastName , string phoneNumber,
    DateTime RegisterDate , List<VehicleDTO> VehicleDTOs):IRequest<AddCustomerResponse>;   
