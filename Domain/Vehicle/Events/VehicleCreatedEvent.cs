using Domain.Customer.Events;
using OctaApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Vehicle.Events
{
    public class VehicleCreatedEvent:DomainEvent
    {
        public VehicleCreatedEvent() : base(nameof(VehicleCreatedEvent))
        {

        }
        public Guid VehicleId { get; set; }
        public string Code { get; set; }
        public string Name{ get; set; }
        public string Color{ get; set; }
        public string Plate{ get; set; }
    }
}
