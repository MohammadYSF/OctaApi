using MediatR;
using OctaApi.Application.Features.InvoiceFeatures.GetSellInvoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InvoiceFeatures.GetDailySellInvoices
{
    public sealed record GetDailySellInvoicesRequest() : IRequest<GetDailySellInvoicesResponse>;
}
