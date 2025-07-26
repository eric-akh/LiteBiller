using NUnit.Framework;
using LiteBiller.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace LiteBiller.Tests.Models
{
    [TestFixture]
    public class InvoiceTests
    {
        [Test, Category("Unit")]
        public void InvoiceSubtotal_IsSumOfItemTotals()
        {
            var invoice = new Invoice
            {
                Items = new List<InvoiceItem>
                {
                    new InvoiceItem { Quantity = 2, UnitPrice = 100 },
                    new InvoiceItem { Quantity = 1, UnitPrice = 50 }
                }
            };

            decimal expectedSubtotal = 250;
            decimal actualSubtotal = invoice.Items.Sum(i => i.Total);

            Assert.That(expectedSubtotal, Is.EqualTo(actualSubtotal));
        }

        [Test, Category("Unit")]
        public void InvoiceTotal_WithDiscountAndTax_IsCorrect()
        {
            var invoice = new Invoice
            {
                Items = new List<InvoiceItem>
                {
                    new InvoiceItem { Quantity = 1, UnitPrice = 100 }
                },
                DiscountPercent = 10,
                TaxPercent = 10
            };

            decimal subtotal = invoice.Items.Sum(i => i.Total);  // 100
            decimal expected = (subtotal * 0.9m) * 1.1m;          // 100 - 10% + 10%
            decimal actual = (subtotal - (subtotal * invoice.DiscountPercent / 100m)) *
                             (1 + (invoice.TaxPercent / 100m));

            Assert.That(expected, Is.EqualTo(actual));
        }
    }
}
