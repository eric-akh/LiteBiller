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
        public decimal DiscountPercent { get; set; }  // e.g., 10 for 10%
        public decimal TaxPercent { get; set; }       // e.g., 10 for 10% GST
        public decimal DiscountAmount => Subtotal * (DiscountPercent / 100m);
        public decimal TaxAmount => (Subtotal - DiscountAmount) * (TaxPercent / 100m);
        public decimal Subtotal => Items.Sum(i => i.Total);
        public decimal Total => Subtotal - DiscountAmount + TaxAmount;
    }
}
