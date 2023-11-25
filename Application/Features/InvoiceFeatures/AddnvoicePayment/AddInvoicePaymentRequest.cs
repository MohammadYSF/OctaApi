using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InvoiceFeatures.AddSellInvoicePayment
{
    public sealed record AddInvoicePaymentRequest(Guid InvoiceId , List<Tuple<string ,long>> TrackCodeAndAmountList ) :IRequest<AddInvoicePaymentResponse>;/* List<Tuple<string ,long> TrackCodeAndAmountList = string TrackCode , long Amount*/
}
