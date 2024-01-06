using Query.Application.ReadModels;
namespace OctaApi.Application.Features.Inventory.GetServices;

public sealed record GetServicesResponse(List<ServiceRM> ServiceDTOs);