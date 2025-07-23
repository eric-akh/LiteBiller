using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LiteBiller.Core.Models;

namespace LiteBiller.Core.Interfaces
{
    /// <summary>
    /// Represents a view interface for managing and displaying invoice-related data and actions.
    /// </summary>
    /// <remarks>This interface defines the contract for interacting with an invoice view, including
    /// properties for  customer and invoice details, methods for updating invoice data, and events for user actions. 
    /// Implementations of this interface are used in UI layers to facilitate user interaction with invoice
    /// data.</remarks>
    public interface IInvoiceView
    {
        /// <summary>
        /// Gets or sets the name of the customer for the invoice.
        /// </summary>
        string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the date of the invoice.
        /// </summary>
        DateTime InvoiceDate { get; set; }

        /// <summary>
        /// Gets the collection of invoice items associated with the invoice.
        /// </summary>
        List<InvoiceItem> InvoiceItems { get; }

        /// <summary>
        /// Gets the discount percentage to be applied to the total price.
        /// </summary>
        decimal DiscountPercent { get; }

        /// <summary>
        /// Gets the tax percentage to be applied to the total amount.
        /// </summary>
        decimal TaxPercent { get; }

        /// <summary>
        /// Sets the unique identifier for the invoice.
        /// </summary>
        /// <remarks>This method assigns a new identifier to the invoice. Ensure that the provided
        /// <paramref name="id"/>  is unique and not already in use to avoid conflicts.</remarks>
        /// <param name="id">The unique identifier to assign to the invoice. Must be a valid <see cref="Guid"/>.</param>
        void SetInvoiceId(Guid id);

        /// <summary>
        /// Sets the invoice number for the current transaction.
        /// </summary>
        /// <param name="invoiceNo">The invoice number to assign. Must be a positive integer.</param>
        void SetInvoiceNumber(long invoiceNo);

        /// <summary>
        /// Sets the collection of invoice items for the current invoice.
        /// </summary>
        /// <param name="items">A list of <see cref="InvoiceItem"/> objects representing the items to be included in the invoice. Cannot be
        /// null.</param>
        void SetInvoiceItems(List<InvoiceItem> items);

        /// <summary>
        /// Updates the total amounts for the specified invoice, including any applicable taxes and discounts.
        /// </summary>
        /// <remarks>This method recalculates the total amounts for the provided invoice based on its
        /// current line items,  tax rates, and discount rules. Ensure that the invoice contains valid data before
        /// calling this method.</remarks>
        /// <param name="invoice">The invoice to update. Must not be <see langword="null"/>.</param>
        void UpdateTotals(Invoice invoice);

        /// <summary>
        /// Displays a message to the user in a message box with the specified icon.
        /// </summary>
        /// <remarks>This method provides a simple way to display a message to the user with an optional
        /// icon to indicate the type of message, such as information, warning, or error.</remarks>
        /// <param name="message">The message text to display in the message box. Cannot be null or empty.</param>
        /// <param name="icon">The icon to display in the message box. Defaults to <see cref="MessageBoxIcon.Information"/> if not
        /// specified.</param>
        void ShowMessage(string message, MessageBoxIcon icon = MessageBoxIcon.Information);

        /// <summary>
        /// Resets the form to its initial state, clearing all input fields and resetting any modified values.
        /// </summary>
        /// <remarks>This method is typically used to revert the form to its default state, such as when
        /// canceling an operation or preparing the form for new input. Any unsaved changes will be discarded.</remarks>
        void ResetForm();

        /// <summary>
        /// Occurs when the "Save Invoice" action is triggered by the user.
        /// </summary>
        /// <remarks>This event is typically raised when the user clicks "Save" button in the UI to save
        /// the current invoice. Subscribers can handle this event to perform custom save logic or respond to the save
        /// action.</remarks>
        event EventHandler SaveInvoiceClicked;
    }
}
