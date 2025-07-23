using LiteBiller.Core.Interfaces;
using LiteBiller.Core.Models;
using System;

namespace LiteBiller.Core.Presenters
{
    public class InvoicePresenter
    {
        private readonly IInvoiceView _view;
        private readonly IInvoiceRepository _repository;

        public InvoicePresenter(IInvoiceView view, IInvoiceRepository repository)
        {
            _view = view;
            _repository = repository;
            _view.SaveInvoiceClicked += OnSaveInvoiceClicked;
        }

        private void OnSaveInvoiceClicked(object sender, EventArgs e)
        {
            var invoice = new Invoice
            {
                CustomerName = _view.CustomerName,
                InvoiceDate = _view.InvoiceDate,
                Items = _view.InvoiceItems
            };

            var invoiceId = _repository.SaveInvoice(invoice);
            _view.SetInvoiceId(invoiceId);
            _view.SetInvoiceNumber(invoice.InvoiceNo);
            _view.ShowMessage($"Invoice INV-{invoice.InvoiceNo:D6} saved successfully.");
            _view.ResetForm();
        }
    }
}