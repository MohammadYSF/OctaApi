using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InvoiceFeatures.CreateBuyInvoice
{
    public sealed record CreateBuyInvoiceRequest(List<CreateBuyInvoice_InventoryItemDTO> Dtos , int Code,string SellerName , DateTime RegisterDate):IRequest<CreateBuyInvoiceResponse>;
   
}
