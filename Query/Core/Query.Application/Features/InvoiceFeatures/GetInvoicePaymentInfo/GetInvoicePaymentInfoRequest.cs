using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Application.Features.InvoiceFeatures.GetInvoicePaymentInfo;

public sealed record GetInvoicePaymentInfoRequest(Guid InvoiceId) : IRequest<GetInvoicePaymentInfoResponse>;    
