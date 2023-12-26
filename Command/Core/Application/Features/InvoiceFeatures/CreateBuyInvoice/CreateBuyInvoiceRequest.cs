using MediatR;

namespace Command.Core.Application.Features.InvoiceFeatures.CreateBuyInvoice
{
    public sealed record CreateBuyInvoiceRequest(List<CreateBuyInvoice_InventoryItemDTO> Dtos , int Code,string SellerName , DateTime RegisterDate):IRequest<CreateBuyInvoiceResponse>;
   
}
