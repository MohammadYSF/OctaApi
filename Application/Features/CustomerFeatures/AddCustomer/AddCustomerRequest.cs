using Application.Core;
using MediatR;

namespace OctaApi.Application.Features.CustomerFeatures.AddCustomer;

public sealed record AddCustomerRequest(string FirstName, string LastName, string phoneNumber,
    DateTime RegisterDate, List<VehicleDTO> VehicleDTOs) : IRequest<AddCustomerResponse>;
