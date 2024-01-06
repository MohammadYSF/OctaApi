using Query.Application.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Application.Features.InvoiceFeatures.GetSellInvoicesByCustomerId;

public record GetSellInvoicesByCustomerIdResponse
{
    public int TotalCount { get; set; }
    public List<SellInvoiceRM> SellInvoiceRMs { get; set; }
}
