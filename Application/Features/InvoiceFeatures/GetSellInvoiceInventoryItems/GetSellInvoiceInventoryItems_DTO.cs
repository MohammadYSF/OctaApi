using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InvoiceFeatures.GetSellInvoiceInventoryItems
{
    public sealed record GetSellInvoiceInventoryItems_DTO(Guid InventoryItemId,Guid InvoiceInventoryItemId,string InventoryItemCode,int RowNumber, string InventoryItemName, float InventoryItemCount, long UnitBuyPrice,long UnitSellPrice, float TotalPrice);
}
