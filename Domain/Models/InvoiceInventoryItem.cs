using System.ComponentModel.DataAnnotations;

namespace OctaApi.Domain.Models;

public class InvoiceInventoryItem
{
    public Guid Id { get; set; }
    public Guid InvoiceId { get; set; }
    public Guid InventoryItemId { get; set; }
    public float Count { get; set; }
    public DateTime RegisterDate { get; set; }
    public virtual Invoice Invoice { get; set; }
    public virtual InventoryItem InventoryItem { get; set; }

}
