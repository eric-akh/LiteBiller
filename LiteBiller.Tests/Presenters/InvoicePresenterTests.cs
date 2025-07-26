using LiteBiller.Core.Interfaces;
using LiteBiller.Core.Models;
using LiteBiller.Core.Presenters;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LiteBiller.Tests.Presenters
{
    [TestFixture]
    public class InvoicePresenterTests
    {
        private Mock<IInvoiceView> _mockView;
        private Mock<IInvoiceRepository> _mockRepo;
        private InvoicePresenter _presenter;

        [SetUp]
        public void SetUp()
        {
            _mockView = new Mock<IInvoiceView>();
            _mockRepo = new Mock<IInvoiceRepository>();
            _presenter = new InvoicePresenter(_mockView.Object, _mockRepo.Object);
        }

        [Test, Category("Unit")]
        public void OnSaveInvoiceClicked_ValidInvoice_SavesAndResetsForm()
        {
            // Arrange
            var items = new List<InvoiceItem>
            {
                new InvoiceItem { Description = "Item 1", Quantity = 1, UnitPrice = 100 }
            };

            var invoiceToSave = new Invoice
            {
                CustomerName = "John",
                InvoiceDate = DateTime.Now,
                Items = items,
                DiscountPercent = 10,
                TaxPercent = 5
            };

            var savedInvoice = new Invoice
            {
                InvoiceId = Guid.NewGuid(),
                InvoiceNo = 123,
                CustomerName = "John",
                InvoiceDate = invoiceToSave.InvoiceDate,
                Items = items,
                DiscountPercent = 10,
                TaxPercent = 5
            };

            _mockView.SetupGet(v => v.CustomerName).Returns(invoiceToSave.CustomerName);
            _mockView.SetupGet(v => v.InvoiceDate).Returns(invoiceToSave.InvoiceDate);
            _mockView.SetupGet(v => v.InvoiceItems).Returns(invoiceToSave.Items);
            _mockView.SetupGet(v => v.DiscountPercent).Returns(invoiceToSave.DiscountPercent);
            _mockView.SetupGet(v => v.TaxPercent).Returns(invoiceToSave.TaxPercent);

            _mockRepo.Setup(r => r.SaveInvoice(It.IsAny<Invoice>())).Returns(savedInvoice);

            // Act
            _mockView.Raise(v => v.SaveInvoiceClicked += null, EventArgs.Empty);

            // Assert
            _mockRepo.Verify(r => r.SaveInvoice(It.Is<Invoice>(i => i.CustomerName == "John")), Times.Once);
            _mockView.Verify(v => v.SetInvoiceId(savedInvoice.InvoiceId), Times.Once);
            _mockView.Verify(v => v.SetInvoiceNumber(savedInvoice.InvoiceNo), Times.Once);
            _mockView.Verify(v => v.ShowMessage(It.Is<string>(s => s.Contains("saved successfully")), MessageBoxIcon.Information), Times.Once);
            _mockView.Verify(v => v.ResetForm(), Times.Once);
        }
    }
}
