namespace OctaApi.Application.Features.CustomerFeatures.GetCustomers;

public record GetCustomersResponse
{
    public int Count { get; set; }
    public List<GetCustomersResponse_DTO> Data { get; set; }
}
