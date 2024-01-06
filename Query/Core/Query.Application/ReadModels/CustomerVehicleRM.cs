namespace Query.Application.ReadModels;

public class CustomerVehicleRM
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid VehicleId { get; set; }
    public string? VehicleName { get; set; }
    public string? VehicleCode { get; set; }
    public string? VehiclePlate { get; set; }
}
