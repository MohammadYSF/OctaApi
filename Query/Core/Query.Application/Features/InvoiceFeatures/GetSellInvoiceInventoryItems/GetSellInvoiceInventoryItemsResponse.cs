using Application.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InvoiceFeatures.GetSellInvoiceInventoryItems
{
    public sealed record GetSellInvoiceInventoryItemsResponse(List<SellInvoiceInventoryItemRM> Data);    
}
