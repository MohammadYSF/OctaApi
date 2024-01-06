namespace OctaApi.Application.Features.CustomerFeatures.GetCustomers;
public record GetCustomersResponse_DTO
{
    public int RowNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Code { get; set; }
    public string CustomerPhoneNumber { get; set; }
    public string FullName => FirstName + " " + LastName;

}
