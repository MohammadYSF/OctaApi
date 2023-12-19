using OctaApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.InventoryItem.Events;

public class InventoryItemUsedEvent : DomainEvent
{
    public Guid InventoryItemId { get; set; }
    public float Count { get; set; }
}
