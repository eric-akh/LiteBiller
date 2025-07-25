using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using LiteBiller.Core.Models;
using LiteBiller.Data.Repositories;
using NUnit.Framework;

namespace LiteBiller.Tests.Integration
{
    [TestFixture]
    public class InvoiceRepositoryIntegrationTests
    {
        private string _connectionString;
        private InvoiceRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["LiteBillerDb"].ConnectionString;
            _repository = new InvoiceRepository();
        }

        [Test, Category("Integration")]
        public void SaveInvoice_ShouldInsertInvoiceAndItemsCorrectly()
        {
            // Arrange
            var invoice = new Invoice
            {
                CustomerName = "Integration Test Customer",
                InvoiceDate = DateTime.Now,
                DiscountPercent = 5,
                TaxPercent = 10,
                Items = new List<InvoiceItem>
                {
                    new InvoiceItem { Description = "Test Item 1", Quantity = 2, UnitPrice = 100 },
                    new InvoiceItem { Description = "Test Item 2", Quantity = 1, UnitPrice = 50 }
                }
            };

            // Act
            var savedInvoice = _repository.SaveInvoice(invoice);

            // Assert
            Assert.That(savedInvoice.InvoiceId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(savedInvoice.InvoiceNo, Is.GreaterThan(0));
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up inserted data
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var deleteItemsCmd = new SqlCommand("DELETE FROM InvoiceItems WHERE Description LIKE 'Test Item%'", conn);
                deleteItemsCmd.ExecuteNonQuery();

                var deleteInvoicesCmd = new SqlCommand("DELETE FROM Invoices WHERE CustomerName = 'Integration Test Customer'", conn);
                deleteInvoicesCmd.ExecuteNonQuery();
            }
        }
    }
}
