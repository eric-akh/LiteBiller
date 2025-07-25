using LiteBiller.Core.Models;
using System.Linq;

namespace LiteBiller.Core.Validators
{
    public static class InvoiceValidator
    {
        public static ValidationResult Validate(Invoice invoice)
        {
            if (invoice.Items == null || !invoice.Items.Any())
                return ValidationResult.Fail("Invoice must have at least one item.");

            if (invoice.Items.Any(i => i.Quantity <= 0 || i.UnitPrice < 0))
                return ValidationResult.Fail("Items must have valid quantity and price.");

            if (invoice.DiscountPercent < 0 || invoice.DiscountPercent > 100)
                return ValidationResult.Fail("Discount must be between 0 and 100.");

            if (invoice.TaxPercent < 0 || invoice.TaxPercent > 100)
                return ValidationResult.Fail("Tax must be between 0 and 100.");

            return ValidationResult.Success();
        }
    }

    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }

        public static ValidationResult Success() => new ValidationResult { IsValid = true };
        public static ValidationResult Fail(string message) => new ValidationResult { IsValid = false, Message = message };
    }
}
