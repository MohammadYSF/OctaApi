﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaApi.Application.Features.InvoiceFeatures.GetBuyInvoices
{
    public sealed record GetBuyInvoicesResponse(List<GetBuyInvoices_InvoiceDTO> Data);    
}
