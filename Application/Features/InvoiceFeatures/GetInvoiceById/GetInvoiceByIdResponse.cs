using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InvoiceFeatures.GetInvoiceById
{
    public sealed record GetInvoiceByIdResponse(Guid Id  , string Code,bool UseBuyPrice , string Description);    
}
