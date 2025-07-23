namespace LiteBiller.UI.Forms
{
    partial class InvoiceForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpInvoiceDate;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblInvoiceNo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblCustomer = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpInvoiceDate = new System.Windows.Forms.DateTimePicker();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblInvoiceNo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Location = new System.Drawing.Point(20, 20);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(85, 13);
            this.lblCustomer.TabIndex = 0;
            this.lblCustomer.Text = "Customer Name:";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(120, 17);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(250, 20);
            this.txtCustomerName.TabIndex = 1;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(20, 50);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(71, 13);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = "Invoice Date:";
            // 
            // dtpInvoiceDate
            // 
            this.dtpInvoiceDate.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInvoiceDate.Location = new System.Drawing.Point(120, 47);
            this.dtpInvoiceDate.Name = "dtpInvoiceDate";
            this.dtpInvoiceDate.Size = new System.Drawing.Size(250, 20);
            this.dtpInvoiceDate.TabIndex = 3;
            this.dtpInvoiceDate.Value = new System.DateTime(2025, 4, 23, 1, 49, 0, 0);
            // 
            // dgvItems
            // 
            this.dgvItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Location = new System.Drawing.Point(20, 85);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.Size = new System.Drawing.Size(600, 250);
            this.dgvItems.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSave.Location = new System.Drawing.Point(520, 350);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save Invoice";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Location = new System.Drawing.Point(20, 350);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(64, 13);
            this.lblSubtotal.TabIndex = 6;
            this.lblSubtotal.Text = "Subtotal: $0";
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(20, 370);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(58, 13);
            this.lblTotal.TabIndex = 7;
            this.lblTotal.Text = "Total: $0";
            // 
            // lblInvoiceNo
            // 
            this.lblInvoiceNo.AutoSize = true;
            this.lblInvoiceNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblInvoiceNo.Location = new System.Drawing.Point(400, 20);
            this.lblInvoiceNo.Name = "lblInvoiceNo";
            this.lblInvoiceNo.Size = new System.Drawing.Size(87, 15);
            this.lblInvoiceNo.TabIndex = 4;
            this.lblInvoiceNo.Text = "Invoice No: -";
            // 
            // InvoiceForm
            // 
            this.ClientSize = new System.Drawing.Size(650, 400);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.txtCustomerName);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.dtpInvoiceDate);
            this.Controls.Add(this.lblInvoiceNo);
            this.Controls.Add(this.dgvItems);
            this.Controls.Add(this.lblSubtotal);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnSave);
            this.Name = "InvoiceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LiteBiller - Create Invoice";
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
