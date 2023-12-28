using MediatR;

namespace Command.Core.Application.Features.InvoiceFeatures.AddSellInvoicePayment
{
    public sealed record AddInvoicePaymentRequest(Guid InvoiceId, List<Tuple<string, long>> TrackCodeAndAmountList) : IRequest<AddInvoicePaymentResponse>;/* List<Tuple<string ,long> TrackCodeAndAmountList = string TrackCode , long Amount*/
}
