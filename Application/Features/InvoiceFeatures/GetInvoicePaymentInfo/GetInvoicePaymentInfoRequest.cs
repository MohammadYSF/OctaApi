using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InvoiceFeatures.GetInvoicePaymentInfo
{
    public sealed record GetInvoicePaymentInfoRequest(Guid InvoiceId) : IRequest<GetInvoicePaymentInfoResponse>;    
}
