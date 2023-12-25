using OctaApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InvoiceFeatures.GetInvoicePaymentInfo
{
    public sealed record GetInvoicePaymentInfoResponse
    {
        public InvoiceType InvoiceType{ get; set; }
        public Guid InvoiceId { get; set; }
        public string InvoiceCode { get; set; }
        public string SellerName { get; set; }
        public string CustomerName { get; set; }
        public string VehicleName { get; set; }
        public string VehiclePlate { get; set; }
        public string VehicleColor { get; set; }
        public Guid VehicleId { get; set; }
        public Guid CustomerId { get; set; }
        public float Total { get; set; }
        public float PaidAmoutSoFar { get; set; }
        public List<GetInvoicePaymentInfo_InvoicePaymentHistoryDTO> PaymentHistoryDTOs { get; set; }
    }
}
