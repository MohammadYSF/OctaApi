using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InvoiceFeatures.UpdateInvoiceServicesAndInventoryItems
{
    public sealed record UpdateInvoiceServicesAndInventoryItemsRequest(Guid InvoiceId , List<(Guid,float)> InventoryItemIdsAndCounts,
        List<(Guid , long)> ServiceIdsAndPrices,List<Guid> ToRemoveInvoiceInventoryItemIds , List<Guid> ToRemoveInvoiceServiceIds , bool UseBuyPrice , string Description):IRequest<UpdateInvoiceServicesAndInventoryItemsResponse>;    
}
