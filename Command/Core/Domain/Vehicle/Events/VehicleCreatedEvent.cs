using Command.Core.Domain.Core;

namespace Command.Core.Domain.Vehicle.Events
{
    public class VehicleCreatedEvent:DomainEvent
    {
        public VehicleCreatedEvent() : base(nameof(VehicleCreatedEvent))
        {

        }
        public Guid VehicleId { get; set; }
        public Guid CustomerId { get; set; }
        public int Code { get; set; }
        public string Name{ get; set; }
        public string Color{ get; set; }
        public string Plate{ get; set; }
    }
}
