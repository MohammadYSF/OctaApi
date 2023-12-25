using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InvoiceFeatures.GetSellInvoiceServices
{
    public sealed record GetSellInvoiceServices_DTO(string Code,Guid ServiceId , Guid InvoiceServiceId,int RowNumber,string ServiceName,int Count,long UnitPrice , long TotalPrice);    
}
