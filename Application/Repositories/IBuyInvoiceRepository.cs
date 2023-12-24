using Domain.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories;

public interface IBuyInvoiceRepository
{
    Task UpdateAsync(BuyInvoiceAggregate buyInvoiceAggregate);
}
