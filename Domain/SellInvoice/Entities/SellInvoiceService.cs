using OctaApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SellInvoice.Entities
{
    public class SellInvoiceService:Entity
    {
        public Guid SellInvoiceId { get; set; }
        public Guid ServiceId { get; set; }
        public long ServicePrice { get; set; }
    }
}
