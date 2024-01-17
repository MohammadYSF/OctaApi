using OctaShared.ReadModels;

namespace OctaApi.Application.Features.CustomerFeatures.GetCustomers;

public record GetCustomersResponse
{
    public int Count { get; set; }
    public List<CustomerRM> Data { get; set; }
}
