using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InvoiceFeatures.GetInvoiceReportInfo
{
    public sealed record GetInvoiceReportInfoResponse(  string InvoiceCode,string VehicleCode , string CustomerTitle , string VehicleTitle,string VehiclePlate , string VehicleColor , DateTime InvoiceDate , float TotalPrice , float Discount , float Tax , float ToPay,string Description,List<GetInvoiceReportInfo_ItemDTO> Items);    
}
