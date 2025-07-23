using System;

namespace LiteBiller.Core.Models
{
    public class InvoiceItem
    {
        public Guid InvoiceItemId { get; set; }  // DB PK
        public Guid InvoiceId { get; set; }      // FK
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total => Quantity * UnitPrice;
    }
}
