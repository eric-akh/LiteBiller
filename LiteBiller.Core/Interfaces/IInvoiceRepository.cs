using LiteBiller.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteBiller.Core.Interfaces
{
    public interface IInvoiceRepository
    {
        Invoice SaveInvoice(Invoice invoice);
    }
}
