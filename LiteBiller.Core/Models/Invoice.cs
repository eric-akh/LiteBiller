using System;
using System.Collections.Generic;
using System.Linq;

namespace LiteBiller.Core.Models
{
    public class Invoice
    {
        public Guid InvoiceId { get; set; }
        public long InvoiceNo { get; set; }  // Used for display: INV-000001
        public string CustomerName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public List<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();

        public decimal Subtotal => Items.Sum(i => i.Total);
        public decimal Total => Subtotal; // taxes/discounts can be added later
    }
}
