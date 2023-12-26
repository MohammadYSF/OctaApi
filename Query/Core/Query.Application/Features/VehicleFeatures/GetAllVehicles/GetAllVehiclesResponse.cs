using Application.ReadModels;
namespace OctaApi.Application.Features.VehicleFeatures.GetAllVehicles;
public sealed record GetAllVehiclesResponse
{
    public int Count { get; set; }
    public List<VehicleRM> Data { get; set; }
}
