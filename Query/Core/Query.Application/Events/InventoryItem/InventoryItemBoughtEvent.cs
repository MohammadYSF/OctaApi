using OctaApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.InventoryItem.Events;

public class InventoryItemBoughtEvent : DomainEvent
{
    public InventoryItemBoughtEvent() : base(nameof(InventoryItemBoughtEvent))
    {

    }
    public string Name { get; set; }
    public string Code { get; set; }
    public long BuyPrice { get; set; }
    public long SellPrice { get; set; }
}
