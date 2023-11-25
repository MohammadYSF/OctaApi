using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Domain.Models
{
    public class InvoicePaymentHistory
    {
        public Guid Id { get; set; }
        public Guid InvoicePaymentId { get; set; }
        public string? TrackCode { get; set; }

        public long PaidAmount { get; set; }
        public DateTime UpdateDate { get; set; }
        public virtual InvoicePayment InvoicePayment{ get; set; }
    }
}
