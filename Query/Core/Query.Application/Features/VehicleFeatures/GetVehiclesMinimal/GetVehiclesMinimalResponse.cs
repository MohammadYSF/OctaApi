using OctaShared.ReadModels;

namespace OctaApi.Application.Features.VehicleFeatures.GetVehiclesMinimal;
public sealed record GetVehiclesMinimalResponse(List<VehicleRM> Data);
