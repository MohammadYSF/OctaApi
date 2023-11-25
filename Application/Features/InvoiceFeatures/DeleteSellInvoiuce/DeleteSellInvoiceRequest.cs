using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InvoiceFeatures.DeleteSellInvoiuce
{
    public sealed record DeleteSellInvoiceRequest(Guid Id):IRequest<DeleteSellInvoiceResponse>;
    
}
