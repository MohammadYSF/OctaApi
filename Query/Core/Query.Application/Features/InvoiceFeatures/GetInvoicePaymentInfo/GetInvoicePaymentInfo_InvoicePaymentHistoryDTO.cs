using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InvoiceFeatures.GetInvoicePaymentInfo
{
    public sealed record GetInvoicePaymentInfo_InvoicePaymentHistoryDTO
    {
        public Guid InvoicePaymentId { get; set; }
        public Guid InvoiceId { get; set; }
        public string TrackCode { get; set; }
        public long PaidAmount { get; set; }
        public DateTime PaidDate { get; set; }
    }
}
