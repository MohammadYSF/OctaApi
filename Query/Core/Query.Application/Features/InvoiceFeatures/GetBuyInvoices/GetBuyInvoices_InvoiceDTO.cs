using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InvoiceFeatures.GetBuyInvoices
{
    public sealed record GetBuyInvoices_InvoiceDTO(int RowNumber,Guid InvoiceId,string InvoiceCode , DateTime InvoiceDate , string InvoiceDateString,string SellerName , float InvoiceTotalPrice, float InvoicePaidAmount);    
}
