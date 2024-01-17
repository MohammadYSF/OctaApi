using MediatR;
using OctaShared.DTOs;
namespace Command.Core.Application.Features.CustomerFeatures.AddCustomer;
public sealed record AddCustomerRequest(string FirstName, string LastName, string phoneNumber,
    DateTime RegisterDate, List<VehicleDTO> VehicleDTOs) : IRequest<AddCustomerResponse>;
