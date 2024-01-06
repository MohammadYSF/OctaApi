namespace Query.Application.ReadModels;

public class CustomerRM
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string? CustomerFirstName { get; set; }
    public string? CustomerLastName { get; set; }
    public string? CustomerName => $"{CustomerFirstName} {CustomerLastName}";
    public string? CustomerCode { get; set; }
    public string? CustomerPhoneNumber { get; set; }
}
