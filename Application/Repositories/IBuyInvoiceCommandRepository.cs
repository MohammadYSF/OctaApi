using Domain.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories;

public interface IBuyInvoiceCommandRepository
{
    Task UpdateAsync(BuyInvoiceAggregate buyInvoiceAggregate);
}
