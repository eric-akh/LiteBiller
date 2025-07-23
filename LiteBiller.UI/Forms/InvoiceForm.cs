using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LiteBiller.Core.Interfaces;
using LiteBiller.Core.Models;
using LiteBiller.Core.Presenters;
using LiteBiller.Data.Repositories;

namespace LiteBiller.UI.Forms
{
    public partial class InvoiceForm : Form, IInvoiceView
    {
        private InvoicePresenter _presenter;
        private List<InvoiceItem> _invoiceItems = new List<InvoiceItem>();
        private Guid _invoiceId = new Guid();
        private long _invoiceNo = 0;

        public InvoiceForm()
        {
            InitializeComponent();
            _presenter = new InvoicePresenter(this, new InvoiceRepository());
            SetupDataGridView();
        }

        // IInvoiceView implementation
        public string CustomerName
        {
            get => txtCustomerName.Text;
            set => txtCustomerName.Text = value;
        }

        public DateTime InvoiceDate
        {
            get => dtpInvoiceDate.Value;
            set => dtpInvoiceDate.Value = value;
        }

        public List<InvoiceItem> InvoiceItems => _invoiceItems;

        public event EventHandler SaveInvoiceClicked;

        public void SetInvoiceId(Guid id)
        {
            _invoiceId = id;
        }

        public void SetInvoiceNumber(long invoiceNo)
        {
            _invoiceNo = invoiceNo;
            lblInvoiceNo.Text = $"Invoice #: INV-{invoiceNo:D6}";
        }

        public void SetInvoiceItems(List<InvoiceItem> items)
        {
            _invoiceItems = items;
            dgvItems.DataSource = null;
            dgvItems.DataSource = _invoiceItems;
            UpdateTotalLabels();
        }

        public void UpdateTotals(decimal subtotal, decimal total)
        {
            lblSubtotal.Text = $"Subtotal: {subtotal:C}";
            lblTotal.Text = $"Total: {total:C}";
        }

        public void ShowMessage(string message, MessageBoxIcon icon = MessageBoxIcon.Information)
        {
            MessageBox.Show(message, "Info", MessageBoxButtons.OK, icon);
        }

        // UI Setup
        private void SetupDataGridView()
        {
            dgvItems.AutoGenerateColumns = false;
            dgvItems.AllowUserToAddRows = true;
            dgvItems.AllowUserToDeleteRows = true;
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Description",
                HeaderText = "Description",
                Width = 200
            });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantity",
                HeaderText = "Qty",
                Width = 60
            });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UnitPrice",
                HeaderText = "Unit Price",
                Width = 100
            });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Total",
                Width = 100,
                ReadOnly = true
            });

            dgvItems.CellValueChanged += DgvItems_CellValueChanged;
            dgvItems.UserDeletedRow += (s, e) => RefreshInvoiceItems();
        }

        private void DgvItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && (e.ColumnIndex == 1 || e.ColumnIndex == 2)) // Quantity or UnitPrice
            {
                var row = dgvItems.Rows[e.RowIndex];

                if (int.TryParse(row.Cells[1].Value?.ToString(), out int qty) &&
                    decimal.TryParse(row.Cells[2].Value?.ToString(), out decimal unitPrice))
                {
                    decimal total = qty * unitPrice;
                    row.Cells[3].Value = total.ToString("$0.00");
                }
            }

            RefreshInvoiceItems();
        }

        private void RefreshInvoiceItems()
        {
            _invoiceItems.Clear();
            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                if (row.IsNewRow) continue;

                try
                {
                    var desc = row.Cells[0].Value?.ToString() ?? "";
                    var qty = Convert.ToInt32(row.Cells[1].Value ?? 0);
                    var unitPrice = Convert.ToDecimal(row.Cells[2].Value ?? 0m);
                    _invoiceItems.Add(new InvoiceItem
                    {
                        Description = desc,
                        Quantity = qty,
                        UnitPrice = unitPrice
                    });
                }
                catch
                {
                    // Invalid row — skip it
                }
            }

            UpdateTotalLabels();
        }

        private void UpdateTotalLabels()
        {
            decimal subtotal = 0;
            foreach (var item in _invoiceItems)
            {
                subtotal += item.Total;
            }
            UpdateTotals(subtotal, subtotal); // Add tax/discount logic later
        }

        // Event Handler for saving an invoice
        private void btnSave_Click(object sender, EventArgs e)
        {
            RefreshInvoiceItems();

            if (!ValidateInvoice())
                return;

            SaveInvoiceClicked?.Invoke(this, EventArgs.Empty);
        }

        // Reset the form
        public void ResetForm()
        {
            _invoiceId = Guid.Empty;
            _invoiceNo = 0;

            txtCustomerName.Text = string.Empty;
            dtpInvoiceDate.Value = DateTime.Now;

            _invoiceItems.Clear();
            dgvItems.DataSource = null;
            dgvItems.Rows.Clear();

            lblInvoiceNo.Text = "Invoice #: -";
            lblSubtotal.Text = "Subtotal: $0.00";
            lblTotal.Text = "Total: $0.00";
        }

        // Validation logic
        private bool ValidateInvoice()
        {
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                ShowMessage("Customer name is required.", MessageBoxIcon.Error);
                txtCustomerName.Focus();
                return false;
            }

            if (_invoiceItems.Count == 0)
            {
                ShowMessage("At least one invoice item is required.", MessageBoxIcon.Error);
                return false;
            }

            foreach (var item in _invoiceItems)
            {
                if (string.IsNullOrWhiteSpace(item.Description))
                {
                    ShowMessage("Each item must have a description.", MessageBoxIcon.Error);
                    return false;
                }

                if (item.Quantity <= 0)
                {
                    ShowMessage("Item quantity must be greater than 0.", MessageBoxIcon.Error);
                    return false;
                }

                if (item.UnitPrice < 0)
                {
                    ShowMessage("Item unit price cannot be negative.", MessageBoxIcon.Error);
                    return false;
                }
            }

            if (dtpInvoiceDate.Value > DateTime.Now)
            {
                ShowMessage("Invoice date cannot be in the future.");
                return false;
            }

            return true;
        }
    }
}
