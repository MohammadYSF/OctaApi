namespace Query.Application.Core;

public class InvoiceDescriptionModel
{
    public string CurrentMileage { get; set; } = "";
    public string MileageOfTheNextEngineOilService { get; set; } = "";
    public string MileageOfTheNextGearboxOilService { get; set; } = "";
    public string MileageOfTheNextSteeringOilService { get; set; } = "";
    public string Description { get; set; } = "";
}
