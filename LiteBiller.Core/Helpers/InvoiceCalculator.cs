using System.Collections.Generic;
using System.Linq;

namespace LiteBiller.Core.Helpers
{
    public static class InvoiceCalculator
    {
        public static decimal CalculateSubtotal(List<Models.InvoiceItem> items)
        {
            return items?.Sum(i => i.Total) ?? 0;
        }
    }
}