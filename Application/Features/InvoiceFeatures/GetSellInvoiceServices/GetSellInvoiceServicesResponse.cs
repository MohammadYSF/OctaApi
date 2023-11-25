using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InvoiceFeatures.GetSellInvoiceServices
{
    public sealed record GetSellInvoiceServicesResponse(List<GetSellInvoiceServices_DTO> Data);    
}
