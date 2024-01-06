using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Application.Features.InvoiceFeatures.GetSellInvoicesByCustomerId;

public record GetSellInvoicesByCustomerIdRequest:IRequest<GetSellInvoicesByCustomerIdResponse>
{
    public string CustomerCode{ get; set; } = string.Empty;
}
