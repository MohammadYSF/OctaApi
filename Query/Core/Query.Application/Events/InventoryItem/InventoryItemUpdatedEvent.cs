using OctaApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Application.Events.InventoryItem;

public class InventoryItemUpdatedEvent : DomainEvent
{
    public InventoryItemUpdatedEvent() : base(nameof(InventoryItemUpdatedEvent))
    {

    }
    public Guid InventoryItemId { get; set; }
    public int Code { get; set; }
    public string NewName{ get; set; }
    public long NewBuyPrice { get; set; }
    public long NewSellPrice { get; set; }
    public float NewCount{ get; set; }
    public DateTime UpdateDate { get; set; }
}
