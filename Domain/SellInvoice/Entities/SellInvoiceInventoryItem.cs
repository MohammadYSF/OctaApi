using OctaApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SellInvoice.Entities
{
    public class SellInvoiceInventoryItem : Entity
    {
        public Guid SellInvoiceId{ get; set; }
        public Guid InventoryItemId{ get; set; }
        public float Count{ get; set; }
    }
}
