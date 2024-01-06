using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Application.Features.InvoiceFeatures.GetSellInvoiceDetailsBySellInvoiceIdAndCustomerId;

public record GetSellInvoiceDetailsBySellInvoiceIdAndCustomerIdRequest : IRequest<GetSellInvoiceDetailsBySellInvoiceIdAndCustomerIdResponse>
{
    public string CustomerCode { get; set; } = string.Empty;
    public Guid SellInvoiceId { get; set; }
}
