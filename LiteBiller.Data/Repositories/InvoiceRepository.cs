using LiteBiller.Core.Interfaces;
using LiteBiller.Core.Models;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace LiteBiller.Data.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private string _connectionString = String.Empty;

        // Constructor to initialize the connection string from configuration
        public InvoiceRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["LiteBillerDb"].ConnectionString;
        }

        // Method to save an invoice and its line items
        public Invoice SaveInvoice(Invoice invoice)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var tran = conn.BeginTransaction();

                try
                {
                    invoice.InvoiceId = Guid.NewGuid();

                    var cmd = new SqlCommand(@"
                INSERT INTO Invoices (InvoiceId, InvoiceDate, CustomerName, DiscountPercent, TaxPercent)
                OUTPUT INSERTED.InvoiceNo
                VALUES (@InvoiceId, @InvoiceDate, @CustomerName, @DiscountPercent, @TaxPercent)", conn, tran);

                    cmd.Parameters.AddWithValue("@InvoiceId", invoice.InvoiceId);
                    cmd.Parameters.AddWithValue("@CustomerName", invoice.CustomerName);
                    cmd.Parameters.AddWithValue("@InvoiceDate", invoice.InvoiceDate);
                    cmd.Parameters.AddWithValue("@DiscountPercent", invoice.DiscountPercent);
                    cmd.Parameters.AddWithValue("@TaxPercent", invoice.TaxPercent);

                    invoice.InvoiceNo = (long)cmd.ExecuteScalar(); // Fetch sequential number

                    // Save line items
                    foreach (var item in invoice.Items)
                    {
                        item.InvoiceItemId = Guid.NewGuid();
                        item.InvoiceId = invoice.InvoiceId;

                        var itemCmd = new SqlCommand(@"
                    INSERT INTO InvoiceItems (InvoiceItemId, InvoiceId, Description, Quantity, UnitPrice)
                    VALUES (@InvoiceItemId, @InvoiceId, @Description, @Quantity, @UnitPrice)", conn, tran);

                        itemCmd.Parameters.AddWithValue("@InvoiceItemId", item.InvoiceItemId);
                        itemCmd.Parameters.AddWithValue("@InvoiceId", item.InvoiceId);
                        itemCmd.Parameters.AddWithValue("@Description", item.Description);
                        itemCmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                        itemCmd.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                        itemCmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }

            return invoice;
        }
    }
}