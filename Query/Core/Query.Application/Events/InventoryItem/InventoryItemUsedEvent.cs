using OctaApi.Domain.Common;
using Query.Application.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Application.Events.InventoryItem;

public class InventoryItemUsedEvent : DomainEvent
{
    public InventoryItemUsedEvent() : base(nameof(InventoryItemUsedEvent))
    {

    }
    public Guid InventoryItemId { get; set; }
    public float Count { get; set; }
}
