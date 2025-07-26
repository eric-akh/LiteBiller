using NUnit.Framework;
using LiteBiller.Core.Models;
using LiteBiller.Core.Validators;
using System.Collections.Generic;

namespace LiteBiller.Tests.Validators
{
    [TestFixture]
    public class InvoiceValidatorTests
    {
        [Test, Category("Unit")]
        public void Invoice_WithNegativeQuantity_FailsValidation()
        {
            var invoice = new Invoice
            {
                Items = new List<InvoiceItem>
                {
                    new InvoiceItem { Quantity = -2, UnitPrice = 100 }
                }
            };

            var result = InvoiceValidator.Validate(invoice);

            Assert.That(result.IsValid, Is.False);
        }

        [Test, Category("Unit")]
        public void Invoice_WithDiscountGreaterThan100_FailsValidation()
        {
            var invoice = new Invoice
            {
                DiscountPercent = 150,
                Items = new List<InvoiceItem>
                {
                    new InvoiceItem { Quantity = 1, UnitPrice = 100 }
                }
            };

            var result = InvoiceValidator.Validate(invoice);

            Assert.That(result.IsValid, Is.False);
        }

        [Test, Category("Unit")]
        public void Invoice_WithTaxGreaterThan100_FailsValidation()
        {
            var invoice = new Invoice
            {
                TaxPercent = 120,
                Items = new List<InvoiceItem>
                {
                    new InvoiceItem { Quantity = 1, UnitPrice = 100 }
                }
            };

            var result = InvoiceValidator.Validate(invoice);

            Assert.That(result.IsValid, Is.False);
        }

        [Test, Category("Unit")]
        public void Invoice_WithNoItems_FailsValidation()
        {
            var invoice = new Invoice
            {
                CustomerName = "ACME Corp",
                DiscountPercent = 10,
                TaxPercent = 10
                // No Items
            };

            var result = InvoiceValidator.Validate(invoice);

            Assert.That(result.IsValid, Is.False);
        }

        [Test, Category("Unit")]
        public void Invoice_WithValidData_PassesValidation()
        {
            var invoice = new Invoice
            {
                CustomerName = "ACME Corp",
                DiscountPercent = 10,
                TaxPercent = 10,
                Items = new List<InvoiceItem>
                {
                    new InvoiceItem { Quantity = 2, UnitPrice = 100 }
                }
            };

            var result = InvoiceValidator.Validate(invoice);

            Assert.That(result.IsValid, Is.True);
        }
    }
}
